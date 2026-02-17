using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WakeyWakeyBackendAPI.Dtos;
using WakeyWakeyBackendAPI.Models;

namespace WakeyWakeyBackendAPI.Controllers;

/// <summary>Controller handling the management of users' sleep records.</summary>
[ApiController]
[Route("api/users/{userId:int}/sessions")]
public class SleepSessionController : ControllerBase
{
    private readonly AppDbContext _context;

    public SleepSessionController(AppDbContext context)
    {
        _context = context;
    }
    
    /// <summary>Retrieves all sleep sessions associated with a given user.</summary>
    /// <param name="userId">The unique id of the user to retrieve for.</param>
    /// <returns>A sequence containing all sleep sessions tracked for the user, if any.</returns>
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<SleepSession>>> GetSleepSessions(int userId)
    {
        // Validate incoming userId against that in the jwt token.
        var actualUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (actualUserId == null || !actualUserId.Equals(userId.ToString()))
            return Unauthorized();
        
        // Retrieve sleep session records.
        var sleepSessions = await _context.SleepSessions
            .Where(session => session.UserId == userId)
            .Select(session => ExistingSleepSessionDto(session))
            .ToListAsync();
        return Ok(sleepSessions);
    }

    private static SleepSession SleepSessionEntity(int userId, FreshSleepSessionDto session)
    {
        return new SleepSession
        {
            UserId = userId,
            BedTime = session.BedTime,
            WakeTime = session.WakeTime,
            LightSleepDuration = session.LightSleepDuration,
            RemSleepDuration = session.RemSleepDuration,
            DeepSleepDuration = session.DeepSleepDuration,
            AverageHeartRate = session.AverageHeartRate
        };
    }

    private static ExistingSleepSessionDto ExistingSleepSessionDto(SleepSession session)
    {
        return new ExistingSleepSessionDto(
            Id: session.Id,
            BedTime: session.BedTime,
            WakeTime: session.WakeTime,
            LightSleepDuration: session.LightSleepDuration,
            RemSleepDuration: session.RemSleepDuration,
            DeepSleepDuration: session.DeepSleepDuration,
            AverageHeartRate: session.AverageHeartRate
        );
    }
}
