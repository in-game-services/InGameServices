using InGameServices.Data.Entities;

namespace InGameServices.Application.Validators.Abstractions;

public interface IServiceValidator
{
  void ValidateRequestId(Guid id);
  void ValidateServiceNotNull(Service service);
  Task ValidateService(Service service);
}