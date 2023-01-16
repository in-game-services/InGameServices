namespace InGameServices.Application.Services;

public class RandomPasswordGenerator
{
  public static string Generate()
  {
    Random random = new Random();
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    var password = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());
    return password;
  }
}