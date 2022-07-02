using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Database.Contexts;
using VOD.Database.Migrations.DbInitializer;

namespace VOD.Database.Tests.ContextTests
{
    public class CourseTestBase
    {
        private readonly VODContext _db;

        public CourseTestBase()
        {
            _db = new VODContextFactory().CreateDbContext(new string[0]);
            CleanDatabase();
        }

        [TearDown]
        public void CleanDatabase()
        {
            SampleDataInitializer.ClearData(_db);
        }

        
    }
}
