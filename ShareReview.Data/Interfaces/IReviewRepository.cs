using ShareReview.Models.Reviews;

namespace ShareReview.Data.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllReviewsAsync();
        Task<Review> GetReviewByIdAsync(string reviewId);

        bool Add(Review review);
        bool Update(Review review);
        bool Delete(string reviewId);
        bool SaveChanges();
    }
}
