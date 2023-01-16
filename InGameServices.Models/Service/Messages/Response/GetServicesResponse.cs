using InGameServices.Infrastructure.Messages.Common;

namespace InGameServices.Models.Service.Messages.Response;

public class GetServicesResponse : BaseResponse
{
  public List<ServiceDto> Services { get; set; }
}