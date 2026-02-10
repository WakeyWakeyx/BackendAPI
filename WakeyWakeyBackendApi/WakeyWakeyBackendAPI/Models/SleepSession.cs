using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WakeyWakeyBackendAPI.Models;

/// <summary>Represents a single instance of a user's sleep records.</summary>
[Table("SleepSessions")]
public class SleepSession
{
    /// <summary>The unique id of this sleep session.</summary>
    [Key]
    [Required]
    public int SessionId { get; set; }
    
    /// <summary>The unique id of the user this sleep session applies to.</summary>
    [Required]
    [ForeignKey(nameof(User))]
    public required int UserId { get; set; }
    
    /// <summary>The bedtime of this sleep session.</summary>
    [Required]
    public required DateTime StartTime { get; set; }
    
    /// <summary>The wake time of this sleep session.</summary>
    [Required]
    public required DateTime EndTime { get; set; }
    
    /// <summary>The sleep score assigned to this sleep session.</summary>
    [Required]
    public required int SleepScore { get; set; }
    
    public virtual User User { get; set; }
}
