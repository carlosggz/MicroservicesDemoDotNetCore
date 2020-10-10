using ActorsApi.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ActorsApi.Application
{
    public class GetActorDetailsQuery: IRequest<ActorEntity>
    {
        public string Id { get; private set; }

        public GetActorDetailsQuery(string id)
            => Id = id;
    }
}
