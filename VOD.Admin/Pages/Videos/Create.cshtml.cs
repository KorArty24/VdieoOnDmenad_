using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Threading.Tasks;
using VOD.Admin.Filters;
using VOD.Common.DTOModels.Admin;
using VOD.Service.UserService.Interfaces;
using VOD.Admin.Service.Services.Videos;

namespace VOD.Admin.Pages.Videos
{
    [ValidateModel, Authorize(Policy = "AdminOnly")]
    public class CreateModel : PageModel
    {
        private readonly IVideoService _videoService;
        [BindProperty] public VideoDTO Input { get; set; } =  new VideoDTO();
        [TempData] public string Alert { get; set; }
        /// <summary>
        /// Create an instance of a  <see cref = EditModel/>
        /// </summary>
        /// <param name="userService"></param>
        public CreateModel(IVideoService videoService)
        {
           _videoService = videoService;
        }
        public async Task<IActionResult> OnGetAsync(int videoId)
        {
            try {
                Input = await _videoService.GetVideoAsync(videoId);
                return Page();
                }  catch
            {
                return RedirectToPage("/Index", new { alert = "You do not have access to this page." });
            }         
            
        }
        
        public async Task<IActionResult> OnPostAsync() 
        {
                var result = await _videoService.AddVideosInfoAsync(Input);
                var succeeded = result > 0;
                if (succeeded)
                {
                    Alert = $"Created a new Video for {Input.Name}.";
                    return RedirectToPage("Index");
                }
           
            return Page();
        }
    }
}
