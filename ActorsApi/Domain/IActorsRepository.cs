using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActorsApi.Domain
{
    public interface IActorsRepository
    {
        IEnumerable<ActorDto> GetAll();
        ActorEntity GetById(string id);
        void Update(ActorEntity actor);
    }
}