using AuthApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Infrastructure
{
    public class FakeCryptPassword : ICryptPassword
    {
        public string GetCrypted(string value)
            => value;
    }
}
