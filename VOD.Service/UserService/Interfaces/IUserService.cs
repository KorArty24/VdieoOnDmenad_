using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.Admin;
using VOD.Common.Entities;

namespace VOD.Service.UserService.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetUserAsync(string userId);
        Task<IEnumerable<UserDTO>> GetUsersAsync();
        Task<UserDTO> GetUserByEmailAsync(string email);
        Task<IdentityResult> AddUserAsync(RegisterUserDTO user);
        Task<bool> UpdateUserAsync(UserDTO user);
        Task<bool> DeleteUserAsync(string userId);
        Task<VODUser> GetUserAsync(LoginUserDTO loginUser, bool includeClaims = false);
    }
}
