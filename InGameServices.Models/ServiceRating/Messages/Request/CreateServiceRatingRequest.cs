namespace InGameServices.Models.ServiceRating.Messages.Request
{
    public class CreateServiceRatingRequest
    {
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
