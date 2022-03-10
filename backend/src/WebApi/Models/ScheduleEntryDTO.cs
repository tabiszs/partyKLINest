namespace PartyKlinest.WebApi.Models
{
    /// <summary>
    /// Single entry of the Schedule.
    /// </summary>
    /// <param name="DayOfWeek"></param>
    /// <param name="Start">Time in format HH:MM from which availability starts.</param>
    /// <param name="End">Time in format HH:MM from which availability ends.</param>
    public record ScheduleEntryDTO(
        DayOfWeek DayOfWeek,
        string Start,
        string End
        );
}
