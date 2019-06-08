using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Authentication.Controllers
{
    [Authorize("Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public string GetUserById(int userId)
        {
            return "This id belongs to some user";
        }

        [HttpGet]
        public string GetUserByUsername(string username)
        {
            return username;
        }

        [HttpPost]
        public string AddNewUser(string username)
        {
            return $"User {username} added.";
        }

        [HttpDelete]
        public string DeleteUserById(int userId)
        {
            return "User deleted.";
        }
    }
}