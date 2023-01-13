using InGameServices.Data.Entities;

namespace InGameServices.Data.Repositories.Abstractions
{
    public interface IServiceAccessRepository
    {
        Task<int> GetCount(Guid serviceId);
        Task Create(ServiceAccess serviceAccess);
    }
}
