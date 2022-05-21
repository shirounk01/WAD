using WAD.Models;
using WAD.Repositories.Interfaces;

namespace WAD.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(BookNGoContext locationContext) : base(locationContext)
        {
        }
    }
}