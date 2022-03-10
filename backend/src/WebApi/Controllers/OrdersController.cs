using Microsoft.AspNetCore.Mvc;
using PartyKlinest.WebApi.Models;

namespace PartyKlinest.WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;

        private static readonly OrderDTO[] orders = new[]
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
        public IEnumerable<OrderDTO> GetOrders()
        {
            _logger.LogInformation("Getting orders");

            return orders;
        }

        [HttpPost]
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
        public ActionResult<OrderDTO> GetOrderInfo(int orderId)
        {
            bool isExisting = orderId > 0 && orderId <= orders.Length;

            if(!isExisting)
            {
                return NotFound();
            }

            return orders[orderId - 1];
        }

        [HttpPost("{orderId}")]
        public IActionResult ModifyOrderInfo(int orderId, [FromBody]OrderDTO order)
        {
            bool isExisting = orderId > 0 && orderId <= orders.Length;

            if (!isExisting)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
