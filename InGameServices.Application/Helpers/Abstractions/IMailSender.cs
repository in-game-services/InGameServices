using InGameServices.Data.Entities;

namespace InGameServices.Application.Helpers.Abstractions;

public interface IMailSender
{
  void SendMail(User user, string newUserPassword);
}