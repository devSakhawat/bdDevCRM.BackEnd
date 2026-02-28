# bdDevCRM Backend - Enterprise Implementation Guide (Part 2)

## 4. Audit Logging Implementation

### 🎯 কেন Complete Audit Logging প্রয়োজন?

**Compliance Requirements:**
- **GDPR**: Who accessed/modified personal data
- **SOC 2**: Security and availability controls
- **HIPAA**: Access to sensitive health information
- **PCI DSS**: Credit card data access tracking

**Business Benefits:**
- Investigate security incidents
- Track user activities
- Resolve disputes
- Performance analysis
- Legal evidence

### 📊 Complete Audit Trail Components:

```
┌──────────────────────────────────────────────────────────┐
│ Audit Log Entry Structure                                │
├──────────────────────────────────────────────────────────┤
│ • Who: UserId, Username, IP Address, User Agent         │
│ • What: Action (CREATE, UPDATE, DELETE, VIEW, LOGIN)    │
│ • When: Timestamp (UTC)                                  │
│ • Where: Endpoint, Module, Entity Type                   │
│ • Details: Before/After values (JSON)                    │
│ • Context: CorrelationId, SessionId, RequestId          │
│ • Result: Success/Failure, StatusCode, Error            │
└──────────────────────────────────────────────────────────┘
```

### 🗄️ Database Schema

#### Create AuditLog Entity:

```csharp
// Entities/Entities/System/AuditLog.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bdDevCRM.Entities.Entities.System;

[Table("AuditLogs", Schema = "dbo")]
public class AuditLog
{
    [Key]
    public long AuditId { get; set; }

    // Who
    public int? UserId { get; set; }
    [MaxLength(100)]
    public string? Username { get; set; }

    [MaxLength(50)]
    public string? IpAddress { get; set; }

    [MaxLength(500)]
    public string? UserAgent { get; set; }

    // What
    [Required]
    [MaxLength(50)]
    public string Action { get; set; } = string.Empty; // CREATE, UPDATE, DELETE, VIEW, LOGIN, LOGOUT

    [Required]
    [MaxLength(100)]
    public string EntityType { get; set; } = string.Empty; // "User", "Company", "Application"

    [MaxLength(100)]
    public string? EntityId { get; set; }

    [MaxLength(200)]
    public string? Endpoint { get; set; }

    [MaxLength(100)]
    public string? Module { get; set; }

    // Details
    public string? OldValue { get; set; } // JSON of old state
    public string? NewValue { get; set; } // JSON of new state
    public string? Changes { get; set; } // Summary of changes

    // When
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    // Context
    [MaxLength(100)]
    public string? CorrelationId { get; set; }

    [MaxLength(100)]
    public string? SessionId { get; set; }

    [MaxLength(100)]
    public string? RequestId { get; set; }

    // Result
    public bool Success { get; set; } = true;
    public int? StatusCode { get; set; }

    [MaxLength(2000)]
    public string? ErrorMessage { get; set; }

    public int? DurationMs { get; set; }

    // Relationships
    [ForeignKey(nameof(UserId))]
    public virtual Users? User { get; set; }
}
```

#### SQL Migration Script:

