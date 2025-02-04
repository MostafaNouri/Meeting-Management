using MeetingManagement.Domain.Enums;

namespace MeetingManagement.Infrastructure.Interfaces.Services;

public interface INotificationClient
{
    ContactMethodType MethodType { get; }
    Task SendNotificationAsync(string recipientName, string message, string contactAddress);
}