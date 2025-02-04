using Hangfire;
using Hangfire.MemoryStorage;
using MeetingManagement.Application.Interfaces;
using MeetingManagement.Application.Interfaces.Repositories;
using MeetingManagement.Application.Services;
using MeetingManagement.Infrastructure.Interfaces.Services;
using MeetingManagement.Infrastructure.Persistence;
using MeetingManagement.Infrastructure.Persistence.Repositories;
using MeetingManagement.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure Hangfire (In-Memory)
builder.Services.AddHangfire(config => config.UseMemoryStorage());
builder.Services.AddHangfireServer();

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("MeetingDB"));

builder.Services.AddScoped<IMeetingRepository, MeetingRepository>();
builder.Services.AddScoped<IScheduledJobRepository, ScheduledJobRepository>();

builder.Services.AddScoped<MeetingService>();
builder.Services.AddSingleton<INotificationClient, EmailNotificationClient>();
builder.Services.AddSingleton<INotificationClient, SmsNotificationClient>();
builder.Services.AddSingleton<INotificationService, NotificationService>();
builder.Services.AddScoped<INotificationScheduler, NotificationScheduler>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    SeedData.Initialize(context);
}

app.UseHangfireDashboard();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHangfireDashboard();

app.Run();