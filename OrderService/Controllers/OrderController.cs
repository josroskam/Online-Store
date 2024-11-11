using Microsoft.AspNetCore.Mvc;
using OrderService.Interfaces;
using OrderService.Models;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            var createdOrder = await _orderService.CreateOrderAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = new List<Order>()
            {
                new Order { Id = Guid.NewGuid(), ProductName = "Product 1", Quantity = 1, Price = 10, Status = "Pending" },
            };
            return Ok(orders);
        }
    }
}
