using Microsoft.AspNetCore.Mvc;
using PartyKlinest.WebApi.Models;

namespace PartyKlinest.WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;

        private static readonly OrderDTO[] _orders = new[]
        {
            new OrderDTO(1, "1", "1", OrderStatus.Active, 420.69m, 1, MessLevel.Disaster),
            new OrderDTO(2, "1", "2", OrderStatus.Closed, 1500m, 4, MessLevel.Huge),
            new OrderDTO(3, "2", "1", OrderStatus.Closed, 1501m, 6, MessLevel.Moderate),
            new OrderDTO(4, "3", "2", OrderStatus.Closed, 1502m, 9, MessLevel.Low),
        };

        public OrdersController(ILogger<OrdersController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get all orders.
        /// </summary>
        /// <returns>All orders.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IEnumerable<OrderDTO> GetOrders()
        {
            _logger.LogInformation("Getting orders");

            return _orders;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult AddOrder([FromBody]NewOrderDTO newOrder)
        {
            _logger.LogInformation("Added new order", newOrder);

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
        public ActionResult<OrderDTO> GetOrderInfo(int orderId)
        {
            bool isExisting = orderId > 0 && orderId <= _orders.Length;

            if(!isExisting)
            {
                return NotFound();
            }

            return _orders[orderId - 1];
        }

        [HttpPost("{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ModifyOrderInfo(int orderId, [FromBody]OrderDTO order)
        {
            bool isExisting = orderId > 0 && orderId <= _orders.Length;

            if (!isExisting)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete(template:"{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteOrder(int orderId)
        {
            bool isExisting = orderId > 0 && orderId <= _orders.Length;

            if (!isExisting)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
