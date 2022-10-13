using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.Admin;

namespace VOD.Service.UserService.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetUsersAsync();
        public Task<UserDTO> GetUsersAsync(string userId);
    }
}
