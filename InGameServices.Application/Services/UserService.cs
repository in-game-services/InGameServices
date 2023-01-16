using InGameServices.Application.Helpers;
using InGameServices.Application.Helpers.Abstractions;
using InGameServices.Application.Services.Abstractions;
using InGameServices.Application.Validators.Abstractions;
using InGameServices.Data.Entities;
using InGameServices.Data.Repositories.Abstractions;
using InGameServices.Models;
using InGameServices.Models.User.Messages.Request;
using InGameServices.Models.User.Messages.Response;

namespace InGameServices.Application.Services;

public class UserService : IUserService
{
  private readonly IUserRepository _userRepository;
  private readonly IUserValidator _userValidator;
  private ITokenGenerator _tokenGenerator;

  public UserService(IUserRepository userRepository, IUserValidator userValidator, ITokenGenerator tokenGenerator)
  {
    _userRepository = userRepository;
    _userValidator = userValidator;
    _tokenGenerator = tokenGenerator;
  }

  public async Task<CreateUserResponse> Create(CreateUserRequest request)
  {
    var encryptedUserPassword = SHA512Generator.Generate(request.Password);
    var user = new User(request.FirstName, request.LastName, request.Email, encryptedUserPassword);
    await _userValidator.ValidateUser(user);
    _userValidator.ValidatePasswordLength(request.Password);
    _userValidator.ValidatePasswordMatch(request.Password, request.PasswordConfirmation);
    await _userRepository.Create(user);
    var token = _tokenGenerator.Generate(user);

    return new CreateUserResponse
    {
      User = (UserDto)user,
      Token = token
    };
  }

  public async Task<UpdateUserResponse> Update(UpdateUserRequest request, Guid id)
  {
    _userValidator.ValidateRequestId(id);
    _userValidator.ValidatePasswordLength(request.Password);
    _userValidator.ValidatePasswordMatch(request.Password, request.PasswordConfirmation);
    var user = await _userRepository.GetById(id);
    _userValidator.ValidateUserNotNull(user);
    var encryptedUserPassword = SHA512Generator.Generate(request.Password);
    user.Update(request.FirstName, request.LastName, request.Email, encryptedUserPassword, request.PictureUrl, request.Description);
    await _userValidator.ValidateUser(user);
    await _userRepository.Update(user);
    var token = _tokenGenerator.Generate(user);

    return new UpdateUserResponse
    {
      User = (UserDto)user,
      Token = token
    };
  }

  public async Task<GetByIdUserResponse> GetById(Guid id)
  {
    _userValidator.ValidateRequestId(id);
    var user = await _userRepository.GetById(id);
    _userValidator.ValidateUserNotNull(user);

    return new GetByIdUserResponse
    {
      User = (UserDto)user
    };
  }
}