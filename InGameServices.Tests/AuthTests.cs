using InGameServices.Application.Services;
using InGameServices.Application.Validators.Abstractions;
using InGameServices.Application.Validators;
using InGameServices.Data.Repositories.Abstractions;
using InGameServices.Data.Repositories;
using InGameServices.Helpers;
using InGameServices.Models.User.Messages.Request;
using Xunit;
using InGameServices.Application.Helpers.Abstractions;
using InGameServices.Application.Helpers;
using Microsoft.Extensions.Configuration;
using InGameServices.Models.Auth.Messages.Response;
using InGameServices.Models.Auth.Messages.Request;
using InGameServices.Application.Services.Abstractions;

namespace InGameServices.Tests
{
    public class AuthTests
    {
        public IUserService _userService;
        public IAuthService _authService;
        public AuthTests()
        {
            var context = InMemoryContext.CreateContext();
            IUserRepository userRepository = new UserRepository(context);
            IUserValidator userValidator = new UserValidator(userRepository);
            IConfiguration configuration = ConfigurationMock.CreateConfiguration();
            ITokenGenerator tokenGenerator = new TokenGenerator(configuration);

            _userService = new UserService(userRepository, userValidator, tokenGenerator);
            _authService = new AuthService(userRepository, tokenGenerator, userValidator);
        }

        [Fact]
        public async void Authenticate_ShouldReturn_AuthResponse_WhenCorrect()
        {
            var user = new CreateUserRequest
            {
                FirstName = "Authenticate_ShouldReturn_UserAndToken_WhenCorrect",
                LastName = "Authenticate_ShouldReturn_UserAndToken_WhenCorrect",
                Email = "Authenticate_ShouldReturn_UserAndToken_WhenCorrect@mail.com",
                Password = "Authenticate_ShouldReturn_UserAndToken_WhenCorrect",
                PasswordConfirmation = "Authenticate_ShouldReturn_UserAndToken_WhenCorrect",
            };
            await _userService.Create(user);
            var authRequest = new AuthRequest
            {
                Email = user.Email,
                Password = user.Password
            };

            var response = await _authService.Authenticate(authRequest);
            Assert.IsType<AuthResponse>(response);
        }

        [Fact]
        public async void Authenticate_ShouldThrow_WhenEmailNotRegistered()
        {
            var authRequest = new AuthRequest
            {
                Email = "Authenticate_ShouldThrow_WhenEmailNotRegistered",
                Password = "Authenticate_ShouldThrow_WhenEmailNotRegistered"
            };

            var function = async () => await _authService.Authenticate(authRequest);
            await Assert.ThrowsAsync<ArgumentException>(function);
        }

        [Fact]
        public async void Authenticate_ShouldThrow_WhenPasswordIsWrong()
        {
            var user = new CreateUserRequest
            {
                FirstName = "Authenticate_ShouldThrow_WhenPasswordIsWrong",
                LastName = "Authenticate_ShouldThrow_WhenPasswordIsWrong",
                Email = "Authenticate_ShouldThrow_WhenPasswordIsWrong@mail.com",
                Password = "Authenticate_ShouldThrow_WhenPasswordIsWrong",
                PasswordConfirmation = "Authenticate_ShouldThrow_WhenPasswordIsWrong",
            };
            await _userService.Create(user);
            var authRequest = new AuthRequest
            {
                Email = user.Email,
                Password = "wrong password"
            };

            var function = async () => await _authService.Authenticate(authRequest);
            await Assert.ThrowsAsync<ArgumentException>(function);
        }
    }
}