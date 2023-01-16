namespace InGameServices.Models.ServiceRating.Messages.Request;

public class DeleteServiceRatingRequest
{
  public Guid UserId { get; set; }
  public Guid ServiceId { get; set; }
}