using InGameServices.Data.Entities;
using InGameServices.Data.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace InGameServices.Data.Repositories;

public class ServiceAccessRepository : IServiceAccessRepository
{
  private readonly InGameServicesDbContext _context;
  
  public ServiceAccessRepository(InGameServicesDbContext context)
  {
    _context = context;
  }

  public async Task Create(ServiceAccess serviceAccess)
  {
    await _context.AddAsync(serviceAccess);
    await _context.SaveChangesAsync();
  }

  public async Task<int> GetCount(Guid serviceId)
  {
    var count = await _context.ServiceAccesses.CountAsync(x => x.ServiceId == serviceId);
    return count;
  }
}