namespace InGameServices.Models.User.Messages.Request;

public class UpdateUserRequest
{
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string Password { get; set; }
  public string PasswordConfirmation { get; set; }
  public string Email { get; set; }
  public string PictureUrl { get; set; }
  public string Description { get; set; }
}