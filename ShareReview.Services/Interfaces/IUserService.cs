using Microsoft.AspNetCore.Http;
using ShareReview.Contracts.Users;

namespace ShareReview.Services.Interfaces
{
    public interface IUserService
    {
        Task<Status> RegisterAsync(RegisterUserDTO userDTO);
        Task<Status> RegisterAdminAsync(RegisterUserDTO userDTO);
        Task<Status> LoginAsync(LoginUserDTO userDTO);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(string userId);
        Task<UserDTO> GetCurrentUserAsync(HttpContext httpContext);
        Status DeleteUser(string userId);
        Task LogoutAsync();
    }
}
