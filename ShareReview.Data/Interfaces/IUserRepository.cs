using ShareReview.Models.Users;

namespace ShareReview.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(string userId);

        bool Add(User user);
        bool Update(User user);
        bool Delete(string userId);
        bool SaveChanges();
    }
}
