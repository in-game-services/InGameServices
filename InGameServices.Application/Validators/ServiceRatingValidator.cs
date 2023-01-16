using InGameServices.Application.Validators.Abstractions;
using InGameServices.Data.Entities;
using InGameServices.Data.Repositories.Abstractions;

namespace InGameServices.Application.Validators;

public class ServiceRatingValidator : IServiceRatingValidator
{
  private readonly IUserRepository _userRepository;
  private readonly IServiceRepository _serviceRepository;
  private readonly IServiceRatingRepository _serviceRatingRepository;

  public ServiceRatingValidator(IUserRepository userRepository, IServiceRepository serviceRepository, IServiceRatingRepository serviceRatingRepository)
  {
    _userRepository = userRepository;
    _serviceRepository = serviceRepository;
    _serviceRatingRepository = serviceRatingRepository;
  }

  public async Task ValidateIfUserExists(Guid userId)
  {
    var user = await _userRepository.GetById(userId);
    if (user is null)
    {
      throw new ArgumentException("User not found");
    }
  }


  public async Task ValidateIfServiceExists(Guid serviceId)
  {
    var service = await _serviceRepository.GetById(serviceId);
    if (service is null)
    {
      throw new ArgumentException("Service not found");
    }
  }

  public void ValidateRatingRange(int rating)
  {
    if (rating < 1 || rating > 5)
    {
      throw new ArgumentException("Invalid rating, value has to be between 1 and 5");
    }
  }

  public async Task ValidateIfServiceRatingExists(Guid serviceId, Guid userId)
  {
    var rating = await _serviceRatingRepository.GetServiceRatingByUserIdAndServiceId(userId, serviceId);
    if (rating is not null)
    {
      throw new ArgumentException("Service rating already exists");
    }
  }

  public void ValidateIfServiceRatingIsNull(ServiceRating serviceRating)
  {
    if (serviceRating is null)
    {
      throw new ArgumentException("Service rating not fonud");
    }
  }
}