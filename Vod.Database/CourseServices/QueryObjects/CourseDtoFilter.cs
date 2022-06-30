using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;

namespace VOD.Database.CourseServices.QueryObjects
{
    public class CourseDtoFilter
    {
        public enum CoursesFilterBy
        {
            [Display(Name = "All")] NoFilter = 0,
            [Display(Name = "By Title")] ByTitle,
            [Display(Name = "By Instructor")] ByInstructor,
            [Display(Name ="ByDuration..")] ByDuration
        }

        public static class CoursesDtoFilter
        {
            public static IQueryable<CourseDTO> FilterCoursesBy(this IQueryable<CourseDTO> courses,
                CoursesFilterBy filterBy, string filterValue)
            {
                if (string.IsNullOrEmpty(filterValue))
                    return courses;

                switch (filterBy)
                {
                    case CoursesFilterBy.NoFilter:
                        return courses;
                    case CoursesFilterBy.ByTitle:
                        return courses.Where(c => c.CourseTitle.Contains(filterValue));
                    //case CoursesFilterBy.ByInstructor:
                    //    return courses.Where(c => c.);
                }
            }
                
        }
    }
}
