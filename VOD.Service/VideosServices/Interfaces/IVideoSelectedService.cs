using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;
using VOD.Common.Entities;

namespace VOD.Service.VideosServices.Interfaces
{
    public interface IVideoSelectedService
    {
        public Task<VideoDTO> SelectVideoAsync(string userId, int videoId);
        public Task<Video> SelectVideoEntityAsync(string userId, int videoId); 
    }
}
