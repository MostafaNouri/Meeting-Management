using MeetingManagement.Domain.Entities;
using MeetingManagement.Domain.Enums;

namespace MeetingManagement.Domain.Interfaces;

public interface IMeetingRepository
{
    Task<bool> ScheduleMeeting(Meeting meeting);
    Task<List<Meeting>> GetMeetings();
    Task<MeetingCancellationResult> CancelMeeting(int id);
    Task<bool> AddMeetingReport(int id, string report);
}