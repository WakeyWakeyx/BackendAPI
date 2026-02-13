using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WakeyWakeyBackendAPI.Models;

// TODO: Check what other alarm data needs to be stored.
/// <summary>
/// Represents an active alarm as set by a specific user.
/// </summary>
public class Alarm
{
    /// <summary>
    /// The unique identifier of this alarm.
    /// </summary>
    [Key]
    public int AlarmId { get; set; }
    
    /// <summary>
    /// the unique identifier of the user who set this alarm.
    /// </summary>
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    
    /// <summary>
    /// The name of this alarm (optional).
    /// </summary>
    [MaxLength(32)]
    public string AlarmName { get; set; } = string.Empty;
    
    /// <summary>
    /// The minimum time to wake the user at.
    /// </summary>
    [Required]
    public required DateTime EarliestWakeTime { get; set; }
    
    /// <summary>
    /// The maximum time to wake the user at.
    /// </summary>
    public required DateTime LatestWakeTime { get; set; }
    
    /// <summary>
    /// User navigation property.
    /// </summary>
    public virtual User User { get; set; }
}
