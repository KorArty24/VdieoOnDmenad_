using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Database.Migrations.DbInitializer;
using VOD.Database.Tests.Base;

namespace VOD.Database.Tests.InitializationTests
{
    public class InitializationTest: TestBase
    {
        [Test]
        public void ShouldDropAndCreateDatabase()
        {
            //Arrange 
            SampleDataInitializer.DropAndCreateDatabase(context);
            //Assert
            Assert.IsEmpty(context.Courses.ToList());
            Assert.IsEmpty(context.Modules.ToList());
            Assert.IsEmpty(context.Instructors.ToList());
            Assert.IsEmpty(context.Downloads.ToList()); 
        }

        [Test]
        public void ShouldLoadCoursesWithModulesAndInstructors()
        {
            SampleDataInitializer.DropAndCreateDatabase(context);
            SampleDataInitializer.InitializeData(context);
            Assert.That(context.Courses.Count(), Is.EqualTo(3));
            Assert.That(context.Modules.Count(), Is.EqualTo(4));
            Assert.That(context.Instructors.Count(), Is.EqualTo(3));
            SampleDataInitializer.DropAndCreateDatabase(context);
        }
    }
}
