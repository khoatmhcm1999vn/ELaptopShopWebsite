using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProjectShopLaptop.Models
{
    [Table("Order")]
    public class Order
    {
        public int Id { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        [StringLength(50)]
        public string ShipName { get; set; }

        [StringLength(50)]
        public string ShipMobile { get; set; }

        [StringLength(50)]
        public string ShipAddress { get; set; }

        [StringLength(50)]
        public string ShipEmail { get; set; }

        public int? Status { get; set; }
    }
}