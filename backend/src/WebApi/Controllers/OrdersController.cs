using Microsoft.AspNetCore.Mvc;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Orders.Opinions;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Services;
using PartyKlinest.WebApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace PartyKlinest.WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly OrderFacade _orderFacade;
        private readonly CleanerFacade _cleanerFacade;

        public OrdersController(ILogger<OrdersController> logger, OrderFacade orderFacade, CleanerFacade cleanerFacade)
        {
            _logger = logger;
            _orderFacade = orderFacade;
            _cleanerFacade = cleanerFacade;
        }

        /// <summary>
        /// Get all orders.
        /// </summary>
        /// <returns>All orders.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = "CleanerOnly")]
        public async Task<List<Order>> GetOrdersAsync()
        {
            _logger.LogInformation("Getting orders");

            return await _orderFacade.ListOrdersAsync();
        }

        /// <summary>
        /// Get assigned orders.
        /// </summary>
        /// <returns>Orders with cleaners assigned.</returns>
        [HttpGet("Cleaner")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = "CleanerOnly")]
        public async Task<List<Order>> GetAssignedOrdersAsync()
        {
            _logger.LogInformation("Getting assigned orders");

            return await _orderFacade.ListAssignedOrdersAsync();
        }

        /// <summary>
        /// Get created orders.
        /// </summary>
        /// <returns>Orders which await for assignment.</returns>
        [HttpGet("Client")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = "CleanerOnly")]
        public async Task<List<Order>> GetCreatedOrdersAsync()
        {
            _logger.LogInformation("Getting client orders");

            return await _orderFacade.ListCreatedOrdersAsync();
        }

        // TODO: Missing token with id
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="opinion"></param>
        /// <returns></returns>
        [HttpPost("{orderId}/Rate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = "CleanerOnly")]
        public async Task<IActionResult> RateOrderAsync(long orderId, [FromBody] Opinion opinion)
        {
            _logger.LogInformation("Rating order");
            return Ok();
        }


        /// <summary>
        /// Add order.
        /// </summary>
        /// <param name="newOrder"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AddOrderAsync([FromBody] NewOrderDTO newOrder)
        {
            _logger.LogInformation("Adding new order {newOrder}", newOrder);

            var orderToBeCreated = new Order(newOrder.MaxPrice, newOrder.MinRating,
                newOrder.MessLevel, newOrder.Date, newOrder.ClientId, newOrder.Address);

            try
            {
                await _orderFacade.AddOrderAsync(orderToBeCreated);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while adding order");
                return BadRequest();
            }

            _logger.LogInformation("Added new order {newOrder}", newOrder);
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

        // TODO: missing specification
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

        [HttpGet("{orderId}/Matching")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Cleaner>>> ListCleanersMatchingOrder(long orderId)
        {
            try
            {
                var cleaners = await _cleanerFacade.ListCleanersMatchingOrderAsync(orderId);
                return cleaners;
            }
            catch (OrderNotFoundException)
            {
                return NotFound("Order with given id not found");
            }
        }

    }
}
