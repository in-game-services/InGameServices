using InGameServices.Data.Entities;

namespace InGameServices.Data.Repositories.Abstractions
{
    public interface IUserRepository
    {
        Task<User> GetById(Guid id);
        Task<User> GetByEmail(string email);
        Task Create(User user);
        Task Update(User user);
    }
}
