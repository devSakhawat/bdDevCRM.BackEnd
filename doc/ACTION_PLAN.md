# bdDevCRM Backend - অ্যাকশন প্ল্যান (Action Plan)

## 📋 ইমপ্লিমেন্টেশন রোডম্যাপ

এই ডকুমেন্টটি আপনার bdDevCRM Backend প্রজেক্টকে এন্টারপ্রাইজ-লেভেলে পৌঁছানোর জন্য একটি বাস্তবসম্মত অ্যাকশন প্ল্যান প্রদান করে।

---

## 🚨 Phase 1: Critical Security Fixes (তাৎক্ষণিক - ০-৩ দিন)

### ✅ Task 1.1: Password Validation Fix (দিন ১ - ২ ঘণ্টা)

**অগ্রাধিকার:** 🔴 P0 - CRITICAL
**প্রভাব:** নিরাপত্তা ভেদ্যতা (Authentication bypass)

**করণীয়:**

1. BCrypt package install করুন:
```bash
cd bdDevCRM.Service
dotnet add package BCrypt.Net-Next
```

2. `AuthenticationService.cs` edit করুন:
```csharp
// File: bdDevCRM.Service/Authentication/AuthenticationService.cs
// Line: 51

// BEFORE:
public bool ValidateUser(UserForAuthenticationDto userForAuth)
{
    var user = _repository.Users.GetUserByLoginIdRaw(userForAuth.LoginId, trackChanges: false);
    if (user == null) return false;
    return true; // ❌ VULNERABILITY
}

// AFTER:
public bool ValidateUser(UserForAuthenticationDto userForAuth)
{
    var user = _repository.Users.GetUserByLoginIdRaw(userForAuth.LoginId, trackChanges: false);
    if (user == null) return false;

    // Verify password using BCrypt
    return BCrypt.Net.BCrypt.Verify(userForAuth.Password, user.Password);
}
```

3. User registration-এ password hash করুন:
```csharp
// When creating user:
user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
```

4. Testing:
```bash
# Invalid password দিয়ে login attempt করুন - fail হওয়া উচিত
# Valid password দিয়ে login attempt করুন - success হওয়া উচিত
```

---

### ✅ Task 1.2: Token Blacklist Enable (দিন ১ - ৩০ মিনিট)

**অগ্রাধিকার:** 🔴 P0 - CRITICAL
**প্রভাব:** Logout করার পর token valid থাকে

**করণীয়:**

1. `ServiceExtensions.cs` edit করুন:
```csharp
// File: bdDevCRM.Api/Extensions/ServiceExtensions.cs
// Lines: 186-219

// Uncomment the following code:
OnTokenValidated = async context =>
{
    var tokenBlacklistRepo = context.HttpContext.RequestServices
        .GetRequiredService<IRepositoryManager>()
        .TokenBlacklistRepository;

    var jwtToken = context.SecurityToken as JwtSecurityToken;
    var isBlacklisted = await tokenBlacklistRepo
        .IsTokenBlacklistedAsync(jwtToken);

    if (isBlacklisted)
    {
        context.Fail("This token has been revoked.");
    }
}
```

2. Testing:
```bash
# Login করুন → token পাবেন
# Logout করুন → token blacklist-এ যাবে
# Same token দিয়ে API call করুন → 401 Unauthorized পাবেন
```

---

### ✅ Task 1.3: SQL Injection Fix (দিন ২-৩ - ৪-৬ ঘণ্টা)

**অগ্রাধিকার:** 🔴 P0 - CRITICAL
**প্রভাব:** Database breach সম্ভাবনা

**করণীয়:**

1. Helper methods তৈরি করুন:
```csharp
// File: bdDevCRM.Repositories/Helpers/SqlInjectionValidator.cs

public static class SqlInjectionValidator
{
    private static readonly Regex SqlInjectionPattern = new(
        @"(\b(SELECT|INSERT|UPDATE|DELETE|DROP|CREATE|ALTER|EXEC|EXECUTE)\b)|(--|;|\/\*|\*\/|xp_|sp_)|(\bOR\b.*=.*|1\s*=\s*1)",
        RegexOptions.IgnoreCase | RegexOptions.Compiled
    );

    public static bool ContainsSqlInjection(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        return SqlInjectionPattern.IsMatch(input);
    }

    public static bool IsValidOrderBy(string orderBy, HashSet<string> allowedColumns)
    {
        if (string.IsNullOrWhiteSpace(orderBy))
            return true;

        var parts = orderBy.Split(',', StringSplitOptions.RemoveEmptyEntries);

        foreach (var part in parts)
        {
            var trimmed = part.Trim();
            var columnName = trimmed.Split(' ')[0];

            if (!allowedColumns.Contains(columnName, StringComparer.OrdinalIgnoreCase))
                return false;

            // Check for SQL injection in the entire part
            if (ContainsSqlInjection(trimmed))
                return false;
        }

        return true;
    }

    public static HashSet<string> GetAllowedColumns<T>()
    {
        return typeof(T).GetProperties()
            .Select(p => p.Name)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);
    }
}
```

