using ApproviaChallenge.TaskManager.Core.DTOs;
using ApproviaChallenge.TaskManager.Core.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace ApproviaChallenge.TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class TaskManagerController : ControllerBase
    {
        private readonly ITaskManagerServices _taskManagerServices;
        public TaskManagerController(ITaskManagerServices taskManagerServices)
        {
            _taskManagerServices = taskManagerServices;
        }


        /// <summary>
        /// Gets all tasks
        /// </summary>
        /// <returns>Return a json object of all tasks</returns>
        /// <remarks>
        /// sample:
        /// 
        /// </remarks> 
        /// <response code ="200">OK success </response>
        /// <response code ="400"> BadRequest </response>
        /// <response code ="404"> Not Found </response>
        /// <response code ="500">Internal server error</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> GetAllTasks()
        {
            var result = await _taskManagerServices.GetAllTasksAsync();
            return Ok(result);
        }

        /// <summary>
        /// Get task by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Return a json object of a given task</returns>
        /// <remarks>
        /// sample:
        /// 
        /// </remarks> 
        /// <response code ="200">OK success </response>
        /// <response code ="400"> BadRequest </response>
        /// <response code ="404"> Not Found </response>
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
        public async Task<IActionResult> GetTaskById(string Id)
        {
            var response = await _taskManagerServices.GetTaskByIdAsync(Id);
            return Ok(response);
        }

        /// <summary>
        /// Create task
        /// </summary>
        /// <param name="request"></param>
        /// <returns>No content</returns>
        /// <remarks>
        /// sample:
        /// 
        /// </remarks> 
        /// <response code ="201">created</response>
        /// <response code ="400"> BadRequest </response>
        /// <response code ="417"> Expectation Failed </response>
        /// <response code ="500"> Internal Server Error </response>
        [HttpPost("CreateTask")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTasks([FromBody] CreateTaskDto request)
        {
            var response = await _taskManagerServices.AddTaskAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Delete task by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>No content</returns>
        /// <remarks>
        /// sample:
        /// 
        /// </remarks> 
        /// <response code ="204"> No Content </response>
        /// <response code ="400"> BadRequest </response>
        /// <response code ="404"> Not Found </response>
        /// <response code ="500"> Internal Server Error </response>
        [HttpDelete("DeleteTask/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status417ExpectationFailed)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTask(string Id)
        {
            var response = await _taskManagerServices.DeleteAsync(Id);
            return Ok(response);
        }
        
    }
}
