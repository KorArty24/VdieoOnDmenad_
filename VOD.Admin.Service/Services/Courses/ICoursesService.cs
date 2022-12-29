using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.Admin;
using VOD.Common.Entities;

namespace VOD.Admin.Service.Services.Courses
{
    public interface ICoursesService
    {
        public Task<List<CourseDTO>> GetCoursesAsync();
        public Task<CourseDTO> GetCourseAsync(int courseId);
        public Task<int> DeleteCourseAsync(int courseId);
        public Task<Course> UpdateCourseInfoAsync(int courseId, CourseDTO dto);
        public Task<int> AddCourseInfoAsync(CourseDTO course);
    }
}
