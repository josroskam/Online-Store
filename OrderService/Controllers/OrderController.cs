using Microsoft.AspNetCore.Mvc;
using OrderService.Services;
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
        public async Task<IActionResult> CreateOrder([FromBody] Order order) =>
            CreatedAtAction(nameof(GetOrderById), new { id = (await _orderService.CreateOrderAsync(order)).Id }, order);

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id) =>
            Ok(await _orderService.GetOrderByIdAsync(id));

        [HttpGet]
        public async Task<IActionResult> GetAllOrders() =>
            Ok(await _orderService.GetAllOrdersAsync());

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] int page = 1, [FromQuery] int pageSize = 200)
        {
            return (IActionResult)await _orderService.GetOrdersAsync(page, pageSize);
        }
    }
}