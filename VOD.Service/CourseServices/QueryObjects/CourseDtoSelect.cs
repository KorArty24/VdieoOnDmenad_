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


namespace VOD.Service.CourseServices.QueryObjects
{
    public static class CourseDtoSelect
    {
        public static CourseDTO
            CreateCourseCard(Course course)
        {
            return new CourseDTO
            {
                CourseId = course.Id,
                CourseTitle = course.Title,
                CourseDescription = course.Description,
                MarqueeImageUrl = course.MarqueeImageUrl,
                CourseImageUrl = course.ImageUrl
            };
        }
    }
}
