using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Database.Migrations.DbInitializer;
using VOD.Database.Tests.Base;

namespace VOD.Database.Tests.CrudTests
{
    public class DeleteTests: TestBase
    {
        public Action RunTheTest { get; private set; }


        [Test]
        public void ShouldREeadThenDeleteRecordsGraph()
        {
            ExecuteInATransaction(RunTheTest);

            void RunTheTest()
            {
                SampleDataInitializer.ClearData(context);
                var data = TestDataUnits.NewCourseWithInstructorAndModule();
                context.Courses.Add(data);
                context.SaveChanges();
                var datatodelete = context.Courses.Include(i => i.Instructor).Include(m => m.Modules).
                    First();
                context.Courses.Remove(data);

                //Assert
                Assert.That(context.Entry(data).State, Is.EqualTo(EntityState.Deleted));
                context.SaveChangesAsync();
                Assert.AreEqual(EntityState.Detached, context.Entry(data).State);
            }

        }
    }
}
