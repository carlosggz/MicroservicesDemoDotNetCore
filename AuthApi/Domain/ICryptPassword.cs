using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Domain
{
    public interface ICryptPassword
    {
        string GetCrypted(string value);
    }
}
