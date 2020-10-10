using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviesApi.Application.Commands
{
    public class AddLikeCommand : IRequest<bool>
    {
        public string Id { get; private set; }

        public AddLikeCommand(string id)
            => Id = id;
    }
}
