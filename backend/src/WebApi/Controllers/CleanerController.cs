using Microsoft.AspNetCore.Mvc;
using PartyKlinest.ApplicationCore.Entities;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.WebApi.Models;

namespace PartyKlinest.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CleanerController : ControllerBase
    {
        private readonly ILogger<CleanerController> _logger;

        private static readonly ScheduleEntryDTO[] scheduleEntries = new[]
        {
            new ScheduleEntryDTO(DayOfWeek.Monday, "07:20", "12:15"),
            new ScheduleEntryDTO(DayOfWeek.Monday, "13:21", "21:21"),
            new ScheduleEntryDTO(DayOfWeek.Wednesday, "13:21", "21:21"),
            new ScheduleEntryDTO(DayOfWeek.Tuesday, "13:21", "21:21"),
            new ScheduleEntryDTO(DayOfWeek.Saturday, "13:21", "21:21"),
            new ScheduleEntryDTO(DayOfWeek.Sunday, "13:21", "23:59"),
        };

        private static readonly CleanerInfoDTO[] _cleaners = new[]
        {
            new CleanerInfoDTO(new[] {scheduleEntries[0], scheduleEntries[1]}, MessLevel.Low, 1, 12.0m, 5.0f),
            new CleanerInfoDTO(new[] {scheduleEntries[1], scheduleEntries[2]}, MessLevel.Moderate, 2, 13.0m, 5.0f),
            new CleanerInfoDTO(new[] {scheduleEntries[0]}, MessLevel.Low, 3, 12.50m, 5.0f),
            new CleanerInfoDTO(new[] {scheduleEntries[0], scheduleEntries[1], scheduleEntries[3], scheduleEntries[4], scheduleEntries[5],},
                MessLevel.Disaster, 4, 69.0m, 5.0f),
            new CleanerInfoDTO(new[] {scheduleEntries[5]}, MessLevel.Low, 8, 12.0m, 5.0f),
        };

        private static readonly Order[] _orders = new[]
        {
            new Order(420.69m, 3, MessLevel.Moderate, new(2019, 2, 2, 1, 23, 12, TimeSpan.Zero), "123",
                new Address("Poland", "Warsaw", "Krakowska", "1", "1", "1")),
        };

        public CleanerController(ILogger<CleanerController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Retrieves cleaner information including filters and schedule.
        /// </summary>
        /// <param name="cleanerId"></param>
        /// <returns></returns>
        [HttpGet("{cleanerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CleanerInfoDTO> GetCleanerInfo(int cleanerId)
        {
            bool isExisting = cleanerId > 0 && cleanerId < _cleaners.Length;

            if (!isExisting)
            {
                return NotFound();
            }

            _logger.LogInformation("Get cleaner info. CleanerId: {cleanerId}", cleanerId);

            return _cleaners[cleanerId - 1];
        }

        /// <summary>
        /// Updates cleaner information.
        /// </summary>
        /// <param name="cleanerId"></param>
        /// <param name="cleanerInfo"></param>
        /// <returns></returns>
        [HttpPost("{cleanerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateCleanerInfo(long cleanerId, [FromBody] CleanerInfoDTO cleanerInfo)
        {
            _logger.LogInformation("Update cleaner info. CleanerId: {cleanerId}", cleanerId);

            return Ok();
        }

        /// <summary>
        /// Gets list of <see cref="Order"/> assigned to cleaner with id <paramref name="cleanerId"/>.
        /// </summary>
        /// <param name="cleanerId"></param>
        /// <returns></returns>
        [HttpGet("{cleanerId}/Orders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Order>> GetAssignedOrders(int cleanerId)
        {
            return _orders;
        }
    }
}
