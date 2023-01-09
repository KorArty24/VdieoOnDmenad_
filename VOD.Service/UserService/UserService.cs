using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.DTOModels;
using VOD.Common.DTOModels.Admin;
using VOD.Common.Entities;
using VOD.Common.Extensions;
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
                IsAdmin = _db.UserClaims.Any(ur => ur.UserId.Equals(user.Id) && ur.ClaimValue.Equals("Admin")),
                Token = new TokenDTO(user.Token, user.TokenExpires)

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
                IsAdmin = _db.UserClaims.Any(ur => ur.UserId.Equals(user.Id) && ur.ClaimValue.Equals("Admin")),
                Token = new TokenDTO(user.Token, user.TokenExpires)

            }).FirstOrDefaultAsync(u => u.Id.Equals(userId));
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            return await  _db.Users.OrderBy(u => u.Email).Select(user => new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                IsAdmin = _db.UserClaims.Any(ur => ur.UserId.Equals(user.Id) && ur.ClaimValue.Equals("Admin")),
                Token = new TokenDTO(user.Token, user.TokenExpires)
            }).ToListAsync();
        }

        public async Task<bool> UpdateUserAsync(UserDTO user)
        {
            //get user 
            var dbUser = await _db.Users.FirstOrDefaultAsync(user => user.Id.Equals(user.Id));
            //corner case
            if (dbUser == null) return false;
            if (string.IsNullOrEmpty(user.Email)) return false;
            dbUser.Email = user.Email;
            if (user.Token != null && user.Token.Token != null && user.Token.Token.Length > 0)
            { 
                dbUser.Token = user.Token.Token;
                dbUser.TokenExpires = user.Token.TokenExpires;
                //Add token claim to the user in the database
                var newTokenClaim = new Claim("Token", user.Token.Token);
                var newTokenExpires = new Claim("TokenExpires", user.Token.TokenExpires.ToString("yyy-MM-dd hh:mm:ss"));
                //add or replace the claims for the token and expiration data
                var userClaims = await _userManager.GetClaimsAsync(dbUser);
                var currentTokenClaim = userClaims.SingleOrDefault(c => c.Type.Equals("Token"));
                var currentTokenClaimExpires = userClaims.SingleOrDefault(c => c.Type.Equals("TokenExpires"));
                #region Create new token here
                if (currentTokenClaim == null)
                {
                    await _userManager.AddClaimAsync(dbUser, newTokenClaim);
                }
                else {
                    await _userManager.ReplaceClaimAsync(dbUser, currentTokenClaim, newTokenClaim);
                }
                if (currentTokenClaimExpires == null)
                    await _userManager.AddClaimAsync(dbUser, newTokenExpires);
                else
                    await _userManager.ReplaceClaimAsync(dbUser, currentTokenClaimExpires, newTokenExpires);
            }
            #endregion
            var admin = "Admin";
            var isAdmin = await _userManager.IsInRoleAsync(dbUser, admin); //suspicious
            var adminClaim = new Claim(admin, "true");
            if(isAdmin && !user.IsAdmin)
            {
                await _userManager.RemoveFromRoleAsync(dbUser, admin);
                await _userManager.RemoveClaimAsync(dbUser, adminClaim);
            } else if (isAdmin && !user.IsAdmin)
            {
                // Add Admin Role
                await _userManager.AddToRoleAsync(dbUser, admin);

                // Add Admin Claim
                await _userManager.AddClaimAsync(dbUser, adminClaim);
            }
            IdentityRoleClaim<string> adminclaim = new IdentityRoleClaim<string> { ClaimType = ClaimTypes.Role, ClaimValue = "Admin" };
            if (isAdmin && !user.IsAdmin )
                //modified to utilize claims, instead of Roles
                await _userManager.RemoveClaimAsync(dbUser, new Claim(ClaimTypes.Role, "Admin")); 
            else if (isAdmin && user.IsAdmin)
                await _userManager.AddClaimAsync(dbUser, new Claim(ClaimTypes.Role,"Admin"));
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
                var deleted = await _userManager.DeleteAsync(dbuser); //claims
                return deleted.Succeeded;
            } catch
            {
                return false;
            }
        }
        
        public async Task<VODUser> GetUserAsync(LoginUserDTO loginUser, bool includeClaims = false)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginUser.Email);
                if (user == null) return null;
                if (loginUser.Password.IsNullOrEmptyOrWhiteSpace() && loginUser.PasswordHash.IsNullOrEmptyOrWhiteSpace())
                return null;

                if (loginUser.Password.Length >0)
                {
                    var password = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, loginUser.Password);
                    if (password == PasswordVerificationResult.Failed) return null;
                } else
                {
                    if (!user.PasswordHash.Equals(loginUser.PasswordHash)) return null;
                }
                if (includeClaims) user.Claims = await _userManager.GetClaimsAsync(user);
                return user;
            }
            catch
            {
                throw;
            }
        }
    }
}