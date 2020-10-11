using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AuthApi.Application
{
    public class GenerateUserTokenCommand: IRequest<string>
    {
        public GenerateUserTokenCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; private set; }
        public string Password { get; private set; }
    }
}
