using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VOD.Common.DTOModels.Admin;
using VOD.Admin.Service.Services.Instructors;
using VOD.Admin.Tests.Base;
using VOD.Database.Contexts;

namespace VOD.Admin.Tests.ServiceTests
{
    public class InstructorCUDTests : TestBase
    {
        private const int INSTRID = 102;
        private const int Instru_to_Delete = 1001; // See Sample Data. This is the instructor without courses
        private IInstructorService _instructorService;

        [SetUp]
        public void Init()
        {
            _instructorService = new InstructorService(context);
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

        [Test]
        [TestCase(1001)]
        public void ShouldUpdateInstructorWithId(int instructorID)
        {
            //Arrange
          
            _instructorService = new InstructorService(context);

            InstructorDTO instructor = new InstructorDTO
            {
                Description = "Author of the bestselling book on Dotnet MVC",
                Name = "Adam_ Freeman",
                Id = instructorID,
                Thumbnail="images/Ice-Age-Scrat-icon.png"
            };

            //Act
            var result = _instructorService.UpdateInstructorsInfoAsync(instructor);
            
            //Assert 
            Assert.That(result.Result, Is.EqualTo(1));
        }

        [Test]
        [TestCase(104)]
        public void ShouldGet(int id)
        {
            //Arrange
            var instructor = new InstructorDTO
            {
                Id = 104,
                Name = "Cercei Lannister",
            };
            //Act 
            var result = _instructorService.GetInstructorAsync(id);
            //Assert 
            Assert.That(result.Result.GetType, Is.EqualTo(typeof(InstructorDTO)));
            Assert.That(result.Result.Id == 104 && 
                result.Result.Name.Contains("Cercei"));
        }

        [Test]
        public void ShouldGetInstructorsList()
        {
            //Act 
            var result = _instructorService.GetInstructorsAsync();
            //Assert 

            Assert.That(result.Result.GetType, Is.EqualTo(typeof(List<InstructorDTO>)));
        }

        [Test]
        public void ShouldAddInstructorWithData()
        {
            InstructorDTO instructor = new InstructorDTO
            {
                Description = "Author of the bestselling book on Dotnet MVC",
                Name = "Adam_ Freeman",
                Thumbnail="images/Ice-Age-Scrat-icon.png"
            };
            //Act
            var result = _instructorService.AddInstructorsInfoAsync(instructor);
            //Assert
            Assert.That(result.Result.Equals(1));

        }
        //Idempotency test
        [Test]
        public void ShouldIgnoreAlreadyExisting() 
        {
            InstructorDTO instructor = new InstructorDTO
            {
                Description = "Author of the bestselling book on Dotnet MVC",
                Name = "Adam_ Freeman",
                Thumbnail="images/Ice-Age-Scrat-icon.png"
            };

            InstructorDTO _instructor = new InstructorDTO
            {
                Description = "Author of the bestselling book on Dotnet MVC",
                Name = "Adam_ Freeman",
                Thumbnail = "images/Ice-Age-Scrat-icon.png"
            };
            //Act
           
            var result = _instructorService.AddInstructorsInfoAsync(instructor).Result;
            var _result = _instructorService.AddInstructorsInfoAsync(_instructor).Result;

            //Assert
            Assert.That(_result.Equals(0));


        }
    }
}
