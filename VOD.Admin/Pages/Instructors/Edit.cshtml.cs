using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using VOD.Admin.Filters;
using VOD.Common.DTOModels.Admin;
using VOD.Service.UserService.Interfaces;

namespace VOD.Admin.Pages.Users
{
    [ValidateModel, Authorize(Policy = "AdminOnly")]
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;
        [BindProperty] public UserDTO Input { get; set; } =  new UserDTO();
        [TempData] public string Alert { get; set; }
        /// <summary>
        /// Create an instance of a  <see cref = CreateModel/>
        /// </summary>
        /// <param name="userService"></param>
        public EditModel(IUserService userService)
        {
            _userService = userService;
        }
        public async Task OnGetAsync(string id)
        {
            Alert = string.Empty;
            Input = await _userService.GetUserAsync(id);
        }

        public async Task<IActionResult> OnPostAsync() 
        {
                var result = await _userService.UpdateUserAsync(Input);
                if (result)
                {
                    Alert = $"User {Input.Email} was updated.";
                    return RedirectToPage("Index");
                }
            return Page();
        }
    }
}
