using MeetingManagement.Domain.Enums;

namespace MeetingManagement.Domain.Entities;

public class ContactMethod
{
    public int Id { get; set; }
    public ContactMethodType Type { get; set; }
    public string Value { get; set; }
}