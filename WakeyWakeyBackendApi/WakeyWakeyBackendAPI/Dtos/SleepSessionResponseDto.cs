namespace WakeyWakeyBackendAPI.Dtos;

/// <summary>Represents basic information about a user's sleep record.</summary>
/// <param name="StartTime">The time the user actually went to sleep at.</param>
/// <param name="EndTime">The time the user actually woke up at.</param>
/// <param name="SleepScore">The numerical quality score assigned to this sleep session.</param>
public class SleepSessionResponseDto(
    DateTime StartTime,
    DateTime EndTime,
    int SleepScore);
