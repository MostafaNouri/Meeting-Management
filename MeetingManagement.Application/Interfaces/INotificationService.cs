using MeetingManagement.Domain.DTOs;

namespace MeetingManagement.Application.Interfaces;

public interface INotificationService
{
    Task SendBulkNotificationsAsync(IEnumerable<NotificationDto> notifications);
}