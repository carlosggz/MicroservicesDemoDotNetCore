using MoviesApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MoviesApi.Infrastructure
{
    public class InMemoryMoviesRepository : IMoviesRepository
    {
        private static List<MovieEntity> _movies = new List<MovieEntity>()
        {
            new MovieEntity(){ Id = "M1", Title = "Movie 1", Likes = 0 },
            new MovieEntity(){ Id = "M2", Title = "Movie 3", Likes = 0 },
            new MovieEntity(){ Id = "M3", Title = "Movie 3", Likes = 0 },
        };

        #region IMoviesRepository
        public void Update(MovieEntity movie)
        {
            var m = GetById(movie.Id);

            if (m != null)
                m.Likes = movie.Likes; //Only updates likes for the moment
        }

        public IEnumerable<MovieDto> GetAll()
            => _movies.Select(x => new MovieDto(x));

        public MovieEntity GetById(string id)
            => _movies.FirstOrDefault(x => x.Id == id);

        public IEnumerable<MovieDto> Search(IEnumerable<string> ids)
            => _movies.Where(x => ids.Contains(x.Id)).Select(x => new MovieDto(x));

        #endregion
    }
}
