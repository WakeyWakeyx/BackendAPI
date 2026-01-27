using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WakeyWakeyBackendAPI.Dtos;
using WakeyWakeyBackendAPI.Models;

namespace WakeyWakeyBackendAPI.Controllers;

[ApiController]
[Route("api/users/{userId:int}/sessions")]
public class SleepSessionController : ControllerBase
{
    private readonly AppDbContext _context;

    public SleepSessionController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<SleepSession>>> GetSleepSessions(int userId)
    {
        // Validate incoming userId against that in the jwt token.
        var actualUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (actualUserId == null || !actualUserId.Equals(userId.ToString()))
            return Unauthorized();
        
        // Retrieve sleep session records.
        var sleepSessions = await _context.SleepSessions.Where(x => x.UserId == userId).ToListAsync();
        var sleepResponses = new List<SleepSessionResponseDto>();
        foreach (var sleepSession in sleepSessions)
        {
            sleepResponses.Add(new SleepSessionResponseDto(sleepSession.StartTime,  sleepSession.EndTime, sleepSession.SleepScore));
        }
        return Ok(sleepResponses);
    }
}
