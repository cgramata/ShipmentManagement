using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipBobShipments.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Required()]
        [Display(Name = "User First Name:")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "First name has to be shorter than 10 characters")]
        public string UserFirstName { get; set; }

        [Required()]
        [Display(Name = "User Last Name:")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Last name has to be shorter than 15 characters")]
        public string UserLastName { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}