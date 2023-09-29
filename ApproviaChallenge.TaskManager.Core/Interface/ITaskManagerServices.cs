using ApproviaChallenge.TaskManager.Core.DTOs;
using ApproviaChallenge.TaskManager.Core.Models;

namespace ApproviaChallenge.TaskManager.Core.Interface
{
    public interface ITaskManagerServices
    {
        Task<TaskList> AddTaskAsync(CreateTaskDto data);
        Task<string> DeleteAsync(string Id);
        Task<IEnumerable<TaskListDto>> GetAllTasksAsync();
        Task<TaskListDto> GetTaskByIdAsync(string Id);
    }
}
