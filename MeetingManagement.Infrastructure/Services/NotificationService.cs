using MeetingManagement.Domain.Interfaces;
using MeetingManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MeetingManagement.Infrastructure.Services;

public class NotificationService(IServiceScopeFactory scopeFactory, INotificationService notificationService)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var upcomingMeetings = await context.Meetings
                .Where(m => !m.IsCanceled && 
                            m.StartTime > DateTime.UtcNow && 
                            m.StartTime <= DateTime.UtcNow.AddHours(2))
                .Include(m => m.Participants)
                .ToListAsync(stoppingToken);

            foreach (var meeting in upcomingMeetings)
            {
                foreach (var participant in meeting.Participants)
                {
                    await notificationService.SendEmailAsync(participant.Email, 
                        $"Reminder: Meeting '{meeting.Title}'", 
                        $"Your meeting '{meeting.Title}' is scheduled at {meeting.StartTime}.");
                        
                    await notificationService.SendSmsAsync(participant.PhoneNumber, 
                        $"Reminder: Your meeting '{meeting.Title}' is at {meeting.StartTime}.");
                }
            }
            
            await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
        }
    }
}