using Microsoft.EntityFrameworkCore;
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
    public class TestBase
    {
        protected VODContext context;

        public TestBase()
        {
            context = new VODContextFactory().CreateDbContext(new string[0]);
            CleanDatabase();
        }

        [TearDown]
        public void CleanDatabase()
        {
            SampleDataInitializer.ClearData(context);
        }

        protected void ExecuteInATransaction(Action actionToExecute)
        {
            var strategy = context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                using (var trans = context.Database.BeginTransaction())
                {
                    actionToExecute();
                    trans.Rollback();
                }
            });

        }
    }
}
