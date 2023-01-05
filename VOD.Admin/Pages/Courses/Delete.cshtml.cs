using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VOD.Admin.Filters;
using VOD.Admin.Service.Services.Instructors;
using VOD.Common.DTOModels;
using VOD.Common.DTOModels.Admin;
using VOD.Service.UserService.Interfaces;

namespace VOD.Admin.Pages.Courses
{
    [ValidateModel, Authorize(Policy = "AdminOnly")]
    public class DeleteModel : PageModel
    {
        #region Properties
        private readonly IInstructorService _instructorService;
        [BindProperty] public InstructorDTO Input { get; set; } = new InstructorDTO();
        [TempData] public string Alert { get; set; }
        #endregion

        #region Constructor
        public DeleteModel(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }
        #endregion

        #region Actions
        public async Task OnGetAsync(int id)
        {
            Alert = string.Empty;
            Input = await _instructorService.GetInstructorAsync(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _instructorService.DeleteInstructorAsync(Input.Id);
                if (result == 1)
                {
                    Alert = $"Instructor {Input.Name} was deleted.";
                    return RedirectToPage("Index");
                }
            }

            return Page();
        }
        #endregion
    }
}
