using System.ComponentModel.DataAnnotations;
using ShareReview.Contracts.Reviews;
using ShareReview.Models.Reviews;

namespace ShareReview.Web.ViewModels.Reviews
{
    public class ReviewViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ArtTitle { get; set; }
        [Required]
        public string Text { get; set; }
        public IFormFile Image { get; set; }
       
        public ReviewGroup Group { get; set; }
        public string UserId { get; set; }
        public CreateReviewDTO GetReviewDTO(string imageUrl)
        {
            return new CreateReviewDTO
            {
                Name = Name,
                ArtTitle = ArtTitle,
                Text = Text,
                ImageUrl = imageUrl,
                Group = Group,
                UserId= UserId
            };
        }
    }
}