```sql
-- Database_Scripts/02_Create_AuditLogs_Table.sql
CREATE TABLE [dbo].[AuditLogs] (
    [AuditId] BIGINT IDENTITY(1,1) PRIMARY KEY,

    -- Who
    [UserId] INT NULL,
    [Username] NVARCHAR(100) NULL,
    [IpAddress] NVARCHAR(50) NULL,
    [UserAgent] NVARCHAR(500) NULL,

    -- What
    [Action] NVARCHAR(50) NOT NULL,
    [EntityType] NVARCHAR(100) NOT NULL,
    [EntityId] NVARCHAR(100) NULL,
    [Endpoint] NVARCHAR(200) NULL,
    [Module] NVARCHAR(100) NULL,

    -- Details
    [OldValue] NVARCHAR(MAX) NULL,
    [NewValue] NVARCHAR(MAX) NULL,
    [Changes] NVARCHAR(MAX) NULL,

    -- When
    [Timestamp] DATETIME2(7) NOT NULL DEFAULT GETUTCDATE(),

    -- Context
    [CorrelationId] NVARCHAR(100) NULL,
    [SessionId] NVARCHAR(100) NULL,
    [RequestId] NVARCHAR(100) NULL,

    -- Result
    [Success] BIT NOT NULL DEFAULT 1,
    [StatusCode] INT NULL,
    [ErrorMessage] NVARCHAR(2000) NULL,
    [DurationMs] INT NULL,

    CONSTRAINT [FK_AuditLogs_Users] FOREIGN KEY ([UserId])
        REFERENCES [dbo].[Users]([UserId]) ON DELETE SET NULL
);

-- Indexes for performance
CREATE INDEX [IX_AuditLogs_UserId] ON [dbo].[AuditLogs]([UserId]);
CREATE INDEX [IX_AuditLogs_Timestamp] ON [dbo].[AuditLogs]([Timestamp] DESC);
CREATE INDEX [IX_AuditLogs_EntityType_EntityId] ON [dbo].[AuditLogs]([EntityType], [EntityId]);
CREATE INDEX [IX_AuditLogs_Action] ON [dbo].[AuditLogs]([Action]);
CREATE INDEX [IX_AuditLogs_CorrelationId] ON [dbo].[AuditLogs]([CorrelationId]);

-- Composite index for common queries
CREATE INDEX [IX_AuditLogs_UserAction] ON [dbo].[AuditLogs]([UserId], [Action], [Timestamp] DESC);
```

### 🔧 Implementation

#### Step 1: Enhanced Audit Middleware

