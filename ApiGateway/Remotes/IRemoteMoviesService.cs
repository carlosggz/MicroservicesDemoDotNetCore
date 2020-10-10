using ApiGateway.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Remotes
{
    public interface IRemoteMoviesService
    {
        Task<IEnumerable<MovieModel>> GetMovies(IEnumerable<string> ids);
    }
}
