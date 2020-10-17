using Consul;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Infrastructure
{
    public static class ConfigHelpers
    {
        public static void RegisterJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("AuthSettings:SecretKey"));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
        public static void RegisterConsulServices(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("ServiceConfig");
            services.Configure<ServiceConfig>(section);
            ServiceConfig serviceConfig = section.Get<ServiceConfig>();

            var consulClient = new ConsulClient(config => config.Address = serviceConfig.ServiceDiscoveryAddress);

            services.AddSingleton(serviceConfig);
            services.AddSingleton<IHostedService, ServiceDiscoveryHostedService>();
            services.AddSingleton<IConsulClient, ConsulClient>(p => consulClient);
        }

        public static async Task<Uri> GetServiceInfo(string discoveryAddress, string serviceName)
        {
            var consulClient = new ConsulClient(config => config.Address = new Uri(discoveryAddress));
            var services = await consulClient.Agent.Services();
            var instance = services.Response.Values.FirstOrDefault(x => x.Service == serviceName);
            return instance == null ? null : new UriBuilder(Uri.UriSchemeHttp, instance.Address, instance.Port).Uri;
        }
    }
}
