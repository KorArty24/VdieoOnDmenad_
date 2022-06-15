using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


using Microsoft.AspNetCore.Identity;
namespace VOD.Common.Entities
{
   public class VODUser :IdentityUser
    {
        public string Token { get; set; }

        public DateTime TokenExpires { get; set; }

        [NotMapped]
        public IList<Claim> Claims { get; set; } = new List<Claim>();

    }
}
