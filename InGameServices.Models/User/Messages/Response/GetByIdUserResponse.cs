using InGameServices.Infrastructure.Messages.Common;

namespace InGameServices.Models.User.Messages.Response;

public class GetByIdUserResponse : BaseResponse
{
  public UserDto User { get; set; }
}