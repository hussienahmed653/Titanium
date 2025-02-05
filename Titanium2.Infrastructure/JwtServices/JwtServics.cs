using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Titanium2.Application.DTOs;
using Titanium2.Application.Interfaces.JwtInterfaces;

namespace Titanium2.Infrastructure.JwtServices
{
    public class JwtServics : IJwtServices
    {
        public IConfiguration _configuration;

        public JwtServics(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GenerateToken(UserDTO userDTO)
        {
            var Key = Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value);
            var Credential = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userDTO.UserId.ToString()),
                new Claim(ClaimTypes.Email, userDTO.Email),
                new Claim(ClaimTypes.Name, userDTO.Email),
            };
            foreach (var roleid in userDTO.Role)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleid.ToString()));
            }
            
            var token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Credential
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
