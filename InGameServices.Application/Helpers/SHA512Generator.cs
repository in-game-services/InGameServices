using System.Security.Cryptography;
using System.Text;

namespace InGameServices.Application.Helpers;

public class SHA512Generator
{
  public static string Generate(string password)
  {
    SHA512 sha512 = SHA512.Create();
    byte[] bytes = Encoding.UTF8.GetBytes(password);
    byte[] hash = sha512.ComputeHash(bytes);
    string hashString = BitConverter.ToString(hash).Replace("-", "").ToLower();
    return hashString;
  }
}