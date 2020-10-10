using ActorsApi.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ActorsApi.Application
{
    public class GetActorsQuery: IRequest<IEnumerable<ActorDto>>
    {
    }
}
