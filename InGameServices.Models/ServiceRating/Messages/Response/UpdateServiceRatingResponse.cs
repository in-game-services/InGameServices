using InGameServices.Infrastructure.Messages.Common;

namespace InGameServices.Models.ServiceRating.Messages.Request;

public class UpdateServiceRatingResponse : BaseResponse
{
  public ServiceRatingDto ServiceRating { get; set; }
}