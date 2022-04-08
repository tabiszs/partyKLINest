using Microsoft.AspNetCore.Mvc;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Services;
using PartyKlinest.WebApi.Models;

namespace PartyKlinest.WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly OrderFacade _orderFacade;

        public OrdersController(ILogger<OrdersController> logger, OrderFacade orderFacade)
        {
            _logger = logger;
            _orderFacade = orderFacade;
        }

        /// <summary>
        /// Get all orders.
        /// </summary>
        /// <returns>All orders.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<List<Order>> GetOrdersAsync()
        {
            _logger.LogInformation("Getting orders");

            return await _orderFacade.ListOrdersAsync();
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AddOrderAsync([FromBody] NewOrderDTO newOrder)
        {
            _logger.LogInformation("Added new order", newOrder);

            var orderToBeCreated = new Order(newOrder.MaxPrice, newOrder.MinRating,
                newOrder.MessLevel, newOrder.Date, newOrder.ClientId, newOrder.Address);

            await _orderFacade.AddOrderAsync(orderToBeCreated);

            return Ok();
        }

        /// <summary>
        /// Get order info.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet("{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Order>> GetOrderInfoAsync(long orderId)
        {
            try
            {
                var order = await _orderFacade.GetOrderAsync(orderId);
                return order;
            }
            catch (OrderNotFoundException)
            {
                return NotFound();
            }
        }

        //[HttpPost("{orderId}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public IActionResult ModifyOrderInfo(int orderId, [FromBody]OrderDTO order)
        //{
        //    bool isExisting = orderId > 0 && orderId <= _orders.Length;

        //    if (!isExisting)
        //    {
        //        return NotFound();
        //    }

        //    return Ok();
        //}

        [HttpDelete(template: "{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOrderAsync(long orderId)
        {
            try
            {
                await _orderFacade.DeleteOrderAsync(orderId);
                return Ok();
            }
            catch (OrderNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
