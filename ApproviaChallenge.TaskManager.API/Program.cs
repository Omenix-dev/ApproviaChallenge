
using ApproviaChallenge.TaskManager.API.ServiceExtension;
using ApproviaChallenge.TaskManager.Core.ExternalServices;
using ApproviaChallenge.TaskManager.Core.Interface;
using ApproviaChallenge.TaskManager.Core.Repositories;
using ApproviaChallenge.TaskManager.Core.Services;
using ApproviaChallenge.TaskManager.Core.Utilities.Profiles;
using TaskManager.Core.ServiceExtensions;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddHttpClient();
    builder.Services.AddScoped<IClient, ClientFactory>();
    builder.Services.AddScoped<ITaskRepository, DataAccess>();
    builder.Services.AddScoped<ITaskManagerServices, TaskManagerServices>();
    builder.Services.AddAutoMapper(typeof(MappingProfile));
    builder.Services.AddSwaggerConfiguration();

    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

   
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.UseMiddleware<ErrorHandlerException>();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{

	throw ex;
}

