using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActorsApi.Domain
{
    public class ActorEntity
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Likes { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public ICollection<string> Movies { get; set; }
    }
}
