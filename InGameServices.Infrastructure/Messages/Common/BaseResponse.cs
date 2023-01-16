namespace InGameServices.Infrastructure.Messages.Common;

#nullable enable
public class BaseResponse
{
  public bool Success { get; set; } = true;
  public object? Errors { get; set; }
}