using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InGameServices.Data.Entities
{
    [Table("ServiceAccess")]
    public class ServiceAccess
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public Guid ServiceId { get; set; }
        
        [ForeignKey("ServiceId")]
        public Service Service { get; set; }
    }
}
