using ApproviaChallenge.TaskManager.Core.DTOs;
using ApproviaChallenge.TaskManager.Core.Interface;
using Microsoft.Extensions.DependencyInjection;
using ApproviaChallenge.TaskManager.Core.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ApproviaChallenge.TaskManager.Core.Services
{
    public class TaskManagerServices : ITaskManagerServices
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TaskManagerServices> _logger;
        public TaskManagerServices(IServiceProvider services)
        {
            _taskRepository = services.GetRequiredService<ITaskRepository>();
            _mapper = services.GetRequiredService<IMapper>();
            _logger = services.GetRequiredService<ILogger<TaskManagerServices>>();
        }
        /// <summary>
        /// Get all task
        /// </summary>
        /// <returns>IEnumerable of task</returns>
        public async Task<IEnumerable<TaskListDto>> GetAllTasksAsync()
        {
            try
            {
                _logger.LogInformation("initiated the services that is used to create a pull from the docuament datastore");
                var tasks = await _taskRepository.GetAllTasksAsync();
                _logger.LogInformation("successfully gotten back the transaction frpom the data base ");
                var response = _mapper.Map<IEnumerable<TaskListDto>>(tasks);
                _logger.LogInformation("completed the mapping into the dto for the transafer to the client");
                if (response == null)
                {
                    _logger.LogInformation("the response is null meaning that the data store is not filled with task");
                    return new List<TaskListDto>();

                }
                foreach (var task in response)
                {
                    _logger.LogInformation("carrying out conversion of the alloted time in integer into date format");
                    task.DueDate = task.StartDate.AddDays(task.AllottedTimeInDays);
                    task.EndDate = task.StartDate.AddDays(task.ElapsedTimeInDays);
                    _logger.LogInformation("manipulating the data to get the value the over due or days late");
                    task.DaysOverDue = !task.TaskStatus ? (task.ElapsedTimeInDays - task.AllottedTimeInDays) : 0;
                    task.DaysLate = task.TaskStatus ? (task.AllottedTimeInDays - task.ElapsedTimeInDays) : 0;
                    _logger.LogInformation("successfully carried out the data mainpulation");
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("something went wrong contact the admin");
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                throw ex;
            }
            
        }

        /// <summary>
        /// Get all task
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>A TasklistDto</returns>
        public async Task<TaskListDto> GetTaskByIdAsync(string Id)
        {
            try
            {
                _logger.LogInformation("initiated the services that is used to create a pull from the docuament datastore");
                _logger.LogInformation("gotten the data for the id " + Id);
                var tasks = await _taskRepository.GetTaskByIdAsync(Id);
                var task = _mapper.Map<TaskListDto>(tasks);
                _logger.LogInformation("carrying out conversion of the alloted time in integer into date format");
                task.DueDate = task.StartDate.AddDays(task.AllottedTimeInDays);
                task.EndDate = task.StartDate.AddDays(task.ElapsedTimeInDays);
                _logger.LogInformation("manipulating the data to get the value the over due or days late");
                task.DaysOverDue = !task.TaskStatus ? (task.ElapsedTimeInDays - task.AllottedTimeInDays) : 0;
                task.DaysLate = task.TaskStatus ? (task.AllottedTimeInDays - task.ElapsedTimeInDays) : 0;
                _logger.LogInformation($"Task with Id {Id} successfully fetched");
                return task;
            }
            catch (Exception ex)
            {
                _logger.LogError("something went wrong contact the admin");
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                throw ex;
            }
        }

        /// <summary>
        /// Add or Create new task
        /// </summary>
        /// <param name="data"></param>
        /// <returns>A Taklist</returns>
        public async Task<TaskList> AddTaskAsync(CreateTaskDto data)
        {
            try
            {
                _logger.LogInformation("about to insert the data for the instance "+ JsonConvert.SerializeObject(data));    
                var model = _mapper.Map<TaskList>(data);
                model.StartDate = DateTime.Now;
                var result = await _taskRepository.AddTaskAsync(model);
                _logger.LogInformation("Task successflly added");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("something went wrong contact the admin");
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                throw ex;
            }
            
        }

        /// <summary>
        /// Delete task by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>A string</returns>
        public async Task<string> DeleteAsync(string Id)
        {
            try
            {
                _logger.LogInformation("about to retrieve the data for the Id " + Id);
                var result = await _taskRepository.DeleteAsync(Id);
                _logger.LogInformation("Task successflly deleted");
                if (!result)
                {
                    return "Item not successfully deleted";
                }
                return $"Item with Id {Id} successfully deleted";
            }
            catch (Exception ex)
            {
                _logger.LogError("something went wrong contact the admin");
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                throw ex;
            }
        }
    }
}
