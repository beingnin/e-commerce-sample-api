using ECommerce.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DL.Abstracts
{
    public interface IOrderDL
    {
        Task Create(Order order);
        Task<IEnumerable<Order>> Get(int page, int pageSize);
        Task<Order> Get(Guid identifier);
    }
}
