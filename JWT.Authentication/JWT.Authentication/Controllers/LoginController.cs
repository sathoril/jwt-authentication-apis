using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using JWT.Authentication.FakeDataAccessLayer;
using JWT.Authentication.FakeDataAccessLayer.Entities;
using JWT.Authentication.JWTConfiguration;
using JWT.Authentication.JWTConfiguration.AuthenticationConfiguration;
using JWT.Authentication.JWTConfiguration.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JWT.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public object Post([FromBody]User user, [FromServices]FakeUserRepository repository, [FromServices]SigningConfigurations signingConfigurations, [FromServices]TokenConfigurations tokenConfigurations)
        {
            bool validCredentials = false;

            if(user == null)
                return new { authenticated = false, message = "Failed to authenticate!" };

            User userFromDatabase = repository.Find(user.Id);

            if (userFromDatabase == null)
                return new { authenticated = false, message = "Failed to authenticate!" };

            validCredentials = (userFromDatabase != null && userFromDatabase.AccessKey == user.AccessKey);

            if(!validCredentials)
                return new { authenticated = false, message = "Failed to authenticate!" };

            var obj = TokenProvider.GetToken(user, tokenConfigurations, signingConfigurations);
            return obj;
        }
    }
}
