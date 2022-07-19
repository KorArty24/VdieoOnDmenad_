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
using VOD.Service.CourseServices;
using VOD.Service.CourseServices.QueryObjects;


namespace VOD.Service.UserCoursesService.Concrete
{
   /// <summary>
   /// Select all the courses for which user registered.    
   /// </summary>
   /// 
    public class UserCourseService
    {
        private readonly VODContext _context;

        public UserCourseService(VODContext context)
        {
            _context = context;
        }

        public  IQueryable<CourseWithInstructorAndVideosDTO> SortUserCoursePage(int userid)
        {
            var courseQuery = _context.UserCourses.
                AsNoTracking().Where(uc => uc.UserId.Equals(userid)).Select(c => c.Course)
                .MapCourseToDTO();

            return courseQuery;
        }
    }
}
