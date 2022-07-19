using Moq;
using VOD.Common.DTOModels.UI;
using VOD.Service.CourseServices.Interfaces;

namespace VOD.Service.Tests
{
    public class Tests
    {
        private Mock<IUserCourseSelectedService> _mockService;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IUserCourseSelectedService>();
        }

        [Test]
        [TestCase("1",1)]
        public void ShouldReturnCoursePageSelected(string userId, int Id)
        {
            _mockService.Setup(x=> x.SelectedCoursePageAsync(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(() => new CourseDTO { CourseId=1, CourseDescription="Des"});
        }
    }
}