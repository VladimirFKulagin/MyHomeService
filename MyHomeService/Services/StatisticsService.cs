using Microsoft.Extensions.Caching.Memory;
using MyHomeService.Models;

namespace MyHomeService.Services
{
    public class StatisticsService
    {
        private readonly ITaskService _taskService;
        private readonly IMemoryCache _cache;
        public StatisticsService(ITaskService taskService, IMemoryCache cache)
        {
            _taskService = taskService;
            _cache = cache;
        }

        public async Task<HomeStatistics> GetHomeStatisticsAsync()
        {
            return (await _cache.GetOrCreateAsync("home_statistics", async entry =>
            {
                entry.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5);

                var tasks = await _taskService.GetTasksAsync();
                return new HomeStatistics
                {
                    ActiveTasksCount = tasks.Count(t => !t.IsCompleted),
                    CompletedTasksCount = tasks.Count(t => t.IsCompleted)
                };
            }))!;
        }
    }
}
