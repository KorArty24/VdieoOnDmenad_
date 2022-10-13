using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels.Admin;
using VOD.Common.Entities;
using VOD.Database.Contexts;
using VOD.Database.Migrations.DbInitializer;
using VOD.Service.UserService.Interfaces;

namespace VOD.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly VODContext _db;
        private readonly UserManager<VODUser> _userManager;
        public UserService(VODContext db, UserManager<VODUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task<UserDTO> GetUsersAsync(string userId) 
        {
            return await _db.Users.Select(user => new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                IsAdmin = _db.UserRoles.Any(ur => ur.UserId.Equals(user.Id) && ur.RoleId.Equals("Admin"))
            }).FirstOrDefaultAsync(u => u.Id.Equals(userId));
        }

        public async Task <IEnumerable<UserDTO>> GetUsersAsync() 
        {
            return await _db.Users.OrderBy(u => u.Email).Select(user => new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                IsAdmin = _db.UserRoles.Any(ur => ur.UserId.Equals(user.Id) && ur.RoleId.Equals("Admin"))
            }).ToListAsync();
        }
       
        public async Task <UserDTO> GetUserByEmailAsync(string email)
        {
            return await _db.Users.Where(ur => ur.Email == email).Select(user => new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                IsAdmin = _db.UserRoles.Any(ur => ur.UserId.Equals(user.Id) && ur.RoleId.Equals("Admin"))
            }).FirstOrDefaultAsync();
        }
    }
}
