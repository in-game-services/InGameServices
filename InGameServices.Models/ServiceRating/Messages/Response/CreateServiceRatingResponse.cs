using InGameServices.Infrastructure.Messages.Common;

namespace InGameServices.Models.ServiceRating.Messages.Request
{
    public class CreateServiceRatingResponse : BaseResponse
    {
        public ServiceRatingDto ServiceRating { get; set; }
    }
}
