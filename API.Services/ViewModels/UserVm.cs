using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace API.Services.ViewModels
{
    public class UserVm
    {
        [Required(ErrorMessage = "The Name Field is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Email Field is Required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        [Required(ErrorMessage = "The Country Field is Required")]
        public string Country { get; set; }

        [Required(ErrorMessage = "The Phone Field is Required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "The Password Field is Required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "The Confirm Password Field is Required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Username { get; set; }
    }
}
