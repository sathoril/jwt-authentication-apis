using Auth0.ManagementApi.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace JWT.Authentication.JWTConfiguration.AuthenticationConfiguration
{
    public class SigningConfigurations
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfigurations()
        {
            using(var provider = new RSACryptoServiceProvider(2048))
            {
                this.Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            this.SigningCredentials = new SigningCredentials(this.Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}
