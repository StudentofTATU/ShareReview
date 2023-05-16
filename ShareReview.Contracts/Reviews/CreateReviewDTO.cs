using ShareReview.Models.Reviews;

namespace ShareReview.Contracts.Reviews
{
    public class CreateReviewDTO
    {
        public string Name { get; set; }
        public string ArtTitle { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public ReviewGroup Group { get; set; }
        public string UserId { get; set; }
    }
}
