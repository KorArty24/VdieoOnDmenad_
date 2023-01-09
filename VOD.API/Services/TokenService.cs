using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using VOD.Common.DTOModels;
using VOD.Common.DTOModels.Admin;
using VOD.Common.Entities;
using VOD.Service.UserService.Interfaces;

namespace VOD.API.Services
{
    public class TokenService : ItokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _users;

        #region Constructors
        public TokenService(IConfiguration configuration, IUserService users)
        {
            _configuration = configuration;
            _users = users;
        }
        #endregion

        public Task<TokenDTO> GenerageTokenAsync(LoginUserDTO loginUserDTO)
        {
            throw new System.NotImplementedException();
        }

        public Task<TokenDTO> GetTokenAsync(LoginUserDTO loginUserDTO, string userid)
        {
            throw new System.NotImplementedException();
        }

        private IList<Claim> GetClaims(VODUser user, bool includeUserClaims) 
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            if (includeUserClaims) foreach (var claim in user.Claims)
                    if (!claim.Type.Equals("Token") && !claim.Type.Equals("TokenExpires")) claims.Add(claim);
            return claims;
        }

        private TokenDTO CreateToken(IList<Claim> claims)
        {
            try
            {
                var signingKey = Convert.FromBase64String(
                _configuration["Jwt:SigningSecret"]);
                var credentials = new SigningCredentials(
                new SymmetricSecurityKey(signingKey),
                SecurityAlgorithms.HmacSha256Signature);
                var duration = int.Parse(_configuration["Jwt:Duration"]);
                var now = DateTime.UtcNow;
                var jwtToken = new JwtSecurityToken(
                    issuer: "http://csharpschool.com",
                    audience: "http://csharpschool.com",
                    notBefore: now,
                    expires: now.AddDays(duration),
                    claims: claims,
                    signingCredentials: credentials
                );
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var token = jwtTokenHandler.WriteToken(jwtToken);
                return new TokenDTO(token, jwtToken.ValidTo);
            }
            catch
            {
                throw;
            }
        }
    }
}
