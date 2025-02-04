using MeetingManagement.Domain.Enums;

namespace MeetingManagement.Infrastructure.Services;

public class SmsNotificationClient : BaseNotificationClient
{
    public override ContactMethodType MethodType => ContactMethodType.SMS;

    public override async Task SendNotificationAsync(
        string recipientName, 
        string message, 
        string phoneNumber)
    {
        // SMS sending implementation
        Console.WriteLine($"Sent SMS to {phoneNumber}: {message}");
        await Task.CompletedTask;
    }
}