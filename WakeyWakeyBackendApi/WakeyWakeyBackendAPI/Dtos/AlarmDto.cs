using WakeyWakeyBackendAPI.Models;

namespace WakeyWakeyBackendAPI.Dtos;

public class FreshAlarmDto
{
    public string AlarmName { get; set; } = string.Empty;
    public required DateTime EarliestWakeTime { get; set; }
    public required DateTime LatestWakeTime { get; set; }
    public WeekDays RepeatingDays { get; set; } = WeekDays.None;
}

public class ExistingAlarmDto
{
    public required string AlarmName { get; set; }
    public required bool IsEnabled { get; set; }
    public required DateTime EarliestWakeTime { get; set; }
    public required DateTime LatestWakeTime { get; set; }
    public required WeekDays RepeatingDays { get; set; }
}

public class UpdatedAlarmDto
{
    public string? AlarmName { get; set; } = null;
    public bool? IsEnabled { get; set; } = null;
    public DateTime? EarliestWakeTime { get; set; } = null;
    public DateTime? LatestWakeTime { get; set; } = null;
    public WeekDays? RepeatingDays { get; set; } = null;
}
