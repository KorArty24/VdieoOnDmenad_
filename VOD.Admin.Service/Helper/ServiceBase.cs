using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Database.Contexts;
using VOD.Database.Exceptions;

namespace VOD.Admin.Service.Helper
{
    public abstract class ServiceBase
    {
        protected readonly VODContext _context;

        public ServiceBase(VODContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChanges()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new VideoOnDemandConcurencyException("A concurrency error happened.",ex);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException)
                {
                    if (sqlException.Message.Contains("FOREIGN KEY constraint", StringComparison.OrdinalIgnoreCase))
                    {
                        if (sqlException.Message.Contains("table \"VOD.Courses\" column 'Id' ", StringComparison.OrdinalIgnoreCase))
                        {
                            throw new VODInvalidCourseException($"Invalid Course Id \r\n{ex.Message}", ex);
                        }
                    }
                }
                throw new VideoOnDemandException("An error occured updating the database", ex);
            }
        } 
    }
}
