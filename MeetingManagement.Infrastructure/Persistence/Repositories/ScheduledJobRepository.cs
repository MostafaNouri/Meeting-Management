using MeetingManagement.Application.Interfaces.Repositories;
using MeetingManagement.Domain.Entities;

namespace MeetingManagement.Infrastructure.Persistence.Repositories;

public class ScheduledJobRepository(AppDbContext context) : IScheduledJobRepository
{
    public Task AddAsync(ScheduledJob job)
    {
        context.ScheduledJobs.Add(job);
        return context.SaveChangesAsync();
    }

    public Task DeleteAsync(ScheduledJob job)
    {
        context.ScheduledJobs.Remove(job);
        return context.SaveChangesAsync();
    }

    public async Task<ScheduledJob?> GetAsync(int id)
    {
        return await context.ScheduledJobs.FindAsync(id);
    }
}