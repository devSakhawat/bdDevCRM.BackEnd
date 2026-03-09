namespace bdDevCRM.ServiceContract.Caching;

/// <summary>
/// Enterprise-level cache abstraction supporting multiple cache strategies
/// Provides unified interface for in-memory and distributed caching
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Gets cached value or creates it using the factory function
    /// </summary>
    /// <typeparam name="T">Type of cached value</typeparam>
    /// <param name="key">Unique cache key</param>
    /// <param name="factory">Factory function to create value if not cached</param>
    /// <param name="expiration">Optional expiration time (default: 5 minutes)</param>
    /// <returns>Cached or newly created value</returns>
    Task<T> GetOrCreateAsync<T>(
        string key,
        Func<Task<T>> factory,
        TimeSpan? expiration = null);

    /// <summary>
    /// Tries to get a value from cache
    /// </summary>
    /// <typeparam name="T">Type of cached value</typeparam>
    /// <param name="key">Cache key</param>
    /// <returns>Tuple with success flag and value</returns>
    Task<(bool Success, T Value)> TryGetAsync<T>(string key);

    /// <summary>
    /// Sets a value in cache
    /// </summary>
    /// <typeparam name="T">Type of value to cache</typeparam>
    /// <param name="key">Cache key</param>
    /// <param name="value">Value to cache</param>
    /// <param name="expiration">Optional expiration time</param>
    Task SetAsync<T>(string key, T value, TimeSpan? expiration = null);

    /// <summary>
    /// Removes a specific key from cache
    /// </summary>
    /// <param name="key">Cache key to remove</param>
    Task RemoveAsync(string key);

    /// <summary>
    /// Removes all keys matching a prefix pattern
    /// Useful for invalidating related cache entries (e.g., "menu:user:*")
    /// </summary>
    /// <param name="prefix">Key prefix pattern</param>
    Task RemoveByPrefixAsync(string prefix);

    /// <summary>
    /// Checks if a key exists in cache
    /// </summary>
    /// <param name="key">Cache key</param>
    /// <returns>True if key exists</returns>
    Task<bool> ExistsAsync(string key);

    /// <summary>
    /// Gets remaining time to live for a cached key
    /// </summary>
    /// <param name="key">Cache key</param>
    /// <returns>Remaining TTL or null if key doesn't exist</returns>
    Task<TimeSpan?> GetTimeToLiveAsync(string key);
}
