using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using VOD.Common.Entities;
using VOD.Database.Contexts;
using VOD.Database.Migrations.DbInitializer;

namespace VOD.DAL.Tests.ContextTests
{
    [TestFixture]
    public class CourseTests
    {
        private readonly VODContext _db;
        
        public CourseTests()
        {
            _db = new VODContextFactory().CreateDbContext(new string[0]);
            CleanDatabase();
        }

       [TearDown]
        public void CleanDatabase()
        {
            SampleDataInitializer.ClearData(_db);
        }

        [Test]
        public void FirstTest() 
        {
            Assert.True(true);
        }

        [Test]
        public void ShouldAddACourseWithDbSet()
        {
            //Arrange 
            var course = new Course { Title = "Essential EF", Description = "Start building your apps with EF",
                Instructor= new Instructor { Name = "Phil Japikse", } };

            //Act 
            _db.Courses.Add(course);

            // Assert
            Assert.That(_db.Entry(course).State, Is.EqualTo(EntityState.Added));
            Assert.Null(course.TimeStamp);
            _db.SaveChanges();
            Assert.That(_db.Entry(course).State, Is.EqualTo(EntityState.Unchanged));
            Assert.NotNull(course.TimeStamp);
            Assert.That(_db.Courses.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ShouldAddCourseWithContext() 
        {
            //Arrange 
            var course = new Course
            {
                Title = "Essential EF",
                Description = "Start building your apps with EF",
                Instructor = new Instructor { Name = "Phil Japikse", }
            };

            //Act 
            _db.Add(course);

            // Assert
            Assert.That(_db.Entry(course).State, Is.EqualTo(EntityState.Added));
            Assert.Null(course.TimeStamp);
            _db.SaveChanges();
            Assert.That(_db.Entry(course).State, Is.EqualTo(EntityState.Unchanged));
            Assert.NotNull(course.TimeStamp);
            Assert.That(_db.Courses.Count(), Is.EqualTo(1));

        }

        [Test]
        public void ShouldGetAllCoursesOrderedByName() 
        {
            //Arrange
            _db.Courses.Add(new Course
            {
                Title = "Essential EF Core",
                Description = "Dive into the hidden depths of the Framework",
                Instructor = new Instructor { Name = "Troelsen Japikse" }
            });
            _db.Courses.Add(new Course
            {
                Title = "Docker in Nutshell",
                Description = "Learn to use the revolutionary containerization DevOps tool",
                Instructor = new Instructor { Name = "Troelsen Japikse" }
            });

            //Act 
            _db.SaveChanges();
            var courses = _db.Courses.OrderBy(c => c.Title).ToList();

            //Assert
            Assert.AreEqual(courses.Count, 2);
            Assert.AreEqual("Docker in Nutshell", courses[0].Title);
            Assert.AreEqual("Essential EF Core", courses[1].Title);

        }
    }
}
