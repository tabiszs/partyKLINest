using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Orders.Opinions;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Services;
using PartyKlinest.WebApi.Extensions;
using PartyKlinest.WebApi.Mapper;
using PartyKlinest.WebApi.Models;

namespace PartyKlinest.WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly OrderFacade _orderFacade;
        private readonly CleanerFacade _cleanerFacade;
        private readonly IMapper _mapper;

        public OrdersController(ILogger<OrdersController> logger, OrderFacade orderFacade, CleanerFacade cleanerFacade, IMapper mapper)
        {
            _logger = logger;
            _orderFacade = orderFacade;
            _cleanerFacade = cleanerFacade;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all orders.
        /// </summary>
        /// <returns>All orders.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize]
        public async Task<List<OrderDTO>> GetOrdersAsync()
        {
            _logger.LogInformation("Getting orders");
            var orders = await _orderFacade.ListOrdersAsync();

            return _mapper.Map<List<OrderDTO>>(orders);
        }

        /// <summary>
        /// Get orders with assigned for cleaner calling this endpoint.
        /// </summary>
        /// <returns>Orders assigned to cleaner.</returns>
        [HttpGet("Cleaner")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = "CleanerOnly")]
        public async Task<List<OrderDTO>> GetAssignedOrdersAsync()
        {
            var cleanerId = User.GetOid();
            _logger.LogInformation("Getting assigned orders for cleaner {cleanerId}", cleanerId);
            var orders = await _orderFacade.ListAssignedOrdersToAsync(cleanerId);

            return _mapper.Map<List<OrderDTO>>(orders);
        }

        /// <summary>
        /// Get orders created by client calling this endpoint.
        /// </summary>
        /// <returns>Orders created by client calling this endpoint</returns>
        [HttpGet("Client")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = "ClientOnly")]
        public async Task<List<OrderDTO>> GetCreatedOrdersAsync()
        {
            var clientId = User.GetOid();
            _logger.LogInformation("Getting orders for client {clientId}", clientId);
            var orders = await _orderFacade.ListCreatedOrdersByAsync(clientId);

            return _mapper.Map<List<OrderDTO>>(orders);
        }

        /// <summary>
        /// Rate order.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="opinionDto"></param>
        /// <returns></returns>
        [HttpPost("{orderId:long}/Rate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = "ClientOrCleaner")]
        public async Task<IActionResult> RateOrderAsync(long orderId, [FromBody] OpinionDTO opinionDto)
        {
            var opinion = _mapper.Map<Opinion>(opinionDto);

            if (User.IsCleaner())
            {
                _logger.LogInformation("Rating order {orderId}. Cleaner's opinion {opinion}", orderId, opinion);
                try
                {
                    await _orderFacade.SubmitOpinionCleanerAsync(orderId, opinion);
                }
                catch (OrderNotFoundException)
                {
                    return NotFound();
                }
                catch (Exception e)
                {
                    _logger.LogWarning(e, "Rate order {orderId}", orderId);
                    return BadRequest();
                }
            }
            else if (User.IsClient())
            {
                _logger.LogInformation("Rating order {orderId}. Client's opinion {opinion}", orderId, opinion);
                try
                {
                    await _orderFacade.SubmitOpinionClientAsync(orderId, opinion);
                }
                catch (OrderNotFoundException)
                {
                    return NotFound();
                }
                catch (Exception e)
                {
                    _logger.LogWarning(e, "Rate order {orderId}", orderId);
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = "ClientOnly")]
        public async Task<IActionResult> AddOrderAsync([FromBody] NewOrderDTO newOrder)
        {
            _logger.LogInformation("Adding new order {newOrder}", newOrder);

            var address = _mapper.Map<Address>(newOrder.Address);

            var orderToBeCreated = new Order(newOrder.MaxPrice, newOrder.MinRating,
                newOrder.MessLevel, newOrder.Date, newOrder.ClientId, address);

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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<ActionResult<OrderDTO>> GetOrderInfoAsync(long orderId)
        {
            try
            {
                var order = await _orderFacade.GetOrderAsync(orderId);
                return _mapper.Map<OrderDTO>(order);
            }
            catch (OrderNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Modify order.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderDTO"></param>
        /// <returns></returns>
        [HttpPost("{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> ModifyOrderInfoAsync(long orderId, [FromBody] OrderDTO orderDTO)
        {
            try
            {
                var order = _mapper.Map<Order>(orderDTO);
                await _orderFacade.ModifyOrderAsync(orderId, order.ClientId, order.CleanerId,
                    order.Status, order.MaxPrice, order.MinCleanerRating, order.Date,
                    order.Address, order.MessLevel);
            }
            catch (OrderNotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Modify order info {orderId} exception", orderId);
                return BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// Delete order from database.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpDelete(template: "{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
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

        /// <summary>
        /// Get list of cleaners that match the criteria for the order.
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet("{orderId}/Matching")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<ActionResult<List<CleanerInfoDTO>>> ListCleanersMatchingOrder(long orderId)
        {
            try
            {
                var cleaners = await _cleanerFacade.ListCleanersMatchingOrderAsync(orderId);

                return CleanerMapper.GetCleanerInfoDTOs(cleaners);
            }
            catch (OrderNotFoundException)
            {
                return NotFound("Order with given id not found");
            }
        }
    }
}
