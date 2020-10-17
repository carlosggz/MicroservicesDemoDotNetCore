using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGateway.Models
{
    public class AppSettingsModel
    {
        public const string Key = "ServiceConfig";
        public string MoviesServiceName { get; set; }
        public string ServiceDiscoveryAddress { get; set; }
    }
}
