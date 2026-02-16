using WakeyWakeyBackendAPI.Models;

namespace WakeyWakeyBackendAPI.Dtos;

public class AlarmDto
{
    public string AlarmName { get; set; } = string.Empty;
    
    public DateTime EarliestWakeTime { get; set; }
    
    public DateTime LatestWakeTime { get; set; }
    
    public WeekDays RepeatingDays { get; set; } = WeekDays.None;
}

public class UpdateAlarmDto
{
    public string? AlarmName { get; set; } = null;
    public DateTime? EarliestWakeTime { get; set; } = null;
    public DateTime? LatestWakeTime { get; set; } = null;
    public WeekDays? RepeatingDays { get; set; } = null;
}
