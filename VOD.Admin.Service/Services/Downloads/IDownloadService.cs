using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.Admin;
using VOD.Common.Entities;

namespace VOD.Admin.Service.Services.Downloads
{
    public interface IDownloadService
    {
        public Task<List<DownloadDTO>> GetDownloadsAsync();
        public Task<DownloadDTO> GetDownloadAsync(int downloadId, int courseId, int moduleId);
        public Task<int> DeleteDownloadAsync(int downloadId);
        public Task<int> UpdateDownloadInfoAsync(DownloadDTO dto);
        public Task<int> AddDownloadInfoAsync(DownloadDTO dto);
    }
}
