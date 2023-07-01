using API.Utilities.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Contracts
{
    public interface ITokenHandler
    {
        public string GenerateToken(IEnumerable<Claim> claims);
    }
}
