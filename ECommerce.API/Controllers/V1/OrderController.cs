using ECommerce.BL.Abstracts;
using ECommerce.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers.V1
{
    [Route("api/v1/order")]
    [Authorize]
    public class OrderController : BaseAPIController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("create")]
        public async Task<Guid> CreateOrder(Order order)
        {
            return await _orderService.Create(order);
        }
        [HttpGet("get")]
        public async Task<IEnumerable<Order>> Get(int page, int pageSize)
        {
            return await _orderService.Get(page, pageSize);
        }
        [HttpGet("get-by-id")]
        public async Task<Order> Get(Guid identifier)
        {
            return await _orderService.Get(identifier);
        }
    }
}
