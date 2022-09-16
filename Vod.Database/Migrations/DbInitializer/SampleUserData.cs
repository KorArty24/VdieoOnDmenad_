using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOD.Common.Entities;

namespace VOD.Database.Migrations.DbInitializer
{
    public class SampleUserData
    {
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
                 #region Seeding Users

        public static IEnumerable<VODUser> GetUsers()
            {
            var listOfUsers = new List<VODUser>
            {
                new VODUser
                {
                    Id = UserConsts.userOneId,
                    Email = "bobo_berens@example.com",
                    EmailConfirmed= true,
                    PasswordHash = HashPassword("pik0puTrikapu"),
                    TwoFactorEnabled=false,
                    AccessFailedCount=0,
                    LockoutEnabled=false,
                    PhoneNumberConfirmed=false
                },
                new VODUser
                {
                    Id = UserConsts.userTwoId,
                    Email = "brad_buckner@example.com",
                    EmailConfirmed= true,
                    PasswordHash = HashPassword("Ross-LemmingLC2001"),
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
                    PasswordHash = HashPassword("Bagauilir-LemmingLC2001"),
                    TwoFactorEnabled=false,
                    AccessFailedCount=0,
                    LockoutEnabled=false,
                    PhoneNumberConfirmed=false
                }
            };
            return listOfUsers;
            }

        private static string HashPassword(string pass) 
        {
            PasswordHasher hasher = new PasswordHasher();
            var hashedpass = hasher.HashPassword(pass);
            return hashedpass;
        }
        #endregion

    }
}
