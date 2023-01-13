using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InGameServices.Data.Entities
{
    [Table("Service")]
    public class Service
    {
        public Service(Guid userId, string title, string description, string mainPictureUrl, decimal price)
        {
            UserId = userId;
            Title = title;
            Description = description;
            MainPictureUrl = mainPictureUrl;
            Price = price;
        }
        [Key]
        [Column("Id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [Column("Title")]
        public string Title { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("MainPictureUrl")]
        public string MainPictureUrl { get; set; }
        [Column("Price")]
        public decimal Price { get; set; }

        public Service() { }

        public void Update(Guid userId, string title, string description, string mainPictureUrl, decimal price)
        {
            UserId = userId;
            Title = title;
            Description = description;
            MainPictureUrl = mainPictureUrl;
            Price = price;
        }
    }
}
