using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.Admin;
using VOD.Common.Entities;
using VOD.Database.Contexts;

namespace VOD.Admin.Service.Services.Courses
{
    public class CoursesService : ICoursesService
    {
        private readonly VODContext _context;
        private const int ROWS_SUCCESSFULLY_UPDATED = 1;

        [TempData] public string Alert { get; set; }
        public CoursesService(VODContext context)
        {
            _context = context;
        }

        public async Task<int> DeleteCourseAsync(int courseId)
        {
                if (!_context.Courses.Any(c=>c.Id == courseId))
                {
                    Course course = await _context.Courses.Include(c=>c.Modules).ThenInclude(m=>m.Downloads).
                    Include(m=>m.Modules).SingleAsync(c=>c.Id == courseId);
                    var userCourses = _context.UserCourses.Where(uc => uc.CourseId == courseId);
                    _context.RemoveRange(userCourses); // In real app usercourses should be placed into archive courses section. 
                    _context.Remove(course);
                    return await _context.SaveChangesAsync();
                }
                else
                {
                    Alert = $"No such course";
                    return 0;
                }  
        }

        public async Task<CourseDTO> GetCourseAsync(int courseId)
        {
                var course = await _context.Courses.Select(
                cour => new CourseDTO {
                Id = cour.Id,
                Title= cour.Title,
                Description = cour.Description,
                Instructor= cour.Instructor.Name,
                ImageUrl= cour.ImageUrl,
                }).SingleAsync(cour => cour.Id == courseId); 
            return course;
        }

        public async Task<List<CourseDTO>> GetCoursesAsync()
        {
            var courses = await _context.Courses.Select(t=> new CourseDTO
            {Id=t.Id,
            Description= t.Description,
            Title = t.Title,
            Instructor = t.Instructor.Name,
            }).ToListAsync();

            return courses;
        }
        /// <summary>
        /// Update Instructor's info
        /// </summary>
        /// <param name="instructorId"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<int> UpdateCourseInfoAsync(CourseDTO dto)
        {
           var course = await _context.Courses.SingleOrDefaultAsync(
                x=>x.Id== dto.Id);
            if (course == null)
            {
                throw new ArgumentException("Course not found");
            }
            UpdateCourseFields(ref course, ref dto);
            await _context.SaveChangesAsync();

            return ROWS_SUCCESSFULLY_UPDATED;
           
            void UpdateCourseFields(ref Course course, ref CourseDTO dto)
            {
                course.Description = dto.Description;
                course.Title = dto.Title;
                course.ImageUrl = dto.ImageUrl;
                course.Instructor.Name = dto.Instructor;
            }
        }

        public async Task<CourseDTO> GetOriginal(int courseId)
        {
            CourseDTO course = await _context.Courses.Select(item => new CourseDTO
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                ImageUrl = item.ImageUrl,
            }).SingleAsync(k=>k.Id == courseId);

            return course;
        }

        public async Task<int> AddCourseInfoAsync(CourseDTO course)
        {
            if(! _context.Courses.AnyAsync(x=>x.Title == course.Title &&
            x.Description == course.Description).Result)
            {
                try
                {
                    Course courseToAdd = new Course
                    {
                        Title = course.Title,
                        Description = course.Description,
                        Instructor = _context.Instructors.SingleOrDefault(x => x.Id == course.InstructorId),
                        ImageUrl = course.ImageUrl
                    };
                    _context.Add(courseToAdd);
                    await _context.SaveChangesAsync();
                    return courseToAdd.Id;

                } catch
                {
                    throw new DbUpdateException("Error while saving the data");
                }
            } else 
            {
                return 0;
            }
        }
    }
}
