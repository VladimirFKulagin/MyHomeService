
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using MyHomeService.Data;
using MyHomeService.Models;
using MyHomeService.Services;

namespace MyHomeService.Tests.Services
{
    public class TaskServiceTests : IDisposable
    {
        private readonly AppDbContext _context;
        private readonly IMemoryCache _cache;
        private readonly TaskService _taskService;

        public TaskServiceTests()
        {
            var option = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new AppDbContext(option);
            _cache = new MemoryCache(new MemoryCacheOptions());
            _taskService = new TaskService(_context, _cache);
        }

        [Fact]
        public async Task GetTaskAsync_EmptyDatabase_ReturnsEmptyList()
        {
            //Arrange
            var result = await _taskService.GetTasksAsync();

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetTaskAsync_WithTasks_ReturnsAllTAsks()
        {
            //Arrange
            _context.TaskItems.Add(new TaskItem { Title = "Test title 1" });
            _context.TaskItems.Add(new TaskItem { Title = "Test title 2" });
            _context.TaskItems.Add(new TaskItem { Title = "Test title 3" });
            await _context.SaveChangesAsync();

            //Act
            var result = await _taskService.GetTasksAsync();

            //Assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task AddTaskAsync_ValidTasks_AddToDB()
        {
            //Arrange
            var newTask = new TaskItem { Title = "Test task", Description = "Added for test add-function" };
            //Act
            await _taskService.AddTaskAsync(newTask);

            //Assert
            var dbTask = await _context.TaskItems.FirstAsync();
            Assert.Equal("Test task", dbTask.Title);
        }

        [Fact]
        public async Task GetTaskAsync_ExisingTasks_ReturnFromDb()
        {
            //Arrange
            var task = new TaskItem { Title = "Test task 1" };
            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();

            //Act
            var result = await _taskService.GetTaskAsync(task.Id);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Test task 1", result.Title);
        }

        [Fact]
        public async Task GetTaskAsync_NonExistingTask_ReturnsNull()
        {
            //Act
            var result = await _taskService.GetTaskAsync(999);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateTaskAsync_ExistingTask_UpdateTitle()
        {
            //Arrange
            var task = new TaskItem { Title = "TeZt task1" };
            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();
            //Act
            task.Title = "Updated title";
            await _taskService.UpdateTaskAsync(task);
            //Assert
            var updatedTask = await _context.TaskItems.FindAsync(task.Id);
            Assert.NotNull(updatedTask);
            Assert.Equal("Updated title", updatedTask.Title);
        }

        [Fact]
        public async Task UpdateTaskAsync_ExistingTask_UpdateIsCompletedStatus()
        {
            //Arrange
            var task = new TaskItem { Title = "Test task1" };
            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();
            //Act
            task.IsCompleted = true;
            await _taskService.UpdateTaskAsync(task);
            //Assert
            var updatedTask = await _context.TaskItems.FindAsync(task.Id);
            Assert.True(updatedTask!.IsCompleted);
        }

        [Fact]
        public async Task DeleteTaskAsync_ExistingTask_DeleteFromDatabase()
        {
            //Arrange
            var task = new TaskItem { Title = "Test task1 to delete" };
            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();
            //Act
            await _taskService.DeleteTaskAsync(task.Id);
            //Assert
            var tasks = await _context.TaskItems.ToListAsync();
            Assert.Empty(tasks);
        }

        [Fact]
        public async Task DeleteTaskAsync_NonExistingTask()
        {
            await _taskService.DeleteTaskAsync(899);
        }

        public void Dispose()
        {
            _context?.Dispose();
            _cache?.Dispose();
        }
    }
}
