using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.Admin;
using VOD.Common.Entities;

namespace VOD.Admin.Service.Services.Videos
{
    public interface IVideoService
    {
        public Task<List<VideoDTO>> GetVideosAsync();
        public Task<VideoDTO> GetVideoAsync(int videoId);
        public Task<int> DeleteVideoAsync(int videoId);
        public Task<int> UpdateVideosInfoAsync(VideoDTO dto);
        public Task<int> AddVideosInfoAsync(VideoDTO video);
    }
}
