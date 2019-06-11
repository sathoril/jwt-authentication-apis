using JWT.Authentication.FakeDataAccessLayer;
using JWT.Authentication.FakeDataAccessLayer.Entities;
using JWT.Authentication.JwtTokenConfiguration;
using JWT.Authentication.JwtTokenConfiguration.AuthenticationConfiguration;
using JWT.Authentication.JwtTokenConfiguration.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(void))]
        /// <summary>
        /// Authenticates the user passed inside request body, and returns token object
        /// </summary>
        public object AuthenticateUser([FromBody]User user, [FromServices]FakeUserRepository repository, [FromServices]SigningConfiguration signingConfigurations, [FromServices]Token token)
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

            var obj = TokenProvider.GetToken(user, token, signingConfigurations);
            return obj;
        }
    }
}
