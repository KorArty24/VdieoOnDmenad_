using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VOD.Admin.Helpers;
using VOD.Common.DTOModels.Admin;
using VOD.Common.Entities;
using VOD.Database.Contexts;

namespace VOD.Admin.Pages.Users
{
    [Authorize(Policy = "AdminOnly")]
    public class DetailsModel : PageModel
    {
        private readonly VODContext _dbContext;
        public DetailsModel(VODContext dbcontext)
        {
            _dbContext = dbcontext;
        }
        public void OnGet()
        {
        }
        public IEnumerable<Course> Courses { get; set; } = new List<Course>();
        public SelectList AvailibleCourses { get; set; }
        [BindProperty, Display(Name ="Availible Courses")] public int CourseId { get; set; }
        public UserDTO Customer { get; set; }
        private async Task FillViewData(string userId)
        {
            var user = await _dbContext.Users.SingleAsync(u=>u.Id == userId);
            Customer = new UserDTO { Id = user.Id.ToString(), Email = user.Email };
            var usercourses = await _dbContext.UserCourses.Where(uc => uc.UserId == userId).ToListAsync();
            var usercourseIds = usercourses.Select(uc => uc.CourseId).ToList();
            Courses = usercourses.Select(uc => uc.Course).ToList();
            var availableCourses = await _dbContext.UserCourses.Include(uc=>uc.Course).Where(uc=>uc.UserId == userId).ToListAsync();
            AvailibleCourses = availableCourses.ToSelectList("Id", "Title");
        }
        public async Task TaskAsync(string id)
        {
            await FillViewData(id);
        }

        public async Task<IActionResult> OnPostAddAsync(string userId)
        {
            try
            {
               await _dbContext.UserCourses.AddAsync(new UserCourse { CourseId = CourseId, UserId = userId });
               var succeeded = await _dbContext.SaveChangesAsync();
            }
            catch
            {

            }
            await FillViewData(userId);
            return Page();
        }
        public async Task<IActionResult> OnPostRemoveAsync(int courseId, string userId)
        {
            try 
            {
                var userCourse = await _dbContext.UserCourses.Where(uc => uc.UserId == userId && uc.CourseId == courseId).SingleAsync();
                if (userCourse != null)
                {
                    var result = _dbContext.UserCourses.Remove(userCourse);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
            await FillViewData(userId);
            return Page();
        }
       
    }
}
