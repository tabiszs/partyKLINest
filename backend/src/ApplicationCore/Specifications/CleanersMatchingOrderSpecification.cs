using Ardalis.Specification;
using PartyKlinest.ApplicationCore.Entities;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using PartyKlinest.ApplicationCore.Extensions;
using System;
using System.Linq;

namespace PartyKlinest.ApplicationCore.Specifications;

public class CleanersMatchingOrderSpecification : Specification<Cleaner>
{
    /// <summary>
    /// Specification returning matching cleaners to order.
    /// </summary>
    /// <param name="date">Date of the cleaning.</param>
    /// <param name="messLevel">Mess level stated in order.</param>
    /// <param name="maxPrice">Max price stated in order.</param>
    /// <param name="clientRating">Client can have not received any reviews. In this case use null.</param>
    public CleanersMatchingOrderSpecification(DateTimeOffset date,
        MessLevel messLevel, decimal maxPrice, double? clientRating)
    {
        var time = date.ToTimeOnly();
        double rating = clientRating ?? double.PositiveInfinity;

        Query
            .Include(o => o.ScheduleEntries)
            .Where(c =>
                c.ScheduleEntries.Any(s =>
                    s.DayOfWeek == date.DayOfWeek && s.Start <= time && s.End >= time)
                &&
                c.OrderFilter.MinPrice <= maxPrice
                &&
                c.OrderFilter.MaxMessLevel >= messLevel
                &&
                c.OrderFilter.MinClientRating <= rating
                &&
                c.Status == CleanerStatus.Active
            );
    }
}