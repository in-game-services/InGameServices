using InGameServices.Data.Entities;

namespace InGameServices.Data.Repositories.Abstractions;

public interface IServiceRatingRepository
{
  Task Create(ServiceRating serviceRating);
  Task Update(ServiceRating serviceRating);
  Task Delete(ServiceRating serviceRating);
  Task<ServiceRating> GetServiceRatingByUserIdAndServiceId(Guid userId, Guid serviceId);
  Task<List<ServiceRating>> GetServiceRatings(Guid serviceId);
  Task<List<ServiceRating>> GetUserRatings(Guid userId);
}