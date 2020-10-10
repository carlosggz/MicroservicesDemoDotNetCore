using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Domain
{
    public class MovieDto
    {
        public string Id { get; set; }
        public string Title { get; set; }

        public MovieDto()
        {}

        public MovieDto(MovieEntity entity)
        {
            Id = entity.Id;
            Title = entity.Title;
        }
    }
}