2. `RepositoryBase.cs` GridData method update করুন:
```csharp
// File: bdDevCRM.Repositories/RepositoryBase.cs
// Method: GridData

public async Task<GridEntity<T>> GridData<T>(
    string baseQuery,
    CRMGridOptions options,
    string orderBy,
    SqlParameter[]? parameters = null,
    string? condition = null)
{
    // Validate orderBy
    var allowedColumns = SqlInjectionValidator.GetAllowedColumns<T>();
    if (!string.IsNullOrEmpty(orderBy) &&
        !SqlInjectionValidator.IsValidOrderBy(orderBy, allowedColumns))
    {
        throw new BadRequestException("Invalid orderBy clause.");
    }

    // Validate condition
    if (!string.IsNullOrEmpty(condition) &&
        SqlInjectionValidator.ContainsSqlInjection(condition))
    {
        throw new BadRequestException("Invalid condition clause.");
    }

    // Rest of implementation...
}
```

3. Testing:
```bash
# SQL injection attempt:
# orderBy = "Name; DROP TABLE Users--"
# Expected: BadRequestException

# Valid orderBy:
# orderBy = "Name ASC, CreatedAt DESC"
# Expected: Success
```

---

### ✅ Task 1.4: Security Headers (দিন ৩ - ১ ঘণ্টা)

**অগ্রাধিকার:** 🟢 P3 - LOW
**প্রভাব:** XSS, Clickjacking protection

**করণীয়:**

1. Middleware তৈরি করুন:
```csharp
// File: bdDevCRM.Api/Middleware/SecurityHeadersMiddleware.cs

public class SecurityHeadersMiddleware
{
    private readonly RequestDelegate _next;

    public SecurityHeadersMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // X-Content-Type-Options
        context.Response.Headers.Add("X-Content-Type-Options", "nosniff");

        // X-Frame-Options
        context.Response.Headers.Add("X-Frame-Options", "DENY");

        // X-XSS-Protection
        context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");

        // Referrer-Policy
        context.Response.Headers.Add("Referrer-Policy", "no-referrer");

        // Content-Security-Policy
        context.Response.Headers.Add("Content-Security-Policy",
            "default-src 'self'; " +
            "script-src 'self'; " +
            "style-src 'self' 'unsafe-inline'; " +
            "img-src 'self' data: https:; " +
            "font-src 'self'; " +
            "connect-src 'self'; " +
            "frame-ancestors 'none';");

        // HSTS (only for HTTPS)
        if (context.Request.IsHttps)
        {
            context.Response.Headers.Add("Strict-Transport-Security",
                "max-age=31536000; includeSubDomains; preload");
        }

        // Remove unnecessary headers
        context.Response.Headers.Remove("Server");
        context.Response.Headers.Remove("X-Powered-By");
        context.Response.Headers.Remove("X-AspNet-Version");

        await _next(context);
    }
}
```

2. `Program.cs`-এ register করুন:
```csharp
// Add after StandardExceptionMiddleware
app.UseMiddleware<SecurityHeadersMiddleware>();
```

---

### ✅ Task 1.5: Rate Limiting (দিন ৩ - ২ ঘণ্টা)

**অগ্রাধিকার:** 🟠 P1 - HIGH
**প্রভাব:** DoS attack protection

**করণীয়:**

1. Package install করুন:
```bash
cd bdDevCRM.Api
dotnet add package AspNetCoreRateLimit
```

2. `appsettings.json`-এ configuration যোগ করুন:
```json
{
  "IpRateLimiting": {
    "EnableEndpointRateLimiting": true,
    "StackBlockedRequests": false,
    "RealIpHeader": "X-Real-IP",
    "ClientIdHeader": "X-ClientId",
    "HttpStatusCode": 429,
    "GeneralRules": [
      {
        "Endpoint": "*",
        "Period": "1m",
        "Limit": 100
      },
      {
        "Endpoint": "POST:/api/Authentication/login",
        "Period": "15m",
        "Limit": 5
      },
      {
        "Endpoint": "POST:/api/Authentication/refresh",
        "Period": "1m",
        "Limit": 10
      }
    ]
  }
}
```

