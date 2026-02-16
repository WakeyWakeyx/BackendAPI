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
    [Authorize]
    [HttpGet("getAlarm")]
    public async Task<ActionResult<AlarmDto>> GetAlarm([FromQuery] int alarmId)
    {
        var userId = User.GetUserId();
        if (userId == null)
            return Unauthorized();
        var alarm = await GetAlarmEntity(userId.Value, alarmId);
        if (alarm == null)
            return NotFound();
        return CreateAlarmDto(alarm);
    }

    /// <summary>Returns all alarms currently set by the given user.</summary>
    /// <returns>A list containing all alarms set by `userId`.</returns>
    [Authorize]
    [HttpGet("getAlarms")]
    public async Task<ActionResult<List<AlarmDto>>> GetAlarms()
    {
        var userId = User.GetUserId();
        if (userId == null)
            return Unauthorized();
        var alarms = await _context.Alarms
            .Where(alarm => alarm.UserId == userId.Value)
            .Select(alarm => CreateAlarmDto(alarm))
            .ToListAsync();
        return alarms;
    }
    
    /// <summary>Registers a new alarm set by a user.</summary>
    /// <param name="alarm">The alarm details provided by user.</param>
    [Authorize]
    [HttpPost("createAlarm")]
    public async Task<ActionResult<AlarmDto>> CreateAlarm([FromBody] AlarmDto alarm)
    {
        var userId = User.GetUserId();
        if (userId == null)
            return Unauthorized();
        _context.Alarms.Add(CreateAlarmEntity(userId.Value, alarm));
        await _context.SaveChangesAsync();
        return alarm;
    }

    /// <summary>Updates attributes of an existing alarm with those provided in the request body.</summary>
    /// <param name="alarmId">The unique identifier of the alarm.</param>
    /// <param name="alarmDto">The new alarm details to set.</param>
    [Authorize]
    [HttpPatch("updateAlarm")]
    public async Task<ActionResult> UpdateAlarm([FromQuery] int alarmId, [FromBody] UpdateAlarmDto alarmDto)
    {
        // TODO: Need to remove these ugly things.
        var userId = User.GetUserId();
        if (userId == null)
            return Unauthorized();
        
        // Fetch the requested alarm.
        var alarmEntity = await GetAlarmEntity(userId.Value, alarmId);
        if (alarmEntity == null)
            return NotFound();
        
        // Update values only if explicitly provided.
        if (alarmDto.AlarmName != null)
            alarmEntity.AlarmName = alarmDto.AlarmName;
        if (alarmDto.EarliestWakeTime != null)
            alarmEntity.EarliestWakeTime = alarmDto.EarliestWakeTime.Value;
        if (alarmDto.LatestWakeTime != null)
            alarmEntity.LatestWakeTime = alarmDto.LatestWakeTime.Value;
        if (alarmDto.RepeatingDays != null)
            alarmEntity.RepeatingDays = alarmDto.RepeatingDays.Value;
        _context.Alarms.Update(alarmEntity);
        await _context.SaveChangesAsync();
        return Ok();
    }

    /// <summary>Deletes an alarm previously created by a user.</summary>
    /// <param name="alarmId">The unique id of the alarm</param>
    /// <returns>OK if the alarm was found and successfully deleted.</returns>
    [Authorize]
    [HttpDelete("deleteAlarm")]
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
    
    private async Task<Alarm?> GetAlarmEntity(int userId, int alarmId)
    {
        return await _context.Alarms
            .Where(alarm => alarm.UserId == userId && alarm.AlarmId == alarmId)
            .FirstOrDefaultAsync();
    }

    private static AlarmDto CreateAlarmDto(Alarm alarm)
    {
        return new AlarmDto()
        {
            AlarmName = alarm.AlarmName,
            EarliestWakeTime = alarm.EarliestWakeTime,
            LatestWakeTime = alarm.LatestWakeTime,
            RepeatingDays = alarm.RepeatingDays
        };
    }

    private static Alarm CreateAlarmEntity(int userId, AlarmDto alarm)
    {
        return new Alarm
        {
            UserId = userId,
            AlarmName = alarm.AlarmName,
            EarliestWakeTime = alarm.EarliestWakeTime,
            LatestWakeTime = alarm.LatestWakeTime,
            RepeatingDays = alarm.RepeatingDays
        };
    }
}
