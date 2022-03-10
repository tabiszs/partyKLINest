namespace PartyKlinest.WebApi.Models
{
    /// <summary>
    /// Cleaner information neccessary to match orders.
    /// </summary>
    /// <param name="ScheduleEntries"></param>
    /// <param name="MaxMess"></param>
    /// <param name="MinClientRating"></param>
    /// <param name="MinPrice"></param>
    /// <param name="MaxLocationRange"></param>
    public record CleanerInfoDTO(
        ScheduleEntryDTO[] ScheduleEntries,
        MessLevel MaxMess,
        int MinClientRating,
        decimal MinPrice,
        float MaxLocationRange
        );
}
