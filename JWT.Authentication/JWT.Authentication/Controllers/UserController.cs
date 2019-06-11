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
        [Route("User/GetUserById")]
        public string GetUserById(int userId)
        {
            return "This id belongs to some user";
        }

        [HttpGet]
        [Route("User/GetUserByUsername")]
        public string GetUserByUsername(string username)
        {
            return username;
        }

        [HttpPost]
        [Route("User/AddNewUser")]
        public string AddNewUser(string username)
        {
            return $"User {username} added.";
        }

        [HttpDelete]
        [Route("User/DeleteUserById")]
        public string DeleteUserById(int userId)
        {
            return "User deleted.";
        }
    }
}