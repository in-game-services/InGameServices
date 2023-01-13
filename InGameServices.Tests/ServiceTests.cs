using InGameServices.Application.Services;
using InGameServices.Application.Validators.Abstractions;
using InGameServices.Application.Validators;
using InGameServices.Data.Repositories.Abstractions;
using InGameServices.Data.Repositories;
using InGameServices.Helpers;
using InGameServices.Models.User.Messages.Request;
using Xunit;
using InGameServices.Models.Service.Messages.Request;
using InGameServices.Models.Service.Messages.Response;
using InGameServices.Application.Helpers.Abstractions;
using InGameServices.Application.Helpers;
using Microsoft.Extensions.Configuration;
using InGameServices.Application.Services.Abstractions;

namespace InGameServices.Tests
{
    public class ServiceTests
    {
        public IServiceService _serviceService;
        public IUserService _userService;
        public ServiceTests()
        {
            var context = InMemoryContext.CreateContext();
            IServiceRepository serviceRepository = new ServiceRepository(context);
            IUserRepository userRepository = new UserRepository(context);
            IServiceValidator serviceValidator = new ServiceValidator(userRepository);
            IUserValidator userValidator = new UserValidator(userRepository);
            IConfiguration configuration = ConfigurationMock.CreateConfiguration();
            ITokenGenerator tokenGenerator = new TokenGenerator(configuration);
            IServiceAccessRepository serviceAccessRepository = new ServiceAccessRepository(context);

            _userService = new UserService(userRepository, userValidator, tokenGenerator);
            _serviceService = new ServiceService(serviceRepository, serviceValidator, serviceAccessRepository);
        }

        #region Create tests
        [Fact]
        public async void Create_ShouldReturn_CreateServiceResponse_WhenValid()
        {
            var user = new CreateUserRequest
            {
                FirstName = "Create_ShouldReturn_CreateServiceResponse_WhenValid",
                LastName = "Create_ShouldReturn_CreateServiceResponse_WhenValid",
                Email = "Create_ShouldReturn_CreateServiceResponse_WhenValid@mail.com",
                Password = "Create_ShouldReturn_CreateServiceResponse_WhenValid",
                PasswordConfirmation = "Create_ShouldReturn_CreateServiceResponse_WhenValid",
            };
            var userResponse = await _userService.Create(user);
            var service = new CreateServiceRequest
            {
                UserId = userResponse.User.Id,
                Title = "Example service",
                Description = "Example Service",
                MainPictureUrl = "",
                Price = 0
            };
            var response = await _serviceService.Create(service);
            Assert.IsType<CreateServiceResponse>(response);
        }

        [Fact]
        public async void Create_ShouldThrow_WhenUserDoesNotExist()
        {
            var service = new CreateServiceRequest
            {
                UserId = Guid.NewGuid(),
                Title = "Example service",
                Description = "Example Service",
                MainPictureUrl = "",
                Price = 0
            };
            var function = async () => await _serviceService.Create(service);
            await Assert.ThrowsAsync<ArgumentException>(function);
        }

        [Fact]
        public async void Create_ShouldThrow_WhenNoUserInformed()
        {
            var service = new CreateServiceRequest
            {
                UserId = Guid.Empty,
                Title = "Example service",
                Description = "Example Service",
                MainPictureUrl = "",
                Price = 0
            };
            var function = async () => await _serviceService.Create(service);
            await Assert.ThrowsAsync<ArgumentException>(function);
        }

        [Theory]
        [InlineData("", "ServiceCreate_ShouldThrow_WhenInvalidDescription", "", 0)]
        [InlineData("ServiceCreate_ShouldThrow_WhenInvalidTitle", "", "", 0)]
        [InlineData("ServiceCreate_ShouldThrow_WhenInvalid", "ServiceCreate_ShouldThrow_WhenInvalid", "", -1)]
        public async void Create_ShouldThrow_WhenInvalid(string title, string description, string mainPictureUrl, decimal price)
        {
            var user = new CreateUserRequest
            {
                FirstName = "Create_ShouldThrow_WhenInvalid",
                LastName = "Create_ShouldThrow_WhenInvalid",
                Email = $"{title}{description}{mainPictureUrl}{price}@mail.com",
                Password = "Create_ShouldThrow_WhenInvalid",
                PasswordConfirmation = "Create_ShouldThrow_WhenInvalid",
            };
            var userResponse = await _userService.Create(user);
            var service = new CreateServiceRequest
            {
                UserId = userResponse.User.Id,
                Title = title,
                Description = description,
                MainPictureUrl = mainPictureUrl,
                Price = price
            };
            var function = async () => await _serviceService.Create(service);
            await Assert.ThrowsAsync<ArgumentException>(function);
        }

        #endregion

