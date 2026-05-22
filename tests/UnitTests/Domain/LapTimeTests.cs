using MotocrossTracker.API.Domain.Entities;

namespace MotocrossTracker.UnitTests.Domain;

public class LapTimeTests
{
    [Fact]
    public void LapTime_WithValidData_ShouldCreateSuccessfully()
    {
        var lapTime = new LapTime
        {
            RiderName = "Jorge Prado",
            TrackName = "MXGP Agueda",
            Time = TimeSpan.FromMinutes(2).Add(TimeSpan.FromSeconds(3.45)),
            Date = DateTime.UtcNow,
            UserId = "user-123"
        };

        Assert.Equal("Jorge Prado", lapTime.RiderName);
        Assert.Equal("MXGP Agueda", lapTime.TrackName);
        Assert.Equal("user-123", lapTime.UserId);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void LapTime_WithEmptyRiderName_ShouldBeInvalid(string? riderName)
    {
        var lapTime = new LapTime { RiderName = riderName! };
        Assert.True(string.IsNullOrWhiteSpace(lapTime.RiderName));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void LapTime_WithEmptyTrackName_ShouldBeInvalid(string? trackName)
    {
        var lapTime = new LapTime { TrackName = trackName! };
        Assert.True(string.IsNullOrWhiteSpace(lapTime.TrackName));
    }

    [Fact]
    public void LapTime_WithNegativeTime_ShouldBeInvalid()
    {
        var lapTime = new LapTime { Time = TimeSpan.FromSeconds(-1) };
        Assert.True(lapTime.Time < TimeSpan.Zero);
    }
}
