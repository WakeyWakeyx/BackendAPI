namespace WakeyWakeyBackendAPI.Models;

/// <summary>
/// Represents a day of week enum type in the database.
/// </summary>
[Flags]
public enum WeekDays
{
    None = 0,
    Sunday = 1,
    Monday = 2,
    Tuesday = 4,
    Wednesday = 8,
    Thursday = 16,
    Friday = 32,
    Saturday = 64,
    All = Sunday | Monday | Tuesday | Wednesday | Thursday | Friday | Saturday
}
