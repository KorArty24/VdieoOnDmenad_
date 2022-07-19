using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using VOD.Common.Entities;
using VOD.Database.Contexts;
using VOD.Database.Migrations.DbInitializer;
using VOD.Database.Tests.Base;
using VOD.Database.Tests.ContextTests.PartrialSeeders;

namespace VOD.Database.Tests.ContextTests
{
    /// <summary>
    /// Integration tests against a dockerized db. See justification at https://docs.microsoft.com/en-us/ef/core/testing/choosing-a-testing-strategy
    /// </summary>

    [TestFixture]
    public class CourseTests : TestBase
    {
        //private readonly VODContext _db;
        protected VODContext _db
        {
            get { return this.context; }
        }

        //public CourseTests()
        //{
        //    _db = new VODContextFactory().CreateDbContext(new string[0]);
        //    CleanDatabase();
        //}


        [Test]
        public void FirstTest() 
        {
            Assert.True(true);
        }

        [Test]
        public void ShouldAddACourseWithDbSet()
        {
            //Arrange 
            var course = CourseFactory.NewFirstCourse();

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
            var course = CourseFactory.NewFirstCourse();

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
            _db.Courses.Add(CourseFactory.NewFirstCourse());
            _db.Courses.Add(CourseFactory.NewSecondCourse());

            //Act 
            _db.SaveChanges();
            var courses = _db.Courses.OrderBy(c => c.Title).ToList();
            
            //Assert
            Assert.That(courses.Count, Is.EqualTo(2));
            Assert.That(courses[0].Title, Is.EqualTo("Course 1. Essential EF Core"));
            Assert.That(courses[1].Title, Is.EqualTo("Docker in Nutshell"));

        }

        [Test]
        public void ShoudUpdateCourse()
        {
            //Arrange
            var course = CourseFactory.NewFirstCourse();

           //Act
            _db.Courses.Add(course);
            _db.SaveChanges();
            course.Title = "Programming abstractions in C++";
            _db.Courses.Update(course);

            //Assert
            Assert.That(_db.Entry(course).State, Is.EqualTo(EntityState.Modified));

            //Act 
            _db.SaveChanges();

            //Assert
            Assert.That(_db.Entry(course).State, Is.EqualTo(EntityState.Unchanged));
            
            //Arrange
            VODContext context;
            
            //Assert 
            using (context = new VODContextFactory().CreateDbContext(null)) 
            {
                Assert.That(context.Courses.First().Title, Is.EqualTo("Programming abstractions in C++"));
            }
        }

        ///Invoking update on the new Entity should throw exception
        [Test]
        public void ShouldNotUpdateNonAttachedCourse()
        {
            //Arrange
            var course = CourseFactory.NewFirstCourse();

            //Act
            _db.Courses.Add(course);
            course.Title = "Microservices in Action with Nancy";

            //Assert 
            Assert.Throws<InvalidOperationException>(() => _db.Courses.Update(course));
        }

        [Test]
        public void ShouldDeleteACourse()
        {
            //Arrange 
            var course = CourseFactory.NewFirstCourse();

            //Act
            _db.Courses.Add(course);
            _db.SaveChanges();

            //Assert 
            Assert.That(_db.Courses.Count(), Is.EqualTo(1));
            
            //Act
            _db.Courses.Remove(course);

            // Assert 
            Assert.That(_db.Entry(course).State, Is.EqualTo(EntityState.Deleted));

            //Act 
            _db.SaveChanges();

            // Assert
            Assert.That(_db.Entry(course).State, Is.EqualTo(EntityState.Detached));
            Assert.That (_db.Courses.Count(), Is.EqualTo(0));
        }
        [Test]
        public void ShouldDeleteACourseWithTimestampData() 
        {
           //Arrange
            var course = CourseFactory.NewFirstCourse();

            //Act
            _db.Courses.Add(course);
            _db.SaveChanges();
            var context = new VODContextFactory().CreateDbContext(null);
            var courToDelete = new Course
            {
                Id = course.Id,
                TimeStamp = course.TimeStamp
            };
            context.Entry(courToDelete).State = EntityState.Deleted;
            var affected = context.SaveChanges();

            //Assert 
            Assert.That(affected, Is.EqualTo(1));
            
        }

        [Test]
        public void ShouldNotDeleteACourseWithoutTimeStampData()
        {
            //Arrange
            var course = CourseFactory.NewFirstCourse();
            
            //Act
            _db.Courses.Add(course);

            _db.SaveChanges();
            var context = new VODContextFactory().CreateDbContext(null);
            var crToDelete = new Course { Id = course.Id };
            context.Courses.Remove(crToDelete);
            var ex = Assert.Throws<DbUpdateConcurrencyException>(() => context.SaveChanges());
            
            //Assert
            Assert.That(ex.Entries.Count, Is.EqualTo(1));
            Assert.That(((Course)ex.Entries[0].Entity).Id, Is.EqualTo(course.Id));

        }

        [Test]
        public void ShouldReturnTotalDurationForCourse() 
        {
            //Arrange 
            var course = CourseFactory.NewFirstCourse();
            _db.Courses.Add(course);
            _db.SaveChanges();
            int courseid = _db.Courses.First().Id;
            var modules = CourseFactory.ReturnNewModuleandVideoForCourse(courseid);
            _db.Modules.AddRange(modules);
            _db.SaveChanges();
            var context = new VODContextFactory().CreateDbContext(null);

            //Act 
            //int duration =
            //    (from vid in context.Videos
            //     where vid.CourseId == courseid
            //     select vid.Duration).Sum();
            int duration = _db.Courses.AsNoTracking().Where(c => c.Id == courseid).SelectMany(c => c.Modules)
                .SelectMany(m => m.Videos).Select(v => v.Duration).Sum();

            //Assert
            Assert.That(duration, Is.EqualTo(45));   

                

        }
    }
}
