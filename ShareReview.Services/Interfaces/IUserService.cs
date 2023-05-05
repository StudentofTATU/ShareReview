using ShareReview.Contracts.Users;

namespace ShareReview.Services.Interfaces
{
    public interface IUserService
    {
        Task<Status> RegisterAsync(RegisterUserDTO userDTO);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(string userId);
        Status DeleteUser(string userId);
    }
}
