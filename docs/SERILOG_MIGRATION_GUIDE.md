# Serilog Migration Guide
## Removing NLog and Centralizing Logging with Serilog

### Executive Summary

This guide provides step-by-step instructions for migrating from the current dual logging framework (Serilog + NLog) to a centralized Serilog-only implementation.

**Current State**:
- Serilog (Primary) + NLog (Secondary)
- Duplicate logging code
- Inconsistent log formats
- Higher maintenance overhead

**Target State**:
- Serilog only (Centralized)
- Consistent structured logging
- Better integration with modern tools (Seq, Application Insights)
- Reduced package size and complexity

---

## Phase 1: Preparation (Estimate: 1 hour)

### Step 1.1: Audit Current NLog Usage

Run these commands to find all NLog usages:

```bash
# Find ILoggerManager references
grep -r "ILoggerManager" --include="*.cs" .

# Find NLog configuration
find . -name "nlog.config" -o -name "NLog.config"

# Count affected files
grep -r "ILoggerManager" --include="*.cs" . | wc -l
```

### Step 1.2: Create Migration Branch

```bash
git checkout -b feature/centralize-logging-serilog
```

### Step 1.3: Install New Serilog Packages

Update `.csproj` files with these additions:

```xml
<ItemGroup>
  <!-- Existing Serilog packages (keep these) -->
  <PackageReference Include="Serilog.AspNetCore" Version="10.0.0" />
  <PackageReference Include="Serilog.Sinks.Console" Version="6.1.1" />
  <PackageReference Include="Serilog.Sinks.File" Version="7.0.0" />
  <PackageReference Include="Serilog.Enrichers.Environment" Version="3.0.1" />
  <PackageReference Include="Serilog.Enrichers.Thread" Version="4.0.0" />

  <!-- New packages to add -->
  <PackageReference Include="Serilog.Enrichers.CorrelationId" Version="3.0.1" />

  <!-- Optional but recommended -->
  <PackageReference Include="Serilog.Sinks.Seq" Version="8.0.0" />
</ItemGroup>
```

Run:
```bash
dotnet restore
```

---

## Phase 2: Remove NLog (Estimate: 30 minutes)

### Step 2.1: Remove NLog Packages

Remove these lines from all `.csproj` files:

```xml
<!-- REMOVE THESE -->
<PackageReference Include="NLog" Version="5.4.0" />
<PackageReference Include="NLog.Extensions.Logging" Version="5.4.0" />
```

### Step 2.2: Delete NLog Files

```bash
# Delete LoggerManager (NLog wrapper)
rm bdDevCRM.LoggerService/LoggerManager.cs
rm bdDevCRM.LoggerService/ILoggerManager.cs

# Delete NLog configuration (if exists)
find . -name "nlog.config" -delete
find . -name "NLog.config" -delete
```

### Step 2.3: Remove NLog Service Registration

In `ServiceExtensions.cs`, remove:

```csharp
// REMOVE THIS METHOD
public static void ConfigureLoggerService(this IServiceCollection services)
{
    services.AddSingleton<ILoggerManager, LoggerManager>();
}
```

In `Program.cs`, remove this line if present:
```csharp
// REMOVE THIS
builder.Services.ConfigureLoggerService();
```

---

## Phase 3: Update Service Classes (Estimate: 2-3 hours)

### Step 3.1: Replace ILoggerManager with ILogger<T>

For each service class, follow this pattern:

**BEFORE**:
```csharp
using bdDevCRM.LoggerService;

public class AuthenticationService : IAuthenticationService
{
    private readonly ILoggerManager _logger;

    public AuthenticationService(
        IRepositoryManager repository,
        ILoggerManager logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<AuthenticationResponseDto> ValidateUserLogin(LoginDto dto)
    {
        _logger.LogInfo($"Login attempt for user: {dto.LoginId}");

        try
        {
            // ... logic
        }
        catch (Exception ex)
        {
            _logger.LogError($"Login failed for {dto.LoginId}: {ex.Message}");
            throw;
        }
    }
}
```

**AFTER**:
```csharp
using Microsoft.Extensions.Logging;

public class AuthenticationService : IAuthenticationService
{
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(
        IRepositoryManager repository,
        ILogger<AuthenticationService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<AuthenticationResponseDto> ValidateUserLogin(LoginDto dto)
    {
        _logger.LogInformation("Login attempt for user: {LoginId}", dto.LoginId);

        try
        {
            // ... logic
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Login failed for {LoginId}", dto.LoginId);
            throw;
        }
    }
}
```

### Step 3.2: Method Name Mapping

