using AuthApi.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AuthApi.Infrastructure
{
    public class JwtTokenBuilder : ITokenBuilder
    {
        private readonly byte[] _secret;
        public JwtTokenBuilder(IConfiguration configuration)
        {
            _secret = System.Text.Encoding.UTF8.GetBytes(configuration.GetValue<string>("AuthSettings:SecretKey"));
        }

        public string BuildToken(UserEntity user)
        {
            var signingKey = new SymmetricSecurityKey(_secret);
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email)
            };
           
            var jwt = new JwtSecurityToken(claims: claims, signingCredentials: signingCredentials, expires: DateTime.Now.AddDays(1));
            
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