```csharp
// Middleware/EnhancedAuditMiddleware.cs
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.Sql.Context;

namespace bdDevCRM.Api.Middleware;

public class EnhancedAuditMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<EnhancedAuditMiddleware> _logger;

    public EnhancedAuditMiddleware(RequestDelegate next, ILogger<EnhancedAuditMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, CRMContext dbContext)
    {
        // Skip audit for certain paths
        if (ShouldSkipAudit(context.Request.Path))
        {
            await _next(context);
            return;
        }

        var stopwatch = Stopwatch.StartNew();
        var correlationId = context.TraceIdentifier;
        var originalBodyStream = context.Response.Body;

        try
        {
            // Capture request body for POST/PUT/PATCH
            string? requestBody = null;
            if (context.Request.Method != "GET" && context.Request.ContentType?.Contains("application/json") == true)
            {
                context.Request.EnableBuffering();
                requestBody = await new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true).ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            // Execute request
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            stopwatch.Stop();

            // Capture response for audit (only for modifications)
            string? responseContent = null;
            if (context.Request.Method != "GET" && responseBody.Length > 0)
            {
                responseBody.Seek(0, SeekOrigin.Begin);
                responseContent = await new StreamReader(responseBody).ReadToEndAsync();
                responseBody.Seek(0, SeekOrigin.Begin);
            }

            // Create audit log
            var auditLog = CreateAuditLog(context, requestBody, responseContent, stopwatch.ElapsedMilliseconds, correlationId);

            // Save audit log asynchronously (fire and forget to not block response)
            _ = Task.Run(async () =>
            {
                try
                {
                    await SaveAuditLogAsync(dbContext, auditLog);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to save audit log for {Path}", context.Request.Path);
                }
            });

            // Copy response back
            await responseBody.CopyToAsync(originalBodyStream);
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            _logger.LogError(ex, "Error in audit middleware");

            // Log the exception
            var errorAuditLog = CreateErrorAuditLog(context, ex, stopwatch.ElapsedMilliseconds, correlationId);
            _ = Task.Run(async () => await SaveAuditLogAsync(dbContext, errorAuditLog));

            throw;
        }
        finally
        {
            context.Response.Body = originalBodyStream;
        }
    }

    private bool ShouldSkipAudit(PathString path)
    {
        var pathValue = path.Value?.ToLower() ?? string.Empty;

        // Skip health checks, swagger, static files
        return pathValue.Contains("/health") ||
               pathValue.Contains("/swagger") ||
               pathValue.Contains("/uploads") ||
               pathValue.Contains("/_framework") ||
               pathValue.Contains("/api/values"); // Add paths to skip
    }

    private AuditLog CreateAuditLog(HttpContext context, string? requestBody, string? responseContent, long durationMs, string correlationId)
    {
        var user = context.User;
        var request = context.Request;
        var response = context.Response;

        var auditLog = new AuditLog
        {
            // Who
            UserId = GetUserId(user),
            Username = user?.Identity?.Name ?? "Anonymous",
            IpAddress = context.Connection.RemoteIpAddress?.ToString(),
            UserAgent = request.Headers["User-Agent"].FirstOrDefault(),

            // What
            Action = GetActionFromMethod(request.Method),
            EntityType = GetEntityTypeFromPath(request.Path),
            EntityId = GetEntityIdFromPath(request.Path),
            Endpoint = $"{request.Method} {request.Path}",
            Module = GetModuleFromPath(request.Path),

            // Details
            OldValue = null, // Will be populated by EF Core interceptor for updates
            NewValue = requestBody,
            Changes = null,

            // When
            Timestamp = DateTime.UtcNow,

            // Context
            CorrelationId = correlationId,
            SessionId = context.Session?.Id,
            RequestId = context.TraceIdentifier,

            // Result
            Success = response.StatusCode >= 200 && response.StatusCode < 300,
            StatusCode = response.StatusCode,
            DurationMs = (int)durationMs
        };

        return auditLog;
    }

    private AuditLog CreateErrorAuditLog(HttpContext context, Exception ex, long durationMs, string correlationId)
    {
        var auditLog = CreateAuditLog(context, null, null, durationMs, correlationId);
        auditLog.Success = false;
        auditLog.StatusCode = 500;
        auditLog.ErrorMessage = $"{ex.GetType().Name}: {ex.Message}";
        return auditLog;
    }

    private async Task SaveAuditLogAsync(CRMContext dbContext, AuditLog auditLog)
    {
        // Use a new scope to avoid conflicts with the main request's DbContext
        dbContext.AuditLogs.Add(auditLog);
        await dbContext.SaveChangesAsync();
    }

    private int? GetUserId(ClaimsPrincipal? user)
    {
        var userIdClaim = user?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.TryParse(userIdClaim, out var userId) ? userId : null;
    }

    private string GetActionFromMethod(string method)
    {
        return method.ToUpper() switch
        {
            "POST" => "CREATE",
            "PUT" => "UPDATE",
            "PATCH" => "UPDATE",
            "DELETE" => "DELETE",
            "GET" => "VIEW",
            _ => method.ToUpper()
        };
    }

    private string GetEntityTypeFromPath(PathString path)
    {
        // Extract entity type from path like /api/users/123 -> "Users"
        var segments = path.Value?.Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (segments != null && segments.Length >= 2)
        {
            return segments[1]; // Assumes /api/{entity}/...
        }
        return "Unknown";
    }

    private string? GetEntityIdFromPath(PathString path)
    {
        // Extract ID from path like /api/users/123 -> "123"
        var segments = path.Value?.Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (segments != null && segments.Length >= 3 && int.TryParse(segments[2], out _))
        {
            return segments[2];
        }
        return null;
    }

    private string GetModuleFromPath(PathString path)
    {
        // Determine module from path
        var pathValue = path.Value?.ToLower() ?? string.Empty;

        if (pathValue.Contains("/systemadmin/")) return "SystemAdmin";
        if (pathValue.Contains("/hr/")) return "HR";
        if (pathValue.Contains("/crm/")) return "CRM";
        if (pathValue.Contains("/dms/")) return "DMS";
        if (pathValue.Contains("/authentication")) return "Authentication";

        return "General";
    }
}
```

#### Step 2: Enhanced EF Core Audit Interceptor

