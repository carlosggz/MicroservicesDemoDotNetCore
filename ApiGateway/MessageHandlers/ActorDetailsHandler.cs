using ApiGateway.Models;
using ApiGateway.Remotes;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateway.MessageHandlers
{
    public class ActorDetailsHandler: DelegatingHandler
    {
        private readonly ILogger<ActorDetailsHandler> _logger;
        private readonly IRemoteMoviesService _moviesService;

        public ActorDetailsHandler(ILogger<ActorDetailsHandler> logger, IRemoteMoviesService moviesService)
        {
            _logger = logger;
            _moviesService = moviesService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting ActorDetailsHandler...");

            var response = await base.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return response;

            var responseContent = await response.Content.ReadAsStringAsync();
            var actorDetails = JsonSerializer.Deserialize<ActorModel>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            var movies = await _moviesService.GetMovies(actorDetails.Movies);

            var actorResponse = new ActorResponseModel()
            {
                Id = actorDetails.Id,
                FirstName = actorDetails.FirstName,
                LastName = actorDetails.LastName,
                Likes = actorDetails.Likes,
                Movies = movies.ToList()
            };

            response.Content = new StringContent(JsonSerializer.Serialize(actorResponse), System.Text.Encoding.UTF8, "application/json");

            _logger.LogInformation("Handler completed");

            return response;
        }
    }
}
