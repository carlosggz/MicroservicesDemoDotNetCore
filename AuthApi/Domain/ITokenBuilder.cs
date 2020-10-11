using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Domain
{
    public interface ITokenBuilder
    {
        string BuildToken(UserEntity user);
    }
}
