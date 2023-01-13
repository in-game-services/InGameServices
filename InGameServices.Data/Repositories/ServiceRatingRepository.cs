using InGameServices.Data.Entities;
using InGameServices.Data.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace InGameServices.Data.Repositories
{
    public class ServiceRatingRepository : IServiceRatingRepository
    {
        private readonly InGameServicesDbContext _context;
        public ServiceRatingRepository(InGameServicesDbContext context)
        {
            _context = context;
        }

        public async Task Create(ServiceRating serviceRating)
        {
            await _context.AddAsync(serviceRating);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ServiceRating serviceRating)
        {
            _context.Update(serviceRating);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(ServiceRating serviceRating)
        {
            _context.Remove(serviceRating);
            await _context.SaveChangesAsync();
        }

        public async Task<ServiceRating> GetServiceRatingByUserIdAndServiceId(Guid userId, Guid serviceId)
        {
            var rating = await _context.ServiceRatings.Where(x => x.UserId == userId && x.ServiceId == serviceId).FirstOrDefaultAsync();
            return rating;
        }

        public async Task<List<ServiceRating>> GetServiceRatings(Guid serviceId)
        {
            var ratings = await _context.ServiceRatings.Where(x => x.ServiceId == serviceId).ToListAsync();
            return ratings;
        }

        public async Task<List<ServiceRating>> GetUserRatings(Guid userId)
        {
            var ratings = await _context.ServiceRatings.Where(x => x.UserId == userId).ToListAsync();
            return ratings;
        }
    }
}
