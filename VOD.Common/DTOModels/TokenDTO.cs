using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOD.Common.DTOModels
{
    public class TokenDTO
    {
        public string Token { get; set; } = "";
        public DateTime TokenExpires { get; set; } = default;
        public bool TokenHasExpired
        {
            get 
            {
                return TokenExpires == default ? true : !(TokenExpires.Subtract(DateTime.UtcNow).Minutes > 0); 
            }
        }
        public TokenDTO(string token, DateTime tokenExpires)
        {
            Token = token;
            TokenExpires = tokenExpires;
        }
        public TokenDTO()
        { }
    }
}
