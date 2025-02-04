using MeetingManagement.Domain.Enums;
using MeetingManagement.Infrastructure.Interfaces.Services;

namespace MeetingManagement.Infrastructure.Services;

public abstract class BaseNotificationClient : INotificationClient
{
    public abstract ContactMethodType MethodType { get; }
    
    public abstract Task SendNotificationAsync(
        string recipientName, 
        string message, 
        string contactAddress
    );
}