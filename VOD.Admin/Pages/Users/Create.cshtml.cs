using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Threading.Tasks;
using VOD.Common.DTOModels.Admin;
using VOD.Service.UserService.Interfaces;

namespace VOD.Admin.Pages.Users
{
    [Authorize(Policy = "AdminOnly")]
    public class CreateModel : PageModel
    {
        private readonly IUserService _userService;
        [BindProperty] public RegisterUserDTO Input { get; set; } =  new RegisterUserDTO();
        [TempData] public string Alert { get; set; }
        /// <summary>
        /// Create an instance of a  <see cref = CreateModel/>
        /// </summary>
        /// <param name="userService"></param>
        public CreateModel(IUserService userService)
        {
            _userService = userService;
        }
        public async Task OnGetAsync()
        {
        }
        public async Task<IActionResult> OnPostAsync() 
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.AddUserAsync(Input);
                if (result.Succeeded)
                {
                    Alert = $"Created a new account for {Input.Email}.";
                    return RedirectToPage("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
    }
}
