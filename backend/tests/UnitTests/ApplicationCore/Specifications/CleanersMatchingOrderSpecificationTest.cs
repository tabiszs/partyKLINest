using System;
using System.Collections.Generic;
using System.Linq;
using PartyKlinest.ApplicationCore.Entities;
using PartyKlinest.ApplicationCore.Entities.Users.Cleaners;
using PartyKlinest.ApplicationCore.Specifications;
using Xunit;

namespace UnitTests.ApplicationCore.Specifications;


public class CleanersMatchingOrderSpecificationTest
{
    private static readonly DateTimeOffset Monday1600 = new(2022, 4, 18, 16, 00, 00, TimeSpan.Zero);
    private static readonly DateTimeOffset Monday1615 = new(2022, 4, 18, 16, 15, 00, TimeSpan.Zero);
    private static readonly DateTimeOffset Monday1830 = new(2022, 4, 18, 18, 30, 00, TimeSpan.Zero);
    private static readonly DateTimeOffset Sunday1900 = new(2022, 4, 24, 19, 00, 00, TimeSpan.Zero);

    [Fact]
    public void MatchesNumberForOrdersOnWrongDay()
    {
        var result = GetCleanersCountForSpec(Sunday1900, MessLevel.Low, 10000m, null);
        Assert.Equal(0, result);
    }

    [Fact]
    public void MatchesNumberForOrdersBetweenDates()
    {
        var result = GetCleanersCountForSpec(Monday1830, MessLevel.Low, 10000m, null);
        Assert.Equal(0, result);
    }
    
    [Fact]
    public void MatchesNumberForOrdersMondayLowRequirements()
    {
        var result = GetCleanersCountForSpec(Monday1615, MessLevel.Low, 10000m, null);
        Assert.Equal(9, result);
    }

    [Fact]
    public void MatchesNumberForOrdersOnDateEdge()
    {
        var result = GetCleanersCountForSpec(Monday1600, MessLevel.Low, 10000m, null);
        Assert.Equal(9, result);
    }

    [Theory]
    [InlineData(9, 8)]
    [InlineData(9.5, 8)]
    [InlineData(10, 9)]
    [InlineData(1, 1)]
    [InlineData(2, 3)]
    public void MatchesNumberForOrdersWithClientRating(double rating, int expectedCount)
    {
        var result = GetCleanersCountForSpec(Monday1615, MessLevel.Low, 10000m, rating);
        Assert.Equal(expectedCount, result);
    }

    [Theory]
    [InlineData(120, 7)]
    [InlineData(121, 7)]
    [InlineData(119, 3)]
    public void MatchesNumberForOrdersWithMaxPrice(decimal maxPrice, int expectedCount)
    {
        var result = GetCleanersCountForSpec(Monday1615, MessLevel.Low, maxPrice, null);
        Assert.Equal(expectedCount, result);
    }

    [Theory]
    [InlineData(MessLevel.Low, 9)]
    [InlineData(MessLevel.Moderate, 7)]
    [InlineData(MessLevel.Huge, 6)]
    [InlineData(MessLevel.Disaster, 3)]
    public void MatchesNumberForOrdersWithMessLevels(MessLevel messLevel, int expectedCount)
    {
        var result = GetCleanersCountForSpec(Monday1615, messLevel, 10000m, null);
        Assert.Equal(expectedCount, result);
    }
    
    private static int GetCleanersCountForSpec(
        DateTimeOffset date,
        MessLevel messLevel,
        decimal maxPrice,
        double? avgClientRating
        )
    {
        var spec = new CleanersMatchingOrderSpecification(
            date, messLevel,
            maxPrice, avgClientRating);

        var result = GetTestCleanersCollection()
            .AsQueryable()
            .Where(spec.WhereExpressions.FirstOrDefault()!.Filter);

        return result.Count();
    }

