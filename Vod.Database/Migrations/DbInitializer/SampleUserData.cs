
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.Entities;

namespace VOD.Database.Migrations.DbInitializer
{
    public class SampleUserData
    {
        #region Seeding Users
        public static class UserConsts
        {
            public const string userOneId = "82ec065c-0d21-47f9-a0a6-5c941950a294";
            public const string userTwoId = "8b7e3679-e289-496b-a685-1308b0ccbbab";
            public const string userThreeId = "9bfc9c84-c029-4922-afa5-5fc842ccc7b6";
        }
        private readonly Microsoft.AspNetCore.Identity.UserManager<VODUser> _userManager;
        public SampleUserData(Microsoft.AspNetCore.Identity.UserManager<VODUser> userManager)
        {
            _userManager = userManager;
        }
          
        public static IEnumerable<VODUser> GetUsers()
            {
            string[] passwords =
            {
                "pik0pu-Trikapu", "Ross-LemmingLC2001", "Bagauilir-LemmingLC2001"
            };

            var listOfUsers = new List<VODUser>
            {
                new VODUser
                {
                    Id = UserConsts.userOneId,
                    Email = "bobo_berens@example.com",
                    UserName="bobo_berens@example.com",
                    NormalizedUserName="bobo_berens@example.com".ToUpper(),
                    EmailConfirmed= true,
                    TwoFactorEnabled=false,
                    AccessFailedCount=0,
                    LockoutEnabled=false,
                    PhoneNumberConfirmed=false,
                    NormalizedEmail = "BOBO_BERENS@EXAMPLE.COM",
                    SecurityStamp=Guid.NewGuid().ToString(),
                    Claims = new List<Claim>
                    {
                        new Claim("Role", "Admins")
                    }
                },
                new VODUser
                {
                    Id = UserConsts.userTwoId,
                    Email = "brad_buckner@example.com",
                    EmailConfirmed= true,
                    TwoFactorEnabled=false,
                    AccessFailedCount=0,
                    LockoutEnabled=false,
                    PhoneNumberConfirmed=false
                },
                 new VODUser
                {
                    Id = UserConsts.userThreeId,
                    Email = "bengamin_lainus@example.com",
                    EmailConfirmed= true,
                    TwoFactorEnabled=false,
                    AccessFailedCount=0,
                    LockoutEnabled=false,
                    PhoneNumberConfirmed=false
                }
            };
            for (int i=0; i<listOfUsers.Count(); i++)  
            {
                listOfUsers[i].PasswordHash = HashPassword(listOfUsers[i], passwords[i]);
            }
            return listOfUsers;
            }

        private static string HashPassword(VODUser user,string pass) 
        {
            PasswordHasher<VODUser> hasher = new PasswordHasher<VODUser>();
            var hashedpass = hasher.HashPassword(user , pass);
            return hashedpass;
        }
        #endregion
        public static IEnumerable<IdentityUserClaim<string>> GetUserClaims()
        {
            var Claims = new List<IdentityUserClaim<string>>
           {
               new IdentityUserClaim<string>
               {
                   UserId = UserConsts.userOneId,
                   ClaimType = ClaimTypes.Role,
                   ClaimValue = "Admin"
               }
           };
           return Claims;
        }
    }
}
