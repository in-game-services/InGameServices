using InGameServices.Application.Validators.Abstractions;
using InGameServices.Data.Entities;
using InGameServices.Data.Repositories.Abstractions;
using System.Text.RegularExpressions;

namespace InGameServices.Application.Validators;

public class UserValidator : IUserValidator
{
  private readonly IUserRepository _userRepository;
  public UserValidator(IUserRepository userRepository)
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

  public void ValidateUserNotNull(User user)
  {
    if (user is null)
    {
      throw new ArgumentException("User not found");
    }
  }

  public void ValidatePasswordLength(string password)
  {
    if (password.Length < 8)
    {
      throw new ArgumentException("Password needs to have at least 8 characters");
    }
  }

  public void ValidatePasswordMatch(string password, string passwordConfirmation)
  {
    if (password != passwordConfirmation)
    {
      throw new ArgumentException("Passwords do not match");
    }
  }

  public void ValidateEmail(string email)
  {
    if (string.IsNullOrWhiteSpace(email))
    {
      throw new ArgumentException("Email cannot be empty");
    }

    if (!email.Contains("@") || !email.Contains("."))
    {
      throw new ArgumentException("Invalid email");
    }

    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
    Match match = regex.Match(email);
    if (!match.Success)
    {
      throw new ArgumentException("Invalid email");
    }
  }

  public async Task ValidateUser(User user)
  {
    if (string.IsNullOrWhiteSpace(user.FirstName))
    {
      throw new ArgumentException("First name cannot be empty");
    }

    if (string.IsNullOrWhiteSpace(user.LastName))
    {
      throw new ArgumentException("Last name cannot be empty");
    }

    ValidateEmail(user.Email);

    var userWithSameEmail = await _userRepository.GetByEmail(user.Email);
    if (userWithSameEmail is not null && userWithSameEmail.Id != user.Id)
    {
      throw new ArgumentException("Email already in use by user with different id");
    }
  }
}