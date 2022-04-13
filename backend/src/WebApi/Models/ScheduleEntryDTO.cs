using System.ComponentModel.DataAnnotations;

namespace PartyKlinest.WebApi.Models
{
    /// <summary>
    /// Single entry of the Schedule.
    /// </summary>
    public record ScheduleEntryDTO
    {
        public ScheduleEntryDTO(DayOfWeek dayOfWeek,
            string start, string end)
        {
            DayOfWeek = dayOfWeek;
            Start = start;
            End = end;
        }

        private const string _timeRegex = "([01][0-9]|2[0-3]):([0-5][0-9])";

        /// <summary>
        /// Day of the week for entry.
        /// </summary>
        public DayOfWeek DayOfWeek { get; init; }

        /// <summary>
        /// Time in format HH:MM from which availability starts.
        /// </summary>
        /// <example>
        /// 12:41
        /// </example>
        [Required]
        [RegularExpression(_timeRegex)]
        public string Start { get; init; }

        /// <summary>
        /// Time in format HH:MM from which availability ends.
        /// </summary>
        /// <example>
        /// 23:00
        /// </example>
        [Required]
        [RegularExpression(_timeRegex)]
        public string End { get; init; }
    }
}
