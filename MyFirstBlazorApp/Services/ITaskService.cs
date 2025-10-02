using MyFirstBlazorApp.Models;

namespace MyFirstBlazorApp.Services
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetTasksAsync();
        Task<TaskItem?> GetTaskAsync(int id);
        Task AddTaskAsync(TaskItem task);
        Task UpdateTaskAsync(TaskItem task);
        Task DeleteTaskAsync(int id);


    }
}