```csharp
// Sql/Interceptors/EnhancedAuditSaveChangesInterceptor.cs
using System.Text.Json;
using bdDevCRM.Entities.Entities.System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace bdDevCRM.Sql.Interceptors;

public class EnhancedAuditSaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EnhancedAuditSaveChangesInterceptor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        if (context == null) return base.SavingChangesAsync(eventData, result, cancellationToken);

        var auditLogs = new List<AuditLog>();
        var httpContext = _httpContextAccessor.HttpContext;

        // Get user info
        var userId = GetCurrentUserId();
        var username = httpContext?.User?.Identity?.Name ?? "System";
        var ipAddress = httpContext?.Connection.RemoteIpAddress?.ToString();
        var correlationId = httpContext?.TraceIdentifier ?? Guid.NewGuid().ToString();

        var entries = context.ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added ||
                       e.State == EntityState.Modified ||
                       e.State == EntityState.Deleted)
            .ToList();

        foreach (var entry in entries)
        {
            // Skip AuditLog entity itself to avoid recursion
            if (entry.Entity is AuditLog)
                continue;

            var auditLog = CreateAuditLogFromEntry(entry, userId, username, ipAddress, correlationId);
            if (auditLog != null)
            {
                auditLogs.Add(auditLog);
            }
        }

        // Add audit logs to context
        foreach (var auditLog in auditLogs)
        {
            context.Set<AuditLog>().Add(auditLog);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private AuditLog? CreateAuditLogFromEntry(EntityEntry entry, int? userId, string username, string? ipAddress, string correlationId)
    {
        var entityType = entry.Entity.GetType().Name;
        var action = entry.State.ToString().ToUpper();

        // Get primary key value
        var keyValues = entry.Properties
            .Where(p => p.Metadata.IsPrimaryKey())
            .Select(p => p.CurrentValue?.ToString())
            .ToList();
        var entityId = string.Join(",", keyValues);

        string? oldValue = null;
        string? newValue = null;
        string? changes = null;

        try
        {
            if (entry.State == EntityState.Modified)
            {
                // Capture old and new values
                var originalValues = entry.Properties
                    .Where(p => p.IsModified)
                    .ToDictionary(
                        p => p.Metadata.Name,
                        p => p.OriginalValue
                    );

                var currentValues = entry.Properties
                    .Where(p => p.IsModified)
                    .ToDictionary(
                        p => p.Metadata.Name,
                        p => p.CurrentValue
                    );

                if (originalValues.Any())
                {
                    oldValue = JsonSerializer.Serialize(originalValues);
                    newValue = JsonSerializer.Serialize(currentValues);

                    // Create human-readable changes summary
                    var changesList = originalValues.Keys.Select(key =>
                        $"{key}: {originalValues[key]} → {currentValues[key]}");
                    changes = string.Join("; ", changesList);
                }
            }
            else if (entry.State == EntityState.Added)
            {
                // For new entities, capture all current values
                var currentValues = entry.Properties
                    .Where(p => p.CurrentValue != null)
                    .ToDictionary(
                        p => p.Metadata.Name,
                        p => p.CurrentValue
                    );

                newValue = JsonSerializer.Serialize(currentValues);
            }
            else if (entry.State == EntityState.Deleted)
            {
                // For deleted entities, capture original values
                var originalValues = entry.Properties
                    .Where(p => p.OriginalValue != null)
                    .ToDictionary(
                        p => p.Metadata.Name,
                        p => p.OriginalValue
                    );

                oldValue = JsonSerializer.Serialize(originalValues);
            }
        }
        catch (Exception)
        {
            // If serialization fails, continue without detailed values
        }

        return new AuditLog
        {
            UserId = userId,
            Username = username,
            IpAddress = ipAddress,
            Action = action,
            EntityType = entityType,
            EntityId = entityId,
            OldValue = oldValue,
            NewValue = newValue,
            Changes = changes,
            Timestamp = DateTime.UtcNow,
            CorrelationId = correlationId,
            Success = true
        };
    }

    private int? GetCurrentUserId()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext?.User?.Identity?.IsAuthenticated == true)
        {
            var userIdClaim = httpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out var userId))
            {
                return userId;
            }
        }
        return null;
    }
}
```

#### Step 3: Update DbContext

```csharp
// Sql/Context/CRMContext.cs - Add this property
public DbSet<AuditLog> AuditLogs { get; set; }

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    // ... existing configurations ...

    // Configure AuditLog
    modelBuilder.Entity<AuditLog>(entity =>
    {
        entity.HasKey(e => e.AuditId);

        entity.HasIndex(e => e.UserId);
        entity.HasIndex(e => e.Timestamp);
        entity.HasIndex(e => new { e.EntityType, e.EntityId });
        entity.HasIndex(e => e.CorrelationId);

        entity.Property(e => e.Timestamp)
            .HasDefaultValueSql("GETUTCDATE()");

        entity.Property(e => e.Success)
            .HasDefaultValue(true);

        // Relationship with Users
        entity.HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    });
}
```

#### Step 4: Register Enhanced Interceptor

