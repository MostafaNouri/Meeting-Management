using MeetingManagement.Domain.Entities;
using MeetingManagement.Domain.Enums;

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
                Title = "Dotin Project Meeting",
                StartTime = DateTime.UtcNow.AddHours(2),
                EndTime = DateTime.UtcNow.AddHours(3),
                Room = "Conference Room A",
                Participants = new List<Participant>
                {
                    new()
                    {
                        Name = "Mostafa Nouri", ContactMethods =
                        [
                            new ContactMethod()
                            {
                                Type = ContactMethodType.Email,
                                Value = "mostafa@xample.com"
                            },
                            new ContactMethod()
                            {
                                Type = ContactMethodType.SMS,
                                Value = "09362222222"
                            }
                        ]
                    },
                    new()
                    {
                        Name = "Sajjad Abbasi", ContactMethods =
                        [
                            new ContactMethod
                            {
                                Type = ContactMethodType.Email,
                                Value = "sajjad@example.com"
                            },
                            new ContactMethod
                            {
                                Type = ContactMethodType.SMS,
                                Value = "09362222223"
                            }
                        ]
                    }
                }
            });
        }

        context.Meetings.Add(new Meeting
        {
            Title = "Daily Report Meeting",
            StartTime = DateTime.UtcNow.AddHours(4),
            EndTime = DateTime.UtcNow.AddHours(5),
            Room = "Conference Room B",
            Participants = new List<Participant>
            {
                new()
                {
                    Name = "Sasan Arham", ContactMethods =
                    [
                        new ContactMethod
                        {
                            Type = ContactMethodType.Email,
                            Value = "sasan@example.com"
                        },
                        new ContactMethod
                        {
                            Type = ContactMethodType.SMS,
                            Value = "09192503450"
                        }
                    ]
                }
            }
        });

        context.SaveChanges();
    }
}