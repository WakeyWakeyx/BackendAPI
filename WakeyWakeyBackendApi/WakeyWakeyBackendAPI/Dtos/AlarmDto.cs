namespace WakeyWakeyBackendAPI.Dtos;

public class AlarmDto
{
    public string AlarmName { get; set; } = string.Empty;
    
    public DateTime EarliestWakeTime { get; set; }
    
    public DateTime LatestWakeTime { get; set; }
}
