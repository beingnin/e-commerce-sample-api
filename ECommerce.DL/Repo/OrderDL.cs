using ECommerce.DL.Abstracts;
using ECommerce.DL.Entity.Dbo;
using ECommerce.DL.Mappers;
using ECommerce.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DL.Repo
{
    public class OrderDL : IOrderDL
    {
        private readonly ECommerceContext _dbContext;
        public OrderDL(ECommerceContext eCommerceContext)
        {
            _dbContext = eCommerceContext;
        }
        public async Task Create(Order order)
        {
            OrderSet entity = order.ToOrderSet();
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Order>> Get(int page, int pageSize)
        {
            return await _dbContext.Orders.Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(order => order.FromOrderSet())
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Order> Get(Guid identifier)
        {
            return _dbContext.Orders.Include(order => order.LineItems)
                .Select(order => order.FromOrderSet())
                .AsNoTracking()
                .FirstOrDefaultAsync(order => order.Identifier == identifier);
        }
    }
}
