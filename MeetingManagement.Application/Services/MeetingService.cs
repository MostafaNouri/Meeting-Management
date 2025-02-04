using MeetingManagement.Application.Interfaces;
using MeetingManagement.Application.Interfaces.Repositories;
using MeetingManagement.Domain.DTOs;
using MeetingManagement.Domain.Entities;
using MeetingManagement.Domain.Enums;

namespace MeetingManagement.Application.Services;

public class MeetingService(
    IMeetingRepository meetingRepository,
    IScheduledJobRepository scheduledJobRepository,
    INotificationScheduler notificationScheduler
    )
{
    public async Task<bool> ScheduleMeeting(Meeting meeting)
    {
        // It was better to use transactions (unit of work pattern)
        if (await meetingRepository.CheckHasConflict(meeting)) return false;
        
        await meetingRepository.ScheduleMeeting(meeting);
        
        // Schedule notifications
        var notifications = meeting.Participants.Select(participant => new NotificationDto()
        {
            Message =
                $"The {meeting.Title} meeting will start at {meeting.StartTime} and end at {meeting.EndTime}. See you there!",
            RecipientName = participant.Name,
            ContactMethods = participant.ContactMethods,
        });
        var notificationTime = meeting.StartTime.AddHours(-2);
        var jobId = notificationScheduler.ScheduleNotification(notifications, notificationTime);
        var scheduledJob = new ScheduledJob
        {
            Id = meeting.Id,
            HangfireJobId = jobId,
            ScheduledTime = notificationTime
        };
        await scheduledJobRepository.AddAsync(scheduledJob);
        return true;
    }

    public async Task<List<Meeting>> GetMeetings()
    {
        return await meetingRepository.GetMeetings();
    }

    public async Task<MeetingCancellationResult> CancelMeeting(int id)
    {
        var meeting = await meetingRepository.GetMeeting(id);
        if (meeting == null) return MeetingCancellationResult.NotFound;
        
        if (meeting.StartTime <= DateTime.UtcNow)
        {
            return MeetingCancellationResult.AlreadyStarted;
        }
        
        meeting.IsCanceled = true;
        await meetingRepository.UpdateMeeting(meeting);
        
        var scheduledJob = await scheduledJobRepository.GetAsync(meeting.Id);
        if (scheduledJob?.HangfireJobId != null) notificationScheduler.CancelNotification(scheduledJob.HangfireJobId);

        return MeetingCancellationResult.Success;
    }

    public async Task<bool> AddMeetingReport(int id, string report)
    {
        return await meetingRepository.AddMeetingReport(id, report);
    }
}