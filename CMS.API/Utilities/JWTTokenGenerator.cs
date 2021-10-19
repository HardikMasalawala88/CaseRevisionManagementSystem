using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CMS.API.Utilities
{
    public class JWTTokenGenerator
    {
        private readonly IConfiguration _configurationSettings;

        public JWTTokenGenerator(IConfiguration configuration)
        {
            _configurationSettings = configuration;
        }

        public string GenerateLoginToken(string userName, IList<string> role)
        {
            List<Claim> authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userName)
            };

            foreach (var userRole in role)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationSettings["JWT:JwtKey"]));
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configurationSettings["JWT:JwtExpireDays"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configurationSettings["JWT:JwtIssuer"],
                _configurationSettings["JWT:JwtIssuer"],
                authClaims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
