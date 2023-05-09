using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ShareReview.Contracts.Users;
using ShareReview.Data.Interfaces;
using ShareReview.Models.Users;
using ShareReview.Services.Interfaces;

namespace ShareReview.Services
{
    public partial class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserService(IUserRepository userRepository,UserManager<User> userManager,
            SignInManager<User> signInManager,RoleManager<IdentityRole> roleManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public async Task<Status> RegisterAsync(RegisterUserDTO userDTO)
        {
            var status = await IsUserExists(userDTO);
            if(status.StatusCode== 1) { return status; }

            User user =GenerateUser(userDTO);

            status=await SaveUser(user,userDTO);

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

        public async Task<Status> LoginAsync(LoginUserDTO userDTO)
        {
            Status status;
            var user = await userManager.FindByNameAsync(userDTO.Name);

            status=await IsUserValid(user, userDTO);

            if(status.StatusCode == 1)
            {
                status=await SignInUser(user,userDTO);
            }

            return status;  
        }


        
        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<UserDTO> GetCurrentUserAsync(HttpContext httpContext)
        {
            string userId = userManager.GetUserId(httpContext.User);

            User user= await userManager.FindByIdAsync(userId);

            return new UserDTO(user);
        }

        public async Task AddRoles()
        {
                List<UserRoles> roles =new List<UserRoles> { UserRoles.User,UserRoles.Admin};
                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role.ToString()))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role.ToString()));
                    }
                }
        }

        public async Task<Status> RegisterAdminAsync(RegisterUserDTO userDTO)
        {
           await AddRoles();
            var status = await IsUserExists(userDTO);
            if (status.StatusCode == 1) { return status; }

            User user = GenerateUser(userDTO);

            status = await SaveUser(user, userDTO);


            if (status.StatusCode == 1)
            {
                if (await roleManager.RoleExistsAsync(UserRoles.Admin.ToString()))
                {
                    await userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());
                }
            }

            return status;
        }
    }
}
