using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Database.Contexts;

namespace VOD.Service.DatabaseServices.Concrete
{
    public class DbReadService : IDbReadService
    {
        private VODContext context;
        public (int courses, int downloads, int instructors, int modules, int videos, int users) Count()
        {
            return(
                courses: context.Courses.Count(),
                downloads: context.Downloads.Count(),
                instructors: context.Instructors.Count(),
                modules: context.Modules.Count(),
                videos: context.Videos.Count(),
                users: context.Users.Count());
        }
    }
}
