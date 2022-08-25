using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;

namespace VOD.Service.VideosServices.QueryObjects
{
    public enum OrderByOptions
    {
        [Display(Name = "sort by...")] SimpleOrder = 0,
        [Display(Name = "Duration ↑")] ByDuration,
        [Display(Name = "Title ↑")] ByTitle
    }
    public static class VideoListDtoSort

    {
        public static IQueryable<VideoDTO> OrderVideoBy 
            (this IQueryable<VideoDTO> videos, OrderByOptions orderByOptions)
        {
            switch(orderByOptions)
            {
                case OrderByOptions.SimpleOrder:
                    return videos.OrderByDescending(x => x.Id);
                case OrderByOptions.ByDuration:
                    return videos.OrderByDescending(x=> x.Duration);
                case OrderByOptions.ByTitle:
                    return videos.OrderByDescending(x=> x.Title);
                default:
                    throw new ArgumentOutOfRangeException
                        (nameof(orderByOptions), orderByOptions, null);
            }
        }
    }
}
