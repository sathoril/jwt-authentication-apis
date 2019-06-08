using JWT.Authentication.JwtTokenConfiguration.AuthenticationConfiguration;
using JWT.Authentication.JwtTokenConfiguration.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Authentication.JwtTokenConfiguration.ServiceConfiguration
{
    public static class JwtServiceConfiguration
    {
        public static void ConfigureAuthenticationServices(IServiceCollection services, IConfiguration configuration)
        {
            // Authentication methods
            var signingConfigurations = new SigningConfiguration();
            services.AddSingleton(signingConfigurations);

            var token = new Token();
            new ConfigureFromConfigurationOptions<Token>(configuration.GetSection("TokenConfigurations")).Configure(token);
            services.AddSingleton(token);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(bearerOptions =>
                {
                    var paramsValidation = bearerOptions.TokenValidationParameters;
                    paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                    paramsValidation.ValidAudience = token.Audience;
                    paramsValidation.ValidIssuer = token.Issuer;

                    // Valida a assinatura de um token recebido
                    paramsValidation.ValidateIssuerSigningKey = true;

                    // Verifica se um token recebido ainda é válido
                    paramsValidation.ValidateLifetime = true;

                    // Tempo de tolerância para a expiração de um token (utilizado
                    // caso haja problemas de sincronismo de horário entre diferentes
                    // computadores envolvidos no processo de comunicação)
                    paramsValidation.ClockSkew = TimeSpan.Zero;
                });

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }
    }
}
