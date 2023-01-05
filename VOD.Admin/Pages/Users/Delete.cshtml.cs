using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VOD.Admin.Filters;
using VOD.Common.DTOModels;
using VOD.Common.DTOModels.Admin;
using VOD.Service.UserService.Interfaces;

namespace VOD.Admin.Pages.Users
{
    [ValidateModel, Authorize(Policy = "AdminOnly")]
    public class DeleteModel : PageModel
    {
        #region Properties
        private readonly IUserService _userService;
        [BindProperty] public UserDTO Input { get; set; } = new UserDTO();
        [TempData] public string Alert { get; set; }
        #endregion

        #region Constructor
        public DeleteModel(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Actions
        public async Task OnGetAsync(string id)
        {
            Alert = string.Empty;
            Input = await _userService.GetUserAsync(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.DeleteUserAsync(Input.Id);
                if (result)
                {
                    Alert = $"User {Input.Email} was deleted.";
                    return RedirectToPage("Index");
                }
            }

            return Page();
        }
        #endregion
    }
}
