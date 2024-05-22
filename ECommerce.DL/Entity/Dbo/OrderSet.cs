using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DL.Entity.Dbo
{
    [Table("tbl_orders", Schema = "dbo")]
    public class OrderSet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderId { get; set; }
        public Guid Identifier { get; set; }
        public IEnumerable<OrderDetailSet> LineItems { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal NetAmount { get; set; }

    }
}
