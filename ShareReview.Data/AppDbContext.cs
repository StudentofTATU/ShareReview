using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShareReview.Models.Reviews;
using ShareReview.Models.Users;

namespace ShareReview.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<User> Users { set; get; }
        public DbSet<Review> Reviews { set; get; }

    }
}
