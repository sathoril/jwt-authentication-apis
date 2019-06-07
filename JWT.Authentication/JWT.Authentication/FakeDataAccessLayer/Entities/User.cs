using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Authentication.FakeDataAccessLayer.Entities
{
    public class User
    {
        public User()
        {
                
        }

        public User(int id, string username)
        {
            this.Id = id;
            this.Username = username;
            this.AccessKey = "94be650011cf412ca906fc335f615cdc";
        }

        public Int32 Id { get; set; }
        public String Username { get; set; }
        public String AccessKey { get; set; }
    }
}
