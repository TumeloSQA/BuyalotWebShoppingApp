using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace BuyalotWebShoppingApp.Models
{
    public class OrderDetail
    {
        [Key]
        public int orderDetailsID { get; set; }

        [Display(Name = "Order #")]
        public int OrderID { get; set; }

        [Display(Name = "Customer #")]
        public int ProductID { get; set; }

        [Display(Name = "Quantity Ordered")]
        public int QuantityOrdered { get; set; }

        [Display(Name = "Price Each")]
        public decimal PriceEach { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }

}