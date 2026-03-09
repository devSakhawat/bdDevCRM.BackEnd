using bdDevCRM.ServiceContract.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace bdDevCRM.Service.Caching;

/// <summary>
/// Enterprise distributed cache implementation using IDistributedCache
/// Supports Redis, SQL Server, and other distributed cache providers
/// </summary>
public class DistributedCacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<DistributedCacheService> _logger;
    private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(5);

    public DistributedCacheService(
        IDistributedCache cache,
        ILogger<DistributedCacheService> logger)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<T> GetOrCreateAsync<T>(
        string key,
        Func<Task<T>> factory,
        TimeSpan? expiration = null)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Cache key cannot be empty", nameof(key));

        try
        {
            // Try to get from cache first
            var cachedValue = await _cache.GetStringAsync(key);

            if (!string.IsNullOrEmpty(cachedValue))
            {
                _logger.LogDebug("Cache hit for key: {CacheKey}", key);
                return JsonSerializer.Deserialize<T>(cachedValue)!;
            }

            _logger.LogDebug("Cache miss for key: {CacheKey}", key);

            // Create new value using factory
            var value = await factory();

            // Cache the result
            await SetAsync(key, value, expiration);

            return value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetOrCreateAsync for key: {CacheKey}", key);
            // On cache failure, return fresh value without caching
            return await factory();
        }
    }

    public async Task<(bool Success, T Value)> TryGetAsync<T>(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return (false, default!);

        try
        {
            var cachedValue = await _cache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cachedValue))
                return (false, default!);

            var value = JsonSerializer.Deserialize<T>(cachedValue);
            return (true, value!);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in TryGetAsync for key: {CacheKey}", key);
            return (false, default!);
        }
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Cache key cannot be empty", nameof(key));

        try
        {
            var serialized = JsonSerializer.Serialize(value);
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? DefaultExpiration
            };

            await _cache.SetStringAsync(key, serialized, options);
            _logger.LogDebug("Cached value for key: {CacheKey} with expiration: {Expiration}",
                key, options.AbsoluteExpirationRelativeToNow);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SetAsync for key: {CacheKey}", key);
            // Swallow exception - cache failure shouldn't break the application
        }
    }

    public async Task RemoveAsync(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return;

        try
        {
            await _cache.RemoveAsync(key);
            _logger.LogDebug("Removed cache key: {CacheKey}", key);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in RemoveAsync for key: {CacheKey}", key);
        }
    }

    public async Task RemoveByPrefixAsync(string prefix)
    {
        if (string.IsNullOrWhiteSpace(prefix))
            return;

        // Note: IDistributedCache doesn't support pattern-based deletion
        // This would require Redis-specific implementation or custom tracking
        _logger.LogWarning(
            "RemoveByPrefixAsync not fully supported by IDistributedCache. " +
            "Consider using Redis-specific implementation for pattern deletion. Prefix: {Prefix}",
            prefix);

        // TODO: Implement Redis-specific pattern deletion if using Redis
        await Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            return false;

        try
        {
            var value = await _cache.GetStringAsync(key);
            return !string.IsNullOrEmpty(value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in ExistsAsync for key: {CacheKey}", key);
            return false;
        }
    }

    public async Task<TimeSpan?> GetTimeToLiveAsync(string key)
    {
        // Note: IDistributedCache doesn't provide TTL information
        // This would require Redis-specific implementation
        _logger.LogWarning(
            "GetTimeToLiveAsync not supported by IDistributedCache. " +
            "Consider using Redis-specific implementation. Key: {CacheKey}",
            key);

        await Task.CompletedTask;
        return null;
    }
}
