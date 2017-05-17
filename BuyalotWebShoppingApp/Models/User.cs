using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuyalotWebShoppingApp.Models
{
    public class User
    {
        [Key]
        public int userId { get; set; }

        [Required(ErrorMessage = "Username is a required field")]
        [Display(Name = "Username")]
        [StringLength(maximumLength:12, ErrorMessage = "Username must be between 8 and 12",MinimumLength =8)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email Address is a required field")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is a required field")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is a required field")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }
    }

}