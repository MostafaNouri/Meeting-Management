using MeetingManagement.Domain.Entities;

namespace MeetingManagement.Domain.Interfaces;

public interface IMeetingRepository
{
    Task<bool> ScheduleMeeting(Meeting meeting);
    Task<List<Meeting>> GetMeetings();
    Task<bool> CancelMeeting(int id);
    Task<bool> AddMeetingReport(int id, string report);
}