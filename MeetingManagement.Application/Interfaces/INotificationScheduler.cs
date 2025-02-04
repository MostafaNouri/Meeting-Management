using MeetingManagement.Domain.DTOs;
using MeetingManagement.Domain.Entities;

namespace MeetingManagement.Application.Interfaces;

public interface INotificationScheduler
{
    string ScheduleNotification(IEnumerable<NotificationDto> notifications, DateTime notificationTime);
    void CancelNotification(string jobId);
}