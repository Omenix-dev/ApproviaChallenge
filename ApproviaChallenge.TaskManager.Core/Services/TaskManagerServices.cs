using ApproviaChallenge.TaskManager.Core.DTOs;
using ApproviaChallenge.TaskManager.Core.Interface;
using Microsoft.Extensions.DependencyInjection;
using ApproviaChallenge.TaskManager.Core.Models;
using AutoMapper;
using Microsoft.Extensions.Logging;

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
                var tasks = await _taskRepository.GetAllTasksAsync();
                var response = _mapper.Map<IEnumerable<TaskListDto>>(tasks);
                if (response == null)
                {
                    _logger.LogError($"Error occured getting from database");
                    return new List<TaskListDto>();

                }
                foreach (var task in response)
                {
                    task.DueDate = task.StartDate.AddDays(task.AllottedTimeInDays);
                    task.EndDate = task.StartDate.AddDays(task.ElapsedTimeInDays);
                    task.DaysOverDue = !task.TaskStatus ? (task.ElapsedTimeInDays - task.AllottedTimeInDays) : 0;
                    task.DaysLate = task.TaskStatus ? (task.AllottedTimeInDays - task.ElapsedTimeInDays) : 0;
                    _logger.LogInformation("getting all task");
                }
                return response;
            }
            catch (Exception ex)
            {

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
                var tasks = await _taskRepository.GetTaskByIdAsync(Id);
                var task = _mapper.Map<TaskListDto>(tasks);
                task.DueDate = task.StartDate.AddDays(task.AllottedTimeInDays);
                task.EndDate = task.StartDate.AddDays(task.ElapsedTimeInDays);
                task.DaysOverDue = !task.TaskStatus ? (task.ElapsedTimeInDays - task.AllottedTimeInDays) : 0;
                task.DaysLate = task.TaskStatus ? (task.AllottedTimeInDays - task.ElapsedTimeInDays) : 0;
                _logger.LogInformation($"Task with Id {Id} successfully fetched");
                return task;
            }
            catch (Exception ex)
            {

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
                var model = _mapper.Map<TaskList>(data);
                model.StartDate = DateTime.Now;
                var result = await _taskRepository.AddTaskAsync(model);
                _logger.LogInformation("Task successflly added");
                return result;
            }
            catch (Exception ex)
            {
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

                throw ex;
            }
            
        }
    }
}
