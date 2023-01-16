using InGameServices.Data.Entities;

namespace InGameServices.Data.Repositories.Abstractions;

public interface IServiceRepository
{
  Task<List<Service>> GetAll();
  Task<Service> GetById(Guid id);
  Task Create(Service service);
  Task Delete(Service service);
  Task Update(Service service);
}