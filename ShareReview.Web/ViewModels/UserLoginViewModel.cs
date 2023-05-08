using System.ComponentModel.DataAnnotations;
using ShareReview.Contracts.Users;

namespace ShareReview.Web.ViewModels
{
    public class UserLoginViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^.{6,}$", ErrorMessage =
            "Minimum length 6.")]
        public string Password { get; set; }

        public LoginUserDTO GetUserDTO()
        {
            return new LoginUserDTO
            {
                Name = this.Name,
                Password = this.Password
            };
        }
    }
}