3. `Program.cs`-এ configure করুন:
```csharp
// Add services:
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

// Add middleware (after CORS, before Authentication):
app.UseIpRateLimiting();
```

4. Testing:
```bash
# Login endpoint-এ 6 বার request পাঠান 15 মিনিটে
# 6th request → 429 Too Many Requests
```

---

## ⚡ Phase 2: Architecture Improvements (১-২ সপ্তাহ)

### ✅ Task 2.1: Response Format Standardization (দিন ৪-৫)

**অগ্রাধিকার:** 🟠 P1 - HIGH
**প্রভাব:** API consistency

**করণীয়:**

1. `GlobalExceptionHandler.cs` মুছে ফেলুন:
```bash
rm bdDevCRM.Api/GlobalExceptionHandler.cs
```

2. Response Wrapper Middleware তৈরি করুন:
```csharp
// File: bdDevCRM.Api/Middleware/ResponseWrapperMiddleware.cs
// Implementation: See PROJECT_ANALYSIS_REPORT.md Section 5.1
```

3. সব controller-এ `ApiResponse` replace করুন `StandardApiResponse` দিয়ে:
```bash
# Find all usages:
grep -r "ApiResponse" --include="*.cs" bdDevCRM.Presentation/
# Replace manually or use refactoring tool
```

---

### ✅ Task 2.2: Unit of Work Pattern (দিন ৬-৮)

**অগ্রাধিকার:** 🟡 P2 - MEDIUM
**প্রভাব:** Transaction management

**করণীয়:**

1. `IUnitOfWork` interface তৈরি করুন:
```csharp
// File: bdDevCRM.RepositoriesContracts/IUnitOfWork.cs
// Implementation: See PROJECT_ANALYSIS_REPORT.md Section 7
```

2. `UnitOfWork` class implement করুন:
```csharp
// File: bdDevCRM.Repositories/UnitOfWork.cs
// Implementation: See PROJECT_ANALYSIS_REPORT.md Section 7
```

3. `Program.cs`-এ register করুন:
```csharp
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
```

4. Controller-গুলো update করুন:
```csharp
// Replace IRepositoryManager with IUnitOfWork
// Use transaction for multi-step operations
```

---

### ✅ Task 2.3: ServiceManager Refactoring (দিন ৯-১০)

**অগ্রাধিকার:** 🟡 P2 - MEDIUM
**প্রভাব:** Code maintainability

**করণীয়:**

1. Area-specific interfaces তৈরি করুন:
```csharp
// File: bdDevCRM.ServiceContract/ISystemAdminServiceManager.cs
// File: bdDevCRM.ServiceContract/IHRServiceManager.cs
// File: bdDevCRM.ServiceContract/ICRMServiceManager.cs
// Implementation: See PROJECT_ANALYSIS_REPORT.md Section 6
```

2. Implementations তৈরি করুন:
```csharp
// File: bdDevCRM.Service/SystemAdminServiceManager.cs
// File: bdDevCRM.Service/HRServiceManager.cs
// File: bdDevCRM.Service/CRMServiceManager.cs
```

3. Main `ServiceManager` update করুন:
```csharp
public class ServiceManager : IServiceManager
{
    public ISystemAdminServiceManager SystemAdmin { get; }
    public IHRServiceManager HR { get; }
    public ICRMServiceManager CRM { get; }

    // ... constructor
}
```

---

### ✅ Task 2.4: BaseRepository Split (দিন ১১-১২)

**অগ্রাধিকার:** 🟡 P2 - MEDIUM
**প্রভাব:** Code maintainability

**করণীয়:**

1. Partial class-এ ভাগ করুন:
```csharp
// File: bdDevCRM.Repositories/RepositoryBase.Core.cs (CRUD)
// File: bdDevCRM.Repositories/RepositoryBase.Transactions.cs (Transaction)
// File: bdDevCRM.Repositories/RepositoryBase.Bulk.cs (Bulk operations)
// File: bdDevCRM.Repositories/RepositoryBase.RawSql.cs (Raw SQL)
// File: bdDevCRM.Repositories/RepositoryBase.Grid.cs (Grid data)
```

2. Commented code মুছে ফেলুন (Lines 435-527)

