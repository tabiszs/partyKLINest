using Microsoft.AspNetCore.Mvc;
using PartyKlinest.WebApi.Models;

namespace PartyKlinest.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Rate client/cleaner by opposite side (in connection to execution of an order)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="addRating"></param>
        /// <returns></returns>
        [HttpPost("{id}/Rate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Rate(int id, [FromBody] AddRatingDTO addRating)
        {
            return Ok();
        }

        /// <summary>
        /// Ban client/cleaner.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/Ban")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Ban(int id)
        {
            return Ok();
        }
    }
}
