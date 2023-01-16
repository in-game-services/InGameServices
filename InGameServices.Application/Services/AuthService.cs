using InGameServices.Application.Helpers;
using InGameServices.Application.Helpers.Abstractions;
using InGameServices.Application.Services.Abstractions;
using InGameServices.Application.Validators.Abstractions;
using InGameServices.Data.Repositories.Abstractions;
using InGameServices.Models;
using InGameServices.Models.Auth.Messages.Request;
using InGameServices.Models.Auth.Messages.Response;

namespace InGameServices.Application.Services;

public class AuthService : IAuthService
{
  private readonly ITokenGenerator _tokenGenerator;
  private readonly IUserRepository _userRepository;
  private readonly IUserValidator _userValidator;

  public AuthService(IUserRepository userRepository, ITokenGenerator tokenGenerator, IUserValidator userValidator)
  {
    _userRepository = userRepository;
    _tokenGenerator = tokenGenerator;
    _userValidator = userValidator;
  }

  public async Task<AuthResponse> Authenticate(AuthRequest request)
  {
    var user = await _userRepository.GetByEmail(request.Email);
    _userValidator.ValidateUserNotNull(user);
    var encryptedRequestPassword = SHA512Generator.Generate(request.Password);
    _userValidator.ValidatePasswordMatch(encryptedRequestPassword, user.Password);

    string token = _tokenGenerator.Generate(user);

    return new AuthResponse((UserDto)user, token);
  }
}