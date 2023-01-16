using InGameServices.Models.ServiceRating.Messages.Request;

namespace InGameServices.Application.Services.Abstractions;

public interface IServiceRatingService
{
  Task<CreateServiceRatingResponse> Create(CreateServiceRatingRequest request);
  Task<UpdateServiceRatingResponse> Update(UpdateServiceRatingRequest request);
  Task<DeleteServiceRatingResponse> Delete(DeleteServiceRatingRequest request);
}