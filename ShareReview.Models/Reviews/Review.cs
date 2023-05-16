
namespace ShareReview.Models.Reviews
{
    public class Review
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ArtTitle { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public ReviewGroup Group { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public Guid CreatedUserId  { get; set; }
    }
}
