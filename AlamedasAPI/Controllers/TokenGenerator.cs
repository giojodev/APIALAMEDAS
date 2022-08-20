using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using AlamedasAPI.Settings;
using System.Text;

namespace AlamedasAPI.Controllers
{
    internal static class TokenGenerator
    {
        public static string GenerateTokenJwt(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.SecretKey));
            var signIngCredentials =new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) });

            var tokenHandler = new JwtSecurityTokenHandler();

            var jwtToken = tokenHandler.CreateJwtSecurityToken(
              subject: claimsIdentity,
              notBefore: DateTime.UtcNow,
              expires: DateTime.UtcNow.AddHours(JwtConfig.HourExpirationTime),
              signingCredentials: signIngCredentials

            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject =claimsIdentity,
                Expires = DateTime.UtcNow.AddHours(JwtConfig.HourExpirationTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.SecretKey)), SecurityAlgorithms.HmacSha256)
            };


            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;

        }
    }
}
