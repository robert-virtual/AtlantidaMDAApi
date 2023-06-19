using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Web;
using System.Security.Claims;

namespace AtlantidaMDAApi.Helpers
{
    public class JWT
    {
        private static string jwtSecret = ConfigurationManager.AppSettings.Get("JWT_SECRET");
        public static string generateToken(string username)
        {
            string jwtAudience = ConfigurationManager.AppSettings.Get("JWT_AUDIENCE");
            string jwtIssuer = ConfigurationManager.AppSettings.Get("JWT_ISSUER");
            string jwtExpireMinutes = ConfigurationManager.AppSettings.Get("JWT_EXPIRE_MINUTES");
            byte[] symmetricKey = Convert.FromBase64String(jwtSecret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, username)
                    }
                    ),
                Expires = now.AddMinutes(Convert.ToDouble(jwtExpireMinutes)),
                Audience = jwtAudience,
                Issuer = jwtIssuer,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                        symmetricKey
                    ),
                SecurityAlgorithms.HmacSha256Signature
                )
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

    }
}