namespace InGameServices.Application.Services.Abstractions;

public interface IPasswordRecoveryService
{
  Task RecoverPassword(string email);
}
