using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Authentication.JwtTokenConfiguration.Models
{
    public class Token
    {
        public String Audience { get; set; }
        public String Issuer { get; set; }
        public Int32 Seconds { get; set; }
    }
}
