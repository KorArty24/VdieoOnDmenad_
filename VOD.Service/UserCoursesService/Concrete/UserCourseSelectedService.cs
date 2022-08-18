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
using VOD.Service.CourseServices.Interfaces;
using VOD.Service.CourseServices.QueryObjects;


namespace VOD.Service.UserCoursesService.Concrete
{
    /// <summary>
    /// Provides a service for selection one particular course from the list of all
    /// the courses availible in /List (for any registered user). !Not UserCourses
    /// </summary>
    public class UserCourseSelectedService : IUserCourseSelectedService
    {
        private readonly VODContext _context;

        public UserCourseSelectedService(VODContext context)
        {
            _context = context;
        }

        public async Task<CourseDTO> SelectedCoursePageAsync(string userId, int courseId)
        {
            var courseQuery = await _context.UserCourses
                .AsNoTracking().SingleAsync(k => k.UserId.Equals(userId) && k.CourseId.Equals(courseId));
            if (courseQuery == null) return default;
            return UserCourseDtoSelect.CreateCourseCard(courseQuery);
        }

        public async Task<Course> GetUserCourseSelected(string userId, int courseId)
        {
            var courseQuery = await _context.UserCourses
                .AsNoTracking().SingleAsync(k => k.UserId.Equals(userId) && k.CourseId.Equals(courseId));
            if (courseQuery == null) return default;
            return courseQuery.Course;
        }
    }
}
