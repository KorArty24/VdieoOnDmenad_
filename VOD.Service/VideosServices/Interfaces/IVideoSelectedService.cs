﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;

namespace VOD.Service.VideosServices.Interfaces
{
    public interface IVideoSelectedService
    {
        public Task<VideoDTO> SelectedVideoAsync(string userId, int videoId);
    }
}
