using Microsoft.Extensions.Configuration;

namespace InGameServices.Helpers;

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