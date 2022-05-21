using WAD.Models;
using WAD.Repositories.Interfaces;
using WAD.Services.Interfaces;

namespace WAD.Services
{
    public class UserService : IUserService
    {
        private IRepositoryWrapper _repo;

        public UserService(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        public List<User> GetUserByName(string name)
        {
            var users = new List<User>();

            users = _repo.UserRepository.FindByCondition(l => l.FirstName == name).ToList();

            return users;
        }
    }
}
