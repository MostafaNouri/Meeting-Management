using Hangfire;
using MeetingManagement.Application.Interfaces;
using MeetingManagement.Domain.DTOs;

namespace MeetingManagement.Infrastructure.Services;

public class NotificationScheduler(INotificationService notificationService)
    : INotificationScheduler
{
    public string ScheduleNotification(IEnumerable<NotificationDto> notifications, DateTime notificationTime)
    {
        return BackgroundJob.Schedule(() => notificationService.SendBulkNotificationsAsync(notifications), notificationTime);
    }

    public void CancelNotification(string jobId)
    {
        BackgroundJob.Delete(jobId);
    }
}