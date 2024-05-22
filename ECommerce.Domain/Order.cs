using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain
{
    public class Order
    {
        public long OrderId { get; set; }
        public Guid Identifier { get; set; }
        [Required, MinLength(1)]
        public IEnumerable<OrderDetail> LineItems { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal NetAmount { get; set; }
    }
}
