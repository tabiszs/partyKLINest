using System;
using System.Linq;
using Ardalis.Specification;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using PartyKlinest.ApplicationCore.Extensions;

namespace PartyKlinest.ApplicationCore.Specifications;

public class CleanersMatchingOrderSpecification : Specification<Cleaner>
{
    public CleanersMatchingOrderSpecification(DateTimeOffset date)
    {
        Date = date;
        var time = date.ToTimeOnly();
        Query
            .Include(o => o.ScheduleEntries)
            .Where(c => 
                c.ScheduleEntries.Any(s => 
                    s.DayOfWeek == date.DayOfWeek && s.Start >= time && s.End <= time)
            );
    }
    
    public DateTimeOffset Date { get; }
}