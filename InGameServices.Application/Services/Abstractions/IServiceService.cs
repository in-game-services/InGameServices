using InGameServices.Models.Service.Messages.Request;
using InGameServices.Models.Service.Messages.Response;

namespace InGameServices.Application.Services.Abstractions;

public interface IServiceService
{
  Task<CreateServiceResponse> Create(CreateServiceRequest request);
  Task<UpdateServiceResponse> Update(UpdateServiceRequest request, Guid id);
  Task<DeleteServiceResponse> Delete(Guid id);
  Task<GetServicesResponse> GetAll();
  Task<GetByIdServiceResponse> GetById(Guid id, Guid userId);
}