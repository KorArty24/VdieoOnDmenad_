using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace VOD.Database.Contexts
{
    public class VODContextFactory : IDesignTimeDbContextFactory<VODContext>
    {
        public VODContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<VODContext>();
            var connectionString = @"Server=.,6433;Database=VOD;User ID=sa;Password=<MyPassw0rd>;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
            Console.WriteLine(connectionString);
            return new VODContext(optionsBuilder.Options);
        }
    }
}
