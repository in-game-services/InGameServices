using InGameServices.Application.Helpers;
using InGameServices.Application.Helpers.Abstractions;
using InGameServices.Application.Services;
using InGameServices.Application.Services.Abstractions;
using InGameServices.Application.Validators;
using InGameServices.Application.Validators.Abstractions;
using InGameServices.Data.Repositories;
using InGameServices.Data.Repositories.Abstractions;
using InGameServices.Helpers;
using InGameServices.Models.User.Messages.Request;
using InGameServices.Models.User.Messages.Response;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace InGameServices.Tests
{
    public class UserTests
    {
        public IUserService _userService;
        public UserTests()
        {
            var context = InMemoryContext.CreateContext();
            IUserRepository userRepository = new UserRepository(context);
            IUserValidator userValidator = new UserValidator(userRepository);
            IConfiguration configuration = ConfigurationMock.CreateConfiguration();
            ITokenGenerator tokenGenerator = new TokenGenerator(configuration);

            _userService = new UserService(userRepository, userValidator, tokenGenerator);
        }

        #region Create tests

        [Fact]
        public async void Create_ShouldReturn_CreateUserResponse_WhenValid()
        {
            var user = new CreateUserRequest
            {
                FirstName = "Create_ShouldReturn_CreateUserResponse_WhenValid",
                LastName = "Create_ShouldReturn_CreateUserResponse_WhenValid",
                Email = "Create_ShouldReturn_CreateUserResponse_WhenValid@mail.com",
                Password = "Create_ShouldReturn_CreateUserResponse_WhenValid",
                PasswordConfirmation = "Create_ShouldReturn_CreateUserResponse_WhenValid",
            };
            var response = await _userService.Create(user);
            Assert.IsType<CreateUserResponse>(response);
        }

        [Theory]
        [InlineData("John", "Doe", "JohnDoe@mail.com", "JohnDoe12",  "JohnDoe123")]
        [InlineData("John", "Doe", "JohnDoe@mail.com", "JohnDoe",    "JohnDoe")]
        [InlineData("JohnDoe", "", "JohnDoe@mail.com", "JohnDoe123", "JohnDoe123")]
        [InlineData("John", "Doe", "",                 "JohnDoe123", "JohnDoe123")]
        [InlineData("", "JohnDoe", "JohnDoe@mail.com", "JohnDoe123", "JohnDoe123")]
        [InlineData("John", "Doe", "JohnDoe@mail", "JohnDoe123", "JohnDoe123")]
        public async void Create_ShouldThrow_WhenInvalid(string firstName, string lastName, string email, string password, string confirmation)
        {
            var user = new CreateUserRequest
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                PasswordConfirmation = confirmation
            };
            var function = async () => await _userService.Create(user);
            await Assert.ThrowsAsync<ArgumentException>(function);
        }

        [Fact]
        public async void Create_ShouldThrow_WhenEmailAlreadyRegistered()
        {
            var user = new CreateUserRequest
            {
                FirstName = "Create_ShouldThrow_WhenUserAlreadyExists",
                LastName = "Create_ShouldThrow_WhenUserAlreadyExists",
                Email = "Create_ShouldThrow_WhenUserAlreadyExists@mail.com",
                Password = "Create_ShouldThrow_WhenUserAlreadyExists",
                PasswordConfirmation = "Create_ShouldThrow_WhenUserAlreadyExists",
            };
            var response = await _userService.Create(user);
            var function = async () => await _userService.Create(user);
            await Assert.ThrowsAsync<ArgumentException>(function);
        }
        #endregion

        #region Update tests
        [Fact]
        public async void Update_ShouldReturn_UpdateUserResponse_WhenValid()
        {
            var user = new CreateUserRequest
            {
                FirstName = "Update_ShouldReturn_UpdateUserResponse_WhenValid",
                LastName = "Update_ShouldReturn_UpdateUserResponse_WhenValid",
                Email = "Update_ShouldReturn_UpdateUserResponse_WhenValid@mail.com",
                Password = "Update_ShouldReturn_UpdateUserResponse_WhenValid",
                PasswordConfirmation = "Update_ShouldReturn_UpdateUserResponse_WhenValid",
            };
            var response = await _userService.Create(user);
            var id = response.User.Id;

            var userUpdate = new UpdateUserRequest
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                PasswordConfirmation = user.PasswordConfirmation,
            };
            var result = await _userService.Update(userUpdate, id);
            Assert.IsType<UpdateUserResponse>(result);
        }

        [Fact]
        public async void Update_ShouldThrow_WhenEmailAlreadyRegistered()
        {
            var user1 = new CreateUserRequest
            {
                FirstName = "Update_ShouldThrow_WhenEmailAlreadyRegistered1",
                LastName = "Update_ShouldThrow_WhenEmailAlreadyRegistered1",
                Email = "Update_ShouldThrow_WhenEmailAlreadyRegistered1@mail.com",
                Password = "Update_ShouldThrow_WhenEmailAlreadyRegistered",
                PasswordConfirmation = "Update_ShouldThrow_WhenEmailAlreadyRegistered",
            };
            var response = await _userService.Create(user1);
            var id = response.User.Id;

            var user2 = new CreateUserRequest
            {
                FirstName = "Update_ShouldThrow_WhenEmailAlreadyRegistered2",
                LastName = "Update_ShouldThrow_WhenEmailAlreadyRegistered2",
                Email = "Update_ShouldThrow_WhenEmailAlreadyRegistered2@mail.com",
                Password = "Update_ShouldThrow_WhenEmailAlreadyRegistered",
                PasswordConfirmation = "Update_ShouldThrow_WhenEmailAlreadyRegistered",
            };

            response = await _userService.Create(user2);

            var userUpdate = new UpdateUserRequest
            {
                FirstName = user1.FirstName,
                LastName = user1.LastName,
                Email = user2.Email,
                Password = user1.Password,
                PasswordConfirmation = user1.PasswordConfirmation,
            };
            var function = async () => await _userService.Update(userUpdate, id);
            await Assert.ThrowsAsync<ArgumentException>(function);
        }

        #endregion

        #region GetById tests
        [Fact]
        public async void GetById_ShouldReturn_GetByIdUserResponse_WhenUserExists()
        {
            var user = new CreateUserRequest
            {
                FirstName = "GetById_ShouldReturnUser_WhenUserExists",
                LastName = "GetById_ShouldReturnUser_WhenUserExists",
                Email = "GetById_ShouldReturnUser_WhenUserExists@mail.com",
                Password = "GetById_ShouldReturnUser_WhenUserExists",
                PasswordConfirmation = "GetById_ShouldReturnUser_WhenUserExists",
            };
            var response = await _userService.Create(user);
            var userInDataBase = await _userService.GetById(response.User.Id);
            Assert.IsType<GetByIdUserResponse>(userInDataBase);
        }

        [Fact]
        public async void GetById_ShouldThrow_WhenUserDoesNotExist()
        {
            var function = async () => await _userService.GetById(Guid.NewGuid());
            await Assert.ThrowsAsync<ArgumentException>(function);
        }

        [Fact]
        public async void GetById_ShouldThrow_WhenIdIsInvalid()
        {
            var function = async () => await _userService.GetById(Guid.Empty);
            await Assert.ThrowsAsync<ArgumentException>(function);
        }

        #endregion

    }
}