```csharp
// Extensions/ServiceExtensions.cs
public static void ConfigureInterceptors(this IServiceCollection services)
{
    services.AddHttpContextAccessor();

    // Replace old interceptor with enhanced version
    services.AddScoped<EnhancedAuditSaveChangesInterceptor>();
}

public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
{
    services.AddDbContext<CRMContext>((serviceProvider, options) =>
    {
        var connectionString = configuration.GetConnectionString("DbLocation");
        options.UseSqlServer(connectionString);

        // Add enhanced interceptor
        var auditInterceptor = serviceProvider.GetService<EnhancedAuditSaveChangesInterceptor>();
        if (auditInterceptor != null)
        {
            options.AddInterceptors(auditInterceptor);
        }
    });
}
```

#### Step 5: Update Program.cs

```csharp
// Program.cs
// Replace old AuditMiddleware with EnhancedAuditMiddleware
// Add after app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<EnhancedAuditMiddleware>();
```

### 📊 Audit Query API

#### Create Audit Repository and Service:

```csharp
// RepositoriesContracts/IAuditLogRepository.cs
public interface IAuditLogRepository
{
    Task<IEnumerable<AuditLog>> GetUserActivityAsync(int userId, DateTime? from = null, DateTime? to = null);
    Task<IEnumerable<AuditLog>> GetEntityHistoryAsync(string entityType, string entityId);
    Task<IEnumerable<AuditLog>> GetAuditLogsAsync(AuditQueryDTO query);
    Task<int> GetAuditCountAsync(AuditQueryDTO query);
}

// Repositories/AuditLogRepository.cs
public class AuditLogRepository : RepositoryBase<AuditLog>, IAuditLogRepository
{
    public AuditLogRepository(CRMContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<IEnumerable<AuditLog>> GetUserActivityAsync(int userId, DateTime? from = null, DateTime? to = null)
    {
        var query = FindByCondition(a => a.UserId == userId, trackChanges: false);

        if (from.HasValue)
            query = query.Where(a => a.Timestamp >= from.Value);

        if (to.HasValue)
            query = query.Where(a => a.Timestamp <= to.Value);

        return await query
            .OrderByDescending(a => a.Timestamp)
            .Take(100)
            .ToListAsync();
    }

    public async Task<IEnumerable<AuditLog>> GetEntityHistoryAsync(string entityType, string entityId)
    {
        return await FindByCondition(
            a => a.EntityType == entityType && a.EntityId == entityId,
            trackChanges: false)
            .OrderByDescending(a => a.Timestamp)
            .Take(50)
            .ToListAsync();
    }

    public async Task<IEnumerable<AuditLog>> GetAuditLogsAsync(AuditQueryDTO query)
    {
        var dbQuery = RepositoryContext.AuditLogs.AsNoTracking();

        // Apply filters
        if (query.UserId.HasValue)
            dbQuery = dbQuery.Where(a => a.UserId == query.UserId);

        if (!string.IsNullOrEmpty(query.EntityType))
            dbQuery = dbQuery.Where(a => a.EntityType == query.EntityType);

        if (!string.IsNullOrEmpty(query.Action))
            dbQuery = dbQuery.Where(a => a.Action == query.Action);

        if (query.From.HasValue)
            dbQuery = dbQuery.Where(a => a.Timestamp >= query.From);

        if (query.To.HasValue)
            dbQuery = dbQuery.Where(a => a.Timestamp <= query.To);

        if (!string.IsNullOrEmpty(query.IpAddress))
            dbQuery = dbQuery.Where(a => a.IpAddress == query.IpAddress);

        // Pagination
        return await dbQuery
            .OrderByDescending(a => a.Timestamp)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();
    }

    public async Task<int> GetAuditCountAsync(AuditQueryDTO query)
    {
        var dbQuery = RepositoryContext.AuditLogs.AsNoTracking();

        if (query.UserId.HasValue)
            dbQuery = dbQuery.Where(a => a.UserId == query.UserId);

        if (!string.IsNullOrEmpty(query.EntityType))
            dbQuery = dbQuery.Where(a => a.EntityType == query.EntityType);

        if (query.From.HasValue)
            dbQuery = dbQuery.Where(a => a.Timestamp >= query.From);

        if (query.To.HasValue)
            dbQuery = dbQuery.Where(a => a.Timestamp <= query.To);

        return await dbQuery.CountAsync();
    }
}

// DTOs
public class AuditQueryDTO
{
    public int? UserId { get; set; }
    public string? EntityType { get; set; }
    public string? Action { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public string? IpAddress { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 50;
}
```

#### Controller:

