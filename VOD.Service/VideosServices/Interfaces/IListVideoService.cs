using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;
using VOD.Service.CommonOptions;

namespace VOD.Service.VideosServices.Interfaces
{
    public interface IListVideoService
    {
        public Task<IQueryable<VideoDTO>> GetVideosForUser (string userId, PageOptions options, int moduleId = 0);
    }
}
