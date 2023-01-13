using InGameServices.Data.Entities;
using InGameServices.Data.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace InGameServices.Data.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly InGameServicesDbContext _context;
        public ServiceRepository(InGameServicesDbContext context)
        {
            _context = context;
        }

        public async Task<Service> GetById(Guid id)
        {
            var service = await _context.Services.Where(x => x.Id == id).FirstOrDefaultAsync();
            return service;
        }

        public async Task Create(Service service)
        {
            await _context.AddAsync(service);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Service service)
        {
            _context.Update(service);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Service>> GetAll()
        {
            var services = await _context.Services.ToListAsync();
            return services;
        }

        public async Task Delete(Service service)
        {
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
        }
    }
}
