using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Database.Contexts;
using VOD.Database.Migrations.DbInitializer;

namespace VOD.Database.Tests.Base
{
    [TestFixture]
    public class CourseTestBase
    {
        protected VODContext context;

        [TearDown]
        public void CleanDatabase()
        {
            SampleDataInitializer.ClearData(context);
        }
    }
}
