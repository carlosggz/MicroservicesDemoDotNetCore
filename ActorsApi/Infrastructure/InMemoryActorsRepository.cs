using ActorsApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ActorsApi.Infrastructure
{
    public class InMemoryActorsRepository : IActorsRepository
    {
        private static List<ActorEntity> _actors = new List<ActorEntity>() 
        { 
            new ActorEntity() { Id = "A1", FirstName = "F1", LastName = "L1", Likes = 0, Movies = new List<string>{ "M1" }  },
            new ActorEntity() { Id = "A2", FirstName = "F2", LastName = "L2", Likes = 0, Movies = new List<string>{ "M1", "M2" }  },
            new ActorEntity() { Id = "A3", FirstName = "F3", LastName = "L3", Likes = 0, Movies = new List<string>{ "M1", "M3" }  },
            new ActorEntity() { Id = "A4", FirstName = "F4", LastName = "L4", Likes = 0, Movies = new List<string>{ "M2" }  },
            new ActorEntity() { Id = "A5", FirstName = "F5", LastName = "L5", Likes = 0, Movies = new List<string>{ "M3" }  }
        };

        #region IActorsRepository
        public IEnumerable<ActorDto> GetAll()
            => _actors.Select(x => new ActorDto() { Id = x.Id, FullName = x.FullName });

        public ActorEntity GetById(string id)
            => _actors.FirstOrDefault(x => x.Id == id);

        public IEnumerable<ActorEntity> Search(string movieId)
            => _actors.Where(x => x.Movies.Contains(movieId));

        public void Update(ActorEntity actor)
        {
            var a = GetById(actor.Id);

            if (a != null)
                a.Likes = actor.Likes; //Only updates likes for the moment
        } 

        #endregion
    }
}
