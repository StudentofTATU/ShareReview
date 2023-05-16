using ShareReview.Contracts.Reviews;

namespace ShareReview.Services.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDTO>> GetAllReviewsAsync();
        bool CreateReview(CreateReviewDTO createReviewDTO);
    }
}
