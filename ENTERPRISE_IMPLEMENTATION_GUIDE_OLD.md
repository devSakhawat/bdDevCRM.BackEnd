# bdDevCRM Backend - Enterprise Implementation Guide

## সূচিপত্র
1. [Configuration Security (appsettings.json)](#1-configuration-security)
2. [Rate Limiting Strategy](#2-rate-limiting-strategy)
3. [Distributed Caching Strategy](#3-distributed-caching-strategy)
4. [Audit Logging Implementation](#4-audit-logging-implementation)
5. [Performance Monitoring](#5-performance-monitoring)
6. [Database Optimization](#6-database-optimization)
7. [NLog vs Serilog Comparison](#7-nlog-vs-serilog)

---

## 1. Configuration Security (appsettings.json)

### 🔴 বর্তমান সমস্যা:
```json
// ⚠️ SECURITY RISK - appsettings.json-এ sensitive data
{
  "ConnectionStrings": {
    "DbLocation": "Server=DESKTOP-J6U53UO\\MSSQLSERVER2022; User ID=sa; password=bdDevs@3011; ..."
  },
  "Jwt": {
    "SecretKey": "WeAreBangladashiDevelopersWeAreActiveWeAreProductive3011"
  }
}
```

### ✅ Enterprise-Level Solution:

#### Option 1: User Secrets (Development Environment)
**কেন ব্যবহার করবেন:**
- Development environment-এ secrets source code থেকে আলাদা রাখা
- Git-এ commit হবে না
- Team members-দের নিজস্ব configuration থাকতে পারে

**কিভাবে implement করবেন:**

```bash
# 1. User Secrets initialize করুন
cd bdDevCRM.Api
dotnet user-secrets init

# 2. Secrets add করুন
dotnet user-secrets set "ConnectionStrings:DbLocation" "Server=YOUR_SERVER;User ID=sa;Password=YOUR_PASSWORD;Database=dbDevCRM;TrustServerCertificate=True;"
dotnet user-secrets set "Jwt:SecretKey" "YOUR-SECURE-SECRET-KEY-AT-LEAST-32-CHARACTERS-LONG"

# 3. Verify secrets
dotnet user-secrets list
```

**Configuration reading (automatic):**
```csharp
// Program.cs - .NET 8 automatically reads user secrets in Development
// No code changes needed! Configuration reads in this order:
// 1. appsettings.json
// 2. appsettings.{Environment}.json
// 3. User Secrets (Development only)
// 4. Environment Variables
// 5. Command-line arguments
```

#### Option 2: Azure Key Vault (Production Environment)

**কেন ব্যবহার করবেন:**
- Enterprise-grade secret management
- Centralized secret storage
- Access audit trails
- Automatic secret rotation
- Role-based access control (RBAC)

**Implementation:**

```bash
# 1. Install Azure Key Vault package
dotnet add package Azure.Extensions.AspNetCore.Configuration.Secrets
dotnet add package Azure.Identity
```

```csharp
// Program.cs
using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets;

var builder = WebApplication.CreateBuilder(args);

// Configure Azure Key Vault
if (builder.Environment.IsProduction())
{
    var keyVaultUri = new Uri(builder.Configuration["KeyVault:Uri"]);
    builder.Configuration.AddAzureKeyVault(
        keyVaultUri,
        new DefaultAzureCredential(),
        new AzureKeyVaultConfigurationOptions
        {
            ReloadInterval = TimeSpan.FromMinutes(5) // Auto-reload secrets
        });
}
```

**Azure Key Vault Setup:**
```bash
# Create Key Vault
az keyvault create --name bdDevCRM-KeyVault --resource-group bdDevCRM-RG --location eastus

# Add secrets
az keyvault secret set --vault-name bdDevCRM-KeyVault --name "ConnectionStrings--DbLocation" --value "Server=..."
az keyvault secret set --vault-name bdDevCRM-KeyVault --name "Jwt--SecretKey" --value "YOUR-SECRET-KEY"

# Grant access to your app
az keyvault set-policy --name bdDevCRM-KeyVault --spn YOUR-APP-ID --secret-permissions get list
```

#### Option 3: Environment Variables (Docker/Kubernetes)

**কিভাবে ব্যবহার করবেন:**

```bash
# Linux/Mac
export ConnectionStrings__DbLocation="Server=...;Password=..."
export Jwt__SecretKey="YOUR-SECRET-KEY"

# Windows
setx ConnectionStrings__DbLocation "Server=...;Password=..."
setx Jwt__SecretKey "YOUR-SECRET-KEY"
```

**Docker Compose:**
```yaml
# docker-compose.yml
version: '3.8'
services:
  api:
    image: bddevcrm-api
    environment:
      - ConnectionStrings__DbLocation=${DB_CONNECTION_STRING}
      - Jwt__SecretKey=${JWT_SECRET_KEY}
    env_file:
      - .env  # Never commit this file!
```

**Kubernetes Secret:**
```yaml
apiVersion: v1
kind: Secret
metadata:
  name: bddevcrm-secrets
type: Opaque
data:
  connection-string: <base64-encoded-value>
  jwt-secret: <base64-encoded-value>
```

#### ✅ Updated appsettings.json (Clean Version):

```json
{
  "UserCache": {
    "SlidingExpirationHours": 5,
    "AbsoluteExpirationHours": 5,
    "Priority": "High"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DbLocation": "" // Will be overridden by User Secrets/Key Vault/Environment Variables
  },
  "Jwt": {
    "Issuer": "http://localhost:7290",
    "Audience": "https://localhost:44381",
    "AccessTokenExpiryMinutes": 15,
    "RefreshTokenExpiryDays": 7,
    "SecretKey": "" // Will be overridden
  },
  "Cors": {
    "AllowedOrigins": [
      "http://localhost:4200",
      "https://localhost:4200"
    ]
  },
  "TokenCleanup": {
    "IntervalHours": 24,
    "RetryDelayMinutes": 5
  },
  "AppSettings": {
    "controlPanelModuleId": 1
  },
  "KeyVault": {
    "Uri": "https://bddevcrm-keyvault.vault.azure.net/" // Production only
  }
}
```

### 📋 Configuration Validation

**Startup validation যোগ করুন:**

```csharp
// Models/Configuration/JwtSettings.cs
using System.ComponentModel.DataAnnotations;

public class JwtSettings
{
    [Required]
    [MinLength(32, ErrorMessage = "JWT SecretKey must be at least 32 characters")]
    public string SecretKey { get; set; } = string.Empty;

    [Required]
    public string Issuer { get; set; } = string.Empty;

    [Required]
    public string Audience { get; set; } = string.Empty;

    [Range(1, 1440)]
    public int AccessTokenExpiryMinutes { get; set; }

    [Range(1, 365)]
    public int RefreshTokenExpiryDays { get; set; }
}

public class ConnectionStringSettings
{
    [Required(ErrorMessage = "Database connection string is required")]
    public string DbLocation { get; set; } = string.Empty;
}

// Program.cs - Add validation
builder.Services.AddOptions<JwtSettings>()
    .BindConfiguration("Jwt")
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<ConnectionStringSettings>()
    .BindConfiguration("ConnectionStrings")
    .ValidateDataAnnotations()
    .ValidateOnStart();
```

### 🔐 Security Best Practices:

1. **Never commit secrets to Git**
   ```bash
   # .gitignore
   appsettings.*.json
   !appsettings.json
   !appsettings.Development.json
   .env
   secrets.json
   ```

2. **Use different secrets per environment**
   - Development: User Secrets
   - Staging: Azure Key Vault (separate vault)
   - Production: Azure Key Vault (production vault)

3. **Rotate secrets regularly**
   - JWT Secret: Every 90 days
   - Database passwords: Every 180 days
   - API keys: Every 90 days

4. **Audit secret access**
   - Enable Azure Key Vault logging
   - Monitor who accessed what secrets
   - Alert on suspicious access patterns

---

## 2. Rate Limiting Strategy

### 🎯 কেন Rate Limiting প্রয়োজন?

**সমস্যা যা সমাধান করে:**
1. **Brute Force Attacks** - Login endpoint-এ unlimited password attempts
2. **DDoS Attacks** - Server overload থেকে রক্ষা
3. **API Abuse** - Resource exhaustion prevent করা
4. **Cost Control** - Cloud infrastructure costs কমানো
5. **Fair Usage** - All users-কে equal access ensure করা

**Real-world Examples:**
- GitHub API: 5,000 requests/hour per user
- Twitter API: 300 requests/15 minutes
- Stripe API: 100 requests/second

### 📊 Rate Limiting Types:

#### 1. Fixed Window
```
Time Window: 1 minute
Limit: 100 requests

00:00 - 00:59 → 100 requests allowed
01:00 - 01:59 → 100 requests allowed (counter resets)
```
**✅ Good:** Simple, predictable
**❌ Bad:** Burst traffic at window boundaries

#### 2. Sliding Window
```
Time Window: 1 minute (rolling)
Limit: 100 requests

Request at 00:30 → Counts requests from 23:30 to 00:30
Request at 00:45 → Counts requests from 23:45 to 00:45
```
**✅ Good:** Smooth distribution, prevents bursts
**❌ Bad:** Slightly more complex

#### 3. Token Bucket
```
Bucket Capacity: 100 tokens
Refill Rate: 10 tokens/second

Each request = 1 token
Burst allowed up to bucket capacity
```
**✅ Good:** Allows controlled bursts
**❌ Bad:** More complex implementation

#### 4. Concurrency Limiter
```
Max Concurrent Requests: 50

Limits simultaneous requests
Perfect for resource-intensive operations
```
**✅ Good:** Protects from parallel overload
**❌ Bad:** Not time-based

### 🚀 .NET 8 Built-in Rate Limiting Implementation

#### Step 1: Install Package (Already installed)
```xml
<!-- Already in bdDevCRM.Api.csproj -->
<PackageReference Include="Microsoft.AspNetCore.RateLimiting" Version="8.0.13" />
```

#### Step 2: Create RateLimitingConfiguration.cs

```csharp
// Extensions/RateLimitingConfiguration.cs
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

namespace bdDevCRM.Api.Extensions;

public static class RateLimitingConfiguration
{
    public static IServiceCollection ConfigureRateLimiting(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRateLimiter(options =>
        {
            // 1. Global Default Policy - Fixed Window
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
            {
                // Partition by IP address for unauthenticated requests
                var partitionKey = context.User?.Identity?.Name ?? context.Connection.RemoteIpAddress?.ToString() ?? "anonymous";

                return RateLimitPartition.GetFixedWindowLimiter(partitionKey, _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 100,
                    Window = TimeSpan.FromMinutes(1),
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    QueueLimit = 10
                });
            });

            // 2. General API Policy - Sliding Window (Recommended for most endpoints)
            options.AddPolicy("general", context =>
            {
                var username = context.User?.Identity?.Name ?? context.Connection.RemoteIpAddress?.ToString() ?? "anonymous";

                return RateLimitPartition.GetSlidingWindowLimiter(username, _ => new SlidingWindowRateLimiterOptions
                {
                    PermitLimit = 100,
                    Window = TimeSpan.FromMinutes(1),
                    SegmentsPerWindow = 6, // 10-second segments
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    QueueLimit = 5
                });
            });

            // 3. Authentication Endpoints - Strict Limiting (Prevent Brute Force)
            options.AddPolicy("authentication", context =>
            {
                var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "anonymous";

                return RateLimitPartition.GetFixedWindowLimiter(ipAddress, _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 5,
                    Window = TimeSpan.FromMinutes(1),
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    QueueLimit = 0 // No queuing for auth endpoints
                });
            });

            // 4. Heavy Operations - Token Bucket (Report generation, exports, etc.)
            options.AddPolicy("heavy", context =>
            {
                var username = context.User?.Identity?.Name ?? context.Connection.RemoteIpAddress?.ToString() ?? "anonymous";

                return RateLimitPartition.GetTokenBucketLimiter(username, _ => new TokenBucketRateLimiterOptions
                {
                    TokenLimit = 10,
                    TokensPerPeriod = 2,
                    ReplenishmentPeriod = TimeSpan.FromMinutes(1),
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    QueueLimit = 3
                });
            });

            // 5. File Upload - Concurrency Limiter
            options.AddPolicy("upload", context =>
            {
                var username = context.User?.Identity?.Name ?? "anonymous";

                return RateLimitPartition.GetConcurrencyLimiter(username, _ => new ConcurrencyLimiterOptions
                {
                    PermitLimit = 3, // Max 3 simultaneous uploads per user
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    QueueLimit = 2
                });
            });

            // 6. Premium Users - Higher Limits (if you have paid tiers)
            options.AddPolicy("premium", context =>
            {
                var username = context.User?.Identity?.Name ?? context.Connection.RemoteIpAddress?.ToString() ?? "anonymous";

                // Check if user is premium (you would check database/claims here)
                var isPremium = context.User?.HasClaim("UserType", "Premium") ?? false;

                return RateLimitPartition.GetSlidingWindowLimiter(username, _ => new SlidingWindowRateLimiterOptions
                {
                    PermitLimit = isPremium ? 500 : 100, // 5x limit for premium users
                    Window = TimeSpan.FromMinutes(1),
                    SegmentsPerWindow = 6,
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    QueueLimit = isPremium ? 20 : 5
                });
            });

            // Custom rejection response
            options.OnRejected = async (context, cancellationToken) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;

                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                {
                    context.HttpContext.Response.Headers.RetryAfter = retryAfter.TotalSeconds.ToString();
                }

                var response = new
                {
                    error = "Too Many Requests",
                    message = "Rate limit exceeded. Please try again later.",
                    retryAfter = retryAfter?.TotalSeconds ?? 60
                };

                context.HttpContext.Response.ContentType = "application/json";
                await context.HttpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            };
        });

        return services;
    }
}
```

#### Step 3: Update Program.cs

```csharp
// Program.cs
// Add this line before var app = builder.Build();
builder.Services.ConfigureRateLimiting(builder.Configuration);

// Add this line after app.UseRouting(); and before app.UseAuthentication();
app.UseRateLimiter();
```

#### Step 4: Apply to Controllers

```csharp
// Controllers/AuthenticationController.cs
using Microsoft.AspNetCore.RateLimiting;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    // Apply strict rate limiting to login endpoint
    [HttpPost("login")]
    [EnableRateLimiting("authentication")] // 5 attempts per minute
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
    {
        // Login logic
    }

    // Apply general rate limiting to other endpoints
    [HttpPost("refresh-token")]
    [EnableRateLimiting("general")] // 100 requests per minute
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDTO request)
    {
        // Refresh token logic
    }
}

// Controllers/ReportController.cs
[ApiController]
[Route("api/[controller]")]
[EnableRateLimiting("heavy")] // Apply to entire controller
public class ReportController : ControllerBase
{
    [HttpPost("generate")]
    public async Task<IActionResult> GenerateReport([FromBody] ReportRequestDTO request)
    {
        // Heavy operation
    }
}

// Controllers/FileUploadController.cs
[ApiController]
[Route("api/[controller]")]
public class FileUploadController : ControllerBase
{
    [HttpPost("upload")]
    [EnableRateLimiting("upload")] // Concurrency limit
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        // Upload logic
    }
}

// Controllers/UsersController.cs
[ApiController]
[Route("api/[controller]")]
[EnableRateLimiting("general")] // Apply to entire controller
public class UsersController : ControllerBase
{
    // All endpoints in this controller will have 100 req/min limit
}
```

### 📊 Rate Limiting Strategy for Your Project:

```
┌─────────────────────────────────────────────────────────┐
│ Rate Limiting Hierarchy                                 │
├─────────────────────────────────────────────────────────┤
│ 1. Authentication Endpoints (Strictest)                 │
│    • Login: 5 attempts/minute per IP                    │
│    • Register: 3 attempts/minute per IP                 │
│    • Password Reset: 3 attempts/minute per IP           │
│                                                          │
│ 2. Heavy Operations (Token Bucket)                      │
│    • Report Generation: 10 tokens, +2/minute            │
│    • Data Export: 5 tokens, +1/minute                   │
│    • Bulk Operations: 10 tokens, +2/minute              │
│                                                          │
│ 3. File Operations (Concurrency)                        │
│    • Upload: 3 concurrent per user                      │
│    • Download: 5 concurrent per user                    │
│                                                          │
│ 4. General API (Sliding Window)                         │
│    • CRUD Operations: 100 requests/minute               │
│    • Search: 50 requests/minute                         │
│    • List: 100 requests/minute                          │
│                                                          │
│ 5. Public Endpoints (Most Lenient)                      │
│    • Health Check: Unlimited                            │
│    • Static Files: 500 requests/minute                  │
└─────────────────────────────────────────────────────────┘
```

### ✅ Good Side (Advantages):

1. **Security Protection**
   - Prevents brute force attacks
   - Stops credential stuffing
   - Protects against DDoS

2. **Resource Management**
   - Prevents server overload
   - Fair resource distribution
   - Predictable performance

3. **Cost Control**
   - Reduces infrastructure costs
   - Prevents abuse-related expenses
   - Better capacity planning

4. **User Experience**
   - System remains responsive
   - No single user can degrade service
   - Predictable API behavior

5. **Compliance**
   - Many regulations require rate limiting
   - Audit trail of access patterns
   - Demonstrates security controls

### ❌ Bad Side (Disadvantages):

1. **Legitimate Users Blocked**
   - Power users may hit limits
   - Automated testing can fail
   - Integration partners affected

2. **Implementation Complexity**
   - Need distributed rate limiting for multiple servers
   - State management across instances
   - Edge cases handling

3. **False Positives**
   - Shared IP addresses (NAT, corporate proxies)
   - Multiple users behind same IP
   - Legitimate high-volume use cases

4. **Additional Infrastructure**
   - Redis for distributed limiting
   - Monitoring and alerting
   - Logging overhead

### 🔧 Main Responsibilities:

1. **Request Counting**
   - Track requests per user/IP
   - Maintain counters in memory/Redis
   - Reset counters per time window

2. **Limit Enforcement**
   - Block requests exceeding limits
   - Return 429 status code
   - Provide Retry-After header

3. **Monitoring & Logging**
   - Log rate limit violations
   - Alert on suspicious patterns
   - Track legitimate user impacts

4. **Configuration Management**
   - Different limits per endpoint
   - User-specific limits
   - Dynamic limit adjustments

### 🎓 Enterprise Best Practices:

#### 1. Distributed Rate Limiting (Multi-Server)

```csharp
// Use Redis for distributed rate limiting
services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = configuration.GetConnectionString("Redis");
    options.InstanceName = "RateLimiting:";
});

// Custom distributed rate limiter using Redis
public class RedisRateLimiter
{
    private readonly IDistributedCache _cache;

    public async Task<bool> IsAllowedAsync(string key, int limit, TimeSpan window)
    {
        var count = await _cache.GetStringAsync(key);
        var currentCount = string.IsNullOrEmpty(count) ? 0 : int.Parse(count);

        if (currentCount >= limit)
            return false;

        await _cache.SetStringAsync(key, (currentCount + 1).ToString(),
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = window });

        return true;
    }
}
```

#### 2. Rate Limit Headers (Industry Standard)

```csharp
// Add rate limit info to response headers
options.OnRejected = async (context, cancellationToken) =>
{
    var response = context.HttpContext.Response;
    response.StatusCode = StatusCodes.Status429TooManyRequests;

    // Standard headers (GitHub, Twitter style)
    response.Headers["X-RateLimit-Limit"] = "100";
    response.Headers["X-RateLimit-Remaining"] = "0";
    response.Headers["X-RateLimit-Reset"] = DateTimeOffset.UtcNow.AddMinutes(1).ToUnixTimeSeconds().ToString();

    if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
    {
        response.Headers["Retry-After"] = retryAfter.TotalSeconds.ToString();
    }
};
```

#### 3. Monitoring & Alerting

```csharp
// Log rate limit violations
public class RateLimitLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RateLimitLoggingMiddleware> _logger;

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
        {
            _logger.LogWarning(
                "Rate limit exceeded for {User} from {IP} on {Path}",
                context.User?.Identity?.Name ?? "Anonymous",
                context.Connection.RemoteIpAddress,
                context.Request.Path);
        }
    }
}
```

#### 4. Bypass for Internal/Admin Users

```csharp
options.AddPolicy("bypass", context =>
{
    // Check if user is admin or internal
    if (context.User?.HasClaim("Role", "Admin") ?? false)
    {
        return RateLimitPartition.GetNoLimiter<string>("admin");
    }

    // Regular users get normal limits
    return RateLimitPartition.GetFixedWindowLimiter(
        context.User?.Identity?.Name ?? "anonymous",
        _ => new FixedWindowRateLimiterOptions { PermitLimit = 100, Window = TimeSpan.FromMinutes(1) });
});
```

---

## 3. Distributed Caching Strategy

### 🎯 আপনার Project-এর জন্য Caching Strategy

#### বর্তমান অবস্থা:
- ✅ `Microsoft.Extensions.Caching.StackExchangeRedis` installed
- ✅ `IMemoryCache` ব্যবহার হচ্ছে
- ❌ Redis configured নয়
- ❌ Cache invalidation strategy নেই

### 📊 3-Tier Caching Architecture (Recommended)

```
┌─────────────────────────────────────────────────────────┐
│ L1: In-Memory Cache (Fastest)                           │
│ • Hot data (frequently accessed)                        │
│ • TTL: 5-15 minutes                                     │
│ • Size: ~100MB per server                               │
│ • Availability: Single server only                      │
└─────────────────────────────────────────────────────────┘
                         ↓ (miss)
┌─────────────────────────────────────────────────────────┐
│ L2: Redis Distributed Cache (Fast)                      │
│ • Warm data (session shared across servers)            │
│ • TTL: 1-24 hours                                       │
│ • Size: ~1-10GB                                         │
│ • Availability: Shared across all servers               │
└─────────────────────────────────────────────────────────┘
                         ↓ (miss)
┌─────────────────────────────────────────────────────────┐
│ L3: Database (Slow)                                     │
│ • Cold data (original source)                           │
│ • TTL: Permanent                                        │
│ • Size: Unlimited                                       │
│ • Availability: Primary data store                      │
└─────────────────────────────────────────────────────────┘
```

### 🚀 Implementation

#### Step 1: appsettings.json Configuration

```json
{
  "Redis": {
    "Configuration": "localhost:6379",
    "InstanceName": "bdDevCRM:"
  },
  "CacheSettings": {
    "DefaultExpirationMinutes": 60,
    "EnableDistributedCache": true,
    "EnableL1Cache": true,
    "L1CacheSizeMB": 100,
    "CacheProfiles": {
      "Static": {
        "ExpirationHours": 24,
        "Priority": "High"
      },
      "User": {
        "ExpirationHours": 4,
        "Priority": "High"
      },
      "Dynamic": {
        "ExpirationMinutes": 15,
        "Priority": "Normal"
      },
      "Session": {
        "ExpirationMinutes": 30,
        "Priority": "High"
      }
    }
  }
}
```

#### Step 2: Create Hybrid Cache Service

```csharp
// Services/Caching/HybridCacheService.cs
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace bdDevCRM.Service.Caching;

public interface IHybridCacheService
{
    Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiry = null, CacheProfile profile = CacheProfile.Default);
    Task<T?> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);
    Task RemoveAsync(string key);
    Task RemoveByPrefixAsync(string prefix);
}

public enum CacheProfile
{
    Default,
    Static,      // 24 hours - Countries, Currencies, System Settings
    User,        // 4 hours - User profile, Permissions
    Dynamic,     // 15 minutes - Dashboard stats, Recent activities
    Session      // 30 minutes - User session data
}

public class HybridCacheService : IHybridCacheService
{
    private readonly IMemoryCache _memoryCache;
    private readonly IDistributedCache _distributedCache;
    private readonly ILogger<HybridCacheService> _logger;
    private readonly IConfiguration _configuration;
    private readonly bool _enableL1Cache;
    private readonly bool _enableDistributedCache;

    public HybridCacheService(
        IMemoryCache memoryCache,
        IDistributedCache distributedCache,
        ILogger<HybridCacheService> logger,
        IConfiguration configuration)
    {
        _memoryCache = memoryCache;
        _distributedCache = distributedCache;
        _logger = logger;
        _configuration = configuration;
        _enableL1Cache = configuration.GetValue<bool>("CacheSettings:EnableL1Cache", true);
        _enableDistributedCache = configuration.GetValue<bool>("CacheSettings:EnableDistributedCache", false);
    }

    public async Task<T?> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiry = null, CacheProfile profile = CacheProfile.Default)
    {
        var cacheKey = GetCacheKey(key);
        var expiryTime = expiry ?? GetExpiryForProfile(profile);

        // L1: Try memory cache first (fastest)
        if (_enableL1Cache && _memoryCache.TryGetValue<T>(cacheKey, out var memoryValue))
        {
            _logger.LogDebug("Cache HIT (L1 Memory): {Key}", cacheKey);
            return memoryValue;
        }

        // L2: Try distributed cache (Redis)
        if (_enableDistributedCache)
        {
            try
            {
                var distributedValue = await _distributedCache.GetStringAsync(cacheKey);
                if (!string.IsNullOrEmpty(distributedValue))
                {
                    _logger.LogDebug("Cache HIT (L2 Redis): {Key}", cacheKey);
                    var value = JsonSerializer.Deserialize<T>(distributedValue);

                    // Store in L1 cache for faster subsequent access
                    if (_enableL1Cache && value != null)
                    {
                        var l1Expiry = TimeSpan.FromMinutes(5); // L1 cache expires faster
                        _memoryCache.Set(cacheKey, value, l1Expiry);
                    }

                    return value;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading from distributed cache for key: {Key}", cacheKey);
            }
        }

        // L3: Get from source (database/API)
        _logger.LogDebug("Cache MISS: {Key} - Fetching from source", cacheKey);
        var data = await factory();

        if (data != null)
        {
            await SetAsync(cacheKey, data, expiryTime);
        }

        return data;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var cacheKey = GetCacheKey(key);

        // Try L1 first
        if (_enableL1Cache && _memoryCache.TryGetValue<T>(cacheKey, out var memoryValue))
        {
            return memoryValue;
        }

        // Try L2
        if (_enableDistributedCache)
        {
            try
            {
                var distributedValue = await _distributedCache.GetStringAsync(cacheKey);
                if (!string.IsNullOrEmpty(distributedValue))
                {
                    var value = JsonSerializer.Deserialize<T>(distributedValue);

                    if (_enableL1Cache && value != null)
                    {
                        _memoryCache.Set(cacheKey, value, TimeSpan.FromMinutes(5));
                    }

                    return value;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading from distributed cache for key: {Key}", cacheKey);
            }
        }

        return default;
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        var cacheKey = GetCacheKey(key);
        var expiryTime = expiry ?? TimeSpan.FromHours(1);

        // Set in L1 (Memory)
        if (_enableL1Cache)
        {
            var l1Expiry = expiryTime > TimeSpan.FromMinutes(5)
                ? TimeSpan.FromMinutes(5)
                : expiryTime;

            _memoryCache.Set(cacheKey, value, l1Expiry);
        }

        // Set in L2 (Redis)
        if (_enableDistributedCache)
        {
            try
            {
                var serialized = JsonSerializer.Serialize(value);
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expiryTime
                };
                await _distributedCache.SetStringAsync(cacheKey, serialized, options);
                _logger.LogDebug("Cache SET: {Key} (Expiry: {Expiry})", cacheKey, expiryTime);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error writing to distributed cache for key: {Key}", cacheKey);
            }
        }
    }

    public async Task RemoveAsync(string key)
    {
        var cacheKey = GetCacheKey(key);

        // Remove from L1
        if (_enableL1Cache)
        {
            _memoryCache.Remove(cacheKey);
        }

        // Remove from L2
        if (_enableDistributedCache)
        {
            try
            {
                await _distributedCache.RemoveAsync(cacheKey);
                _logger.LogDebug("Cache REMOVED: {Key}", cacheKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing from distributed cache for key: {Key}", cacheKey);
            }
        }
    }

    public async Task RemoveByPrefixAsync(string prefix)
    {
        // Note: This requires Redis SCAN command or tracking keys separately
        // For now, log that pattern-based removal is requested
        _logger.LogWarning("Pattern-based cache removal requested for prefix: {Prefix}. Implement using Redis SCAN or key tracking.", prefix);

        // You could implement this using StackExchange.Redis directly:
        // var redis = ConnectionMultiplexer.Connect(configuration);
        // var server = redis.GetServer(redis.GetEndPoints().First());
        // var keys = server.Keys(pattern: $"{prefix}*");
        // foreach (var key in keys) await _distributedCache.RemoveAsync(key);
    }

    private string GetCacheKey(string key)
    {
        var instanceName = _configuration["Redis:InstanceName"] ?? "bdDevCRM:";
        return $"{instanceName}{key}";
    }

    private TimeSpan GetExpiryForProfile(CacheProfile profile)
    {
        return profile switch
        {
            CacheProfile.Static => TimeSpan.FromHours(_configuration.GetValue<int>("CacheSettings:CacheProfiles:Static:ExpirationHours", 24)),
            CacheProfile.User => TimeSpan.FromHours(_configuration.GetValue<int>("CacheSettings:CacheProfiles:User:ExpirationHours", 4)),
            CacheProfile.Dynamic => TimeSpan.FromMinutes(_configuration.GetValue<int>("CacheSettings:CacheProfiles:Dynamic:ExpirationMinutes", 15)),
            CacheProfile.Session => TimeSpan.FromMinutes(_configuration.GetValue<int>("CacheSettings:CacheProfiles:Session:ExpirationMinutes", 30)),
            _ => TimeSpan.FromHours(_configuration.GetValue<int>("CacheSettings:DefaultExpirationMinutes", 60))
        };
    }
}
```

#### Step 3: Configure in ServiceExtensions.cs

```csharp
// Extensions/ServiceExtensions.cs
public static void ConfigureDistributedCache(this IServiceCollection services, IConfiguration configuration)
{
    var enableDistributedCache = configuration.GetValue<bool>("CacheSettings:EnableDistributedCache", false);

    if (enableDistributedCache)
    {
        // Configure Redis
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration["Redis:Configuration"];
            options.InstanceName = configuration["Redis:InstanceName"];
        });
    }
    else
    {
        // Fallback to in-memory distributed cache for development
        services.AddDistributedMemoryCache();
    }

    // Register hybrid cache service
    services.AddSingleton<IHybridCacheService, HybridCacheService>();
}
```

#### Step 4: Update Program.cs

```csharp
// Program.cs
// Add after builder.Services.AddMemoryCache();
builder.Services.ConfigureDistributedCache(builder.Configuration);
```

#### Step 5: Usage Examples

```csharp
// Services/UsersService.cs
public class UsersService : IUsersService
{
    private readonly IRepositoryManager _repository;
    private readonly IHybridCacheService _cache;
    private readonly ILogger<UsersService> _logger;

    public UsersService(
        IRepositoryManager repository,
        IHybridCacheService cache,
        ILogger<UsersService> logger)
    {
        _repository = repository;
        _cache = cache;
        _logger = logger;
    }

    public async Task<UsersDTO?> GetUserByIdAsync(int userId)
    {
        // Cache key pattern: entity:id
        var cacheKey = $"user:{userId}";

        return await _cache.GetOrSetAsync(
            key: cacheKey,
            factory: async () =>
            {
                var user = await _repository.Users.GetUserByIdAsync(userId, trackChanges: false);
                return user; // Will be cached automatically
            },
            profile: CacheProfile.User // 4 hours expiry
        );
    }

    public async Task<IEnumerable<CountryDTO>> GetCountriesAsync()
    {
        // Static data - cache for 24 hours
        return await _cache.GetOrSetAsync(
            key: "countries:all",
            factory: async () => await _repository.Countries.GetAllCountriesAsync(trackChanges: false),
            profile: CacheProfile.Static // 24 hours expiry
        );
    }

    public async Task<DashboardStatsDTO> GetDashboardStatsAsync(int userId)
    {
        // Dynamic data - cache for 15 minutes
        return await _cache.GetOrSetAsync(
            key: $"dashboard:stats:{userId}",
            factory: async () => await CalculateDashboardStatsAsync(userId),
            profile: CacheProfile.Dynamic // 15 minutes expiry
        );
    }

    public async Task<bool> UpdateUserAsync(int userId, UpdateUserDTO dto)
    {
        var result = await _repository.Users.UpdateUserAsync(userId, dto);

        if (result)
        {
            // Invalidate cache when data changes
            await _cache.RemoveAsync($"user:{userId}");

            // Optionally invalidate related caches
            await _cache.RemoveByPrefixAsync($"userlist:"); // All user lists
        }

        return result;
    }
}
```

### 📋 Cache Invalidation Strategies

#### 1. Time-based Expiration (TTL)
```csharp
// Automatic expiration after specified time
await _cache.SetAsync("key", value, TimeSpan.FromHours(1));
```

#### 2. Event-based Invalidation
```csharp
// Invalidate when data changes
public async Task UpdateProductAsync(int productId, UpdateProductDTO dto)
{
    await _repository.Products.UpdateAsync(productId, dto);

    // Invalidate specific item
    await _cache.RemoveAsync($"product:{productId}");

    // Invalidate related items
    await _cache.RemoveAsync($"product:details:{productId}");
    await _cache.RemoveByPrefixAsync("products:list:");
    await _cache.RemoveByPrefixAsync("categories:"); // If product affects categories
}
```

#### 3. Version-based Invalidation
```csharp
// Include version in cache key
var cacheKey = $"user:{userId}:v{userVersion}";
await _cache.GetOrSetAsync(cacheKey, () => GetUserAsync(userId));

// When updating, increment version
userVersion++;
```

#### 4. Dependency-based Invalidation
```csharp
// Track dependencies
public class CacheInvalidator
{
    private readonly IHybridCacheService _cache;
    private readonly Dictionary<string, List<string>> _dependencies;

    public async Task InvalidateDependencies(string key)
    {
        if (_dependencies.TryGetValue(key, out var dependentKeys))
        {
            foreach (var dependentKey in dependentKeys)
            {
                await _cache.RemoveAsync(dependentKey);
            }
        }
    }
}
```

### 🎯 Caching Strategy Matrix for Your Project:

| Data Type | Cache Profile | TTL | Invalidation Strategy |
|-----------|--------------|-----|----------------------|
| **Static Reference Data** |
| Countries | Static | 24h | Manual/Deploy |
| Currencies | Static | 24h | Manual/Deploy |
| System Settings | Static | 12h | Event (on update) |
| Status Lookups | Static | 24h | Event (on update) |
| **User Data** |
| User Profile | User | 4h | Event (on update) |
| User Permissions | User | 4h | Event (role change) |
| User Sessions | Session | 30min | Event (logout) |
| **Dynamic Data** |
| Dashboard Stats | Dynamic | 15min | Time-based |
| Recent Activities | Dynamic | 5min | Time-based |
| Search Results | Dynamic | 10min | Time-based |
| Notifications | Dynamic | 2min | Event (new notification) |
| **Business Data** |
| Company Details | User | 2h | Event (on update) |
| Employee List | User | 1h | Event (CRUD operations) |
| Application List | Dynamic | 30min | Event (status change) |

### 🏗️ Redis Setup for Production:

#### Docker Compose:
```yaml
version: '3.8'
services:
  redis:
    image: redis:7-alpine
    ports:
      - "6379:6379"
    command: redis-server --appendonly yes --requirepass your_password
    volumes:
      - redis-data:/data
    restart: always
    healthcheck:
      test: ["CMD", "redis-cli", "ping"]
      interval: 10s
      timeout: 3s
      retries: 3

volumes:
  redis-data:
```

#### Azure Redis Cache:
```bash
# Create Azure Redis Cache
az redis create \
  --name bddevcrm-redis \
  --resource-group bdDevCRM-RG \
  --location eastus \
  --sku Standard \
  --vm-size c1

# Get connection string
az redis list-keys --name bddevcrm-redis --resource-group bdDevCRM-RG
```

#### appsettings.Production.json:
```json
{
  "Redis": {
    "Configuration": "bddevcrm-redis.redis.cache.windows.net:6380,password=YOUR_KEY,ssl=True,abortConnect=False",
    "InstanceName": "bdDevCRM:"
  },
  "CacheSettings": {
    "EnableDistributedCache": true,
    "EnableL1Cache": true
  }
}
```

### 📊 Cache Performance Monitoring:

```csharp
// Add cache metrics
public class CacheMetricsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CacheMetricsMiddleware> _logger;

    public async Task InvokeAsync(HttpContext context)
    {
        var startTime = DateTime.UtcNow;
        await _next(context);
        var duration = (DateTime.UtcNow - startTime).TotalMilliseconds;

        // Log if request was cached (add custom header in controller)
        if (context.Response.Headers.TryGetValue("X-Cache-Status", out var cacheStatus))
        {
            _logger.LogInformation(
                "Cache {Status} - Path: {Path}, Duration: {Duration}ms",
                cacheStatus,
                context.Request.Path,
                duration);
        }
    }
}

// In controller
[HttpGet("{id}")]
public async Task<IActionResult> GetUser(int id)
{
    var user = await _usersService.GetUserByIdAsync(id);

    // Add cache header for monitoring
    Response.Headers["X-Cache-Status"] = "HIT"; // or "MISS"

    return Ok(user);
}
```

---

**(Continued in next sections...)**

এই guide-টি continue করব পরবর্তী sections-এ। এখন পর্যন্ত আমরা cover করেছি:
1. ✅ Configuration Security (Complete)
2. ✅ Rate Limiting Strategy (Complete)
3. ✅ Distributed Caching Strategy (Complete)

পরবর্তী sections আসছে:
4. Audit Logging Implementation
5. Performance Monitoring
6. Database Optimization
7. NLog vs Serilog Comparison

আপনি চাইলে এখনই implement শুরু করতে পারেন অথবা আমি বাকি sections complete করব?
