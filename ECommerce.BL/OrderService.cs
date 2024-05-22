using ECommerce.BL.Abstracts;
using ECommerce.DL.Abstracts;
using ECommerce.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BL
{
    public class OrderService : IOrderService
    {
        private readonly IOrderDL _orderDL;
        public OrderService(IOrderDL orderDL)
        {
            _orderDL = orderDL;
        }
        public async Task<Guid> Create(Order order)
        {
            ArgumentNullException.ThrowIfNull(order);
            order.Identifier = Guid.NewGuid();
            decimal gross = 0, net = 0, tax = 0;
            foreach (var item in order.LineItems)
            {
                item.GrossAmount = item.UnitPrice * item.Quantity;
                var itemTax = item.GrossAmount * (item.TaxPercentage / 100);
                item.NetAmount = item.GrossAmount + itemTax;
                gross += item.GrossAmount;
                net += item.NetAmount;
                tax += itemTax;
            }
            order.GrossAmount = gross;
            order.NetAmount = net;
            order.TotalTax = tax;
            await _orderDL.Create(order);
            return order.Identifier;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page">page starts from 1</param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public Task<IEnumerable<Order>> Get(int page, int pageSize)
        {
            return _orderDL.Get(page, pageSize);
        }

        public Task<Order> Get(Guid identifier)
        {
            var order = _orderDL.Get(identifier);
            if (order == null)
            {
                throw new ArgumentNullException("Order not found for the provided identifier");
            }
            return order;
        }
    }
}
