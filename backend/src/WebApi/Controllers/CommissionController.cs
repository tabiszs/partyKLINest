using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartyKlinest.ApplicationCore.Interfaces;
using PartyKlinest.WebApi.Extensions;
using PartyKlinest.WebApi.Models;


namespace PartyKlinest.WebApi.Controllers
{
    [ApiController]
    [Route("api/Configuration/[controller]")]
    public class CommissionController : ControllerBase
    {
        private readonly ILogger<CommissionController> _logger;
        private readonly ICommissionService _commissionService;

        public CommissionController(ILogger<CommissionController> logger, ICommissionService commissionService)
        {
            _logger = logger;
            _commissionService = commissionService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<SetCommissionDTO>> GetCommissionAsync()
        {
            if (User.IsAdmin())
            {
                try
                {
                    decimal commissionValue = await _commissionService.GetCommissionAsync();
                    return new SetCommissionDTO(commissionValue);
                }
                catch (Exception e)
                {
                    _logger.LogWarning(e, "Get commission failed");
                    return BadRequest();
                }
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> SetCommissionAsync([FromBody] SetCommissionDTO setCommission)
        {
            if (User.IsAdmin())
            {
                try
                {
                    await _commissionService.SetCommissionAsync(setCommission.NewProvision);
                    return Ok();
                }
                catch (Exception e)
                {
                    _logger.LogWarning(e, "Get commission failed");
                    return BadRequest();
                }
            }
            else
            {
                return Forbid();
            }
        }
    }
}
