namespace MeetingManagement.Domain.Entities;

public class ScheduledJob
{
    public int Id { get; set; }
    public string HangfireJobId { get; set; }
    public DateTime ScheduledTime { get; set; }
}