using InGameServices.Models.Auth.Messages.Request;
using InGameServices.Models.Auth.Messages.Response;

namespace InGameServices.Application.Services.Abstractions
{
    public interface IPasswordRecoveryService
    {
        Task RecoverPassword(string email);
    }
}
