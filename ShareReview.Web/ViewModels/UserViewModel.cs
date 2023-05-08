using ShareReview.Contracts.Users;

namespace ShareReview.Web.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(UserDTO userDTO)
        {
            this.Name= userDTO.Name;   
            this.Email= userDTO.Email;
        }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
