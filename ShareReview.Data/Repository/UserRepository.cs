using Microsoft.EntityFrameworkCore;
using ShareReview.Data.Interfaces;
using ShareReview.Models.Users;

namespace ShareReview.Data.Repository
{
    internal class UserRepository : IUserRepository
    {
        private readonly AppDbContext context;

        public UserRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await context.Users.FirstOrDefaultAsync(i => i.Id.Equals(userId));
        }

        public bool Add(User user)
        {
            context.Add(user);

            return SaveChanges();
        }
        public bool Update(User user)
        {
            User updateUser = context.Users.Find(user.Id);

            updateUser.UserName = user.UserName;
            updateUser.Email = user.Email;
            updateUser.PasswordHash = user.PasswordHash;
            updateUser.UserState = user.UserState;

            return SaveChanges();
        }

        public bool Delete(User user)
        {
            User deleteUser = context.Users.Find(user.Id);
            context.Users.Remove(deleteUser);

            return SaveChanges();
        }

        public bool SaveChanges()
        {
            var saved = context.SaveChanges();

            return saved > 0;
        }
    }
}