using Microsoft.EntityFrameworkCore;
using MyFirstBlazorApp.Data;
using MyFirstBlazorApp.Models;

namespace MyFirstBlazorApp.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;
        public TaskService(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<List<TaskItem>> GetTasksAsync()
        {
            return await _context.TaskItems.ToListAsync();
        }

        public async Task<TaskItem?> GetTaskAsync(int id)
        {
            return await _context.TaskItems.FindAsync(id);
        }
        public async Task AddTaskAsync(TaskItem task)
        {
            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateTaskAsync(TaskItem task)
        {
            _context.TaskItems.Update(task);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task!=null)
            {
                _context.TaskItems.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
