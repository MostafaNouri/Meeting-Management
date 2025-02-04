using MeetingManagement.Application.Interfaces;
using MeetingManagement.Domain.DTOs;
using MeetingManagement.Domain.Enums;
using MeetingManagement.Infrastructure.Interfaces;
using MeetingManagement.Infrastructure.Interfaces.Services;

namespace MeetingManagement.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly Dictionary<ContactMethodType, INotificationClient> _strategies;

    public NotificationService(IEnumerable<INotificationClient> strategies)
    {
        _strategies = strategies.ToDictionary(s => s.MethodType);
    }

    public async Task SendBulkNotificationsAsync(IEnumerable<NotificationDto> notifications)
    {
        var parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount * 2
        };
        
        await Parallel.ForEachAsync(notifications, parallelOptions, async (model, _) =>
        {
            foreach (var contactMethod in model.ContactMethods)
            {
                if (_strategies.TryGetValue(contactMethod.Type, out var strategy))
                {
                    await strategy.SendNotificationAsync(
                        model.RecipientName,
                        model.Message,
                        contactMethod.Value
                    );
                }
            }
        });
    }
}