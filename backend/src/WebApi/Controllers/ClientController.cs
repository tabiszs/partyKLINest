using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace PartyKlinest.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;

        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Deletes account from the system.
        /// </summary>
        /// <param name="id">Client's id.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="403">Not Authorized</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = "ClientOnly")]
        public IActionResult DeleteClient(int id)
        {
            _logger.LogInformation($"Delete client {id}");
            return Ok();
        }
    }
}