3. Hardcoded timeout-গুলো configurable করুন:
```csharp
// appsettings.json:
{
  "Database": {
    "CommandTimeout": 30,
    "LongRunningCommandTimeout": 300
  }
}
```

---

## 📅 Phase 3: Enterprise Features (১-২ মাস)

### ✅ Task 3.1: Multi-Factor Authentication (সপ্তাহ ৩)

**করণীয়:**

1. Package install করুন:
```bash
dotnet add package GoogleAuthenticator
```

2. `MfaService` তৈরি করুন:
```csharp
// File: bdDevCRM.Service/Authentication/MfaService.cs
// Implementation: See PROJECT_ANALYSIS_REPORT.md Section 4.1
```

3. User table-এ MFA columns যোগ করুন:
```sql
ALTER TABLE Users ADD MfaEnabled BIT DEFAULT 0;
ALTER TABLE Users ADD MfaSecretKey NVARCHAR(100);
```

4. MFA endpoints তৈরি করুন:
```csharp
// POST /api/Authentication/mfa/setup
// POST /api/Authentication/mfa/verify
// POST /api/Authentication/mfa/disable
```

---

### ✅ Task 3.2: Audit Log Encryption (সপ্তাহ ৩-৪)

**করণীয়:**

1. Encryption service তৈরি করুন:
```csharp
public interface IAuditEncryptionService
{
    string Encrypt(string plaintext);
    string Decrypt(string ciphertext);
    string ComputeSignature(string data);
    bool VerifySignature(string data, string signature);
}
```

2. Implementation:
```csharp
// Use AES-256 for encryption
// Use HMAC-SHA256 for signature
```

3. `EnhancedAuditMiddleware` update করুন:
```csharp
// Encrypt request/response body before storing
// Add digital signature
```

---

### ✅ Task 3.3: Soft Delete Support (সপ্তাহ ৪)

**করণীয়:**

1. `ISoftDeletable` interface তৈরি করুন:
```csharp
// Implementation: See PROJECT_ANALYSIS_REPORT.md Section 7
```

2. Base entity update করুন
3. DbContext-এ global query filter যোগ করুন
4. Repository-তে `SoftDeleteAsync` method যোগ করুন

---

### ✅ Task 3.4: OAuth2/OIDC Support (সপ্তাহ ৫-৬)

**করণীয়:**

1. Package install করুন:
```bash
dotnet add package Microsoft.AspNetCore.Authentication.Google
dotnet add package Microsoft.AspNetCore.Authentication.MicrosoftAccount
```

2. `appsettings.json`-এ configuration যোগ করুন:
```json
{
  "Authentication": {
    "Google": {
      "ClientId": "YOUR_CLIENT_ID",
      "ClientSecret": "YOUR_CLIENT_SECRET"
    },
    "Microsoft": {
      "ClientId": "YOUR_CLIENT_ID",
      "ClientSecret": "YOUR_CLIENT_SECRET"
    }
  }
}
```

3. `Program.cs`-এ configure করুন:
```csharp
builder.Services.AddAuthentication()
    .AddGoogle(...)
    .AddMicrosoftAccount(...);
```

---

## 📊 Phase 4: Observability & Monitoring (২-৩ মাস)

### ✅ Task 4.1: Application Insights Integration (সপ্তাহ ৭)

**করণীয়:**

1. Package install করুন:
```bash
dotnet add package Microsoft.ApplicationInsights.AspNetCore
```

2. `Program.cs`-এ configure করুন:
```csharp
builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];
});
```

3. Custom telemetry যোগ করুন:
```csharp
// Track custom events
// Track dependencies
// Track exceptions
```

---

### ✅ Task 4.2: Prometheus Metrics (সপ্তাহ ৮)

**করণীয়:**

1. Package install করুন:
```bash
dotnet add package prometheus-net.AspNetCore
```

2. `Program.cs`-এ configure করুন:
```csharp
app.UseMetricServer(); // /metrics endpoint
app.UseHttpMetrics();
```

3. Custom metrics যোগ করুন:
```csharp
// Counter, Gauge, Histogram, Summary
```

---

### ✅ Task 4.3: Health Check Endpoints (সপ্তাহ ৮)

**করণীয়:**

1. Package install করুন:
```bash
dotnet add package AspNetCore.HealthChecks.SqlServer
dotnet add package AspNetCore.HealthChecks.Redis
```

