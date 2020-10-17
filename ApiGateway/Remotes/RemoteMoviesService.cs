using ApiGateway.Models;
using Common.Infrastructure;
using Consul;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiGateway.Remotes
{
    public class RemoteMoviesService : IRemoteMoviesService
    {
        private readonly ILogger<RemoteMoviesService> _logger;
        private readonly IHttpClientFactory _httpClient;
        private readonly AppSettingsModel _settings;

        public RemoteMoviesService(ILogger<RemoteMoviesService> logger, IHttpClientFactory httpClient, IOptions<AppSettingsModel> settings)
        {
            _logger = logger;
            _httpClient = httpClient;
            _settings = settings.Value;
        }
        public async Task<IEnumerable<MovieModel>> GetMovies(IEnumerable<string> ids)
        {
            _logger.LogInformation("Calling remote service...");

            try
            {
                var json = JsonSerializer.Serialize(ids);
                var toSend = new StringContent(json, Encoding.UTF8, "application/json");

                var serviceUrl = await ConfigHelpers.GetServiceInfo(_settings.ServiceDiscoveryAddress, _settings.MoviesServiceName);

                var client = _httpClient.CreateClient("MoviesService");
                client.BaseAddress = serviceUrl;
                var response = await client.PostAsync("/api/v1/movies/search", toSend);

                if (!response.IsSuccessStatusCode)
                    return new List<MovieModel>();

                var content = await response.Content.ReadAsStringAsync();
                var movies = JsonSerializer.Deserialize<IList<MovieModel>>(content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return movies;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return null;
            }
        }
    }
}
