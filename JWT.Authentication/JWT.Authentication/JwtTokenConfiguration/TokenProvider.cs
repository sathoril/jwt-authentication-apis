using JWT.Authentication.FakeDataAccessLayer.Entities;
using JWT.Authentication.JwtTokenConfiguration.AuthenticationConfiguration;
using JWT.Authentication.JwtTokenConfiguration.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace JWT.Authentication.JwtTokenConfiguration
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

        public static object GetToken(User user, Token token, SigningConfiguration signingConfigurations)
        {
            ClaimsIdentity identity = GetIdentity(user);

            DateTime creationDate = DateTime.Now;
            DateTime expirationDate = creationDate + TimeSpan.FromSeconds(token.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = token.Issuer,
                Audience = token.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = creationDate,
                Expires = expirationDate
            });
            var novoToken = handler.WriteToken(securityToken);

            return new
            {
                authenticated = true,
                created = creationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = novoToken,
                message = "OK"
            };
        }
    }
}
