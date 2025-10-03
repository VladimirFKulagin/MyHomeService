using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MyHomeService.Data;
using MyHomeService.Models;

namespace MyHomeService.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;
        private readonly IMemoryCache _cache;
        public TaskService(AppDbContext context, IMemoryCache cache) 
        {
            _context = context;
            _cache = cache;
        }

        public async Task<List<TaskItem>> GetTasksAsync()
        {
            return await _cache.GetOrCreateAsync("all_tasks", async entry =>
            {
                entry.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5);
                return await _context.TaskItems.ToListAsync();
            }) ?? new List<TaskItem>();
        }

        public async Task<TaskItem?> GetTaskAsync(int id)
        {
            return await _context.TaskItems.FindAsync(id);
        }
        public async Task AddTaskAsync(TaskItem task)
        {
            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();
            _cache.Remove("all_tasks"); 
            _cache.Remove("home_statistics");
        }
        public async Task UpdateTaskAsync(TaskItem task)
        {
            _context.TaskItems.Update(task);
            await _context.SaveChangesAsync();
            _cache.Remove("all_tasks");
            _cache.Remove("home_statistics");
        }
        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task!=null)
            {
                _context.TaskItems.Remove(task);
                await _context.SaveChangesAsync();
                _cache.Remove("all_tasks");
                _cache.Remove("home_statistics");
            }
        }
    }
}
