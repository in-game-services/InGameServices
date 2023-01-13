using InGameServices.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace InGameServices.Data
{
    public class InGameServicesDbContext : DbContext
    {
        public InGameServicesDbContext() { }
        public InGameServicesDbContext(DbContextOptions<InGameServicesDbContext> option) : base(option) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceAccess> ServiceAccesses { get; set; }
        public DbSet<ServiceRating> ServiceRatings { get; set; }
    }
}
