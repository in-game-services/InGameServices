using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace InGameServices.Data.Entities
{
    [Table("ServiceRating")]
    [PrimaryKey("UserId", "ServiceId")]
    public class ServiceRating
    {
        public ServiceRating(Guid userId, Guid serviceId, int rating, string comment)
        {
            UserId = userId;
            ServiceId = serviceId;
            Rating = rating;
            Comment = comment;
        }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public Guid ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public Service Service { get; set; }
        [Column("Rating")]
        public int Rating { get; set; }
        [Column("Comment")]
        public string Comment { get; set; }
    }
}
