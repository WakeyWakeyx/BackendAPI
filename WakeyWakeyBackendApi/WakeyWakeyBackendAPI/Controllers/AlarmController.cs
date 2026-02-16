using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WakeyWakeyBackendAPI.Dtos;
using WakeyWakeyBackendAPI.Models;
using WakeyWakeyBackendAPI.Utils;

namespace WakeyWakeyBackendAPI.Controllers;

// TODO: Cleanup code
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

    /// <summary>Returns a specific alarm with a given id.</summary>
    /// <param name="alarmId">the numeric id of the alarm.</param>
    /// <returns>The alarm details, if any.</returns>
    public async Task<ActionResult<AlarmDto>> GetAlarm([FromQuery] int alarmId)
    {
        var userId = User.GetUserId();
        if (userId == null)
            return Unauthorized();
        var alarm = await _GetAlarm(userId.Value, alarmId);
        if (alarm == null)
            return NotFound();
        return new AlarmDto()
        {
            AlarmName = alarm.AlarmName,
            EarliestWakeTime = alarm.EarliestWakeTime,
            LatestWakeTime = alarm.LatestWakeTime,
            RepeatingDays = alarm.RepeatingDays
        };
    }

    private async Task<Alarm?> _GetAlarm(int userId, int alarmId)
    {
        return await _context.Alarms
            .Where(alarm => alarm.UserId == userId && alarm.AlarmId == alarmId)
            .FirstOrDefaultAsync();
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
                LatestWakeTime = alarm.LatestWakeTime,
                RepeatingDays = alarm.RepeatingDays
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
            LatestWakeTime = alarm.LatestWakeTime,
            RepeatingDays = alarm.RepeatingDays
        };
        _context.Alarms.Add(alarmEntity);
        await _context.SaveChangesAsync();
        return alarm;
    }

    /// <summary>Deletes an alarm previously created by a user.</summary>
    /// <param name="alarmId">The unique id of the alarm</param>
    /// <returns>OK if the alarm was found and successfully deleted.</returns>
    [Authorize]
    [HttpDelete]
    public async Task<ActionResult> DeleteAlarm([FromQuery] int alarmId)
    {
        var userId = User.GetUserId();
        if (userId == null)
            return Unauthorized();
        // Try finding the alarm
        var toDelete = await _context.Alarms
            .Where(alarm => alarm.AlarmId == alarmId && alarm.UserId == userId)
            .FirstOrDefaultAsync();
        if (toDelete == null)
            return NotFound();
        // Then delete it
        _context.Alarms.Remove(toDelete);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
