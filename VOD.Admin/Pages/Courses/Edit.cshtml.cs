using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using VOD.Admin.Filters;
using VOD.Admin.Service.Services.Instructors;
using VOD.Common.DTOModels.Admin;
using VOD.Service.UserService;
using VOD.Service.UserService.Interfaces;

namespace VOD.Admin.Pages.Courses
{
    [ValidateModel, Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly IInstructorService _instructorService;
        [BindProperty] public InstructorDTO Input { get; set; } =  new InstructorDTO();
        [TempData] public string Alert { get; set; }
        /// <summary>
        /// Create an instance of a  <see cref = CreateModel/>
        /// </summary>
        /// <param name="userService"></param>
        public EditModel(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            try 
            {
                Alert = string.Empty;
                Input = await _instructorService.GetInstructorAsync(id);
                return Page();
            } catch 
            {
                return RedirectToPage("/Index", new { alert = "You do not have access to this page" });
            }
            
        }

        public async Task<IActionResult> OnPostAsync() 
        {
                var succeeded = await _instructorService.UpdateInstructorsInfoAsync(Input);
                if (succeeded == 1)
                {
                    Alert = $"Updated Instructor {Input.Name} was updated.";
                    return RedirectToPage("Index");
                }
            // redisplay the form if something failed.
            return Page();
        }
    }
}
