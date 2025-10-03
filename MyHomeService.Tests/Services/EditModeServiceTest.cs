
using MyHomeService.Services;

namespace MyHomeService.Tests.Services
{
    public class EditModeServiceTest
    {
        [Fact]
        public void EditModeService_Constructor_SetIsEditModeToFalse()
        {
            //Arrange
            var service = new EditModeService();
            //Assert
            Assert.False(service.IsEditMode);
        }

        [Fact]
        public void EditModeService_Constructor_SetIsEditModeToTrue()
        {
            //Arrange
            var service = new EditModeService();
            service.IsEditMode = true;
            //Assert
            Assert.True(service.IsEditMode);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void EditModeService_IsEditMode_CanChange(bool mode)
        {
            //Arrange
            var service = new EditModeService { IsEditMode = mode };
            service.IsEditMode = !mode;
            //Assert
            Assert.NotEqual(mode, service.IsEditMode);
        }

    }
}
