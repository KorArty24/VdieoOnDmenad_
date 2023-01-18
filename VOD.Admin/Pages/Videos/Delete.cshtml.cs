using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VOD.Admin.Filters;
using VOD.Admin.Service.Services.Videos;
using VOD.Common.DTOModels;
using VOD.Common.DTOModels.Admin;
using VOD.Service.UserService.Interfaces;

namespace VOD.Admin.Pages.Videos
{
    [ValidateModel, Authorize(Policy = "AdminOnly")]
    public class DeleteModel : PageModel
    {
        #region Properties
        private readonly IVideoService _videoService;
        [BindProperty] public VideoDTO Input { get; set; } = new VideoDTO();
        [TempData] public string Alert { get; set; }
        #endregion

        #region Constructor
        public DeleteModel(IVideoService videoService)
        {
            _videoService = videoService;
        }
        #endregion

        #region Actions
        public async Task OnGetAsync(int id)
        {
            Alert = string.Empty;
            Input = await _videoService.GetVideoAsync(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var result = await _videoService.DeleteVideoAsync(Input.Id);
                if (result == 1)
                {
                    Alert = $"Video {Input.Title} was deleted.";
                    return RedirectToPage("Index");
                }
            }

            return Page();
        }
        #endregion
    }
}
