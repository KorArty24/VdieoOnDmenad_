using Microsoft.AspNetCore.Identity;
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
        Task<UserDTO> GetUserAsync(string userId);
        Task<IEnumerable<UserDTO>> GetUsersAsync();
        Task<UserDTO> GetUserByEmailAsync(string email);
        Task<IdentityResult> AddUserAsync(RegisterUserDTO user);
    }
}
