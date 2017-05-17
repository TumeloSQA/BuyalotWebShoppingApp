using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace BuyalotWebShoppingApp.Models
{
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }


        [Display(Name = "Payment Date")]
        public System.DateTime PaymentDate { get; set; }

        [Display(Name = "Customer # / Name")]
        public int CustomerID { get; set; }

        [Display(Name = "Order #")]
        public int OrderID { get; set; }

        [Display(Name = "Payment Type")]
        public string PaymentType { get; set; }

        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Order Order { get; set; }

    }

}