using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;
using VOD.Common.Entities;
using VOD.Database.Contexts;
using VOD.Database.QueryObjects;
using VOD.Service.CourseServices.QueryObjects;

namespace VOD.Service.CourseServices.Concrete
{
    public class ListCourseService
    {
        private readonly VODContext _context;

        public ListCourseService(VODContext context)
        {
            _context = context;
        }

        public IQueryable<CourseWithInstructorAndVideosDTO> SortCoursePage
            (SortFilterPageOptions options)
        {
            var courseQuery = _context.Courses
                .AsNoTracking().MapCourseToDTO().OrderCoursesBy(options.OrderByOptions)
                .FilterCoursesBy(options.FilterBy, options.FilterValue)
                ;

            return courseQuery.Page(options.PageNum-1, options.PageSize);
        }
    }
}