| NLog Method | Serilog/ILogger Method | Notes |
|-------------|------------------------|-------|
| `LogInfo(string)` | `LogInformation(string, params)` | Use structured logging |
| `LogError(string)` | `LogError(Exception, string, params)` | Include exception object |
| `LogWarn(string)` | `LogWarning(string, params)` | Use structured logging |
| `LogDebug(string)` | `LogDebug(string, params)` | Use structured logging |

### Step 3.3: Convert to Structured Logging

**WRONG** (String interpolation):
```csharp
_logger.LogInformation($"User {userId} logged in at {DateTime.Now}");
_logger.LogError($"Failed to process order {orderId} for customer {customerId}");
```

**CORRECT** (Structured logging):
```csharp
_logger.LogInformation("User {UserId} logged in at {LoginTime}", userId, DateTime.Now);
_logger.LogError("Failed to process order {OrderId} for customer {CustomerId}", orderId, customerId);
```

**Why?**
- Properties are queryable in Seq/Elasticsearch
- Better performance (no string formatting if logging disabled)
- Type-safe
- Easier to search and filter

### Step 3.4: Exception Logging

**WRONG**:
```csharp
catch (Exception ex)
{
    _logger.LogError($"Error: {ex.Message}");
}
```

**CORRECT**:
```csharp
catch (Exception ex)
{
    _logger.LogError(ex, "Failed to process customer {CustomerId}", customerId);
}
```

---

## Phase 4: Enhanced Serilog Configuration (Estimate: 1 hour)

### Step 4.1: Update Program.cs

Replace the existing Serilog configuration with this enhanced version:

```csharp
using Serilog;
using Serilog.Events;
using Serilog.Enrichers.CorrelationId;

// Create enhanced Serilog configuration
Log.Logger = new LoggerConfiguration()
    // Minimum log levels
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Information)
    .MinimumLevel.Override("System", LogEventLevel.Warning)

    // Enrichers (add context to logs)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .Enrich.WithProperty("Application", "bdDevCRM")
    .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
    .Enrich.WithCorrelationId()

    // Console sink (for development)
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] [{SourceContext}] {Message:lj} {Properties:j}{NewLine}{Exception}",
        restrictedToMinimumLevel: LogEventLevel.Information
    )

    // Application log file (all levels)
    .WriteTo.File(
        path: "logs/app-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] [{SourceContext}] {Message:lj} {Properties:j}{NewLine}{Exception}",
        restrictedToMinimumLevel: LogEventLevel.Information
    )

    // Error log file (errors only)
    .WriteTo.File(
        path: "logs/errors-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 90,
        restrictedToMinimumLevel: LogEventLevel.Error,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] [{SourceContext}] {Message:lj} {Properties:j}{NewLine}{Exception}"
    )

    // Seq sink (optional - for production monitoring)
    .WriteTo.Seq(
        serverUrl: builder.Configuration["Seq:ServerUrl"] ?? "http://localhost:5341",
        apiKey: builder.Configuration["Seq:ApiKey"],
        restrictedToMinimumLevel: LogEventLevel.Information
    )

    .CreateLogger();

// Use Serilog for ASP.NET Core
builder.Host.UseSerilog();

try
{
    Log.Information("Starting bdDevCRM Backend API");

    var app = builder.Build();

    // ... existing middleware

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start");
    throw;
}
finally
{
    Log.CloseAndFlush();
}
```

### Step 4.2: Update appsettings.json

Add Serilog configuration section:

```json
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "Microsoft.EntityFrameworkCore": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "Console"
      },
      {
        "Name": "File",
        "Args": {
          "path": "logs/app-.log",
          "rollingInterval": "Day",
          "retainedFileCountLimit": 30
        }
      }
    ],
    "Enrich": [
      "FromLogContext",
      "WithMachineName",
      "WithThreadId",
      "WithCorrelationId"
    ]
  },
  "Seq": {
    "ServerUrl": "http://localhost:5341",
    "ApiKey": ""
  }
}
```

---

## Phase 5: Add Correlation ID Support (Estimate: 30 minutes)

### Step 5.1: Create Correlation ID Middleware

Create new file: `bdDevCRM.Api/Middleware/CorrelationIdMiddleware.cs`

```csharp
using Serilog.Context;

namespace bdDevCRM.Api.Middleware
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private const string CorrelationIdHeader = "X-Correlation-ID";

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Get or generate correlation ID
            var correlationId = context.Request.Headers[CorrelationIdHeader].FirstOrDefault()
                ?? Guid.NewGuid().ToString();

            // Add to response headers
            context.Response.Headers.Append(CorrelationIdHeader, correlationId);

            // Add to Serilog log context
            using (LogContext.PushProperty("CorrelationId", correlationId))
            {
                await _next(context);
            }
        }
    }
}
```

