using InGameServices.Infrastructure.Messages.Common;

namespace InGameServices.Models.User.Messages.Response
{
    public class CreateUserResponse : BaseResponse
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
