using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WakeyWakeyBackendAPI.Dtos;
using WakeyWakeyBackendAPI.Models;

namespace WakeyWakeyBackendAPI.Controllers;

/// <summary>Controller handling the management of users' sleep records.</summary>
[ApiController]
[Route("api/[controller]")]
public class SleepSessionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public SleepSessionsController(AppDbContext context)
    {
        _context = context;
    }
    
    /// <summary>Retrieves all sleep sessions associated with a given user.</summary>
    /// <returns>A list containing all sleep sessions tracked for the user, if any.</returns>
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<ExistingSleepSessionDto>>> GetSleepSessions()
    {
        var userId = GetUserId(User);
        if (userId == null)
            return Unauthorized();
        
        // Retrieve sleep session records.
        var sessions = await _context.SleepSessions
            .Where(session => session.UserId == userId.Value)
            .Select(session => ExistingSleepSessionDto(session))
            .ToListAsync();
        return Ok(sessions);
    }

    /// <summary>Creates and stores a new sleep session in a user's history.</summary>
    /// <param name="session">The sleep session details.</param>
    /// <returns>The provided sleep session details plus its assigned id.</returns>
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ExistingSleepSessionDto>> CreateSleepSession([FromBody] FreshSleepSessionDto session)
    {
        var userId = GetUserId(User);
        if (userId == null)
            return Unauthorized();
        
        // Create and save the new sleep session, then delete outdate records.
        var sessionEntity = SleepSessionEntity(userId.Value, session);
        _context.SleepSessions.Add(sessionEntity);
        await _context.SaveChangesAsync();
        await DeleteOutdatedSleepSessions(userId.Value);
        
        return ExistingSleepSessionDto(sessionEntity);
    }

    /// <summary>Deletes a specific sleep session from a user's history.</summary>
    /// <param name="sessionId">The id of the sleep session.</param>
    /// <returns>HTTP OK if the requested sleep session was found and deleted.</returns>
    [Authorize]
    [HttpDelete("{sessionId:int}")]
    public async Task<ActionResult> DeleteSleepSession([FromRoute] int sessionId)
    {
        var userId = GetUserId(User);
        if (userId == null)
            return Unauthorized();
        
        // Try finding the requested session.
        var sessionEntity = await GetSleepSession(userId.Value, sessionId);
        if (sessionEntity == null)
            return NotFound();
        
        // If found, delete the sleep session.
        _context.SleepSessions.Remove(sessionEntity);
        await _context.SaveChangesAsync();
        return Ok();
    }

    /// <summary>Deletes all sleep session records older than 30 days for a given user.</summary>
    /// <param name="userId">The id of the user.</param>
    /// <remarks>Sleep session "freshness" is based on WakeTime converted to UTC.</remarks>
    private async Task DeleteOutdatedSleepSessions(int userId)
    {
        await _context.SleepSessions
            .Where(session => session.UserId == userId &&
                              session.WakeTime.ToUniversalTime() <= DateTime.UtcNow.AddDays(-30))
            .ExecuteDeleteAsync();
    }

    /// <summary>Helper method for finding a specific sleep session.</summary>
    /// <param name="userId">The id of the user.</param>
    /// <param name="sessionId">The id of the sleep session.</param>
    /// <returns>The requested SleepSession entity, or null if not found.</returns>
    private async Task<SleepSession?> GetSleepSession(int userId, int sessionId)
    {
        return await _context.SleepSessions
            .FirstOrDefaultAsync(session => session.UserId == userId && session.Id == sessionId);
    }

    /// <summary>Helper method for extracting a user's id from their jwt token.</summary>
    /// <param name="principal">The claims principal from which to extract the id from.</param>
    /// <returns>The integer id of the user, or null if none could be found.</returns>
    /// <remarks>This method should be replaced with an extension once the alarms branch is merged.</remarks>
    private static int? GetUserId(ClaimsPrincipal principal)
    {
        var userIdText = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdText != null && int.TryParse(userIdText, out var userId))
            return userId;
        return null;
    }

    /// <summary>Helper method for converting a FreshSleepSession to a SleepSession entity.</summary>
    /// <param name="userId">The id of the user.</param>
    /// <param name="session">The sleep session details.</param>
    /// <returns>An SleepSession entity with the same details as the dto.</returns>
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

    /// <summary>Helper method for converting a SleepSession entity to an ExistingSleepSession.</summary>
    /// <param name="session">The sleep session entity.</param>
    /// <returns>An ExistingSleepSession with the same details as the entity.</returns>
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
