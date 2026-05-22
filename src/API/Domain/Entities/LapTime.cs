namespace MotocrossTracker.API.Domain.Entities;

public class LapTime
{
    public int Id { get; set; }
    public string RiderName { get; set; } = string.Empty;
    public string TrackName { get; set; } = string.Empty;
    public TimeSpan Time { get; set; }
    public DateTime Date { get; set; }
    public string UserId { get; set; } = string.Empty;
}
