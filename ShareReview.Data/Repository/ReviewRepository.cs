
using Microsoft.EntityFrameworkCore;
using ShareReview.Data.Interfaces;
using ShareReview.Models.Reviews;

namespace ShareReview.Data.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext context;

        public ReviewRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Review>> GetAllReviewsAsync()
        {
            return await context.Reviews.ToListAsync();
        }

        public async Task<Review> GetReviewByIdAsync(string reviewId)
        {
            return await context.Reviews
                .FirstOrDefaultAsync(i => i.Id.ToString().Equals(reviewId));
        }

        public bool Add(Review review)
        {
            context.Reviews.Add(review);

            return SaveChanges();
        }

        public bool Delete(string reviewId)
        {
            Review reviewDelete = context.Reviews.Find(reviewId);
            context.Reviews.Remove(reviewDelete);

            return SaveChanges();
        }

        public bool Update(Review review)
        {
           Review reviewUpdate=context.Reviews.Find(review);
            reviewUpdate.Name = review.Name;
            reviewUpdate.ArtTitle = review.ArtTitle;
            reviewUpdate.Text = review.Text;
            reviewUpdate.Image = review.Image;
            reviewUpdate.Group = review.Group;

            return SaveChanges();
        }

        public bool SaveChanges()
        {
            var saved = context.SaveChanges();

            return saved > 0;
        }
    }
}
