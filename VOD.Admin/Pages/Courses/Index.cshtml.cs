using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using VOD.Admin.Service.Services.Courses;
using VOD.Common.DTOModels.Admin;
using VOD.Service.UserService.Interfaces;
using VOD.Common.Extensions;
namespace VOD.Admin.Pages.Courses
{
    [Authorize("AdminOnly")]
    public class IndexModel : PageModel
    {
        #region Properties
        private readonly ICoursesService _courseService;
        public IEnumerable<CourseDTO> Courses = new List<CourseDTO>();
        [TempData] public string Alert { get; set; }

        #endregion

        #region Constructor 
        /// <summary>
        /// Create instance of a <see cref="IndexModel"/>.
        /// </summary>
        /// <param name ="userservice"> Instance of <inheritdoc cref="IUserService"/>.</param>
        public IndexModel(ICoursesService courseService)
        {
            _courseService = courseService;
        }
        #endregion
        public async Task<IActionResult> OnGetAsync()
        {
           try
            {
                Courses = await _courseService.GetCoursesAsync();
                return Page();
            } catch
            {
                Alert = "You don't have access to this page.";
                return RedirectToPage("/Index");
            }
        }
    }
}




