namespace MeetingManagement.Domain.Entities;

public class Participant
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ContactMethod> ContactMethods { get; set; }
}