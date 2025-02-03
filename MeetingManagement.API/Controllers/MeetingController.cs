using MeetingManagement.Application.Services;
using MeetingManagement.Domain.Entities;
using MeetingManagement.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManagement.API.Controllers;

[ApiController]
[Route("api/meetings")]
public class MeetingController(MeetingService meetingService) : ControllerBase
{
    [HttpPost("schedule")]
    public async Task<IActionResult> ScheduleMeeting([FromBody] Meeting meeting)
    {
        var success = await meetingService.ScheduleMeeting(meeting);
        return success ? Ok("Meeting scheduled successfully.") : BadRequest("Time conflict detected.");
    }

    [HttpGet]
    public async Task<IActionResult> GetMeetings() =>
        Ok(await meetingService.GetMeetings());

    [HttpPut("cancel/{id}")]
    public async Task<IActionResult> CancelMeeting(int id)
    {
        var result = await meetingService.CancelMeeting(id);

        return result switch
        {
            MeetingCancellationResult.Success => Ok("Meeting canceled successfully."),
            MeetingCancellationResult.NotFound => NotFound("Meeting not found."),
            MeetingCancellationResult.AlreadyStarted => BadRequest("Cannot cancel a meeting that has already started."),
            _ => BadRequest("An unknown error occurred.")
        };
    }

    [HttpPut("report/{id}")]
    public async Task<IActionResult> AddReport(int id, [FromBody] string report) =>
        Ok(await meetingService.AddMeetingReport(id, report));
}