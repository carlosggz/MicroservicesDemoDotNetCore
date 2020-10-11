using AuthApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Infrastructure
{
    public class InMemoryUsersRepository : IUsersRepository
    {
        private static List<UserEntity> _users = new List<UserEntity>() 
        { 
            new UserEntity(){ UserName = "U1", FullName = "User1", Email = "u1@example.com", Password = "U1", Roles = new string[] {"Role1", "Role2" } },
            new UserEntity(){ UserName = "U2", FullName = "User2", Email = "u2@example.com", Password = "U2", Roles = new string[] {"Role1", "Role3" } }
        };

        public UserEntity GetUser(string userName)
            => _users.SingleOrDefault(x => x.UserName == userName);
    }
}
