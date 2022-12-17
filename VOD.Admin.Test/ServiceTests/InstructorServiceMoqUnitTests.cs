using Moq;
using VOD.Admin.Service.Services.Instructors;

namespace VOD.Admin.Tests.ServiceTests
{
    public class InstructorServiceMoqUnitTests
    {
        private Mock<IInstructorService> _instructorService;
        private const int INSTR_IDMock = 2;
        [SetUp]
        public void SetUp()
        {
            _instructorService = new Mock<IInstructorService>();
        }

        //[Test]
        //public void ShouldDeleteInstructor() 
        //{
        //    _instructorService.Setup(service => service.DeleteInstructorAsync(It.IsAny<int>())).Returns()
        //}

    }
}
