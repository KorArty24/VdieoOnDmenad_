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
        private const int CoursID = 1001;
        private const int Course_to_Delete = 103; // See Sample Data. This is the course without courses
        private ICoursesService _courseService;
        
        [SetUp]
        public void Init()
        {
            _courseService = new CoursesService(context);
        }

        [Test]
        [TestCase(101)]
        public void ShouldGet(int id)
        {

            var result = _courseService.GetCourseAsync(id);
           
            //Assert 
            Assert.That(result.Result.GetType, Is.EqualTo(typeof(CourseDTO)));
            Assert.That(result.Result.Id == 101 && 
                result.Result.Title.Contains("Foundations"));
        }

        [Test]
        [TestCase(CoursID)]
        public async Task ShouldReturnErrorUponDeleteNonExistCourse(int courseId)
        {
            //Arrange 
            //Act 
            var result = await Task.FromResult(_courseService.DeleteCourseAsync(courseId)).Result;
            //Assert 
            Assert.That(result, Is.Zero);
        }

        [Test]
        [TestCase(Course_to_Delete)]
        public async Task ShouldDeleteCourseWithID(int id)
        {
            //Act 
            var result = await Task.FromResult(_courseService.DeleteCourseAsync(id)).Result;

            //Assert 
            Assert.That(result, Is.GreaterThan(1));
        }

        [Test]
        [TestCase(102)]
        public void ShouldUpdateCourseWithId(int courseId)
        {
          
            CourseDTO course = new CourseDTO
            {
                Description = "Author of the bestselling book on Dotnet MVC",
                Title = "Computer geometry with NumPy",
                Id = 101,
                MarqueeImageUrl="images/Ice-Age-Scrat-icon.png",
                InstructorId =102,
                Instructor = "John Doe"
            };

            //Act
            var result = _courseService.UpdateCourseInfoAsync(course);
            
            //Assert 
            Assert.That(result.Result, Is.EqualTo(1));
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
                Description = "WPF FOR CAD",
                Title = "WPF IN ACTION",
                MarqueeImageUrl="images/Ice-Age-Scrat-icon.png",
                InstructorId=102
            };
            //Act
            var result = _courseService.AddCourseInfoAsync(data);

            //Assert
            Assert.That(result.Result, Is.GreaterThan(0));
            Assert.That(result.Result, Is.InstanceOf<int>());
        }

        //Idempotency test
        [Test]
        public async Task ShouldIgnoreAlreadyExisting() 
        {
            CourseDTO data = new CourseDTO
            {
                InstructorId=102,
                Title="Course 1. Foundations of C#",
                Description= "master the powerful programming language for only 100$ a month",
                ImageUrl = "images/course1.jpg",
                MarqueeImageUrl="/images/laptop.jpg",
            };

            //Act
            var result = await Task.FromResult(_courseService.AddCourseInfoAsync(data)).Result;

            //Assert
            Assert.That(result.Equals(0));
        }
    }
}
