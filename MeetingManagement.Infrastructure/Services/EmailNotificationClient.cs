using MeetingManagement.Domain.Enums;

namespace MeetingManagement.Infrastructure.Services;

public class EmailNotificationClient : BaseNotificationClient
{
    public override ContactMethodType MethodType => ContactMethodType.Email;

    public override async Task SendNotificationAsync(
        string recipientName, 
        string message, 
        string emailAddress)
    {
        Console.WriteLine($"Sent email to {emailAddress}: {message}");
        await Task.CompletedTask;
    }
}