using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace BuyalotWebShoppingApp.Models
{
    public class Cart
    {

        [Key]
        public int RecordId { get; set; }

        [Display(Name = "Card #")]
        public string CartId { get; set; }

        [Display(Name = "Product # / Name")]
        public int ProductId { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Date Created")]
        public System.DateTime DateCreated { get; set; }

        public virtual Product Product { get; set; }

    }

}