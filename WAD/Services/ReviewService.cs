using WAD.Models;
using WAD.Repositories.Interfaces;
using WAD.Services.Interfaces;

namespace WAD.Services
{
    public class ReviewService : IReviewService
    {
        private IRepositoryWrapper _repo;

        public ReviewService(IRepositoryWrapper repo)
        {
            _repo = repo;
        }

        public void AddReview(Review review, int id, string userGuid)
        {
            var hotel = _repo.HotelRepository.FindByCondition(item => item.HotelId == id).FirstOrDefault();
            review.Hotel = hotel;
            review.UserGuid = userGuid;
            review.Created = DateTime.Now;
            _repo.ReviewRepository.Create(review);
            _repo.HotelRepository.Update(hotel);
            _repo.Save();
        }

        public List<Review> FindReviewsByHotelId(int id)
        {
            var reviews = _repo.ReviewRepository.FindByCondition(item => item.HotelId == id).OrderByDescending(item => item.Created).ToList();
            return reviews;
        }
    }
}
