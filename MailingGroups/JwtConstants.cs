using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailingGroups
{
    public static class JwtConstants
    {
        public const string Issuer = Audience;
        public const string Audience = "http://localhost:52057";
        public const string Secret = "my_new_secret_here";
    }
}
