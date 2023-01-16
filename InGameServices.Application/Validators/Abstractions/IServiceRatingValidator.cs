using InGameServices.Data.Entities;

namespace InGameServices.Application.Validators.Abstractions;

public interface IServiceRatingValidator
{
  Task ValidateIfUserExists(Guid userId);
  Task ValidateIfServiceExists(Guid serviceId);
  void ValidateRatingRange(int rating);
  Task ValidateIfServiceRatingExists(Guid serviceId, Guid userId);
  void ValidateIfServiceRatingIsNull(ServiceRating serviceRating);
}
