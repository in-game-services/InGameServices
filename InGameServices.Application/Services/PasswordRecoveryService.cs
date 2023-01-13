using InGameServices.Application.Helpers;
using InGameServices.Application.Helpers.Abstractions;
using InGameServices.Application.Services.Abstractions;
using InGameServices.Application.Validators.Abstractions;
using InGameServices.Data.Entities;
using InGameServices.Data.Repositories.Abstractions;
using InGameServices.Models;
using InGameServices.Models.Auth.Messages.Request;
using InGameServices.Models.Auth.Messages.Response;

namespace InGameServices.Application.Services
{
    public class PasswordRecoveryService : IPasswordRecoveryService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserValidator _userValidator;
        private readonly IMailSender _mailSender;
        public PasswordRecoveryService(IUserRepository userRepository, IUserValidator userValidator, IMailSender mailSender)
        {
            _userRepository = userRepository;
            _userValidator = userValidator;
            _mailSender = mailSender;
        }

        public async Task RecoverPassword(string email)
        {
            _userValidator.ValidateEmail(email);
            var user = await _userRepository.GetByEmail(email);
            await _userValidator.ValidateUser(user);
            string newPassword = RandomPasswordGenerator.Generate();
            string newEncryptedPassword = SHA512Generator.Generate(newPassword);
            user.Password = newEncryptedPassword;
            await _userRepository.Update(user);
            _mailSender.SendMail(user, newPassword);
        }
    }
}