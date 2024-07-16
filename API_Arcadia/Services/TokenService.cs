using API_Arcadia.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Arcadia.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }            
        
        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
            //Create Claims
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            Claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        //JWT Security Token Params

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            //return Token

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