```csharp
// Controllers/SystemAdmin/AuditController.cs
[ApiController]
[Route("api/systemadmin/[controller]")]
[Authorize]
public class AuditController : ControllerBase
{
    private readonly IAuditLogRepository _auditRepository;
    private readonly ILogger<AuditController> _logger;

    public AuditController(IAuditLogRepository auditRepository, ILogger<AuditController> logger)
    {
        _auditRepository = auditRepository;
        _logger = logger;
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserActivity(int userId, [FromQuery] DateTime? from, [FromQuery] DateTime? to)
    {
        var logs = await _auditRepository.GetUserActivityAsync(userId, from, to);
        return Ok(logs);
    }

    [HttpGet("entity/{entityType}/{entityId}")]
    public async Task<IActionResult> GetEntityHistory(string entityType, string entityId)
    {
        var logs = await _auditRepository.GetEntityHistoryAsync(entityType, entityId);
        return Ok(logs);
    }

    [HttpPost("query")]
    public async Task<IActionResult> QueryAuditLogs([FromBody] AuditQueryDTO query)
    {
        var logs = await _auditRepository.GetAuditLogsAsync(query);
        var total = await _auditRepository.GetAuditCountAsync(query);

        return Ok(new
        {
            data = logs,
            total,
            page = query.Page,
            pageSize = query.PageSize
        });
    }
}
```

### 📈 Audit Reports & Dashboards:

```sql
-- Top 10 most active users
SELECT TOP 10
    UserId,
    Username,
    COUNT(*) as ActivityCount,
    MIN(Timestamp) as FirstActivity,
    MAX(Timestamp) as LastActivity
FROM AuditLogs
WHERE Timestamp >= DATEADD(day, -30, GETUTCDATE())
GROUP BY UserId, Username
ORDER BY ActivityCount DESC;

-- Failed login attempts
SELECT
    Username,
    IpAddress,
    COUNT(*) as AttemptCount,
    MAX(Timestamp) as LastAttempt
FROM AuditLogs
WHERE Action = 'LOGIN' AND Success = 0
    AND Timestamp >= DATEADD(hour, -24, GETUTCDATE())
GROUP BY Username, IpAddress
HAVING COUNT(*) >= 5
ORDER BY AttemptCount DESC;

-- Data modification summary
SELECT
    EntityType,
    Action,
    COUNT(*) as Count,
    COUNT(DISTINCT UserId) as UniqueUsers
FROM AuditLogs
WHERE Action IN ('CREATE', 'UPDATE', 'DELETE')
    AND Timestamp >= DATEADD(day, -7, GETUTCDATE())
GROUP BY EntityType, Action
ORDER BY Count DESC;

-- Audit trail for specific entity
SELECT
    AuditId,
    Username,
    Action,
    Changes,
    Timestamp
FROM AuditLogs
WHERE EntityType = 'Users' AND EntityId = '123'
ORDER BY Timestamp DESC;
```

---

## 5. Performance Monitoring

### 🎯 What to Monitor:

```
┌─────────────────────────────────────────────────────────┐
│ Performance Metrics                                     │
├─────────────────────────────────────────────────────────┤
│ 1. API Performance                                      │
│    • Request/Response time (p50, p95, p99)             │
│    • Throughput (requests per second)                  │
│    • Error rate (4xx, 5xx)                             │
│    • Endpoint-specific metrics                         │
│                                                          │
│ 2. Database Performance                                 │
│    • Query execution time                               │
│    • Connection pool usage                              │
│    • Deadlocks                                          │
│    • Index usage                                        │
│                                                          │
│ 3. System Resources                                     │
│    • CPU usage                                          │
│    • Memory usage                                       │
│    • Disk I/O                                           │
│    • Network I/O                                        │
│                                                          │
│ 4. Application Insights                                 │
│    • Custom events                                      │
│    • Dependencies                                       │
│    • Exceptions                                         │
│    • User flows                                         │
└─────────────────────────────────────────────────────────┘
```

### 🚀 Implementation

#### Step 1: Request/Response Timing Middleware

