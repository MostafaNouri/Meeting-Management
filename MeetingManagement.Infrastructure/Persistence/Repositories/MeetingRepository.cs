using MeetingManagement.Application.Interfaces.Repositories;
using MeetingManagement.Domain.Entities;
using MeetingManagement.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace MeetingManagement.Infrastructure.Persistence.Repositories;

public class MeetingRepository(AppDbContext context) : IMeetingRepository
{
    public Task ScheduleMeeting(Meeting meeting)
    {
        context.Meetings.Add(meeting);
        return context.SaveChangesAsync();
    }

    public async Task<bool> CheckHasConflict(Meeting meeting)
    {
        return await context.Meetings.AnyAsync(m =>
            !m.IsCanceled &&
            m.Room == meeting.Room &&
            ((meeting.StartTime >= m.StartTime && meeting.StartTime < m.EndTime) ||
             (meeting.EndTime > m.StartTime && meeting.EndTime <= m.EndTime)));
    }

    public async Task<List<Meeting>> GetMeetings() => 
        await context.Meetings.Where(m => !m.IsCanceled).Include(a => a.Participants)
            .ThenInclude(m => m.ContactMethods)
            .ToListAsync();

    public async Task UpdateMeeting(Meeting meeting)
    {
        context.Update(meeting);
        await context.SaveChangesAsync();
    }

    public async Task<Meeting?> GetMeeting(int id)
    {
        return await context.Meetings.FindAsync(id);
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