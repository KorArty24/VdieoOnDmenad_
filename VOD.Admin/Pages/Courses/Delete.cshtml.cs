using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VOD.Admin.Filters;
using VOD.Admin.Service.Services.Courses;
using VOD.Common.DTOModels;
using VOD.Common.DTOModels.Admin;
using VOD.Service.UserService.Interfaces;

namespace VOD.Admin.Pages.Courses
{
    [ValidateModel, Authorize(Policy = "AdminOnly")]
    public class DeleteModel : PageModel
    {
        #region Properties
        private readonly ICoursesService _courseService;
        [BindProperty] public CourseDTO Input { get; set; } = new CourseDTO();
        [TempData] public string Alert { get; set; }
        #endregion

        #region Constructor
        public DeleteModel(ICoursesService courseService)
        {
            _courseService = courseService;
        }
        #endregion

        #region Actions
        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                Alert = string.Empty;
                Input = await _courseService.GetCourseAsync(id);
                return Page();
            } catch
            {
                return RedirectToPage("/Index", new { alert = "You do not have access to this page" });
            }

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var id = Input.Id;
            var result = await _courseService.DeleteCourseAsync(id);
            if (result == 1)
            {
                Alert = $"Course {Input.Title} was deleted.";
                return RedirectToPage("Index");
            }
            // Something failed, redisplay the form.
            Input = await _courseService.GetCourseAsync(id);
            return Page();
        }
        #endregion
    }
}
