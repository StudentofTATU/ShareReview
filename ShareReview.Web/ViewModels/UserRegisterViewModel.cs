﻿using System.ComponentModel.DataAnnotations;
using ShareReview.Contracts.Users;

namespace ShareReview.Web.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^.{6,}$", ErrorMessage = 
            "Minimum length 6.")]
        public string Password { get; set; }

        public RegisterUserDTO GetUserDTO()
        {
            return new RegisterUserDTO
            {
                Name = this.Name,
                Email = this.Email,
                Password = this.Password
            };
        }
    }
}