```csharp
// Middleware/PerformanceMonitoringMiddleware.cs
using System.Diagnostics;

namespace bdDevCRM.Api.Middleware;

public class PerformanceMonitoringMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<PerformanceMonitoringMiddleware> _logger;
    private const int SlowRequestThresholdMs = 1000;
    private const int VerySlowRequestThresholdMs = 5000;

    public PerformanceMonitoringMiddleware(RequestDelegate next, ILogger<PerformanceMonitoringMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var requestPath = context.Request.Path.Value;
        var requestMethod = context.Request.Method;

        try
        {
            // Add request start time to items
            context.Items["RequestStartTime"] = DateTime.UtcNow;

            await _next(context);

            stopwatch.Stop();
            var elapsedMs = stopwatch.ElapsedMilliseconds;

            // Log based on duration
            if (elapsedMs >= VerySlowRequestThresholdMs)
            {
                _logger.LogWarning(
                    "VERY SLOW REQUEST: {Method} {Path} took {Duration}ms (Status: {StatusCode})",
                    requestMethod, requestPath, elapsedMs, context.Response.StatusCode);
            }
            else if (elapsedMs >= SlowRequestThresholdMs)
            {
                _logger.LogWarning(
                    "SLOW REQUEST: {Method} {Path} took {Duration}ms (Status: {StatusCode})",
                    requestMethod, requestPath, elapsedMs, context.Response.StatusCode);
            }
            else
            {
                _logger.LogInformation(
                    "Request: {Method} {Path} completed in {Duration}ms (Status: {StatusCode})",
                    requestMethod, requestPath, elapsedMs, context.Response.StatusCode);
            }

            // Add performance header
            context.Response.Headers["X-Response-Time-Ms"] = elapsedMs.ToString();
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            _logger.LogError(ex,
                "Request FAILED: {Method} {Path} after {Duration}ms",
                requestMethod, requestPath, stopwatch.ElapsedMilliseconds);
            throw;
        }
    }
}
```

#### Step 2: Slow Query Logging Interceptor

```csharp
// Sql/Interceptors/SlowQueryLoggingInterceptor.cs
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace bdDevCRM.Sql.Interceptors;

public class SlowQueryLoggingInterceptor : DbCommandInterceptor
{
    private readonly ILogger<SlowQueryLoggingInterceptor> _logger;
    private const int SlowQueryThresholdMs = 500;
    private const int VerySlowQueryThresholdMs = 2000;

    public SlowQueryLoggingInterceptor(ILogger<SlowQueryLoggingInterceptor> logger)
    {
        _logger = logger;
    }

    public override ValueTask<DbDataReader> ReaderExecutedAsync(
        DbCommand command,
        CommandExecutedEventData eventData,
        DbDataReader result,
        CancellationToken cancellationToken = default)
    {
        var elapsedMs = eventData.Duration.TotalMilliseconds;

        if (elapsedMs >= VerySlowQueryThresholdMs)
        {
            _logger.LogWarning(
                "VERY SLOW QUERY detected: {Query} took {Duration}ms",
                SanitizeQuery(command.CommandText),
                elapsedMs);
        }
        else if (elapsedMs >= SlowQueryThresholdMs)
        {
            _logger.LogWarning(
                "SLOW QUERY detected: {Query} took {Duration}ms",
                SanitizeQuery(command.CommandText),
                elapsedMs);
        }

        return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
    }

    public override ValueTask<int> NonQueryExecutedAsync(
        DbCommand command,
        CommandExecutedEventData eventData,
        int result,
        CancellationToken cancellationToken = default)
    {
        var elapsedMs = eventData.Duration.TotalMilliseconds;

        if (elapsedMs >= VerySlowQueryThresholdMs)
        {
            _logger.LogWarning(
                "VERY SLOW NON-QUERY: {Query} took {Duration}ms",
                SanitizeQuery(command.CommandText),
                elapsedMs);
        }

        return base.NonQueryExecutedAsync(command, eventData, result, cancellationToken);
    }

    private string SanitizeQuery(string query)
    {
        // Truncate long queries and remove sensitive data
        const int maxLength = 500;
        if (query.Length > maxLength)
        {
            return query.Substring(0, maxLength) + "...";
        }
        return query;
    }
}
```

#### Step 3: Application Insights Configuration