        #region Update tests
        [Fact]
        public async void Update_ShouldReturn_UpdateServiceResponse_WhenValid()
        {
            var user = new CreateUserRequest
            {
                FirstName = "Update_ShouldReturn_UpdateServiceResponse_WhenValid",
                LastName = "Update_ShouldReturn_UpdateServiceResponse_WhenValid",
                Email = "Update_ShouldReturn_UpdateServiceResponse_WhenValid@mail.com",
                Password = "Update_ShouldReturn_UpdateServiceResponse_WhenValid",
                PasswordConfirmation = "Update_ShouldReturn_UpdateServiceResponse_WhenValid",
            };
            var userResponse = await _userService.Create(user);
            var serviceCreate = new CreateServiceRequest
            {
                UserId = userResponse.User.Id,
                Title = "Example service",
                Description = "Example Service",
                MainPictureUrl = "",
                Price = 0
            };
            var responseCreate = await _serviceService.Create(serviceCreate);

            var serviceUpdate = new UpdateServiceRequest
            {
                UserId = userResponse.User.Id,
                Title = "Example",
                Description = "Example",
                MainPictureUrl = "",
                Price = 0
            };
            var responseUpdate = await _serviceService.Update(serviceUpdate, responseCreate.Service.Id);

            Assert.IsType<UpdateServiceResponse>(responseUpdate);
        }

        [Fact]
        public async void Update_ShouldThrow_WhenUserDoesNotExist()
        {
            var user = new CreateUserRequest
            {
                FirstName = "Update_ShouldThrow_WhenUserDoesNotExist",
                LastName = "Update_ShouldThrow_WhenUserDoesNotExist",
                Email = "Update_ShouldThrow_WhenUserDoesNotExist@mail.com",
                Password = "Update_ShouldThrow_WhenUserDoesNotExist",
                PasswordConfirmation = "Update_ShouldThrow_WhenUserDoesNotExist",
            };
            var userResponse = await _userService.Create(user);
            var serviceCreate = new CreateServiceRequest
            {
                UserId = userResponse.User.Id,
                Title = "Example service",
                Description = "Example Service",
                MainPictureUrl = "",
                Price = 0
            };
            var responseCreate = await _serviceService.Create(serviceCreate);

            var serviceUpdate = new UpdateServiceRequest
            {
                UserId = Guid.NewGuid(),
                Title = "Example",
                Description = "Example",
                MainPictureUrl = "",
                Price = 0
            };
            var function = async () => await _serviceService.Update(serviceUpdate, responseCreate.Service.Id);

            await Assert.ThrowsAsync<ArgumentException>(function);
        }

        #endregion

        #region Delete tests

        [Fact]
        public async void Delete_ShouldThrow_WhenServiceDoesNotExist()
        {
            var function = async () => await _serviceService.Delete(Guid.NewGuid());
            await Assert.ThrowsAsync<ArgumentException>(function);
        }

        [Fact]
        public async void Delete_ShouldThrow_WhenIdIsInvalid()
        {
            var function = async () => await _serviceService.Delete(Guid.Empty);
            await Assert.ThrowsAsync<ArgumentException>(function);
        }

        #endregion

        #region GetById tests
        [Fact]
        public async void GetById_ShouldReturn_GetByIdServiceResponse_WhenServiceExists()
        {
            var user = new CreateUserRequest
            {
                FirstName = "GetById_ShouldReturn_GetByIdServiceResponse_WhenServiceExists",
                LastName = "GetById_ShouldReturn_GetByIdServiceResponse_WhenServiceExists",
                Email = "GetById_ShouldReturn_GetByIdServiceResponse_WhenServiceExists@mail.com",
                Password = "GetById_ShouldReturn_GetByIdServiceResponse_WhenServiceExists",
                PasswordConfirmation = "GetById_ShouldReturn_GetByIdServiceResponse_WhenServiceExists",
            };
            var userResponse = await _userService.Create(user);

            var service = new CreateServiceRequest
            {
                Title = "GetById_ShouldReturn_GetByIdServiceRespo",
                Description = "GetById_ShouldReturn_GetByIdServiceResponse_WhenServiceExists",
                UserId = userResponse.User.Id,
                MainPictureUrl = "",
                Price = 0,
            };

            var response = await _serviceService.Create(service);
            var serviceResponse = await _serviceService.GetById(response.Service.Id, userResponse.User.Id);
            Assert.IsType<GetByIdServiceResponse>(serviceResponse);
        }

        [Fact]
        public async void GetById_ShouldThrow_WhenServiceDoesNotExist()
        {
            var function = async () => await _serviceService.GetById(Guid.NewGuid(), Guid.NewGuid());
            await Assert.ThrowsAsync<ArgumentException>(function);
        }

        [Fact]
        public async void GetById_ShouldThrow_WhenIdIsInvalid()
        {
            var function = async () => await _serviceService.GetById(Guid.Empty, Guid.NewGuid());
            await Assert.ThrowsAsync<ArgumentException>(function);
        }

        #endregion

        #region GetAll tests

        [Fact]
        public async void GetAll_ShouldReturn_GetServiceResponse_WhenValid()
        {
            var result = await _serviceService.GetAll();
            Assert.IsType<GetServicesResponse>(result);
        }

        #endregion
    }
}