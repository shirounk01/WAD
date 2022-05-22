using WAD.Models;

namespace WAD.Services.Interfaces
{
    public interface IReviewService
    {
        List<Review> FindReviewsByHotelId(int id);
        void AddReview(Review review, int id, string userGuid);
    }
}
