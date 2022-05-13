using PartyKlinest.ApplicationCore.Extensions;
using System;
using Xunit;

namespace UnitTests.ApplicationCore.Extensions.DateTimeOffsetExtensionTest;

public class ToTimeOnly
{
    [Fact]
    public void GetsCorrectTime()
    {
        var dateTimeOffset = new DateTimeOffset(2022, 2, 22, 16, 22, 15, TimeSpan.Zero);
        var expected = new TimeOnly(16, 22, 15);

        var actual = dateTimeOffset.ToTimeOnly();

        Assert.Equal(expected, actual);
    }
}