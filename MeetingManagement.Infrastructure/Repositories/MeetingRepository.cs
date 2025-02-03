using MeetingManagement.Domain.Entities;
using MeetingManagement.Domain.Enums;
using MeetingManagement.Domain.Interfaces;
using MeetingManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeetingManagement.Infrastructure.Repositories;

public class MeetingRepository(AppDbContext context) : IMeetingRepository
{
    public async Task<bool> ScheduleMeeting(Meeting meeting)
    {
        var conflictExists = await context.Meetings.AnyAsync(m =>
            !m.IsCanceled &&
            m.Room == meeting.Room &&
            ((meeting.StartTime >= m.StartTime && meeting.StartTime < m.EndTime) ||
             (meeting.EndTime > m.StartTime && meeting.EndTime <= m.EndTime)));

        if (conflictExists) return false;

        context.Meetings.Add(meeting);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<List<Meeting>> GetMeetings() => 
        await context.Meetings.Where(m => !m.IsCanceled)
            .Include(a => a.Participants)
            .ToListAsync();

    public async Task<MeetingCancellationResult> CancelMeeting(int id)
    {
        var meeting = await context.Meetings.FindAsync(id);
        if (meeting == null) return MeetingCancellationResult.NotFound;

        if (meeting.StartTime <= DateTime.UtcNow)
        {
            return MeetingCancellationResult.AlreadyStarted;
        }

        meeting.IsCanceled = true;
        await context.SaveChangesAsync();
        return MeetingCancellationResult.Success;
    }

    public async Task<bool> AddMeetingReport(int id, string report)
    {
        var meeting = await context.Meetings.FindAsync(id);
        if (meeting == null) return false;

        meeting.Report = report;
        await context.SaveChangesAsync();
        return true;
    }
}