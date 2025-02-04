using MeetingManagement.Domain.Entities;

namespace MeetingManagement.Application.Interfaces.Repositories;

public interface IScheduledJobRepository
{
    Task AddAsync(ScheduledJob job);
    Task DeleteAsync(ScheduledJob job);
    Task<ScheduledJob?> GetAsync(int id);
}