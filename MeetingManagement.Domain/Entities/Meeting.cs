namespace MeetingManagement.Domain.Entities;

public class Meeting
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Room { get; set; }
    public List<Participant> Participants { get; set; } = new();
    public string Report { get; set; }
    public bool IsCanceled { get; set; } = false;
}