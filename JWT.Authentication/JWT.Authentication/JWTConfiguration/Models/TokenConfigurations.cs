using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Authentication.JWTConfiguration.Models
{
    public class TokenConfigurations
    {
        public String Audience { get; set; }
        public String Issuer { get; set; }
        public Int32 Seconds { get; set; }
    }
}
