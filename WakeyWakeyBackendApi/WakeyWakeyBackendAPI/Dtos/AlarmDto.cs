using WakeyWakeyBackendAPI.Models;

namespace WakeyWakeyBackendAPI.Dtos;

/// <summary>Holds the details of a newly-created alarm.</summary>
public class FreshAlarmDto
{
    /// <summary>The name of the alarm, if any.</summary>
    public string AlarmName { get; set; } = string.Empty;
    
    /// <summary>The earliest possible time that the alarm should activate at.</summary>
    public required DateTime EarliestWakeTime { get; set; }
    
    /// <summary>The latest possible time that the alarm should activate at.</summary>
    public required DateTime LatestWakeTime { get; set; }
    
    /// <summary>The weekdays on which this alarm can activate, if any.</summary>
    public WeekDays RepeatingDays { get; set; } = WeekDays.None;
}

/// <summary>Holds the details of a previously-created alarm.</summary>
public class ExistingAlarmDto
{
    /// <summary>The name of the alarm, if any.</summary>
    public required string AlarmName { get; set; }
    
    /// <summary>Is this alarm currently enabled?</summary>
    public required bool IsEnabled { get; set; }
    
    /// <summary>The earliest possible time that the alarm should activate at.</summary>
    public required DateTime EarliestWakeTime { get; set; }
    
    /// <summary>The latest possible time that the alarm should activate at.</summary>
    public required DateTime LatestWakeTime { get; set; }
    
    /// <summary>The weekdays on which this alarm can activate, if any.</summary>
    public required WeekDays RepeatingDays { get; set; }
}

/// <summary>Holds the details to update in an existing alarm.</summary>
public class UpdatedAlarmDto
{
    /// <summary>The new name of the alarm, if any.</summary>
    public string? AlarmName { get; set; } = null;
    
    /// <summary>Is this alarm currently enabled?</summary>
    public bool? IsEnabled { get; set; } = null;
    
    /// <summary>The new earliest possible time that the alarm should activate at.</summary>
    public DateTime? EarliestWakeTime { get; set; } = null;
    
    /// <summary>The new latest possible time that the alarm should activate at.</summary>
    public DateTime? LatestWakeTime { get; set; } = null;
    
    /// <summary>The new weekdays on which this alarm can activate, if any.</summary>
    public WeekDays? RepeatingDays { get; set; } = null;
}
