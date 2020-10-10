using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Models
{
    public class ActorModel: ActorBaseModel
    {
        public ICollection<string> Movies { get; set; }
    }
}
