using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Services;
using PartyKlinest.WebApi.Mapper;
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

                var cleanerDTO = CleanerMapper.GetCleanerInfoDTO(cleaner);

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
                var cleaner = CleanerMapper.GetCleaner(cleanerId, cleanerInfo);
                await _cleanerFacade.UpdateCleanerAsync(cleaner);
            }
            catch (Exception e)
            {
                _logger.LogWarning(e, "Invalid update cleaner info for {cleanerId}", cleanerId);
                return BadRequest();
            }

            return Ok();
        }
    }
}