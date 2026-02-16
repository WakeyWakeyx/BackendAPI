using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WakeyWakeyBackendAPI.Models;

// TODO: Check what other alarm data needs to be stored.
/// <summary>Represents an active alarm as set by a specific user.</summary>
public class Alarm
{
    /// <summary>The unique identifier of this alarm.</summary>
    [Key]
    public int Id { get; set; }
    
    /// <summary>The unique identifier of the user who set this alarm.</summary>
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    
    /// <summary>The name of the alarm, if any.</summary>
    [MaxLength(32)]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>Is this alarm currently enabled?</summary>
    public bool Enabled { get; set; } = true;
    
    /// <summary>The earliest possible time that the alarm should activate at.</summary>
    [Required]
    public required TimeOnly EarliestWakeTime { get; set; }
    
    /// <summary>The latest possible time that the alarm should activate at.</summary>
    public required TimeOnly LatestWakeTime { get; set; }

    /// <summary>The weekdays on which this alarm can activate, if any.</summary>
    public WeekDays DaysToRepeat { get; set; } = WeekDays.None;
    
    /// <summary>User navigation property.</summary>
    public virtual User User { get; set; }
}
