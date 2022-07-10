using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.Entities;
using Microsoft.EntityFrameworkCore;
using VOD.Database.Tests.Base;
using VOD.Database.Migrations.DbInitializer;

namespace VOD.Database.Tests.CrudTests
{
    public class CreationTests : TestBase
    {
        
        [Test]
        public void ShouldAddNewCourseRecord()
        {
            var cour = new Course
            {
                Instructor = new Instructor { Name = "John Doe" },
                Title = "CLR via",
                Description = "No description"
            };
            ExecuteInATransaction(RunTheTest);

            void RunTheTest()
            {
                //Arrange
                SampleDataInitializer.ClearData(context);
                context.Courses.Add(cour);
                context.SaveChanges();

                //Act
                var singlecourse = context.Courses.ToList();

                //Assert
                Assert.That(singlecourse.Count(), Is.EqualTo(1));
                
            }
        }
        [Test]
        public void ShouldAddAnObjectGraph()
        {
           //Arrange
            var cours = new List<Course>()
            {
                new Course
                {
                    Title = "Essential EF Core",
                    Description = "Dive into the hidden depths of the Framework",
                    Instructor = new Instructor { Name = "Troelsen Japikse" },
                    Modules = new List<Module>
                    {
                       new Module
                       {
                           Title="Module 1"
                       }
                    }
                },
                new Course
                {
                    Title = "Course2",
                    Description = "Course 2 Descript",
                    Instructor = new Instructor { Name = "Japikse" },
                    Modules = new List<Module>
                    {
                       new Module
                       {
                           Title="Module 2"
                       }
                    }
                }
            };

            //Act
            ExecuteInATransaction(RunTheTest);
            void RunTheTest()
            {
                SampleDataInitializer.ClearData(context);
                context.Courses.AddRange(cours);
                context.SaveChanges();

                //Assert
                Assert.That(context.Courses.Count(), Is.EqualTo(2));
            }
        }
    }
}
