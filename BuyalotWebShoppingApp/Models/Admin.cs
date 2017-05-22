using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace BuyalotWebShoppingApp.Models
{
    public class Admin
    {
        [Key]
        public int adminID { get; set; }

        [Display(Name = "Admin Name")]
        [Required(ErrorMessage = "Admin Name is a required field")]
        [DataType(DataType.Text)]
        public string adminName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Address is a required field")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is a required field")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("password", ErrorMessage = "Passwords do not match")]
        public string confirmPassword { get; set; }

        public bool isValid(string email, string password)
        {
            BuyalotDbContext context = new BuyalotDbContext();
            var u = (from s in context.Admins
                     where s.email == email && s.password == password
                     select s).ToList();
            if (u.Count > 0)
                return true;
            else
                return false;
        }

    }

}