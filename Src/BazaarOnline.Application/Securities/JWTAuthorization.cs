// dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BazaarOnline.Application.DTOs.JwtDTOs;
using BazaarOnline.Domain.Entities.Users;
using Microsoft.IdentityModel.Tokens;

namespace BazaarOnline.Application.Securities
{
    public class JwtAuthorization
    {
        public static GeneratedTokenDTO GenerateToken(User user, string issuer, string signKey, string encKey,
            int expireMinutes = 40000)
        {
            if (string.IsNullOrEmpty(signKey))
                throw new ArgumentNullException($"{nameof(signKey)} is null");

            if (string.IsNullOrEmpty(encKey))
                throw new ArgumentNullException($"{nameof(encKey)} is null");


            // Configuring "Claims" to your JWT Token
            var claims = new List<Claim>
            {
                // In RFC 7519 (Section#4), there are defined 7 built-in Claims, but we mostly use 2 of them.
                new Claim(ClaimTypes.Name, user.Id), // User.Identity.Name
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // JWT ID
            };

            var userClaimsIdentity = new ClaimsIdentity(claims);

            // Create a SymmetricSecurityKey for JWT Token signatures
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signKey));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // encryption
            var encryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(encKey));
            var encryptingCredentials = new EncryptingCredentials(encryptionKey, SecurityAlgorithms.Aes128KW,
                SecurityAlgorithms.Aes128CbcHmacSha256);

            // Create SecurityTokenDescriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                //Audience = issuer, // Sometimes you don't have to define Audience.
                Subject = userClaimsIdentity,
                Expires = DateTime.Now.AddMinutes(expireMinutes),
                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptingCredentials
            };


            // Generate a JWT securityToken, than get the serialized Token result (string)
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var serializeToken = tokenHandler.WriteToken(securityToken);

            return new GeneratedTokenDTO
            {
                Token = serializeToken,
                ExpireDate = securityToken.ValidTo
            };
        }
    }
}