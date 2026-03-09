using bdDevCRM.ServiceContract.Caching;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace bdDevCRM.Service.Caching;

/// <summary>
/// Enterprise hybrid cache service combining in-memory (L1) and distributed (L2) caching
/// Provides fast local access with distributed consistency
/// </summary>
public class HybridCacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;
    private readonly ICacheService _distributedCache;
    private readonly ILogger<HybridCacheService> _logger;
    private static readonly TimeSpan DefaultMemoryCacheExpiration = TimeSpan.FromMinutes(2);

    public HybridCacheService(
        IMemoryCache memoryCache,
        ICacheService distributedCache,
        ILogger<HybridCacheService> logger)
    {
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<T> GetOrCreateAsync<T>(
        string key,
        Func<Task<T>> factory,
        TimeSpan? expiration = null)
    {
        // L1: Check memory cache first (fastest)
        if (_memoryCache.TryGetValue(key, out T memoryValue))
        {
            _logger.LogDebug("L1 cache hit (memory) for key: {CacheKey}", key);
            return memoryValue;
        }

        // L2: Check distributed cache
        var (distributedSuccess, distributedValue) = await _distributedCache.TryGetAsync<T>(key);
        if (distributedSuccess)
        {
            _logger.LogDebug("L2 cache hit (distributed) for key: {CacheKey}", key);

            // Populate L1 cache
            var memoryCacheExpiration = expiration < DefaultMemoryCacheExpiration
                ? expiration
                : DefaultMemoryCacheExpiration;

            _memoryCache.Set(key, distributedValue, memoryCacheExpiration ?? DefaultMemoryCacheExpiration);

            return distributedValue;
        }

        _logger.LogDebug("Cache miss (L1 + L2) for key: {CacheKey}", key);

        // Create new value
        var value = await factory();

        // Store in both caches
        await SetAsync(key, value, expiration);

        return value;
    }

    public async Task<(bool Success, T Value)> TryGetAsync<T>(string key)
    {
        // Check L1 first
        if (_memoryCache.TryGetValue(key, out T memoryValue))
        {
            return (true, memoryValue);
        }

        // Check L2
        var result = await _distributedCache.TryGetAsync<T>(key);
        if (result.Success)
        {
            // Populate L1
            _memoryCache.Set(key, result.Value, DefaultMemoryCacheExpiration);
        }

        return result;
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        // Set in L1 (memory) with shorter expiration
        var memoryCacheExpiration = expiration < DefaultMemoryCacheExpiration
            ? expiration
            : DefaultMemoryCacheExpiration;

        _memoryCache.Set(key, value, memoryCacheExpiration ?? DefaultMemoryCacheExpiration);

        // Set in L2 (distributed)
        await _distributedCache.SetAsync(key, value, expiration);

        _logger.LogDebug("Set value in hybrid cache for key: {CacheKey}", key);
    }

    public async Task RemoveAsync(string key)
    {
        // Remove from both caches
        _memoryCache.Remove(key);
        await _distributedCache.RemoveAsync(key);

        _logger.LogDebug("Removed key from hybrid cache: {CacheKey}", key);
    }

    public async Task RemoveByPrefixAsync(string prefix)
    {
        // Note: Memory cache doesn't support prefix-based removal efficiently
        // Consider implementing a tracking mechanism if needed
        _logger.LogWarning("RemoveByPrefixAsync has limited support in hybrid cache. Prefix: {Prefix}", prefix);

        await _distributedCache.RemoveByPrefixAsync(prefix);
    }

    public async Task<bool> ExistsAsync(string key)
    {
        // Check L1 first
        if (_memoryCache.TryGetValue(key, out _))
            return true;

        // Check L2
        return await _distributedCache.ExistsAsync(key);
    }

    public async Task<TimeSpan?> GetTimeToLiveAsync(string key)
    {
        // Delegate to distributed cache (memory cache doesn't expose TTL)
        return await _distributedCache.GetTimeToLiveAsync(key);
    }
}
