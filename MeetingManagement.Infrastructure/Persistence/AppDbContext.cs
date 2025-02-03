using MeetingManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeetingManagement.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<Participant> Participants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Meeting>()
            .HasMany(m => m.Participants)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}