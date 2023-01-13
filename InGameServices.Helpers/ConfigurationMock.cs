using InGameServices.Data.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace InGameServices.Helpers
{
    public static class ConfigurationMock
    {
        public static IConfiguration CreateConfiguration()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"Jwt:Key", "AnyValueItsMocking"},
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            return configuration;
        }
    }
}
