using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VOD.Admin.Pages.Users
{
    [Authorize(Policy = "AdminOnly")]
    public class DetailsModel : PageModel
    {
        public DetailsModel()
        {

        }
        public void OnGet()
        {
        }
    }
}
