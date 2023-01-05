using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using VOD.Admin.Filters;
using VOD.Admin.Service.Services.Courses;
using VOD.Admin.Service.Services.Instructors;
using VOD.Common.DTOModels.Admin;
using VOD.Common.Extensions;
using VOD.Service.UserService;
using VOD.Service.UserService.Interfaces;

namespace VOD.Admin.Pages.Courses
{
    [ValidateModel, Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly ICoursesService _courseService;
        private readonly IInstructorService _instructorService;
        [BindProperty] public CourseDTO Input { get; set; } =  new CourseDTO();
        [TempData] public string Alert { get; set; }
        /// <summary>
        /// Create an instance of a  <see cref = CreateModel/>
        /// </summary>
        /// <param name="userService"></param>
        public EditModel(ICoursesService courseService, IInstructorService instructorService)
        {
            _instructorService = instructorService;
            _courseService = courseService;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            try 
            {
                Alert = string.Empty;
                ViewData["Instructors"] = (await _instructorService.GetInstructorsAsync()).ToSelectList("Id", "Name");
                Input = await _courseService.GetCourseAsync(id);
                return Page();
            } catch 
            {
                return RedirectToPage("/Index", new { alert = "You do not have access to this page" });
            }
            
        }

        public async Task<IActionResult> OnPostAsync() 
        {
                var succeeded = await _courseService.UpdateCourseInfoAsync(Input);
                if (succeeded == 1)
                {
                    Alert = $"Updated Course {Input.Title}.";
                    return RedirectToPage("Index");
                }
            // redisplay the form if something failed.
            return Page();
        }
    }
}
