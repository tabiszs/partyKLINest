using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PartyKlinest.ApplicationCore.Exceptions;
using PartyKlinest.ApplicationCore.Services;

namespace PartyKlinest.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly ClientFacade _clientFacade;

        public ClientController(ILogger<ClientController> logger, ClientFacade clientFacade)
        {
            _logger = logger;
            _clientFacade = clientFacade;
        }

        /// <summary>
        /// Deletes account from the system.
        /// </summary>
        /// <param name="clientId">Client's id.</param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="403">Not Authorized</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> DeleteClient(string clientId)
        {
            _logger.LogInformation($"Delete client {clientId}");
            try
            {
                await _clientFacade.DeleteClientAsync(clientId);
                return Ok();
            }
            catch (ClientNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Delete client {clientId} failed", clientId);
                return BadRequest();
            }
        }
    }
}
