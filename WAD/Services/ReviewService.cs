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

        public void AddReview(Review review, int id)
        {
            var hotel = _repo.HotelRepository.FindByCondition(item => item.HotelId == id).FirstOrDefault();
            var user = _repo.UserRepository.FindAll().FirstOrDefault();
            review.Hotel = hotel;
            review.User = user;
            review.Created = DateTime.Now;
            _repo.ReviewRepository.Create(review);
            _repo.HotelRepository.Update(hotel);
            _repo.UserRepository.Update(user);
            _repo.Save();
        }

        public List<Review> FindReviewsByHotelId(int id)
        {
            var reviews = _repo.ReviewRepository.FindByCondition(item => item.HotelId == id).OrderByDescending(item => item.Created).ToList();
            return reviews;
        }
    }
}
