using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;
using VOD.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace VOD.Service.CourseServices.QueryObjects
{
    public static class CourseListDTOSelect
    {
        public static IQueryable<CourseWithInstructorAndVideosDTO>
            MapCourseToDTO(this IQueryable<Course> courses)
        {
            return courses.Select(course => new CourseWithInstructorAndVideosDTO
            {
                CourseId = course.Id,
                CourseTitle = course.Title,
                CourseDescription = course.Description,
                Instructor = course.Instructor.Name,
                Duration = course.Modules.Where(m=>m.CourseId==course.Id)
                .SelectMany(m => m.Videos).Select(v => v.Duration).Count()
            });
        }
    }
}
