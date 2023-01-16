using InGameServices.Data.Repositories.Abstractions;
using InGameServices.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace InGameServices.Data.Extensions;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddRepositories(this IServiceCollection services)
  {
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IServiceRepository, ServiceRepository>();
    services.AddScoped<IServiceAccessRepository, ServiceAccessRepository>();
    services.AddScoped<IServiceRatingRepository, ServiceRatingRepository>();

    return services;
  }
}
