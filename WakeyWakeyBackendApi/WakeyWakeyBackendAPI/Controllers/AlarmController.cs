using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WakeyWakeyBackendAPI.Dtos;
using WakeyWakeyBackendAPI.Models;

namespace WakeyWakeyBackendAPI.Controllers;

// TODO: Refine user & alarm verification.
/// <summary>Handles management of alarms set by users.</summary>
[Route("api/{userId:int}/alarms")]
[ApiController]
public class AlarmController
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
    public async Task<ActionResult<List<AlarmDto>>> GetAlarms(int userId)
    {
        var alarms = await _context.Alarms
            .Where(alarm => alarm.UserId == userId)
            .Select(alarm => new AlarmDto { MinWakeTime = alarm.MinWakeTime })
            .ToListAsync();
        return alarms;
    }
    
    /// <summary>Registers a new alarm set by a user.</summary>
    /// <param name="userId">The unique id of the user.</param>
    /// <param name="alarm">The alarm details provided by user.</param>
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<AlarmDto>> CreateAlarm(int userId, [FromBody] AlarmDto alarm)
    {
        var alarmEntity = new Alarm { UserId = userId, MinWakeTime = alarm.MinWakeTime, MaxWakeTime = alarm.MaxWakeTime };
        _context.Alarms.Add(alarmEntity);
        await _context.SaveChangesAsync();
        return alarm;
    }
}