2. `Program.cs`-এ configure করুন:
```csharp
builder.Services.AddHealthChecks()
    .AddSqlServer(connectionString, name: "database")
    .AddRedis(redisConnection, name: "redis")
    .AddCheck("audit-queue", () => { /* check */ });

app.MapHealthChecks("/health");
app.MapHealthChecks("/health/ready", /* readiness */);
app.MapHealthChecks("/health/live", /* liveness */);
```

---

## 🚀 Phase 5: DevOps & Automation (৩-৪ মাস)

### ✅ Task 5.1: CI/CD Pipeline (সপ্তাহ ৯-১০)

**করণীয়:**

1. GitHub Actions workflow তৈরি করুন:
```yaml
# .github/workflows/ci-cd.yml
name: CI/CD Pipeline

on:
  push:
    branches: [ main, develop ]
  pull_request:
    branches: [ main, develop ]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      - name: Setup .NET
        uses: actions/setup-dotnet@v3
        with:
          dotnet-version: '8.0.x'
      - name: Restore dependencies
        run: dotnet restore
      - name: Build
        run: dotnet build --no-restore
      - name: Test
        run: dotnet test --no-build --verbosity normal
      - name: Publish
        run: dotnet publish -c Release -o ./publish
```

---

### ✅ Task 5.2: Automated Testing (সপ্তাহ ১১-১২)

**করণীয়:**

1. Unit test project তৈরি করুন:
```bash
dotnet new xunit -n bdDevCRM.UnitTests
```

2. Integration test project তৈরি করুন:
```bash
dotnet new xunit -n bdDevCRM.IntegrationTests
```

3. Test coverage target: 70%+

---

### ✅ Task 5.3: Security Scanning (সপ্তাহ ১৩)

**করণীয়:**

1. SAST (Static Application Security Testing):
```yaml
# Use SonarCloud, CodeQL, or Snyk
```

2. DAST (Dynamic Application Security Testing):
```yaml
# Use OWASP ZAP
```

3. Dependency scanning:
```bash
dotnet list package --vulnerable
```

---

## 📈 Progress Tracking

### সপ্তাহ ১ (Phase 1):
- [ ] Password validation fix
- [ ] Token blacklist enable
- [ ] SQL injection fix
- [ ] Security headers
- [ ] Rate limiting

### সপ্তাহ ২ (Phase 2 - Part 1):
- [ ] Response format standardization
- [ ] Unit of Work pattern
- [ ] ServiceManager refactoring

### সপ্তাহ ৩ (Phase 2 - Part 2):
- [ ] BaseRepository split
- [ ] Code cleanup
- [ ] Documentation

### সপ্তাহ ৪-৬ (Phase 3 - Part 1):
- [ ] Multi-factor authentication
- [ ] Audit log encryption
- [ ] Soft delete support

### সপ্তাহ ৭-৮ (Phase 3 - Part 2):
- [ ] OAuth2/OIDC support
- [ ] Password policy enforcement
- [ ] Session management

### সপ্তাহ ৯-১০ (Phase 4):
- [ ] Application Insights
- [ ] Prometheus metrics
- [ ] Health checks

### সপ্তাহ ১১-১৩ (Phase 5):
- [ ] CI/CD pipeline
- [ ] Automated testing
- [ ] Security scanning

---

## 🎯 Success Metrics

### নিরাপত্তা (Security):
- ✅ কোনো P0 vulnerability নেই
- ✅ OWASP Top 10 compliance
- ✅ Security scan pass rate: 100%

### কর্মক্ষমতা (Performance):
- ✅ API response time: P95 < 500ms
- ✅ Database query time: P95 < 100ms
- ✅ Error rate: < 0.1%

### কোড কোয়ালিটি (Code Quality):
- ✅ Code coverage: > 70%
- ✅ SonarQube quality gate: Pass
- ✅ Technical debt ratio: < 5%

### মনিটরিং (Monitoring):
- ✅ Uptime: > 99.9%
- ✅ Alert response time: < 5 minutes
- ✅ Log retention: 30 days

---

## 📞 সহায়তা

কোনো প্রশ্ন বা সাহায্য প্রয়োজন হলে:
1. PROJECT_ANALYSIS_REPORT.md পড়ুন
2. প্রতিটি task-এর detailed implementation সেখানে আছে
3. Step-by-step instructions follow করুন

**মনে রাখবেন:** একবারে সব করতে হবে না। Phase-by-phase এগিয়ে যান। প্রথমে Phase 1 সম্পূর্ণ করুন, তারপর Phase 2, এভাবে এগিয়ে যান।

**শুভকামনা!** 🚀
