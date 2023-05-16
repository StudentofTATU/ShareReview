using ShareReview.Models.Users;

namespace ShareReview.Contracts.Users
{
    public class UserDTO
    {
        public UserDTO(User user)
        {
            this.Id = user.Id;
            this.Name = user.UserName;
            this.Email = user.Email;
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        
    }
}
