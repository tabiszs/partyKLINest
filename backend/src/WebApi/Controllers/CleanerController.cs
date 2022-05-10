using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartyKlinest.ApplicationCore.Entities.Orders;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Services;
using PartyKlinest.WebApi.Extensions;
using PartyKlinest.WebApi.Models;

namespace PartyKlinest.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CleanerController : ControllerBase
    {
        private readonly ILogger<CleanerController> _logger;
        private readonly CleanerFacade _cleanerFacade;

        public CleanerController(ILogger<CleanerController> logger, CleanerFacade cleanerFacade)
        {
            _logger = logger;
            _cleanerFacade = cleanerFacade;
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
        [Authorize(Policy = "CleanerOnly")]
        public async Task<ActionResult<CleanerInfoDTO>> GetCleanerInfo(string cleanerId)
        {
            _logger.LogInformation("Get cleaner info. CleanerId: {cleanerId}", cleanerId);
            try
            {
                var cleaner = await _cleanerFacade.GetCleanerInfo(cleanerId);
                List<ScheduleEntryDTO> schedulesDTO = new();
                foreach (var entry in cleaner.ScheduleEntries)
                {
                    schedulesDTO.Add(
                        new ScheduleEntryDTO(
                            entry.DayOfWeek,
                            entry.Start.ToShortTimeString(),
                            entry.End.ToShortTimeString()));
                }

                var cleanerDTO = new CleanerInfoDTO(
                    schedulesDTO.ToArray(),
                    cleaner.OrderFilter.MaxMessLevel,
                    cleaner.OrderFilter.MinClientRating,
                    cleaner.OrderFilter.MinPrice,
                    0,
                    cleaner.Status);
                return cleanerDTO;
            }
            catch (CleanerNotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "Not able to get cleaner {cleanerId} info", cleanerId);
                return BadRequest();
            }
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
        [Authorize(Policy = "CleanerOnly")]
        public async Task<IActionResult> UpdateCleanerInfo(string cleanerId, [FromBody] CleanerInfoDTO cleanerInfo)
        {
            _logger.LogInformation("Update cleaner info. CleanerId: {cleanerId}", cleanerId);
            try
            {
                List<ScheduleEntry> scheduleEntries = new();
                foreach (var entry in cleanerInfo.ScheduleEntries)
                {
                    var ts = TimeOnly.Parse(entry.Start);
                    var te = TimeOnly.Parse(entry.End);
                    scheduleEntries.Add(new ScheduleEntry(ts, te, entry.DayOfWeek));
                }
                var filter = new OrderFilter(cleanerInfo.MaxMess, cleanerInfo.MinClientRating, cleanerInfo.MinPrice);
                var status = cleanerInfo.Status;
                var cleaner = new Cleaner(cleanerId, status, scheduleEntries, filter);
                await _cleanerFacade.UpdateCleanerAsync(cleaner);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "Invalid update cleaner info for {cleanerId}", cleanerId);
                return BadRequest();
            }

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
        [Authorize(Policy = "CleanerOnly")]
        public async Task<ActionResult<IEnumerable<Order>>> GetAssignedOrders(string cleanerId)
        {
            _logger.LogInformation("Update cleaner info. CleanerId: {cleanerId}", cleanerId);
            try
            {
                return await _cleanerFacade.GetAssignedOrdersAsync(cleanerId);
            }
            catch (CleanerNotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "Getting assigned orders to {cleanerId} failed", cleanerId);
                return BadRequest();
            }
        }
    }
}