using Microsoft.AspNetCore.Identity;
using ShareReview.Contracts.Users;
using ShareReview.Data.Interfaces;
using ShareReview.Models.Users;
using ShareReview.Services.Interfaces;

namespace ShareReview.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        private readonly IUserRepository userRepository;

        public UserService(UserManager<User> userManager, IUserRepository userRepository)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
        }

        public async Task<Status> RegisterAsync(RegisterUserDTO userDTO)
        {
            var status=new Status();
            var userExists = await userManager.FindByNameAsync(userDTO.Name);

            if(userExists !=null)
            {
                status.StatusCode = 0; 
                status.Message="User already exist";
                return status;
            }

            User user = new()
            {
                UserName = userDTO.Name,
                Email = userDTO.Email,
                CreatedDateTime = DateTimeOffset.UtcNow,
                UserState = UserState.ACTIVE,
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var saveUser=await userManager.CreateAsync(user,userDTO.Password);

            if (!saveUser.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User registration is failed.";
                return status;
            }

            status.StatusCode = 1;
            status.Message = "You have registered successfully";
            return status;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            IEnumerable<User> users = await userRepository.GetAllUsersAsync();
            List<UserDTO> userDTOList =new List<UserDTO>();
            foreach (var user in users)
            {
                userDTOList.Add(new UserDTO(user));
            }

            return userDTOList;
        }

        public async Task<UserDTO> GetUserByIdAsync(string userId)
        {
            User user= await userRepository.GetUserByIdAsync(userId);

            return new UserDTO(user);
        }

        public Status DeleteUser(string userId)
        {
            var status = new Status();
            bool userDeleted =  userRepository.Delete(userId);

            if (userDeleted)
            {
                status.StatusCode = 1;
                status.Message = "User is deleted.";
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "User is not deleted.";
            }

            return status;
        }
    }
}
