using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Domain
{
    public interface IMoviesRepository
    {
        IEnumerable<MovieDto> GetAll();
        MovieEntity GetById(string id);
        void Update(MovieEntity movie);
    }
}
