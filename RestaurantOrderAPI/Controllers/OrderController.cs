using Microsoft.AspNetCore.Mvc;
using RestaurantOrderAPI.DTOs;
using RestaurantOrderAPI.Services;

namespace RestaurantOrderAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _service;

        public OrderController(OrderService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderCreateDto dto)
        {
            var order = await _service.CreateAsync(dto);

            return Ok(new
            {
                message = "Pedido criado com sucesso",
                data = order
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _service.GetAllAsync();
            return Ok(orders);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(string id, UpdateOrderStatusDto dto)
        {
            try
            {
                var order = await _service.UpdateStatusAsync(id, dto);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
