using WAD.Models;
using WAD.Repositories.Interfaces;

namespace WAD.Repositories
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(BookNGoContext locationContext) : base(locationContext)
        {
        }
    }
}