### Step 5.2: Register Middleware

In `Program.cs`, add early in the pipeline:

```csharp
var app = builder.Build();

// Add correlation ID middleware (EARLY in pipeline)
app.UseMiddleware<CorrelationIdMiddleware>();

// ... existing middleware
app.UseAuthentication();
app.UseAuthorization();
```

---

## Phase 6: Update Existing Middleware (Estimate: 1 hour)

### Step 6.1: Update StandardExceptionMiddleware

```csharp
public class StandardExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<StandardExceptionMiddleware> _logger;

    public StandardExceptionMiddleware(
        RequestDelegate next,
        ILogger<StandardExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var correlationId = context.Response.Headers["X-Correlation-ID"].FirstOrDefault()
                ?? Guid.NewGuid().ToString();

            _logger.LogError(ex,
                "Unhandled exception occurred. CorrelationId: {CorrelationId}, Path: {Path}",
                correlationId, context.Request.Path);

            await HandleExceptionAsync(context, ex, correlationId);
        }
    }

    private async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception,
        string correlationId)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, message) = exception switch
        {
            BaseCustomException customEx => (customEx.StatusCode, customEx.Message),
            UnauthorizedException => (StatusCodes.Status401Unauthorized, "Unauthorized"),
            NotFoundException => (StatusCodes.Status404NotFound, "Resource not found"),
            _ => (StatusCodes.Status500InternalServerError, "Internal server error")
        };

        context.Response.StatusCode = statusCode;

        var response = new StandardApiResponse<object>
        {
            Success = false,
            StatusCode = statusCode,
            Message = message,
            CorrelationId = correlationId,
            Error = new ErrorDetails
            {
                Code = $"ERR_{statusCode}",
                Type = exception.GetType().Name,
                Details = message,
                // Only include stack trace in development
                StackTrace = context.RequestServices
                    .GetRequiredService<IWebHostEnvironment>()
                    .IsDevelopment() ? exception.StackTrace : null
            }
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}
```

### Step 6.2: Update Other Middleware

Apply similar patterns to:
- `StructuredLoggingMiddleware`
- `PerformanceMonitoringMiddleware`
- `EnhancedAuditMiddleware`

Replace `ILoggerManager` with `ILogger<T>`.

---

## Phase 7: Testing (Estimate: 1 hour)

### Step 7.1: Build and Verify

```bash
# Clean and rebuild
dotnet clean
dotnet build

# Check for compilation errors
# Should compile successfully
```

### Step 7.2: Test Logging Output

Start the application:
```bash
dotnet run --project bdDevCRM.Api
```

**Expected Console Output**:
```
[10:30:15 INF] [bdDevCRM.Api.Program] Starting bdDevCRM Backend API
[10:30:16 INF] [Microsoft.Hosting.Lifetime] Now listening on: https://localhost:7290
[10:30:20 INF] [bdDevCRM.Service.AuthenticationService] Login attempt for user: john.doe CorrelationId="abc-123-def"
```

### Step 7.3: Verify Log Files

Check that files are created:
```bash
ls -lh logs/
# Expected:
# app-20260301.log
# errors-20260301.log
```

Check log content:
```bash
tail -f logs/app-20260301.log
```

**Expected format**:
```
2026-03-01 10:30:15.123 +00:00 [INF] [bdDevCRM.Service.AuthenticationService] Login attempt for user: john.doe {"CorrelationId":"abc-123","UserId":123}
```

### Step 7.4: Test Error Logging

Trigger an error and verify:
```bash
# Check error log
tail -f logs/errors-20260301.log
```

### Step 7.5: Test Correlation ID

Make a request with curl:
```bash
curl -H "X-Correlation-ID: test-123" http://localhost:7290/api/customers

# Check response headers - should include:
# X-Correlation-ID: test-123
```

Check logs - all entries for this request should have `CorrelationId: test-123`.

---

## Phase 8: Optional - Setup Seq (Estimate: 30 minutes)

### Step 8.1: Install Seq with Docker

```bash
docker run -d \
  --name seq \
  -e ACCEPT_EULA=Y \
  -p 5341:80 \
  -v seq-data:/data \
  datalust/seq:latest
```

### Step 8.2: Access Seq Dashboard

Open browser: http://localhost:5341

### Step 8.3: Generate API Key

1. Click Settings → API Keys
2. Create new API key
3. Copy the key

### Step 8.4: Update appsettings.json

```json
{
  "Seq": {
    "ServerUrl": "http://localhost:5341",
    "ApiKey": "your-api-key-here"
  }
}
```

