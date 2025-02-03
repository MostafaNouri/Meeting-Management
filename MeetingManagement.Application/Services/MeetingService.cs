using MeetingManagement.Domain.Entities;
using MeetingManagement.Domain.Enums;
using MeetingManagement.Domain.Interfaces;

namespace MeetingManagement.Application.Services;

public class MeetingService
{
    private readonly IMeetingRepository _meetingRepository;

    public MeetingService(IMeetingRepository meetingRepository)
    {
        _meetingRepository = meetingRepository;
    }

    public async Task<bool> ScheduleMeeting(Meeting meeting)
    {
        return await _meetingRepository.ScheduleMeeting(meeting);
    }

    public async Task<List<Meeting>> GetMeetings()
    {
        return await _meetingRepository.GetMeetings();
    }

    public async Task<MeetingCancellationResult> CancelMeeting(int id)
    {
        return await _meetingRepository.CancelMeeting(id);
    }

    public async Task<bool> AddMeetingReport(int id, string report)
    {
        return await _meetingRepository.AddMeetingReport(id, report);
    }
}