using AppliRabbit1.DTO;
using AppliRabbit1.Producer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppliRabbit1.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class OrdersController : ControllerBase
    {
        private readonly IMessageProducer _messagePublisher;

        public OrdersController( IMessageProducer messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto)
        {
            //Order order = new()
            //{
            //    ProductName = orderDto.ProductName,
            //    Price = orderDto.Price,
            //    Quantity = orderDto.Quantity
            //};
            ////_context.Order.Add(order);
            // await _context.SaveChangesAsync();

            _messagePublisher.SendMessage(orderDto);

            return Ok();

        }
    }
    
}
