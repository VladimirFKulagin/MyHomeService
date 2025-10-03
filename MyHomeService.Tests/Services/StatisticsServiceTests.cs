
using Microsoft.Extensions.Caching.Memory;
using Moq;
using MyHomeService.Models;
using MyHomeService.Services;

namespace MyHomeService.Tests.Services
{
    public class StatisticsServiceTests : IDisposable
    {
        private readonly Mock<ITaskService> _mockTaskService;
        private readonly IMemoryCache _cache;
        private readonly StatisticsService _statisticsService;

        public StatisticsServiceTests()
        {
            _mockTaskService = new Mock<ITaskService>();
            _cache = new MemoryCache(new MemoryCacheOptions());
            _statisticsService = new StatisticsService(_mockTaskService.Object, _cache);
        }

        [Fact]
        public async Task GetStatisticsAsync_EmptyDatabase_ReturnsZeroActiveTasks()
        {
            //Arrange
            _mockTaskService.Setup(x => x.GetTasksAsync())
                .ReturnsAsync(new List<TaskItem>());
            //Act
            var result = await _statisticsService.GetHomeStatisticsAsync();
            //Assert
            Assert.Equal(0, result.ActiveTasksCount);
        }

        [Fact]
        public async Task GetStatisticsASync_WithMixedTasks_CalculateActiveTasksCorrectly()
        {
            //Arrange
            var tasks = new List<TaskItem>()
            {
                new TaskItem{IsCompleted=true},
                new TaskItem(),
                new TaskItem()
            };
            _mockTaskService.Setup(x => x.GetTasksAsync())
                .ReturnsAsync(tasks);
            //Act
            var result = await _statisticsService.GetHomeStatisticsAsync();
            //Assert
            Assert.Equal(2, result.ActiveTasksCount);
        }

        [Fact]
        public async Task GetStatisticsASync_WithMixedTasks_CalculateCompletedTasksCorrectly()
        {
            //Arrange
            var tasks = new List<TaskItem>()
            {
                new TaskItem{IsCompleted=true},
                new TaskItem{IsCompleted=true},
                new TaskItem(),
                new TaskItem{IsCompleted=true}
            };
            _mockTaskService.Setup(x => x.GetTasksAsync())
                .ReturnsAsync(tasks);
            //Act
            var result = await _statisticsService.GetHomeStatisticsAsync();
            //Assert
            Assert.Equal(3, result.CompletedTasksCount);
        }

        public void Dispose()
        {
            _cache?.Dispose();
        }
    }
}
