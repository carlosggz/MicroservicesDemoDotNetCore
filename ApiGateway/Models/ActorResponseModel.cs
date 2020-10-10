using System.Collections.Generic;

namespace ApiGateway.Models
{
    public class ActorResponseModel : ActorBaseModel
    {
        public ICollection<MovieModel> Movies { get; set; }
    }
}
