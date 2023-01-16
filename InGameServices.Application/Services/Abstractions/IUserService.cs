using InGameServices.Models.User.Messages.Request;
using InGameServices.Models.User.Messages.Response;

namespace InGameServices.Application.Services.Abstractions;

public interface IUserService
{
  Task<CreateUserResponse> Create(CreateUserRequest request);
  Task<UpdateUserResponse> Update(UpdateUserRequest request, Guid id);
  Task<GetByIdUserResponse> GetById(Guid id);
}
