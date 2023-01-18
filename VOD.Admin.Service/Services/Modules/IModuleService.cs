using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.Admin;
using VOD.Common.Entities;

namespace VOD.Admin.Service.Services.Modules
{
    public interface IModuleService
    {
        public Task<List<ModuleDTO>> GetModulesAsync();
        public Task<ModuleDTO> GetModuleAsync(int moduleId);
        public Task<int> DeleteModuleAsync(int moduleId);
        public Task<int> UpdateModulesInfoAsync(ModuleDTO dto);
        public Task<int> AddModulesInfoAsync(ModuleDTO module);
    }
}
