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
public class AlarmsController : ControllerBase
{
    private readonly AppDbContext _context;
    
    public AlarmsController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>Returns a specific alarm with a given id.</summary>
    /// <param name="alarmId">the numeric id of the alarm.</param>
    /// <returns>The alarm details, if any.</returns>
    [Authorize]
    [HttpGet("{alarmId:int}")]
    public async Task<ActionResult<ExistingAlarmDto>> GetAlarm([FromRoute] int alarmId)
    {
        var userId = User.GetUserId();
        if (userId == null)
            return Unauthorized();
        var alarmEntity = await GetAlarmEntity(userId.Value, alarmId);
        if (alarmEntity == null)
            return NotFound();
        return CreateAlarmDto(alarmEntity);
    }

    /// <summary>Returns all alarms currently set by the given user.</summary>
    /// <returns>A list containing all alarms set by `userId`.</returns>
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<ExistingAlarmDto>>> GetAlarms()
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
    /// <param name="alarmDto">The alarm details provided by user.</param>
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ExistingAlarmDto>> CreateAlarm([FromBody] FreshAlarmDto alarmDto)
    {
        var userId = User.GetUserId();
        if (userId == null)
            return Unauthorized();
        
        // Create and save the new alarm.
        var alarmEntity = CreateAlarmEntity(userId.Value, alarmDto);
        _context.Alarms.Add(alarmEntity);
        await _context.SaveChangesAsync();
        return CreateAlarmDto(alarmEntity);
    }

    /// <summary>Updates attributes of an existing alarm with those provided in the request body.</summary>
    /// <param name="alarmId">The unique identifier of the alarm.</param>
    /// <param name="alarmDto">The new alarm details to set.</param>
    [Authorize]
    [HttpPatch("{alarmId:int}")]
    public async Task<ActionResult<ExistingAlarmDto>> UpdateAlarm([FromRoute] int alarmId, [FromBody] UpdatedAlarmDto alarmDto)
    {
        var userId = User.GetUserId();
        if (userId == null)
            return Unauthorized();
        
        // Fetch the requested alarm.
        var alarmEntity = await GetAlarmEntity(userId.Value, alarmId);
        if (alarmEntity == null)
            return NotFound();
        
        // Update values only if explicitly provided.
        if (alarmDto.Name != null)
            alarmEntity.Name = alarmDto.Name;
        if (alarmDto.Enabled != null)
            alarmEntity.Enabled = alarmDto.Enabled.Value;
        if (alarmDto.EarliestWakeTime != null)
            alarmEntity.EarliestWakeTime = alarmDto.EarliestWakeTime.Value;
        if (alarmDto.LatestWakeTime != null)
            alarmEntity.LatestWakeTime = alarmDto.LatestWakeTime.Value;
        if (alarmDto.DaysToRepeat != null)
            alarmEntity.DaysToRepeat = alarmDto.DaysToRepeat.Value;
        _context.Alarms.Update(alarmEntity);
        await _context.SaveChangesAsync();
        return CreateAlarmDto(alarmEntity);
    }

    /// <summary>Deletes an alarm previously created by a user.</summary>
    /// <param name="alarmId">The unique id of the alarm</param>
    /// <returns>OK if the alarm was found and successfully deleted.</returns>
    [Authorize]
    [HttpDelete("{alarmId:int}")]
    public async Task<ActionResult> DeleteAlarm([FromRoute] int alarmId)
    {
        var userId = User.GetUserId();
        if (userId == null)
            return Unauthorized();
        
        // Try finding the alarm.
        var alarmEntity = await GetAlarmEntity(userId.Value, alarmId);
        if (alarmEntity == null)
            return NotFound();
        
        // Then delete it.
        _context.Alarms.Remove(alarmEntity);
        await _context.SaveChangesAsync();
        return Ok();
    }
    
    /// <summary>Helper method for finding a specific alarm attached to a user.</summary>
    /// <param name="userId">The id of the user.</param>
    /// <param name="alarmId">The id of the alarm.</param>
    /// <returns>The alarm entity, or null if none  could be found.</returns>
    private async Task<Alarm?> GetAlarmEntity(int userId, int alarmId)
    {
        return await _context.Alarms
            .FirstOrDefaultAsync(alarm => alarm.UserId == userId && alarm.Id == alarmId);
    }

    /// <summary>Helper method for converting an Alarm entity to an ExistingAlarmDto.</summary>
    /// <param name="alarm">The Alarm entity.</param>
    /// <returns>An ExistingAlarmDto with the same details as the entity.</returns>
    private static ExistingAlarmDto CreateAlarmDto(Alarm alarm)
    {
        return new ExistingAlarmDto
        {
            Id = alarm.Id,
            Name = alarm.Name,
            Enabled = alarm.Enabled,
            EarliestWakeTime = alarm.EarliestWakeTime,
            LatestWakeTime = alarm.LatestWakeTime,
            DaysToRepeat = alarm.DaysToRepeat
        };
    }

    /// <summary>Helper method for converting a FreshAlarmDto to an Alarm entity.</summary>
    /// <param name="userId">The id of the user who created the alarm.</param>
    /// <param name="alarmDto">The details of the newly-created alarm.</param>
    /// <returns>An Alarm entity with the same details as those in the FreshAlarmDto.</returns>
    private static Alarm CreateAlarmEntity(int userId, FreshAlarmDto alarmDto)
    {
        return new Alarm
        {
            UserId = userId,
            Name = alarmDto.Name,
            EarliestWakeTime = alarmDto.EarliestWakeTime,
            LatestWakeTime = alarmDto.LatestWakeTime,
            DaysToRepeat = alarmDto.DaysToRepeat
        };
    }
}
