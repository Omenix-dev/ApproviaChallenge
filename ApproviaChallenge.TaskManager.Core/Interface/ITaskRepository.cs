using ApproviaChallenge.TaskManager.Core.Models;

namespace ApproviaChallenge.TaskManager.Core.Interface
{
    public interface ITaskRepository
    {
        Task<TaskList> AddTaskAsync(TaskList data);
        Task<bool> DeleteAsync(string Id);
        Task<IQueryable<TaskList>> GetAllTasksAsync();
        Task<TaskList> GetTaskByIdAsync(string Id);
    }
}
