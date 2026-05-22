namespace MotocrossTracker.API.Application.DTOs;

public class CreateLapTimeRequest
{
    public string RiderName { get; set; } = string.Empty;
    public string TrackName { get; set; } = string.Empty;
    public TimeSpan Time { get; set; }
    public DateTime Date { get; set; }
}
