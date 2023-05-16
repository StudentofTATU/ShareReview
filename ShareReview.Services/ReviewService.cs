using ShareReview.Contracts.Reviews;
using ShareReview.Data.Interfaces;
using ShareReview.Models.Reviews;
using ShareReview.Services.Interfaces;

namespace ShareReview.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        public bool CreateReview(CreateReviewDTO createReviewDTO)
        {
            Review review = new Review
            {
                Id = Guid.NewGuid(),
                Name = createReviewDTO.Name,
                ArtTitle = createReviewDTO.ArtTitle,
                Text = createReviewDTO.Text,
                Image = createReviewDTO.ImageUrl,
                Group = createReviewDTO.Group,
                CreatedUserId = new Guid(createReviewDTO.UserId),
                CreatedDate=DateTimeOffset.Now,
            };

          return reviewRepository.Add(review);
        }

        public async Task<IEnumerable<ReviewDTO>> GetAllReviewsAsync()
        {
            IEnumerable<Review> reviews=await reviewRepository.GetAllReviewsAsync();
            List<ReviewDTO> reviewDTOList=new List<ReviewDTO>();
            foreach (Review review in reviews)
            {
                reviewDTOList.Add(new ReviewDTO
                {
                    Id=review.Id,
                    Name = review.Name,
                    ArtTitle = review.ArtTitle,
                    Text = review.Text,
                    Image = review.Image,
                    Group = review.Group,
                    Likes = 0,
                    Grade= 0
                }) ;
            }

            return reviewDTOList;

        }
    }
}
