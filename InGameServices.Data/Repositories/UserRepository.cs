using InGameServices.Data.Entities;
using InGameServices.Data.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace InGameServices.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly InGameServicesDbContext _context;
        public UserRepository(InGameServicesDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            return user;
        }

        public async Task Create(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            return user;
        }
    }
}
