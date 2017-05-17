using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BuyalotWebShoppingApp.Models
{
    public class Order
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
            this.Payments = new HashSet<Payment>();
        }

        [Key]
        public int OrderID { get; set; }

        [Display(Name = "Customer #")]
        public int CustomerID { get; set; }

        [Display(Name = "Order Date")]
        public System.DateTime OrderDate { get; set; }

        [Display(Name = "Shipping Date")]
        public System.DateTime ShippingDate { get; set; }

        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; }

        [Display(Name = "Order Status")]
        public string Status { get; set; }

        [Display(Name = "Total Price")]
        public decimal TotalPrice { get; set; }

        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }

    }

}