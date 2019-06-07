using JWT.Authentication.FakeDataAccessLayer.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Authentication.FakeDataAccessLayer
{
    public class FakeUserRepository
    {
        private IConfiguration configuration;

        private User fakeUser { get; set; }

        public FakeUserRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.fakeUser = new User(1, "sathoriL");
        }

        public User Find(int id)
        {
            return id == fakeUser.Id ? this.fakeUser : null;
        }
    }
}
