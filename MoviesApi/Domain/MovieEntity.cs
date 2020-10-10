using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Domain
{
    public class MovieEntity
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Likes { get; set; }
    }
}
