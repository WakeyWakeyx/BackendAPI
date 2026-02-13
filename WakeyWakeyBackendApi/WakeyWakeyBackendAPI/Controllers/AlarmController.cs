using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WakeyWakeyBackendAPI.Dtos;
using WakeyWakeyBackendAPI.Models;
using WakeyWakeyBackendAPI.Utils;

namespace WakeyWakeyBackendAPI.Controllers;

// TODO: Refine user & alarm verification.
/// <summary>Handles management of alarms set by users.</summary>
[Route("api/[controller]")]
[ApiController]
public class AlarmController : ControllerBase
{
    private readonly AppDbContext _context;
    
    public AlarmController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>Returns all alarms currently set by the given user.</summary>
    /// <param name="userId">The unique id of the user.</param>
    /// <returns>A list containing all alarms set by `userId`.</returns>
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<AlarmDto>>> GetAlarms()
    {
        var userId = User.GetUserId();
        if (userId == null)
            return Unauthorized();
        var alarms = await _context.Alarms
            .Where(alarm => alarm.UserId == userId.Value)
            .Select(alarm => new AlarmDto
            {
                AlarmName = alarm.AlarmName,
                EarliestWakeTime = alarm.EarliestWakeTime,
                LatestWakeTime = alarm.LatestWakeTime
            }).ToListAsync();
        return alarms;
    }
    
    /// <summary>Registers a new alarm set by a user.</summary>
    /// <param name="userId">The unique id of the user.</param>
    /// <param name="alarm">The alarm details provided by user.</param>
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<AlarmDto>> CreateAlarm([FromBody] AlarmDto alarm)
    {
        var userId = User.GetUserId();
        if (userId == null)
            return Unauthorized();
        var alarmEntity = new Alarm
        {
            UserId = userId.Value,
            AlarmName = alarm.AlarmName,
            EarliestWakeTime = alarm.EarliestWakeTime,
            LatestWakeTime = alarm.LatestWakeTime
        };
        _context.Alarms.Add(alarmEntity);
        await _context.SaveChangesAsync();
        return alarm;
    }
}
