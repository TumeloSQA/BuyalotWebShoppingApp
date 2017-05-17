using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace BuyalotWebShoppingApp.Models
{
    public class Address
    {
        [Key]
        public int AddressID { get; set; }

        [Display(Name = "Customer #")]
        public int CustomerID { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is a required field")]
        public string Address1 { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City is a required field")]
        [DataType(DataType.Text)]
        public string City { get; set; }

        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "Postal Code is a required field")]
        public int PostalCode { get; set; }

        public virtual Customer Customer { get; set; }

    }

}