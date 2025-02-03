using MeetingManagement.Application.Services;
using MeetingManagement.Domain.Interfaces;
using MeetingManagement.Infrastructure.Persistence;
using MeetingManagement.Infrastructure.Repositories;
using MeetingManagement.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("MeetingDB"));

builder.Services.AddScoped<IMeetingRepository, MeetingRepository>();
builder.Services.AddScoped<MeetingService>();
builder.Services.AddSingleton<INotificationService, MockNotificationService>();
builder.Services.AddHostedService<NotificationService>();

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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();