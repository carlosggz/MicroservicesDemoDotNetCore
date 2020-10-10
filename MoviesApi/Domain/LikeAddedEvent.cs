using Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApi.Domain
{
    public class LikeAddedEvent : DomainEvent
    {
        public LikeAddedEvent(MovieEntity entity)
            : base(entity.Id)
        {
            Title = entity.Title;
        }

        public string Title { get; set; }
    }
}
