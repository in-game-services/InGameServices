namespace InGameServices.Infrastructure.Messages.Common
{
    public class BaseResponse
    {
        public bool Success { get; set; } = true;
#nullable enable
        public object? Errors { get; set; }
    }
}
