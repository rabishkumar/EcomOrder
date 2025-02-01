using Microsoft.AspNetCore.Mvc;
using OrderService.Application.UseCases;
using OrderService.Application.Dtos;
namespace OrderService.API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly CreateOrderUseCase _createOrderUseCase;

        public OrdersController(CreateOrderUseCase createOrderUseCase)
        {
            _createOrderUseCase = createOrderUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            await _createOrderUseCase.ExecuteAsync(request.ProductName, request.Quantity);
            return Ok(new { Message = "Order created and event published." });
        }
    }

    
}
