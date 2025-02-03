﻿namespace MeetingManagement.Domain.Interfaces;

public interface INotificationService
{
    Task SendEmailAsync(string email, string subject, string message);
    Task SendSmsAsync(string phoneNumber, string message);
}