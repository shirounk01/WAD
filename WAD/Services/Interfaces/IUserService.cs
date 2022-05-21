using WAD.Models;

namespace WAD.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetUserByName(string name);
    }
}
