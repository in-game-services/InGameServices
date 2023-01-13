namespace InGameServices.Models
{
    public class ServiceRatingDto
    {
        public Guid ServiceId { get; set; }
        public Guid UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        public static explicit operator ServiceRatingDto(Data.Entities.ServiceRating serviceRating)
        {
            return new ServiceRatingDto()
            {
                ServiceId = serviceRating.ServiceId,
                UserId = serviceRating.UserId,
                Rating = serviceRating.Rating,
                Comment = serviceRating.Comment
            };
        }
    }
}
