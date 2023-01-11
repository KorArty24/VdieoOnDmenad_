using Microsoft.EntityFrameworkCore;
using VOD.Common.DTOModels.Admin;
using VOD.Admin.Service.Services.Courses;
using VOD.Admin.Tests.Base;
using VOD.Database.Contexts;
using VOD.Admin.Service.Services.Courses;

namespace VOD.Admin.Tests.ServiceTests
{
    public class CourseCRUDTests : TestBase
    {
        private const int CoursID = 10002;
        private const int Course_to_Delete = 1001; // See Sample Data. This is the course without courses
        private ICoursesService _courseService;
        
        [SetUp]
        public void Init()
        {
           using var context_1 = new VODContextFactory().CreateDbContext(new string[0]);
            _courseService = new CoursesService(context);
        }

        [Test]
        [TestCase(CoursID)]
        public void ShouldReturnErrorUponDeleteCourse(int courseId)
        {
            //Arrange 

            //Act 
            var result = _courseService.DeleteCourseAsync(courseId);
            
            //Assert 
            Assert.That(result.Result, Is.Zero);
        }

        [Test]
        [TestCase(Course_to_Delete)]
        public void ShouldDeleteCourseWithID(int id)
        {
            //Act 
            var result = _courseService.DeleteCourseAsync(id);

            //Assert 
            Assert.That(result.Result, Is.EqualTo(1));
        }

        [Test]
        [TestCase(1001)]
        public void ShouldUpdateCourseWithId(int courseId)
        {
            //Arrange
          
            _courseService = new CoursesService(context);

            CourseDTO course = new CourseDTO
            {
                Description = "Author of the bestselling book on Dotnet MVC",
                Title = "Computer geometry with NumPy",
                Id = courseId,
                MarqueeImageUrl="images/Ice-Age-Scrat-icon.png"
            };

            //Act
            var result = _courseService.UpdateCourseInfoAsync(course);
            
            //Assert 
            Assert.That(result.Result, Is.EqualTo(1));
        }

        [Test]
        [TestCase(104)]
        public void ShouldGet(int id)
        {
            //Arrange
            var course = new CourseDTO
            {
                Id = 104,
                Title = "Cercei Lannister",
            };
            //Act 
            var result = _courseService.GetCourseAsync(id);
            //Assert 
            Assert.That(result.Result.GetType, Is.EqualTo(typeof(CourseDTO)));
            Assert.That(result.Result.Id == 104 && 
                result.Result.Title.Contains("Cercei"));
        }

        [Test]
        public void ShouldGetCoursesList()
        {
            //Act 
            var result = _courseService.GetCoursesAsync();
            //Assert 

            Assert.That(result.Result.GetType, Is.EqualTo(typeof(List<CourseDTO>)));
        }

        [Test]
        public void ShouldAddCourseWithData()
        {
            CourseDTO data = new CourseDTO
            {
                Description = "Author of the bestselling book on Dotnet MVC",
                Title = "Adam_ Freeman",
                MarqueeImageUrl="images/Ice-Age-Scrat-icon.png"
            };
            //Act
            var result = _courseService.AddCourseInfoAsync(data);

            //Assert
            Assert.That(result.Result, Is.GreaterThan(0));
            Assert.That(result.Result, Is.InstanceOf<int>());
        }

        //Idempotency test
        [Test]
        public void ShouldIgnoreAlreadyExisting() 
        {
            CourseDTO data = new CourseDTO
            {
                Description = "Author of the bestselling book on Dotnet MVC",
                Title = "Adam_ Freeman",
                ImageUrl="images/Ice-Age-Scrat-icon.png"
            };

            CourseDTO _data = new CourseDTO
            {
                Description = "Author of the bestselling book on Dotnet MVC",
                Title = "Adam_ Freeman",
                ImageUrl = "images/Ice-Age-Scrat-icon.png"
            };
            //Act
           
            var result = _courseService.AddCourseInfoAsync(data).Result;
            var _result = _courseService.AddCourseInfoAsync(_data).Result;

            //Assert
            Assert.That(_result.Equals(0));
        }
    }
}
