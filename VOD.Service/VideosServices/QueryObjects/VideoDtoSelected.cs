using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;
using VOD.Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace VOD.Service.VideosServices.QueryObjects
{
    public static class VideoDtoSelected
    {
        public static VideoDTO CreateVideoCard(Video video)
        {
            return new VideoDTO
            {
                Id = video.Id,
                Title = video.Title,
                Description = video.Description,
                Thumbnail = video.Thumbnail,
                Url = video.Url,
                Duration = video.Duration
            };

        }
    }
}
