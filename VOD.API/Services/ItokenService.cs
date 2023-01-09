using System.Threading.Tasks;
using VOD.Common.DTOModels;
using VOD.Common.DTOModels.Admin;

namespace VOD.API.Services
{
    public interface ITokenService
    {
        public Task<TokenDTO> GenerateTokenAsync(LoginUserDTO loginUserDTO);
        public Task<TokenDTO> GetTokenAsync(LoginUserDTO loginUserDTO, string userid);
    }

}
