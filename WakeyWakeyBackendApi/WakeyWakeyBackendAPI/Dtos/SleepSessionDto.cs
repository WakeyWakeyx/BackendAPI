namespace WakeyWakeyBackendAPI.Dtos;

/// <summary>Represents basic information about a user's new sleep record.</summary>
/// 
/// <param name="BedTime">The bedtime of this sleep session.</param>
/// <param name="WakeTime">The wake time of this sleep session.</param>
/// <param name="LightSleepDuration">The duration of the lightest sleep stages</param>
/// <param name="RemSleepDuration">The duration of the rem sleep stages.</param>
/// <param name="DeepSleepDuration">The duration of the deepest sleep stages.</param>
/// <param name="AverageHeartRate">The user's average heart rate during sleep.</param>
public record FreshSleepSessionDto(
    DateTime BedTime,
    DateTime WakeTime,
    TimeSpan LightSleepDuration,
    TimeSpan RemSleepDuration,
    TimeSpan DeepSleepDuration,
    int AverageHeartRate);

/// <summary>Represents basic information about a user's existing sleep record.</summary>
///
/// <param name="Id">The unique id of this sleep session.</param>
/// <param name="BedTime">The bedtime of this sleep session.</param>
/// <param name="WakeTime">The wake time of this sleep session.</param>
/// <param name="LightSleepDuration">The duration of the lightest sleep stages.</param>
/// <param name="RemSleepDuration">The duration of the rem sleep stages.</param>
/// <param name="DeepSleepDuration">The duration of the deepest sleep stages.</param>
/// <param name="AverageHeartRate">The user's average heart rate during sleep.</param>
public record ExistingSleepSessionDto(
    int Id,
    DateTime BedTime,
    DateTime WakeTime,
    TimeSpan LightSleepDuration,
    TimeSpan RemSleepDuration,
    TimeSpan DeepSleepDuration,
    int AverageHeartRate);
    