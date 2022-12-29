using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Threading.Tasks;
using VOD.Admin.Filters;
using VOD.Common.DTOModels.Admin;
using VOD.Service.UserService.Interfaces;
using VOD.Admin.Service.Services.Instructors;

namespace VOD.Admin.Pages.Instructors
{
    [ValidateModel, Authorize(Policy = "AdminOnly")]
    public class CreateModel : PageModel
    {
        private readonly IInstructorService _instructorService;
        [BindProperty] public InstructorDTO Input { get; set; } =  new InstructorDTO();
        [TempData] public string Alert { get; set; }
        /// <summary>
        /// Create an instance of a  <see cref = CreateModel/>
        /// </summary>
        /// <param name="userService"></param>
        public CreateModel(IInstructorService instructorService)
        {
           _instructorService = instructorService;
        }
        public async Task OnGetAsync()
        {
        }
        
        public async Task<IActionResult> OnPostAsync() 
        {
                var result = await _instructorService.AddInstructorsInfoAsync(Input);
                var succeeded = result > 0;
                if (succeeded)
                {
                    Alert = $"Created a new Instructor for {Input.Name}.";
                    return RedirectToPage("Index");
                }
           
            return Page();
        }
    }
}
