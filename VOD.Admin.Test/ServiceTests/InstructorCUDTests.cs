using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VOD.Admin.Service.Services.Instructors;
using VOD.Admin.Tests.Base;
using VOD.Database.Contexts;

namespace VOD.Admin.Tests.ServiceTests
{
    public class InstructorCUDTests : TestBase
    {
        private readonly IMapper mapper;
        private const int INSTRID = 102;
        private const int Instru_to_Delete = 1001; // See Sample Data. This is the instructor without courses
        private InstructorService _instructorService;

        [SetUp]
        public void Init()
        {
            _instructorService = new InstructorService(context, mapper);
        }

        [Test]
        [TestCase(INSTRID)]
        public void ShouldReturnErrorUponDeleteInstructor(int instructorId)
        {
            //Arrange 

            //Act 
            var result = _instructorService.DeleteInstructorAsync(instructorId);
            
            //Assert 
            Assert.That(result.Result, Is.Zero);
        }
        [Test]
        [TestCase(Instru_to_Delete)]
        public void ShouldDeleteInstructorWithID(int id)
        {
            //Act 
            var result = _instructorService.DeleteInstructorAsync(id);

            //Assert 
            Assert.That(result.Result, Is.EqualTo(1));
        }
    }
}
