using Microsoft.AspNetCore.Builder;
using System.Net;
using System.Text.Json;
using ApproviaChallenge.TaskManager.Core.Models;

namespace TaskManager.Core.ServiceExtensions;

public class ErrorHandlerException
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerException> _logger;

    public ErrorHandlerException(RequestDelegate next, ILogger<ErrorHandlerException> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var response = context.Response;

        var errorResponse = new ErrorHandling
        {
            Success = false,
            StatusCode = 500
        };
        switch (exception)
        {
            case ApplicationException ex:
                if (ex.Message.Contains("Invalid Token"))
                {
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    errorResponse.Message = ex.Message;
                    break;
                }
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.Message = ex.Message;
                break;
            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.Message = "Internal Server Error. Please Try Again Later.";
                _logger.LogError($"Something Went Wrong in the {exception.Message}");
                break;
        }
        _logger.LogError(exception.Message);
        var result = JsonSerializer.Serialize(errorResponse);
        await context.Response.WriteAsync(result);
    }
}
