using InGameServices.Data.Entities;

namespace InGameServices.Application.Helpers.Abstractions
{
    public interface ITokenGenerator
    {
        string Generate(User user);
    }
}
