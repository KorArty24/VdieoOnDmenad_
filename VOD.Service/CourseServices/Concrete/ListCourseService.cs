using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;
using VOD.Database.Contexts;

namespace VOD.Service.CourseServices.Concrete
{
    public class ListCourseService
    {
        private readonly VODContext _context;

        public ListCourseService(VODContext context)
        {
            _context = context;
        }

        public IQueryable<CourseDTO> Sort
    }
}
