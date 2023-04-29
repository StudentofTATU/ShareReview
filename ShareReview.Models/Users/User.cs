using Microsoft.AspNetCore.Identity;

namespace ShareReview.Models.Users
{
    public class User : IdentityUser
    {
        public DateTimeOffset CreatedDateTime { get; set; }
        public UserState UserState { get; set; }
    }
}
