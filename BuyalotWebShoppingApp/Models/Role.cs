using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace BuyalotWebShoppingApp.Models
{
    public class Role
    {
        [Key]
        public int roleId { get; set; }

        [Required(ErrorMessage = "Role Name is a required field")]
        [Display(Name = "Role Name")]
        public string roleName { get; set; }
    }

}