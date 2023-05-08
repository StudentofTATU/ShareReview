using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShareReview.Contracts.Users;
using ShareReview.Models.Users;

namespace ShareReview.Services
{
    public partial class UserService
    {
        public async Task<Status> SignInUser(User user, LoginUserDTO userDTO)
        {
            var status = new Status();
            var signInUser = await signInManager.PasswordSignInAsync(user, userDTO.Password, false, false);

            if (signInUser.Succeeded)
            {
                status.StatusCode = 1;
                status.Message = "Logged in  successfully";
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Error on logging in.";
            }
            return status;
        }

        public async Task<Status> SaveUser(User user,RegisterUserDTO userDTO) {

            var status = new Status();
            var saveUser = await userManager.CreateAsync(user, userDTO.Password);

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

        public async Task<Status> IsUserExists(RegisterUserDTO userDTO) {
            var status = new Status();
            var userExists = await userManager.FindByNameAsync(userDTO.Name);

            if (userExists == null)
            {
                status.StatusCode = 0;
                status.Message = "User  does not exist";
            }
            else
            {
                status.StatusCode = 1;
                status.Message = "User already exist";

            }
            return status;
        }

        public async Task<Status> IsUserValid(User user, LoginUserDTO userDTO)
        {
            var status = new Status();
            if (user == null)
            {
                status.StatusCode = 0;
                status.Message = "Invalid user name.";
                return status;
            }
            var checkUserPassword = await userManager.CheckPasswordAsync(user, userDTO.Password);

            if (!checkUserPassword)
            {
                status.StatusCode = 0;
                status.Message = "Invalid user password.";
                return status;
            }
            status.StatusCode = 1;
            status.Message = "User is valid.";
            return status;
        }

        public User GenerateUser(RegisterUserDTO userDTO)
        {
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

            return user;
        }
    }
}
