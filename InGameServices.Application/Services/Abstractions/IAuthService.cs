using InGameServices.Models.Auth.Messages.Request;
using InGameServices.Models.Auth.Messages.Response;

namespace InGameServices.Application.Services.Abstractions
{
    public interface IAuthService
    {
        Task<AuthResponse> Authenticate(AuthRequest request);
    }
}
