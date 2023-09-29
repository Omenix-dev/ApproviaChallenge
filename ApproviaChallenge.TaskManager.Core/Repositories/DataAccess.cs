

using ApproviaChallenge.TaskManager.Core.Interface;
using ApproviaChallenge.TaskManager.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApproviaChallenge.TaskManager.Core.Repositories
{
    public class DataAccess : ITaskRepository
    {
        public IClient client { get; set; }
        public string? _baseUrl { get; set; }    
        
        public DataAccess(IServiceProvider serviceProvider)
        {
            client = serviceProvider.GetRequiredService<IClient>();
            var config = serviceProvider.GetRequiredService<IConfiguration>();
            _baseUrl = config.GetSection("DataAccessEndpoint").Value;
        }

        /// <summary>
        /// Get all task using httpClientCommandHandler
        /// </summary>
        /// <returns>IEnumerable of tasklist</returns>
        public async Task<IQueryable<TaskList>> GetAllTasksAsync()
        {
            return await client.GetRequest<IQueryable<TaskList>>(_baseUrl);
        }

        /// <summary>
        /// Get task by Id using The base Url and the Id of the task
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>A tasklist</returns>
        public async Task<TaskList> GetTaskByIdAsync(string Id)
        {
            string url = $"{_baseUrl}/{Id}";
            return await client.GetRequest<TaskList>(url);
        }

        /// <summary>
        /// Creates task 
        /// </summary>
        /// <param name="data"></param>
        /// <returns>A tasklist</returns>
        public async Task<TaskList> AddTaskAsync(TaskList data)
        {
            return await client.PostRequest<TaskList, TaskList>(data, _baseUrl);
        }

        /// <summary>
        /// Delete task using the base url and the Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteAsync(string Id)
        {
            string url = $"{_baseUrl}/{Id}";
            await client.DeleteRequest<TaskList>(url);
            return true;
        }
    }
}
