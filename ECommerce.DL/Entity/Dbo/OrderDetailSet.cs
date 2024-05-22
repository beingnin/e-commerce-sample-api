using ECommerce.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DL.Entity.Dbo
{
    [Table("tbl_order_details", Schema = "dbo")]
    public class OrderDetailSet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderDetailId { get; set; }
        public long OrderId { get; set; }
        public OrderSet Order { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TaxPercentage { get; set; }
        public int Quantity { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }

    }
}
