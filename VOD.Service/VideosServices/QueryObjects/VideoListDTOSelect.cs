﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;
using VOD.Common.Entities;

namespace VOD.Service.VideosServices.QueryObjects
{
    public static class VideoListDTOSelect
    {
         public static IQueryable<VideoDTO> MapVideoToDTO 
            (this IQueryable<Video> videos)
        {
            return videos.Select(video => new VideoDTO
            {
                Id = video.Id,
                Description = video.Description,
                Duration = video.Duration,
                Thumbnail = video.Thumbnail,
                Url = video.Url
            });
        }
    }
}
