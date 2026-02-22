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
    public int Id { get; set; }
    
    /// <summary>The unique id of the user this sleep session applies to.</summary>
    [Required]
    [ForeignKey(nameof(User))]
    public required int UserId { get; set; }
    
    /// <summary>The bedtime of this sleep session.</summary>
    [Required]
    public required DateTime BedTime { get; set; }
    
    /// <summary>The wake time of this sleep session.</summary>
    [Required]
    public required DateTime WakeTime { get; set; }
    
    /// <summary>The duration of the lightest sleep stages.</summary>
    [Required]
    public required TimeSpan LightSleepDuration { get; set; }
    
    /// <summary>The duration of the rem sleep stages.</summary>
    [Required]
    public required TimeSpan RemSleepDuration { get; set; }
    
    /// <summary>The duration of the deepest sleep stages.</summary>
    [Required]
    public required TimeSpan DeepSleepDuration { get; set; }
    
    /// <summary>The user's average heart rate during sleep.</summary>
    [Required]
    [Range(0, int.MaxValue)]
    public required int AverageHeartRate { get; set; }
    
    public virtual User User { get; set; }
}
