using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain
{
    public class OrderDetail
    {
        [Required]
        public int ProductId { get; set; }
        public long OrderId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TaxPercentage { get; set; }
        [Required]
        public int Quantity { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
    }
}
