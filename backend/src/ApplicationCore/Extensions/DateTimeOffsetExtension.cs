using System;

namespace PartyKlinest.ApplicationCore.Extensions;

public static class DateTimeOffsetExtension
{
    public static TimeOnly ToTimeOnly(this DateTimeOffset date)
    {
        return new TimeOnly(date.Hour, date.Minute, date.Second, date.Millisecond);
    }
}