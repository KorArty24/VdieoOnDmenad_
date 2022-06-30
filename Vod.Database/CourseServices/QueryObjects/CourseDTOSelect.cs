using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;
using VOD.Common.Entities;

namespace VOD.Database.Services.QueryObjects
{
    public static class CourseDTOSelect
    {
        public static IQueryable<CourseDTO> MapCourseToDto(this IQueryable<Course> courses)
        {
            return courses.Select(course => new CourseDTO
            {
                CourseId = course.Id,
                CourseTitle = course.Title,
                CourseDescription = course.Description,
                MarqueeImageUrl = course.MarqueeImageUrl,
                CourseImageUrl = course.ImageUrl

            }
               
        }
    }
}
