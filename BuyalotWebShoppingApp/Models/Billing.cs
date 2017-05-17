using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace BuyalotWebShoppingApp.Models
{
    public class Billing
    {
        [Key]
        public int BillingID { get; set; }

        [Display(Name = "Customer # / Name")]
        public int CustomerID { get; set; }

        [Display(Name = "Bank Card #")]
        [Required(ErrorMessage = "Bank Card # is a required field")]
        public int CardNumber { get; set; }

        [Display(Name = "Card Type")]
        [Required(ErrorMessage = "Card Type is a required field")]
        [DataType(DataType.Text)]
        public string CardType { get; set; }

        [Display(Name = "CVV")]
        [Required(ErrorMessage = "CVV is a required field")]
        [Range(3, 3, ErrorMessage = "CVV is a three digit value found from the back of the card")]
        public int CVV { get; set; }

        [Display(Name = "Expiry Date")]
        [Required(ErrorMessage = "Expiry Date is a required field")]
        [DataType(DataType.Date)]
        public System.DateTime ExpDate { get; set; }

        [Display(Name = "Card Holder Name")]
        [Required(ErrorMessage = "Card Holder Name is a required field")]
        [DataType(DataType.Text)]
        public string CardHolderName { get; set; }

        public virtual Customer Customer { get; set; }

    }

}