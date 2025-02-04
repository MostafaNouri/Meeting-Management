using MeetingManagement.Domain.Entities;
using MeetingManagement.Domain.Enums;

namespace MeetingManagement.Application.Interfaces.Repositories;

public interface IMeetingRepository
{
    Task<bool> CheckHasConflict(Meeting meeting);
    Task ScheduleMeeting(Meeting meeting);
    Task<List<Meeting>> GetMeetings();
    Task UpdateMeeting(Meeting meeting);
    Task<bool> AddMeetingReport(int id, string report);
    Task<Meeting?> GetMeeting(int id);
}