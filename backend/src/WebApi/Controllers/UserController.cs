using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using PartyKlinest.Infrastructure;
using PartyKlinest.WebApi.Extensions;
using PartyKlinest.WebApi.Models;

namespace PartyKlinest.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly GraphServiceClient _graphClient;
        private readonly ILogger<UserController> _logger;
        private readonly ExtensionPropertyNameBuilder _nameBuilder;

        public UserController(GraphServiceClient graphClient, ILogger<UserController> logger, ExtensionPropertyNameBuilder nameBuilder)
        {
            _graphClient = graphClient;
            _logger = logger;
            _nameBuilder = nameBuilder;
        }

        /// <summary>
        /// Get users.
        /// </summary>
        /// <returns>List of registered users.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IEnumerable<UserInfoDTO>> GetUsers()
        {
            _logger.LogInformation("Get all users");
            var users = await _graphClient.Users
                .Request()
                .Select($"id,givenName,surname,identities,{_nameBuilder.GetExtensionName("AccountType")},{_nameBuilder.GetExtensionName("isBanned")}")
                .GetAsync();
            return from user in users
                   select new UserInfoDTO
                   {
                       Oid = user.Id,
                       Name = user.GivenName,
                       Surname = user.Surname,
                       Email = user.GetEmail(),
                       AccountType = user.GetUserTypeFromProperty(_nameBuilder.GetExtensionName("AccountType")),
                       IsBanned = user.GetIsBannedFromProperty(_nameBuilder.GetExtensionName("isBanned"))
                   };
        }

        /// <summary>
        /// Delete your own account.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            bool isDeletingOwnAccount = userId == User.GetOid();

            if (!isDeletingOwnAccount)
            {
                _logger.LogWarning("User {userId} is trying to delete another user {userId}", User.GetOid(), userId);
                return Forbid();
            }

            _logger.LogInformation("Delete user {userId}", userId);
            await _graphClient.Users[userId].Request().DeleteAsync();
            return Ok();
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
            bannedUser.AdditionalData = new Dictionary<string, object> { { _nameBuilder.GetExtensionName("isBanned"), true } };
            await _graphClient.Users[userId].Request().UpdateAsync(bannedUser);
            return Ok();
        }
    }
}
