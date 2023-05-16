using ShareReview.Contracts.Users;

namespace ShareReview.Web.ViewModels.Users
{
    public class UserViewModel
    {
        public UserViewModel(UserDTO userDTO)
        {
            Name = userDTO.Name;
            Email = userDTO.Email;
        }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
