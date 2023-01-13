using InGameServices.Data.Entities;

namespace InGameServices.Application.Validators.Abstractions
{
    public interface IUserValidator
    {
        void ValidateRequestId(Guid id);
        void ValidateUserNotNull(User user);
        void ValidatePasswordLength(string password);
        void ValidatePasswordMatch(string password, string passwordConfirmation);
        void ValidateEmail(string email);
        Task ValidateUser(User user);
    }
}
