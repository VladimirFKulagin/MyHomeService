
using MyHomeService.Models;

namespace MyHomeService.Tests.Models
{
    public class TaskItemTests
    {
        [Fact]
        public void TaskItem_DefaultTitle_IsEmpty()
        {
            //Arrange
            var task = new TaskItem();
            //Act
            //Assert
            Assert.Empty(task.Title);
        }

        [Fact]
        public void TaskItem_DefaultDescription_IsEmpty()
        {
            //Arrange
            var task = new TaskItem();
            //Act
            //Assert
            Assert.Empty(task.Description);
        }

        [Fact]
        public void TaskItem_DefaultIsCompleted_IsFalse()
        {
            //Arrange
            var task = new TaskItem();
            //Act
            //Assert
            Assert.False(task.IsCompleted);
        }


        [Theory]
        [InlineData("Test title")]
        [InlineData("")]
        [InlineData(null)]
        public void TaskItem_SetTitle_SetCorrectValue(string title)
        {
            //Arrange
            var task = new TaskItem();
            //Act
            task.Title = title;
            //Assert
            Assert.Equal(title, task.Title);
        }

        [Theory]
        [InlineData("Test description")]
        [InlineData("")]
        [InlineData(null)]
        public void TaskItem_SetDescription_SetCorrectValue(string description)
        {
            //Arrange
            var task = new TaskItem();
            //Act
            task.Description = description;
            //Assert
            Assert.Equal(description, task.Description);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TaskItem_SetIsCompleted_SetCorrectValue(bool isCompleted)
        {
            //Arrange
            var task = new TaskItem();
            //Act
            task.IsCompleted = isCompleted;
            //Assert
            Assert.Equal(isCompleted, task.IsCompleted);
        }

    }
}
