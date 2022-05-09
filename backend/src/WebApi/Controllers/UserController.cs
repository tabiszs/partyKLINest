using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;

namespace PartyKlinest.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly GraphServiceClient _graphClient;
        private readonly ILogger<UserController> _logger;

        public UserController(GraphServiceClient graphClient, ILogger<UserController> logger)
        {
            _graphClient = graphClient;
            _logger = logger;
        }

        /// <summary>
        /// Ban client/cleaner.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("{userId}/Ban")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Ban(string userId)
        {
            _logger.LogInformation("Ban a user");
            var bannedUser = new User();
            bannedUser.AdditionalData = new Dictionary<string, object> { { "extension_isBanned", true } };
            await _graphClient.Users[userId].Request().UpdateAsync(bannedUser);
            return Ok();
        }
    }
}
