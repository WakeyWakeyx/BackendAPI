namespace WakeyWakeyBackendAPI.Dtos;

/// <summary>Wraps a list paginated items and related metadata.</summary>
/// <param name="NextCursor">The cursor to be used for getting the next page.</param>
/// <param name="PageSize">The number of items in this page.</param>
/// <param name="Items">The items in this page.</param>
/// <typeparam name="TItem">The type of the items in this page.</typeparam>
public record PagedDto<TItem>(
    int NextCursor,
    int PageSize,
    List<TItem> Items);
    