    private static List<Cleaner> GetTestCleanersCollection()
    {
        var cleaners = new List<Cleaner>()
        {
            new Cleaner("abc", CleanerStatus.Active, new[]
                {
                    new ScheduleEntry(new TimeOnly(16, 00), new TimeOnly(18, 00),
                        DayOfWeek.Monday),
                    new ScheduleEntry(new TimeOnly(19, 00), new TimeOnly(20, 00),
                        DayOfWeek.Monday),
                    new ScheduleEntry(new TimeOnly(16, 00), new TimeOnly(18, 00),
                        DayOfWeek.Friday),
                    new ScheduleEntry(new TimeOnly(16, 00), new TimeOnly(18, 00),
                        DayOfWeek.Saturday),
                },
                new OrderFilter(MessLevel.Low, 2, 120m)
            ),
            new Cleaner("abcd", CleanerStatus.Active, new[]
                {
                    new ScheduleEntry(new TimeOnly(16, 00), new TimeOnly(18, 00),
                        DayOfWeek.Monday),
                    new ScheduleEntry(new TimeOnly(19, 00), new TimeOnly(23, 59),
                        DayOfWeek.Monday),
                    new ScheduleEntry(new TimeOnly(00, 00), new TimeOnly(18, 00),
                        DayOfWeek.Tuesday),
                },
                new OrderFilter(MessLevel.Huge, 4, 120m)
            ),
            new Cleaner("abcde", CleanerStatus.Active, new[]
                {
                    new ScheduleEntry(new TimeOnly(16, 00), new TimeOnly(18, 00),
                        DayOfWeek.Monday),
                    new ScheduleEntry(new TimeOnly(19, 00), new TimeOnly(20, 00),
                        DayOfWeek.Monday),
                },
                new OrderFilter(MessLevel.Disaster, 5, 90m)
            ),
            new Cleaner("abcdef", CleanerStatus.Active, new[]
                {
                    new ScheduleEntry(new TimeOnly(16, 00), new TimeOnly(18, 00),
                        DayOfWeek.Monday),
                    new ScheduleEntry(new TimeOnly(19, 00), new TimeOnly(20, 00),
                        DayOfWeek.Monday),
                },
                new OrderFilter(MessLevel.Moderate, 1, 120m)
            ),
            new Cleaner("abcdefg", CleanerStatus.Active, new[]
                {
                    new ScheduleEntry(new TimeOnly(16, 00), new TimeOnly(18, 00),
                        DayOfWeek.Monday),
                    new ScheduleEntry(new TimeOnly(19, 00), new TimeOnly(20, 00),
                        DayOfWeek.Monday),
                },
                new OrderFilter(MessLevel.Low, 6, 120m)
            ),
            new Cleaner("abcdefgh", CleanerStatus.Active, new[]
                {
                    new ScheduleEntry(new TimeOnly(16, 00), new TimeOnly(18, 00),
                        DayOfWeek.Monday),
                    new ScheduleEntry(new TimeOnly(19, 00), new TimeOnly(20, 00),
                        DayOfWeek.Monday),
                },
                new OrderFilter(MessLevel.Disaster, 2, 180m)
            ),
            new Cleaner("abcdefghi", CleanerStatus.Active, new[]
                {
                    new ScheduleEntry(new TimeOnly(16, 00), new TimeOnly(18, 00),
                        DayOfWeek.Monday),
                    new ScheduleEntry(new TimeOnly(19, 00), new TimeOnly(20, 00),
                        DayOfWeek.Monday),
                },
                new OrderFilter(MessLevel.Disaster, 10, 1000m)
            ),
            new Cleaner("abcdefghij", CleanerStatus.Active, new[]
                {
                    new ScheduleEntry(new TimeOnly(16, 00), new TimeOnly(18, 00),
                        DayOfWeek.Friday),
                    new ScheduleEntry(new TimeOnly(16, 00), new TimeOnly(18, 00),
                        DayOfWeek.Saturday),
                },
                new OrderFilter(MessLevel.Moderate, 1, 200m)
            ),
            new Cleaner("abcdefghijk", CleanerStatus.Registered, new[]
                {
                    new ScheduleEntry(new TimeOnly(16, 00), new TimeOnly(18, 00),
                        DayOfWeek.Monday),
                    new ScheduleEntry(new TimeOnly(19, 00), new TimeOnly(20, 00),
                        DayOfWeek.Monday),
                },
                new OrderFilter(MessLevel.Huge, 5, 20m)
            ),
            new Cleaner("abc1421", CleanerStatus.Registered, new[]
                {
                    new ScheduleEntry(new TimeOnly(16, 00), new TimeOnly(18, 00),
                        DayOfWeek.Monday),
                    new ScheduleEntry(new TimeOnly(19, 00), new TimeOnly(20, 00),
                        DayOfWeek.Monday),
                },
                new OrderFilter(MessLevel.Huge, 3, 25m)
            ),
            new Cleaner("abc123", CleanerStatus.Banned, new[]
                {
                    new ScheduleEntry(new TimeOnly(16, 00), new TimeOnly(18, 00),
                        DayOfWeek.Monday),
                    new ScheduleEntry(new TimeOnly(19, 00), new TimeOnly(20, 00),
                        DayOfWeek.Monday),
                },
                new OrderFilter(MessLevel.Low, 2, 122m)
            ),
            new Cleaner("abc12", CleanerStatus.Banned, new[]
                {
                    new ScheduleEntry(new TimeOnly(16, 00), new TimeOnly(18, 00),
                        DayOfWeek.Friday),
                    new ScheduleEntry(new TimeOnly(16, 00), new TimeOnly(18, 00),
                        DayOfWeek.Saturday),
                },
                new OrderFilter(MessLevel.Low, 2, 121m)
            ),
        };

        return cleaners;
    }
}
