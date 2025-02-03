using MeetingManagement.Domain.Interfaces;

namespace MeetingManagement.Infrastructure.Services;

// I created this class for abstraction
// We can implement real Email ana SMS services here or create separate classes for each one
// So this is just test :)
public class MockNotificationService : INotificationService
{
    public Task SendEmailAsync(string email, string subject, string message)
    {
        return Task.CompletedTask;
    }

    public Task SendSmsAsync(string phoneNumber, string message)
    {
        return Task.CompletedTask;
    }
}