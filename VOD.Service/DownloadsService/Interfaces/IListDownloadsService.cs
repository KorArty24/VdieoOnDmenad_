using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.UI;

namespace VOD.Service.DownloadsService.Interfaces
{
    public interface IListDownloadsService
    {
        public Task<IQueryable<DownloadDTO>> GetDownloadsForCourse(string courseId, );
    }
}
