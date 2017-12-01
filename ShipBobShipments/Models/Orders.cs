using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipBobShipments.Models
{
    public class Orders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; }

        public int UserID { get; set; }

        [Required()]
        [Display(Name = "Tracking ID")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Shipment Name has to be shorter than 20 characters")]
        public string TrackingId { get; set; }

        [Required()]
        [Display(Name = "Recipient Name")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Recipient Name has to be shorter than 30 characters")]
        public string RecipientName { get; set; }

        [Required()]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Required()]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required()]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Zipcode has to be 5 characters long")]
        public string Zipcode { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}