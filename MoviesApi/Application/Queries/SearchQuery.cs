using MediatR;
using MoviesApi.Domain;
using System.Collections.Generic;
using System.Linq;

namespace MoviesApi.Application.Queries
{
    public class SearchQuery: IRequest<IEnumerable<MovieDto>>
    {
        public IEnumerable<string> Ids { get; private set; }

        public SearchQuery(IEnumerable<string> ids)
            => Ids = ids;
    }
}
