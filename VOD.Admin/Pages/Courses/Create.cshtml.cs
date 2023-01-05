using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Threading.Tasks;
using VOD.Admin.Filters;
using VOD.Common.DTOModels.Admin;
using VOD.Service.UserService.Interfaces;
using VOD.Admin.Service.Services.Instructors;
using VOD.Admin.Service.Services.Courses;
using VOD.Admin.Helpers;

namespace VOD.Admin.Pages.Courses
{
    [ValidateModel, Authorize(Policy = "AdminOnly")]
    public class CreateModel : PageModel
    {
        private readonly ICoursesService _courseService;
        private readonly IInstructorService _instructorService;
        [BindProperty] public CourseDTO Input { get; set; } =  new CourseDTO();
        [TempData] public string Alert { get; set; }
        /// <summary>
        /// Create an instance of a  <see cref = EditModel/>
        /// </summary>
        /// <param name="userService"></param>
        public CreateModel(ICoursesService courseService, IInstructorService instructorService)
        {
           _courseService = courseService;
           _instructorService = instructorService;
        }
        public async Task<IActionResult> OnGetAsync(int courseId)
        {
            try {
                ViewData["Instructors"] = (await _instructorService.GetInstructorsAsync()).ToSelectList("Id", "Name");
                return Page();
                } 
            catch
                {
                   return RedirectToPage("/Index", new { alert = "You do not have access to this page." });
                } 
        }
        
        public async Task<IActionResult> OnPostAsync() 
        {
                var result = await _courseService.AddCourseInfoAsync(Input);
                var succeeded = result > 0;
                if (succeeded)
                {
                    Alert = $"Created a new Course with title {Input.Title}.";
                    return RedirectToPage("Index");
                }
                ViewData["Instructors"] = (await _instructorService.GetInstructorsAsync()).ToSelectList("Id", "Name");
           
            return Page();
        }
    }
}
