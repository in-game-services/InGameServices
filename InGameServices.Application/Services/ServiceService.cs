using InGameServices.Application.Services.Abstractions;
using InGameServices.Application.Validators.Abstractions;
using InGameServices.Data.Entities;
using InGameServices.Data.Repositories.Abstractions;
using InGameServices.Models;
using InGameServices.Models.Service.Messages.Request;
using InGameServices.Models.Service.Messages.Response;

namespace InGameServices.Application.Services;

public class ServiceService : IServiceService
{
  private readonly IServiceAccessRepository _serviceAccessRepository;
  private readonly IServiceRepository _serviceRepository;
  private readonly IServiceValidator _serviceValidator;

  public ServiceService(IServiceRepository serviceRepository, IServiceValidator serviceValidator, IServiceAccessRepository serviceAccessRepository)
  {
    _serviceRepository = serviceRepository;
    _serviceValidator = serviceValidator;
    _serviceAccessRepository = serviceAccessRepository;
  }

  public async Task<CreateServiceResponse> Create(CreateServiceRequest request)
  {
    var entity = new Service(request.UserId, request.Title, request.Description, request.MainPictureUrl, request.Price);
    await _serviceValidator.ValidateService(entity);
    await _serviceRepository.Create(entity);

    return new CreateServiceResponse
    {
      Service = (ServiceDto)entity
    };
  }

  public async Task<DeleteServiceResponse> Delete(Guid id)
  {
    _serviceValidator.ValidateRequestId(id);
    var service = await _serviceRepository.GetById(id);
    _serviceValidator.ValidateServiceNotNull(service);
    await _serviceRepository.Delete(service);

    return new DeleteServiceResponse();
  }

  public async Task<GetServicesResponse> GetAll()
  {
    var services = await _serviceRepository.GetAll();

    return new GetServicesResponse
    {
      Services = services.Select(s => (ServiceDto)s).ToList(),
    };
  }

  public async Task<GetByIdServiceResponse> GetById(Guid id, Guid userId)
  {
    _serviceValidator.ValidateRequestId(id);
    var service = await _serviceRepository.GetById(id);
    _serviceValidator.ValidateServiceNotNull(service);

    if (userId != service.UserId)
    {
      await _serviceAccessRepository.Create(new ServiceAccess { UserId = userId, ServiceId = id });
    }

    int accessCount = await _serviceAccessRepository.GetCount(id);

    return new GetByIdServiceResponse
    {
      Service = (ServiceDto)service,
      AccessCount = accessCount
    };
  }

  public async Task<UpdateServiceResponse> Update(UpdateServiceRequest request, Guid id)
  {
    _serviceValidator.ValidateRequestId(id);
    var entity = await _serviceRepository.GetById(id);
    _serviceValidator.ValidateServiceNotNull(entity);
    entity.Update(request.UserId, request.Title, request.Description, request.MainPictureUrl, request.Price);
    await _serviceValidator.ValidateService(entity);
    await _serviceRepository.Update(entity);

    return new UpdateServiceResponse
    {
      Service = (ServiceDto)entity
    };
  }
}