using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        public async Task<UserDTO> GetUserByEmailAsync(string email)
        {
            return await _db.Users.Where(ur => ur.Email == email).Select(user => new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                IsAdmin = _db.UserRoles.Any(ur => ur.UserId.Equals(user.Id) && ur.RoleId.Equals("Admin"))
            }).FirstOrDefaultAsync();
        }

        public async Task<IdentityResult> AddUserAsync(RegisterUserDTO user)
        {
            var dbUser = new VODUser
            {
                UserName = user.Email,
                Email = user.Email,
                EmailConfirmed = true,
            };
            var result = await _userManager.CreateAsync(dbUser, user.Password);
            return result;
        }

        public async Task<UserDTO> GetUserAsync(string userId)
        {
            return await _db.Users.Select(user => new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                IsAdmin = _db.UserRoles.Any(ur => ur.UserId.Equals(user.Id) && ur.RoleId.Equals("Admin"))
            }).FirstOrDefaultAsync(u => u.Id.Equals(userId));

        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            return await  _db.Users.OrderBy(u => u.Email).Select(user => new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                IsAdmin = _db.UserRoles.Any(ur => ur.UserId.Equals(user.Id) && ur.RoleId.Equals("Admin"))
            }).ToListAsync();
        }

        public async Task<bool> UpdateUserAsync(UserDTO user)
        {
            //get user 
            var dbuser = await _db.Users.FirstOrDefaultAsync(user => user.Id.Equals(user.Id));
            //corner case
            if (dbuser == null) return false;
            if (string.IsNullOrEmpty(user.Email)) return false;
            dbuser.Email = user.Email;
            var admin = "Admin";
            var isAdmin = await _userManager.IsInRoleAsync(dbuser, admin);
            IdentityRoleClaim<string> adminclaim = new IdentityRoleClaim<string> { ClaimType = ClaimTypes.Role, ClaimValue = "Admin" };
            if (isAdmin && !user.IsAdmin )
                //modified to utilize claims, instead of Roles
                await _userManager.RemoveClaimAsync(dbuser, new Claim(ClaimTypes.Role, "Admin")); 
            else if (isAdmin && user.IsAdmin)
                await _userManager.AddClaimAsync(dbuser, new Claim(ClaimTypes.Role,"Admin"));
            var result = await _db.SaveChangesAsync(); //Check out about that SaveChangesAsync staff
            return result >=0;
        }
        public async Task<bool> DeleteUserAsync (string userId)
        {
            try
            {
                var dbuser = await _db.Users.FirstOrDefaultAsync (user => user.Id.Equals(userId));
                if (dbuser == null) return false;
                var usrClaims = await _userManager.GetClaimsAsync(dbuser);
                var claimsRemoved = _userManager.RemoveClaimsAsync(dbuser, usrClaims);
                var deleted = await _userManager.DeleteAsync(dbuser);
                return deleted.Succeeded;
            } catch
            {
                return false;
            }
        }
    }
}