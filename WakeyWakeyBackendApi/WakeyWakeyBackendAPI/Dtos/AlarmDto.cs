using WakeyWakeyBackendAPI.Models;

namespace WakeyWakeyBackendAPI.Dtos;

/// <summary>Holds the details of a newly-created alarm.</summary>
/// <param name="Name">The name of the alarm, if any.</param>
/// <param name="EarliestWakeTime">The earliest possible time that the alarm should activate at.</param>
/// <param name="LatestWakeTime">The latest possible time that the alarm should activate at.</param>
/// <param name="DaysToRepeat">The weekdays on which this alarm can activate, if any.</param>
public record FreshAlarmDto(
    TimeOnly EarliestWakeTime,
    TimeOnly LatestWakeTime,
    WeekDays DaysToRepeat = WeekDays.None,
    string Name = "");

/// <summary>Holds the details of a previously-created alarm.</summary>
/// <param name="Id">The unique identifier of this alarm.</param>
/// <param name="Name">The name of the alarm, if any.</param>
/// <param name="Enabled">Is this alarm currently enabled?</param>
/// <param name="EarliestWakeTime">The earliest possible time that the alarm should activate at.</param>
/// <param name="LatestWakeTime">The latest possible time that the alarm should activate at.</param>
/// <param name="DaysToRepeat">The weekdays on which this alarm can activate, if any.</param>
public record ExistingAlarmDto(
    int Id,
    string Name,
    bool Enabled,
    TimeOnly EarliestWakeTime,
    TimeOnly LatestWakeTime,
    WeekDays DaysToRepeat);

/// <summary>Holds the details to update in an existing alarm.</summary>
/// <param name="Enabled">Is this alarm currently enabled?</param>
/// <param name="Name">The new name of the alarm, if any.</param>
/// <param name="EarliestWakeTime">The new earliest possible time that the alarm should activate at.</param>
/// <param name="LatestWakeTime">The new latest possible time that the alarm should activate at.</param>
/// <param name="DaysToRepeat">The new weekdays on which this alarm can activate, if any.</param>
public record UpdatedAlarmDto(
    string? Name = null,
    bool? Enabled = null,
    TimeOnly? EarliestWakeTime = null,
    TimeOnly? LatestWakeTime = null,
    WeekDays? DaysToRepeat = null);
