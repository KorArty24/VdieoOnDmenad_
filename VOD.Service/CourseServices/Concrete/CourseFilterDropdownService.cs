using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Database.Contexts;
using VOD.Service.CourseServices.QueryObjects;

namespace VOD.Service.CourseServices.Concrete
{
    public class CourseFilterDropdownService
    {
        private readonly VODContext _context;

        public CourseFilterDropdownService(VODContext context)
        {
            _context = context;
        }

        /// <param name="filterBy"></param>
        /// <returns></returns>
        public IEnumerable<DropdownTuple> GetFilterDropdownValues(CoursesFilterBy filterBy)
        {
            return null;
        }
    }
}
