using JWT.Authentication.FakeDataAccessLayer.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace JWT.Authentication.JWTConfiguration
{
    public static class TokenProvider
    {
        private static ClaimsIdentity GetIdentity(User user)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Username, "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
                    }
                );

            return identity;
        }

        public static object GetToken(User user, Models.TokenConfigurations tokenConfigurations, AuthenticationConfiguration.SigningConfigurations signingConfigurations)
        {
            ClaimsIdentity identity = GetIdentity(user);

            DateTime creationDate = DateTime.Now;
            DateTime expirationDate = creationDate + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = creationDate,
                Expires = expirationDate
            });
            var token = handler.WriteToken(securityToken);

            return new
            {
                authenticated = true,
                created = creationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "OK"
            };
        }
    }
}
