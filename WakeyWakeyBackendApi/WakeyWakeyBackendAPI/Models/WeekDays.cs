namespace WakeyWakeyBackendAPI.Models;

/// <summary>A bitmask for representing days of a week in a compact manner.</summary>
[Flags]
public enum WeekDays
{
    /// <summary>Bitmask for no particular day of the week.</summary>
    None = 0,
    
    /// <summary>Bitmask for Sunday.</summary>
    Sunday = 1 << 0,
    
    /// <summary>Bitmask for Monday.</summary>
    Monday = 1 << 1,
    
    /// <summary>Bitmask for Tuesday.</summary>
    Tuesday = 1 << 2,
    
    /// <summary>Bitmask for Wednesday.</summary>
    Wednesday = 1 << 3,
    
    /// <summary>Bitmask for Thursday.</summary>
    Thursday = 1 << 4,
    
    /// <summary>Bitmask for Friday.</summary>
    Friday = 1 << 5,
    
    /// <summary>Bitmask for Saturday.</summary>
    Saturday = 1 << 6,
    
    /// <summary>Bitmask for all days of the week.</summary>
    All = Sunday | Monday | Tuesday | Wednesday | Thursday | Friday | Saturday
}