### Step 8.5: Restart Application

Logs should now appear in Seq dashboard.

### Step 8.6: Example Seq Queries

```sql
-- Find all errors
select * from stream where @Level = 'Error'

-- Find by correlation ID
select * from stream where CorrelationId = 'abc-123-def'

-- Find slow requests
select * from stream where ElapsedMs > 1000

-- Find by user
select * from stream where UserId = 123
```

---

## Phase 9: Documentation Update (Estimate: 30 minutes)

### Step 9.1: Update Developer Docs

Create/update: `docs/LOGGING.md`

```markdown
# Logging Guidelines

## Framework
We use **Serilog** for all logging.

## Usage

### Inject Logger
```csharp
public class YourService
{
    private readonly ILogger<YourService> _logger;

    public YourService(ILogger<YourService> logger)
    {
        _logger = logger;
    }
}
```

### Log Levels
- **Debug**: Detailed debugging information
- **Information**: General informational messages
- **Warning**: Warning messages (recoverable issues)
- **Error**: Error messages (unhandled exceptions)
- **Fatal**: Critical failures (application cannot continue)

### Structured Logging
Always use structured logging:

```csharp
// ✅ CORRECT
_logger.LogInformation("User {UserId} logged in from {IpAddress}", userId, ipAddress);

// ❌ WRONG
_logger.LogInformation($"User {userId} logged in from {ipAddress}");
```

### Exception Logging
```csharp
try
{
    // ... code
}
catch (Exception ex)
{
    _logger.LogError(ex, "Failed to process {EntityType} with ID {EntityId}",
        nameof(Customer), customerId);
    throw;
}
```

### Correlation ID
Every request automatically has a correlation ID. Access it via:
```csharp
var correlationId = HttpContext.Response.Headers["X-Correlation-ID"];
```

## Log Files
- **Application logs**: `logs/app-YYYYMMDD.log` (retained 30 days)
- **Error logs**: `logs/errors-YYYYMMDD.log` (retained 90 days)

## Seq Dashboard
Production logs: http://seq.yourdomain.com (internal only)
```

---

## Phase 10: Cleanup and Validation (Estimate: 30 minutes)

### Step 10.1: Search for Remaining NLog References

```bash
# Should return no results
grep -r "ILoggerManager" --include="*.cs" .
grep -r "NLog" --include="*.cs" --include="*.csproj" .
```

### Step 10.2: Validate All Tests Pass

```bash
dotnet test
```

### Step 10.3: Code Review Checklist

- [ ] All `ILoggerManager` replaced with `ILogger<T>`
- [ ] All string interpolation replaced with structured logging
- [ ] All exception logging includes exception object
- [ ] Correlation ID middleware added
- [ ] All middleware updated to use ILogger<T>
- [ ] NLog packages removed from all .csproj files
- [ ] NLog files deleted
- [ ] Log files being created correctly
- [ ] Console output looks correct
- [ ] Seq integration working (if enabled)
- [ ] Documentation updated

---

## Rollback Plan

If issues are encountered:

```bash
# Revert changes
git checkout main
git branch -D feature/centralize-logging-serilog

# Or specific file revert
git checkout HEAD -- path/to/file.cs
```

---

## Expected Results

### Before Migration
```
Package size: ~150 MB
Logging packages: Serilog (10 MB) + NLog (3 MB) = 13 MB
Log consistency: Mixed formats
Maintenance: High (2 frameworks)
Tool integration: Limited
```

### After Migration
```
Package size: ~140 MB (-10 MB)
Logging packages: Serilog (10 MB)
Log consistency: Unified format
Maintenance: Low (1 framework)
Tool integration: Excellent (Seq, ELK, etc.)
```

### Performance Impact
- **Build time**: -2-3 seconds
- **Runtime overhead**: Negligible difference
- **Log write speed**: Slightly faster (less overhead)

---

## Frequently Asked Questions

### Q1: Will this break existing logs?
**A**: No. Existing log files remain intact. New logs will have improved format.

### Q2: Do I need Seq?
**A**: No, it's optional but highly recommended for production monitoring.

### Q3: What if I find a bug after migration?
**A**: Use the rollback plan. File an issue with details.

### Q4: How long will migration take?
**A**: For this codebase: 4-6 hours for a single developer.

### Q5: Can I migrate gradually?
**A**: Not recommended. It's better to do it all at once to avoid confusion.

---

## Support

For questions or issues during migration:
- Create issue in repository
- Contact: devteam@bddevs.com
- Slack: #backend-support

---

**Migration Guide Version**: 1.0
**Last Updated**: 2026-03-01
**Author**: bdDevs Team
