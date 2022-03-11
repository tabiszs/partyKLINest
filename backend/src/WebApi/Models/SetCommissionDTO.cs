using System.ComponentModel.DataAnnotations;

namespace PartyKlinest.WebApi.Models
{
    public record SetCommissionDTO
    {
        public SetCommissionDTO(decimal newProvision)
        {
            NewProvision = newProvision;
        }

        /// <summary>
        /// New commission in fraction.
        /// </summary>
        [Range(0, 1, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public decimal NewProvision { get; init; }
    }
}
