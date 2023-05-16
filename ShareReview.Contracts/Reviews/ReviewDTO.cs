using ShareReview.Models.Reviews;

namespace ShareReview.Contracts.Reviews
{
    public class ReviewDTO
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ArtTitle { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public ReviewGroup Group { get; set; }
        public int Likes { get; set; }
        public double Grade { get; set; }
    }
}
