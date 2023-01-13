using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InGameServices.Data.Entities
{
    [Table("User")]
    public class User
    {
        public User(string firstName, string lastName, string email, string password, string? pictureUrl = null, string? description = null)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            PictureUrl = pictureUrl;
            Description = description;
        }

        [Key]
        [Column("Id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Column("FirstName")]
        public string FirstName { get; set; }
        [Column("LastName")]
        public string LastName { get; set; }
        [Column("Email")]
        public string Email { get; set; }
        [Column("Password")]
        public string Password { get; set; }
        [Column("PictureUrl")]
        public string? PictureUrl { get; set; }
        [Column("Description")]
        public string? Description { get; set; }

        public void Update(string firstName, string lastName, string email, string password, string? pictureUrl = null, string? description = null)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            PictureUrl = pictureUrl;
            Description = description;
        }

        public User() { }
    }
}
