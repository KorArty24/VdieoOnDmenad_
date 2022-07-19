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
    /// <summary>
    /// Provides a service for selection one particular course from the list of all
    /// the courses availible in /List (for any registered user). !Not UserCourses
    /// </summary>
    public class CourseSelectedService
    {
        private readonly VODContext _context;

        public CourseSelectedService(VODContext context)
        {
            _context = context;
        }

        public async Task<CourseDTO> SelectedCoursePage(int id)
        {
            var courseQuery = await _context.Courses
                .AsNoTracking().SingleAsync(k => k.Id == id);
            
            return CourseDtoSelect.CreateCourseCard(courseQuery);
        }
    }
}
