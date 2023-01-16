namespace InGameServices.Models.Auth.Messages.Response;

public class AuthResponse
{
  public UserDto User { get; set; }
  public string Token { get; set; }
  public AuthResponse(UserDto user, string token)
  {
    User = user;
    Token = token;
  }
}