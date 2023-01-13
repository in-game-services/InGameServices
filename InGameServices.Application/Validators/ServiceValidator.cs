using InGameServices.Application.Validators.Abstractions;
using InGameServices.Data.Entities;
using InGameServices.Data.Repositories.Abstractions;

namespace InGameServices.Application.Validators
{
    public class ServiceValidator : IServiceValidator
    {

        private readonly IUserRepository _userRepository;
        public ServiceValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void ValidateRequestId(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid id");
            }
        }

        public void ValidateServiceNotNull(Service service)
        {
            if (service is null)
            {
                throw new ArgumentException("Service not found");
            }
        }

        public async Task ValidateService(Service service)
        {
            if (string.IsNullOrWhiteSpace(service.Title))
            {
                throw new ArgumentException("Title cannot be empty");
            }

            if (service.Title.Length > 40)
            {
                throw new ArgumentException("Title cannot be longer than 40 characters");
            }

            if (string.IsNullOrWhiteSpace(service.Description))
            {
                throw new ArgumentException("Description cannot be empty");
            }

            if (service.UserId == Guid.Empty)
            {
                throw new ArgumentException("Invalid user id");
            }

            if(service.Price < 0)
            {
                throw new ArgumentException("Price cannot be negative");
            }

            var userInDatabase = await _userRepository.GetById(service.UserId);

            if (userInDatabase is null)
            {
                throw new ArgumentException("User not found");
            }
        }
    }
}
