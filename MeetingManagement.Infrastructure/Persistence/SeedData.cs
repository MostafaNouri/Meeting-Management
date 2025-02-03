using MeetingManagement.Domain.Entities;

namespace MeetingManagement.Infrastructure.Persistence;

public class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        // Ensure database is created (Only needed for In-Memory DB)
        context.Database.EnsureCreated();

        // Check if there are any existing meetings, avoid duplication
        if (!context.Meetings.Any())
        {
            context.Meetings.Add(new Meeting
            {
                Title = "Project Kickoff Meeting",
                StartTime = DateTime.UtcNow.AddHours(2),
                EndTime = DateTime.UtcNow.AddHours(3),
                Room = "Conference Room A",
                Participants = new List<Participant>
                {
                    new() { Name = "John Doe", Email = "john@example.com", PhoneNumber = "1234567890" },
                    new() { Name = "Jane Smith", Email = "jane@example.com", PhoneNumber = "0987654321" }
                }
            });

            context.Meetings.Add(new Meeting
            {
                Title = "Weekly Team Sync",
                StartTime = DateTime.UtcNow.AddHours(4),
                EndTime = DateTime.UtcNow.AddHours(5),
                Room = "Conference Room B",
                Participants = new List<Participant>
                {
                    new() { Name = "Alice Johnson", Email = "alice@example.com", PhoneNumber = "1112223333" }
                }
            });

            context.SaveChanges();
        }
    }
}