```csharp
// Extensions/ApplicationInsightsConfiguration.cs
using Microsoft.ApplicationInsights.Extensibility;

namespace bdDevCRM.Api.Extensions;

public static class ApplicationInsightsConfiguration
{
    public static IServiceCollection ConfigureApplicationInsights(this IServiceCollection services, IConfiguration configuration)
    {
        var instrumentationKey = configuration["ApplicationInsights:InstrumentationKey"];

        if (!string.IsNullOrEmpty(instrumentationKey))
        {
            services.AddApplicationInsightsTelemetry(options =>
            {
                options.ConnectionString = $"InstrumentationKey={instrumentationKey}";
                options.EnableAdaptiveSampling = true;
                options.EnableQuickPulseMetricStream = true;
            });

            // Custom telemetry initializer
            services.AddSingleton<ITelemetryInitializer, CustomTelemetryInitializer>();
        }

        return services;
    }
}

public class CustomTelemetryInitializer : ITelemetryInitializer
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomTelemetryInitializer(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Initialize(ITelemetry telemetry)
    {
        var context = _httpContextAccessor.HttpContext;
        if (context != null)
        {
            telemetry.Context.User.Id = context.User?.Identity?.Name ?? "Anonymous";
            telemetry.Context.Session.Id = context.Session?.Id;

            // Add custom properties
            if (telemetry is ISupportProperties propTelemetry)
            {
                propTelemetry.Properties["UserAgent"] = context.Request.Headers["User-Agent"].ToString();
                propTelemetry.Properties["IpAddress"] = context.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            }
        }
    }
}
```

#### Step 4: Custom Performance Metrics Service

```csharp
// Services/Monitoring/IPerformanceMetricsService.cs
public interface IPerformanceMetricsService
{
    void TrackRequestDuration(string endpoint, long durationMs);
    void TrackDatabaseQuery(string queryType, long durationMs);
    void TrackCacheHit(string cacheType);
    void TrackCacheMiss(string cacheType);
    void TrackCustomMetric(string metricName, double value);
}

// Services/Monitoring/PerformanceMetricsService.cs
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

public class PerformanceMetricsService : IPerformanceMetricsService
{
    private readonly TelemetryClient? _telemetryClient;
    private readonly ILogger<PerformanceMetricsService> _logger;

    public PerformanceMetricsService(
        TelemetryClient? telemetryClient,
        ILogger<PerformanceMetricsService> logger)
    {
        _telemetryClient = telemetryClient;
        _logger = logger;
    }

    public void TrackRequestDuration(string endpoint, long durationMs)
    {
        _telemetryClient?.GetMetric("API.RequestDuration", "Endpoint").TrackValue(durationMs, endpoint);
        _logger.LogDebug("Request to {Endpoint} took {Duration}ms", endpoint, durationMs);
    }

    public void TrackDatabaseQuery(string queryType, long durationMs)
    {
        _telemetryClient?.GetMetric("Database.QueryDuration", "QueryType").TrackValue(durationMs, queryType);

        if (durationMs > 1000)
        {
            _logger.LogWarning("Slow database query: {QueryType} took {Duration}ms", queryType, durationMs);
        }
    }

    public void TrackCacheHit(string cacheType)
    {
        _telemetryClient?.GetMetric("Cache.Hits", "CacheType").TrackValue(1, cacheType);
    }

    public void TrackCacheMiss(string cacheType)
    {
        _telemetryClient?.GetMetric("Cache.Misses", "CacheType").TrackValue(1, cacheType);
    }

    public void TrackCustomMetric(string metricName, double value)
    {
        _telemetryClient?.TrackMetric(new MetricTelemetry(metricName, value));
        _logger.LogInformation("Custom metric {MetricName}: {Value}", metricName, value);
    }
}
```

#### Step 5: Update Program.cs

```csharp
// Program.cs
// Add Application Insights
builder.Services.ConfigureApplicationInsights(builder.Configuration);

// Add performance monitoring
builder.Services.AddSingleton<IPerformanceMetricsService, PerformanceMetricsService>();

// Add middleware (after Exception middleware)
app.UseMiddleware<PerformanceMonitoringMiddleware>();
```

#### Step 6: Update appsettings.json

```json
{
  "ApplicationInsights": {
    "InstrumentationKey": "YOUR-INSTRUMENTATION-KEY",
    "EnableAdaptiveSampling": true,
    "EnableQuickPulseMetricStream": true
  },
  "PerformanceMonitoring": {
    "SlowRequestThresholdMs": 1000,
    "VerySlowRequestThresholdMs": 5000,
    "SlowQueryThresholdMs": 500
  }
}
```

---

**(To be continued with Database Optimization and NLog vs Serilog sections...)**

আপনি কি চান আমি এই implementation guide-টি সম্পূর্ণ করি? নাকি এখন থেকে actual code implementation শুরু করব?
