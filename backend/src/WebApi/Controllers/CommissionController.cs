using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PartyKlinest.WebApi.Models;

namespace PartyKlinest.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommissionController : ControllerBase
    {
        private readonly ILogger<CommissionController> _logger;

        public CommissionController(ILogger<CommissionController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult SetCommission([FromBody]SetCommissionDTO setCommission)
        {
            return Ok();
        }
    }
}
