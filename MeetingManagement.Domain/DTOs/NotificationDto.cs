using MeetingManagement.Domain.Entities;

namespace MeetingManagement.Domain.DTOs;

public class NotificationDto
{
   public string RecipientName { get; set; }
   public string Message { get; set; }
   public List<ContactMethod> ContactMethods { get; set; }
}