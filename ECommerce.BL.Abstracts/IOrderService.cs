using ECommerce.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BL.Abstracts
{
    public interface IOrderService
    {
        Task<Guid> Create(Order order);
        Task<IEnumerable<Order>> Get(int page, int pageSize);
        Task<Order> Get(Guid identifier);
    }
}
