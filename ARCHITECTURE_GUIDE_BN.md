# bdDevCRM BackEnd — আর্কিটেকচার গাইড (বাংলা)

এই ডকুমেন্টে প্রজেক্টের চারটি মূল মেকানিজম বিস্তারিতভাবে বর্ণনা করা হয়েছে:

1. [লগিন মেকানিজম (Authentication)](#১-লগিন-মেকানিজম-authentication)
2. [Response Mechanism](#২-response-mechanism)
3. [Exception Handling Mechanism](#৩-exception-handling-mechanism)
4. [Logger Mechanism](#৪-logger-mechanism)

---

## ১. লগিন মেকানিজম (Authentication)

### সারসংক্ষেপ

এই প্রজেক্টে **JWT (JSON Web Token)** ভিত্তিক Authentication ব্যবহার করা হয়েছে। লগিন করার পর দুটি টোকেন তৈরি হয়:
- **Access Token** — স্বল্পস্থায়ী (১৫ মিনিট), প্রতিটি API request-এ Header-এ পাঠানো হয়।
- **Refresh Token** — দীর্ঘস্থায়ী (৭ দিন), HTTP-only Cookie-তে সংরক্ষিত হয়।

---

### ১.১ লগিন Flow (ধাপে ধাপে)

```
POST /api/authentication/login
Body: { "loginId": "admin", "password": "abcd1234" }
```

```
Client Request
    │
    ▼
[AuthenticationController.Authenticate()]
    │
    ├─► STEP 1: User খোঁজা
    │       _serviceManager.Users.GetUserByLoginIdRaw(loginId)
    │       → User না পেলে: 401 Unauthorized
    │
    ├─► STEP 2: Login Validation
    │       _serviceManager.CustomAuthentication.ValidateUserLogin(user, userDto)
    │       ┌──────────────────────────────────────────────────────────────┐
    │       │  2a. User Active কিনা চেক করা (IsActive = true/false)       │
    │       │  2b. Account Expired কিনা চেক করা (IsExpired)               │
    │       │  2c. Password মিলানো (ValidationHelper.ValidateLoginPassword) │
    │       │      → ভুল হলে FailedLoginNo বাড়ানো হয়                      │
    │       │      → WrongAttemptNo পার হলে Account Lock হয়ে যায়          │
    │       │  2d. Password Expiry চেক করা (System Settings অনুযায়ী)      │
    │       │  2e. Success হলে FailedLoginNo রিসেট, LastLoginDate আপডেট   │
    │       └──────────────────────────────────────────────────────────────┘
    │
    ├─► STEP 3: Token তৈরি করা
    │       _serviceManager.CustomAuthentication.CreateToken(user)
    │       ┌─────────────────────────────────────────────────────────┐
    │       │  Access Token (JWT):                                     │
    │       │    - Claims: UserId, LoginId, CompanyId, Role, etc.     │
    │       │    - Expiry: 15 minutes                                 │
    │       │    - Algorithm: HMAC SHA-256                            │
    │       │                                                          │
    │       │  Refresh Token:                                          │
    │       │    - Random cryptographic bytes (64 bytes)               │
    │       │    - Hashed (SHA-256) করে Database-এ সংরক্ষণ           │
    │       │    - Expiry: 7 days                                      │
    │       └─────────────────────────────────────────────────────────┘
    │
    ├─► STEP 4: Refresh Token Cookie-তে সেট করা
    │       SetRefreshTokenCookie(refreshToken, expiry)
    │       Cookie: HttpOnly=true, Secure=true, SameSite=Strict
    │
    ├─► STEP 5: User Data Cache-এ সংরক্ষণ
    │       MemoryCache key: "User_{UserId}"
    │       Expiry: 5 hours (Sliding + Absolute)
    │
    └─► STEP 6: Response পাঠানো
            TokenResponseDto:
            {
              "accessToken": "eyJhbGci...",
              "accessTokenExpiry": "2026-03-01T10:15:00",
              "refreshTokenExpiry": "2026-03-08T09:56:00",
              "tokenType": "Bearer",
              "expiresIn": 900,
              "userSession": { ...user info... },
              "status": "Success",
              "isSuccess": true
            }
```

---

### ১.২ Validation Status Codes

| Status | মানে | HTTP Response |
|--------|------|--------------|
| `Success` | লগিন সফল | 200 OK |
| `Inactive` | Account নিষ্ক্রিয় | 401 Unauthorized |
| `Expired` | Account মেয়াদ শেষ | 401 Unauthorized |
| `AccountLocked` | বারবার ভুল পাসওয়ার্ড দেওয়ায় লক | 401 Unauthorized |
| `PasswordChangeRequired` | পাসওয়ার্ড পরিবর্তন করতে হবে | 200 OK (with flag) |
| `Failed` | Invalid credentials | 401 Unauthorized |

---

### ১.৩ JWT Token Structure

```
Header:
{
  "alg": "HS256",
  "typ": "JWT"
}

Payload (Claims):
{
  "UserId": "123",
  "LoginId": "admin",
  "CompanyId": "1",
  "role": "Admin",
  "nbf": 1740826596,
  "exp": 1740827496,    ← 15 মিনিট পরে expire
  "iat": 1740826596,
  "iss": "https://localhost:7145",
  "aud": "https://localhost:7145"
}

Signature: HMACSHA256(base64(header) + "." + base64(payload), secretKey)
```

**JWT Configuration** (`appsettings.json`):
```json
{
  "bdDevsJWT": {
    "SecretKey": "your-secret-key",
    "Issuer": "https://localhost:7145",
    "Audience": "https://localhost:7145",
    "ExpiryInMinutes": "15"
  }
}
```

---

### ১.৪ Token Refresh Flow

```
POST /api/authentication/refresh-token
(Cookie থেকে refreshToken স্বয়ংক্রিয়ভাবে পড়া হয়)

1. Cookie থেকে refreshToken পড়া
2. Token Hash করে Database-এ খোঁজা
3. Token Valid ও Expired নয় কিনা চেক করা
4. নতুন Access Token ও Refresh Token তৈরি করা
5. পুরানো Refresh Token Revoke করা
6. নতুন Refresh Token Database-এ সংরক্ষণ
7. নতুন Refresh Token Cookie-তে সেট করা
8. নতুন Access Token Response-এ পাঠানো
```

---

### ১.৫ Logout Flow

```
POST /api/authentication/logout

1. Authorization Header থেকে Access Token পড়া
2. UserId বের করা (JWT Claims থেকে)
3. Database-এ ঐ User-এর সব Refresh Token Revoke করা
4. Memory Cache থেকে User Data মুছে ফেলা
5. Cookie থেকে Refresh Token মুছে ফেলা
6. Response: 200 OK "Logged out successfully"
```

---

### ১.৬ Authorization (Protected Routes)

```csharp
// Protected endpoint - JWT Token লাগবে
[AuthorizeUser]
[HttpGet("user-info")]
public IActionResult GetUserInfo() { ... }

// Public endpoint - Token লাগবে না
[AllowAnonymous]
[HttpPost("login")]
public async Task<IActionResult> Authenticate(...) { ... }
```

`AuthorizeUserAttribute` কাজ করে এভাবে:
1. `Authorization: Bearer <token>` Header চেক করে।
2. Token Validate করে (Issuer, Audience, Lifetime, Signature)।
3. User Data Memory Cache থেকে বের করে `HttpContext.Items["CurrentUser"]` এ রাখে।
4. Invalid হলে 401 Unauthorized পাঠায়।

---

## ২. Response Mechanism

### সারসংক্ষেপ

এই প্রজেক্টে **দুটি Response Format** ব্যবহার করা হয়:

| Format | ব্যবহার |
|--------|---------|
| `ApiResponse<T>` | সাধারণ/Legacy endpoints |
| `StandardApiResponse<T>` | Exception Handling ও Modern endpoints |

---

### ২.১ ApiResponse<T> — সাধারণ Response

```csharp
public class ApiResponse<T>
{
    public int StatusCode { get; set; }    // HTTP কোড (200, 201, 404...)
    public string Message { get; set; }    // মানুষের পড়ার জন্য বার্তা
    public bool IsSuccess { get; set; }    // সফল হয়েছে কিনা
    public DateTime Timestamp { get; set; } // Response-এর সময়
    public T Data { get; set; }            // মূল ডেটা
}
```

**উদাহরণ (সফল Response):**
```json
{
  "statusCode": 200,
  "message": "User info retrieved",
  "isSuccess": true,
  "timestamp": "2026-03-01T09:56:36.723Z",
  "data": {
    "userId": 1,
    "loginId": "admin",
    "fullName": "Admin User"
  }
}
```

---

### ২.২ StandardApiResponse<T> — উন্নত Response (Error-এর জন্য)

```csharp
public class StandardApiResponse<T>
{
    public int StatusCode { get; set; }        // HTTP Status Code
    public bool Success { get; set; }           // সফলতার নির্দেশক
    public string Message { get; set; }         // বার্তা
    public string Version { get; set; }         // API Version ("1.0")
    public DateTime Timestamp { get; set; }     // UTC সময়
    public T Data { get; set; }                 // ডেটা payload
    public ErrorDetails Error { get; set; }     // Error তথ্য (শুধু error-এর সময়)
    public PaginationMetadata Pagination { get; set; } // Pagination তথ্য (list-এর জন্য)
    public List<ResourceLink> Links { get; set; }      // HATEOAS links
    public string CorrelationId { get; set; }   // Request tracking ID
}
```

**উদাহরণ (Error Response):**
```json
{
  "statusCode": 404,
  "success": false,
  "message": "User not found",
  "version": "1.0",
  "timestamp": "2026-03-01T09:56:36.723Z",
  "data": null,
  "error": {
    "code": "NOT_FOUND",
    "type": "GenericNotFoundException",
    "details": "User with ID 123 was not found",
    "validationErrors": null,
    "stackTrace": null
  },
  "correlationId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

---

### ২.৩ ResponseHelper — Static Factory Pattern

Controller-এ সরাসরি Response তৈরি না করে `ResponseHelper` ব্যবহার করা হয়:

```csharp
// ✅ সফল Response তৈরির উপায়
return Ok(ResponseHelper.Success(data, "User retrieved"));          // 200
return Created(ResponseHelper.Created(data, "User created"));       // 201
return Ok(ResponseHelper.Updated(data, "User updated"));            // 200
return Ok(ResponseHelper.NoContent<object>("No data found"));       // 204

// ❌ Error Response তৈরির উপায়
return Unauthorized(ResponseHelper.Unauthorized("Invalid token"));  // 401
return BadRequest(ResponseHelper.BadRequest("Invalid input"));      // 400
return NotFound(ResponseHelper.NotFound("Item not found"));         // 404
return Conflict(ResponseHelper.Conflict("Already exists"));         // 409
return StatusCode(500, ResponseHelper.InternalServerError("Error")); // 500
```

---

### ২.৪ ErrorDetails — Error-এর বিস্তারিত কাঠামো

```csharp
public class ErrorDetails
{
    public string Code { get; set; }      // "NOT_FOUND", "UNAUTHORIZED", "BAD_REQUEST"
    public string Type { get; set; }      // Exception class-এর নাম
    public string Details { get; set; }   // Developer-এর জন্য বিস্তারিত
    public Dictionary<string, string[]> ValidationErrors { get; set; } // Field-level errors
    public string StackTrace { get; set; }  // শুধু Development mode-এ
    public Dictionary<string, object> AdditionalData { get; set; } // Extra context
}
```

**সব Error Code:**

| Code | Status | অর্থ |
|------|--------|------|
| `NOT_FOUND` | 404 | Resource পাওয়া যায়নি |
| `UNAUTHORIZED` | 401 | Authentication ব্যর্থ |
| `FORBIDDEN` | 403 | Permission নেই |
| `BAD_REQUEST` | 400 | Invalid input |
| `CONFLICT` | 409 | Duplicate data |
| `VALIDATION_ERROR` | 422 | Validation ব্যর্থ |
| `DATABASE_ERROR` | 500 | Database সমস্যা |
| `INTERNAL_ERROR` | 500 | অজানা সার্ভার সমস্যা |
| `TOKEN_EXPIRED` | 401 | JWT Token মেয়াদ শেষ |
| `INVALID_TOKEN` | 401 | JWT Token ভুল |
| `SERVICE_UNAVAILABLE` | 503 | Service বন্ধ |

---

### ২.৫ Pagination Metadata

List data পাঠানোর সময় Pagination তথ্য যোগ করা যায়:

```json
{
  "statusCode": 200,
  "success": true,
  "message": "Users retrieved",
  "data": [...],
  "pagination": {
    "currentPage": 1,
    "pageSize": 20,
    "totalCount": 250,
    "totalPages": 13,
    "hasNextPage": true,
    "hasPreviousPage": false,
    "startIndex": 0,
    "endIndex": 19
  }
}
```

---

### ২.৬ Content Negotiation (Multiple Formats)

একই endpoint থেকে বিভিন্ন format-এ data পাওয়া যায়:

```
Accept: application/json  → JSON (default)
Accept: application/xml   → XML format
Accept: text/csv          → CSV format (Kendo Grid export-এর জন্য উপকারী)
```

**Program.cs-এ Configuration:**
```csharp
builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
    config.OutputFormatters.Add(new CsvOutputFormatter());
})
.AddXmlDataContractSerializerFormatters();
```

---

## ৩. Exception Handling Mechanism

### সারসংক্ষেপ

এই প্রজেক্টে **তিন স্তরে** Exception Handling করা হয়:

```
Layer 1: StandardExceptionMiddleware    ← সবচেয়ে শক্তিশালী (Primary)
Layer 2: ExceptionMiddleware            ← Legacy/Fallback
Layer 3: GlobalExceptionFilter          ← Action Filter স্তরে
```

---

### ৩.১ Custom Exception Hierarchy

```
Exception (System Base)
    └── BaseCustomException (abstract — আমাদের base class)
        │
        ├── BadRequestException          → 400 | "BAD_REQUEST"
        ├── UnauthorizedException        → 401 | "UNAUTHORIZED"
        ├── ForbiddenAccessException     → 403 | "FORBIDDEN"
        ├── NotFoundException            → 404 | "NOT_FOUND"
        ├── ConflictException            → 409 | "CONFLICT"
        └── ServiceUnavailableException  → 503 | "SERVICE_UNAVAILABLE"

Domain-specific Exceptions (উপরোক্ত classes থেকে inherit করা):
    ├── GenericNotFoundException         → 404
    ├── GenericBadRequestException       → 400
    ├── GenericUnauthorizedException     → 401
    ├── GenericConflictException         → 409
    ├── DuplicateRecordException         → 409
    ├── InvalidCreateOperationException  → 400
    ├── InvalidUpdateOperationException  → 400
    ├── UsernamePasswordMismatchException → 400
    ├── IdMismatchBadRequestException    → 400
    ├── NullModelBadRequestException     → 400
    ├── JWTSecurityException             → 401
    ├── FileSizeExceededException        → 400
    ├── DataMappingException             → 500
    └── ... (আরও অনেক)
```

---

### ৩.২ BaseCustomException — Base Class

```csharp
public abstract class BaseCustomException : Exception
{
    // HTTP Status Code (abstract — প্রতিটি subclass নিজে define করে)
    public abstract int StatusCode { get; }

    // Error Code (abstract — "NOT_FOUND", "BAD_REQUEST", etc.)
    public abstract string ErrorCode { get; }

    // User-friendly message (override করা যায়)
    public virtual string UserFriendlyMessage => Message;

    // Extra context data
    public Dictionary<string, object> AdditionalData { get; } = new();

    // Fluent API দিয়ে extra data যোগ করা যায়
    public BaseCustomException WithData(string key, object value)
    {
        AdditionalData[key] = value;
        return this;
    }
}
```

**ব্যবহারের উদাহরণ:**
```csharp
// Simple
throw new GenericNotFoundException("User with ID 123 not found");

// Extra context সহ
throw new GenericNotFoundException("User not found")
    .WithData("userId", 123)
    .WithData("requestedBy", "admin");
```

---

### ৩.৩ StandardExceptionMiddleware (Primary Handler)

**Location:** `bdDevCRM.Api/Middleware/StandardExceptionMiddleware.cs`

এটি ASP.NET Core Middleware হিসেবে **সবার আগে** Request Pipeline-এ যোগ করা হয়।

```
HTTP Request আসে
    ↓
StandardExceptionMiddleware.InvokeAsync()
    ↓
try { await _next(context); }    ← পরবর্তী Middleware/Controller চালানো হয়
catch (Exception ex)
    ↓
HandleExceptionAsync(context, ex)
    ├── 1. CorrelationId তৈরি (Guid.NewGuid().ToString())
    ├── 2. Log করা:
    │       _logger.LogError(ex, $"[{correlationId}] {ex.Type}: {ex.Message}")
    │       _loggerManager.LogError(...)
    ├── 3. MapExceptionToStandardResponse(ex, correlationId) → StandardApiResponse
    └── 4. JSON serialize করে Response পাঠানো
```

**Exception Priority Order:**
```
1. BaseCustomException (সর্বোচ্চ priority — আমাদের custom exceptions)
2. GenericConflictException / DuplicateRecordException → 409
3. InvalidCreateOperationException / GenericBadRequestException → 400
4. GenericNotFoundException → 404
5. GenericUnauthorizedException → 401
6. ForbiddenAccessException → 403
7. ServiceUnavailableException → 503
8. SecurityTokenExpiredException → 401 (JWT expire)
9. SecurityTokenException → 401 (JWT invalid)
10. UnauthorizedAccessException → 401
11. ValidationException → 400
12. ArgumentNullException → 400
13. KeyNotFoundException → 404
14. DbUpdateException → 500 (Database error)
15. Generic Exception → 500 (Unknown error)
```

**Database Error Sanitization:**

Database error message সরাসরি client-কে দেওয়া হয় না। বরং user-friendly message পাঠানো হয়:

| Database Error | Client Response |
|----------------|----------------|
| Foreign key violation | "Cannot delete this record because it is referenced by other data..." |
| Duplicate/Unique constraint | "This data already exists. Please use a different value." |
| Null/Required field | "Required field is missing. Please provide all necessary information." |
| Connection timeout | "Database connection issue. Please try again later." |

---

### ৩.৪ Exception Handling Flow (সম্পূর্ণ উদাহরণ)

**Service Layer-এ Exception throw করা:**
```csharp
// Service
public async Task<UserDto> GetUserById(int id)
{
    var user = await _repository.Users.GetByIdAsync(id);
    if (user == null)
        throw new GenericNotFoundException($"User with ID {id} not found");

    return user;
}
```

**Middleware কর্তৃক handle করা ও Response:**
```json
HTTP/1.1 404 Not Found
Content-Type: application/json

{
  "statusCode": 404,
  "success": false,
  "message": "User with ID 123 not found",
  "version": "1.0",
  "timestamp": "2026-03-01T09:56:36.723Z",
  "data": null,
  "error": {
    "code": "NOT_FOUND",
    "type": "GenericNotFoundException",
    "details": null,
    "validationErrors": null,
    "stackTrace": null
  },
  "correlationId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

> **Note:** Production-এ `stackTrace` সর্বদা `null` থাকে। Development mode-এ stackTrace দেখানো হয়।

---

### ৩.৫ GlobalExceptionFilter (Action Filter Layer)

**Location:** `bdDevCRM.Presentation/ActionFIlters/GlobalExceptionFilter.cs`

এটি Controller/Action level-এ কাজ করে। যদি কোনো exception Middleware-এর আগেই Action Filter level-এ catch হয়, তাহলে এটি handle করে।

---

### ৩.৬ EmptyObjectFilterAttribute

Request body empty/null হলে এটি automatically 400 Bad Request পাঠায়:

```csharp
[ServiceFilter(typeof(EmptyObjectFilterAttribute))]
[HttpPost("login")]
public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
{
    // user কখনো null হবে না — Filter আগেই check করেছে
}
```

---

## ৪. Logger Mechanism

### সারসংক্ষেপ

এই প্রজেক্টে **দুটি Logging Framework** একসাথে ব্যবহার করা হয়:

| Framework | Location | ব্যবহার |
|-----------|---------|---------|
| **Serilog** | `Program.cs` | Structured logging, Console ও File output |
| **NLog** | `LoggerManager.cs` | Custom ILoggerManager interface |

উপরের দুটির সাথে **তিনটি Logging Middleware** আছে:
- `StructuredLoggingMiddleware` — Request/Response log
- `EnhancedAuditMiddleware` — User action audit trail
- `PerformanceMonitoringMiddleware` — Performance tracking

---

### ৪.১ Serilog — Application-level Logging

**Program.cs-এ Configuration:**
```csharp
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    // Microsoft ও EF Core-এর verbose log কমানো হয়েছে
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    // Extra context যোগ করা হয়
    .Enrich.FromLogContext()        // Scoped properties
    .Enrich.WithMachineName()       // Server নাম
    .Enrich.WithThreadId()          // Thread ID
    // Console-এ দেখা যায়
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    // File-এ লেখা হয় (Rolling daily)
    .WriteTo.File(
        path: "logs/app-.log",
        rollingInterval: RollingInterval.Day,       // প্রতিদিন নতুন file
        retainedFileCountLimit: 30)                  // ৩০ দিন রাখা হয়
    .CreateLogger();
```

**Log File Format:**
```
logs/
├── app-20260301.log
├── app-20260228.log
└── ...
```

**Console Output:**
```
[09:56:36 INF] Starting bdDevCRM Backend API
[09:56:37 INF] HTTP POST /api/authentication/login responded 200 in 45ms
[09:57:00 WRN] HTTP GET /api/users/999 responded 404 in 12ms
[09:58:00 ERR] [abc123] GenericNotFoundException: User not found
```

---

### ৪.২ NLog — ILoggerManager Interface

**LoggerManager.cs:**
```csharp
public class LoggerManager : ILoggerManager
{
    private static ILogger logger = LogManager.GetCurrentClassLogger();

    public void LogDebug(string message) => logger.Debug(message);
    public void LogError(string message) => logger.Error(message);
    public void LogInfo(string message)  => logger.Info(message);
    public void LogWarn(string message)  => logger.Warn(message);
}
```

**DI Registration:**
```csharp
// ServiceExtensions.cs
services.AddSingleton<ILoggerManager, LoggerManager>();
```

**Service-এ ব্যবহার:**
```csharp
public class SomeService
{
    private readonly ILoggerManager _logger;

    public SomeService(ILoggerManager logger)
    {
        _logger = logger;
    }

    public void DoSomething()
    {
        _logger.LogInfo("Operation started");
        _logger.LogError("Something went wrong");
        _logger.LogWarn("This is a warning");
        _logger.LogDebug("Debug information");
    }
}
```

---

### ৪.৩ StructuredLoggingMiddleware — Request/Response Logging

**Location:** `bdDevCRM.Api/Middleware/StructuredLoggingMiddleware.cs`

**`appsettings.json` Configuration:**
```json
{
  "Logging": {
    "StructuredLogging": {
      "Enabled": true,
      "LogRequestBody": true,
      "LogResponseBody": false,
      "MaxBodySize": 4096
    }
  }
}
```

**প্রতিটি Request-এ যা capture হয়:**
```
Request Information:
  - Method: POST, GET, PUT, DELETE
  - Path: /api/authentication/login
  - QueryString: ?companyId=1
  - Headers (sensitive headers redact করা হয়):
      Authorization → [REDACTED]
      Cookie       → [REDACTED]
      User-Agent   → "Mozilla/5.0..."
  - Body: { "loginId": "admin", "password": "[REDACTED]" }
  - RemoteIP: 192.168.1.100
  - User: admin (if authenticated)

Response Information:
  - StatusCode: 200
  - Duration: 45ms
  - ContentType: application/json

Correlation ID: 3fa85f64-5717-4562-b3fc-2c963f66afa6
```

**Sensitive Data Protection:**
- `authorization`, `cookie`, `x-api-key`, `x-auth-token` headers **[REDACTED]** হয়।
- Request body-তে `password`, `token`, `apikey`, `secret` fields **[REDACTED]** হয়।

**Log Level নির্ধারণ:**
```
Status >= 500   → Error   (লাল)
Status >= 400   → Warning (হলুদ)
Duration > 5s   → Warning (performance issue)
অন্যথায়        → Information (সাধারণ)
```

---

### ৪.৪ EnhancedAuditMiddleware — User Action Audit Trail

**Location:** `bdDevCRM.Api/Middleware/EnhancedAuditMiddleware.cs`

**`appsettings.json` Configuration:**
```json
{
  "AuditLogging": {
    "EnableAuditMiddleware": true
  }
}
```

**যা track করা হয়:**
- User-এর প্রতিটি data-changing action (POST, PUT, DELETE)
- GET request audit করা হয় না
- User information ClaimsPrincipal থেকে নেওয়া হয়
- Database-এ persist করা হয়

---

### ৪.৫ Exception Handling-এ Logging

`StandardExceptionMiddleware` exception catch করলে দুইভাবে log করে:

```csharp
// 1. Serilog/Microsoft.Extensions.Logging (ILogger<T>)
_logger.LogError(ex, $"[{correlationId}] {ex.GetType().Name}: {ex.Message}");

// 2. NLog (ILoggerManager)
_loggerManager.LogError($"[{correlationId}] {ex.GetType().Name}: {ex.Message}");
```

এর ফলে log **দুটি জায়গায়** যায়: Console/File (Serilog) এবং NLog destination।

---

### ৪.৬ Application Insights (Optional)

```csharp
// ServiceExtensions.cs
builder.Services.ConfigureApplicationInsights(builder.Configuration);
```

Production environment-এ Azure Application Insights-এ log পাঠানো যায়। এটি optional।

---

## মূল Architecture Diagram

```
Client Request
      │
      ▼
┌─────────────────────────────────────────────────────────────────────┐
│                    MIDDLEWARE PIPELINE (Program.cs)                  │
│                                                                       │
│  1. StandardExceptionMiddleware ◄── সব unhandled exception catch     │
│  2. StructuredLoggingMiddleware ◄── Request/Response log             │
│  3. CacheHeaderMiddleware       ◄── Cache headers যোগ করা           │
│  4. PerformanceMonitoringMiddleware ◄── Performance track            │
│  5. Session Middleware                                                │
│  6. EnhancedAuditMiddleware     ◄── User action audit                │
│  7. ResponseCompression (Gzip)                                        │
│  8. HTTPS Redirect                                                    │
│  9. CORS                                                              │
│ 10. Static Files                                                      │
│ 11. Authentication (JWT Validation)                                   │
│ 12. Authorization                                                     │
└──────────────────────────┬──────────────────────────────────────────┘
                           │
                           ▼
             ┌─────────────────────────┐
             │      Controller          │
             │  (Action Filters চলে)   │
             │  - LogActionAttribute    │
             │  - EmptyObjectFilter     │
             │  - ValidateMediaType     │
             └────────────┬────────────┘
                          │
                          ▼
             ┌─────────────────────────┐
             │    Service Layer         │
             │  (Business Logic)        │
             │  Exception throw করা    │
             └────────────┬────────────┘
                          │
                          ▼
             ┌─────────────────────────┐
             │   Repository Layer       │
             │   (Database Access)      │
             └─────────────────────────┘
```

---

## Quick Reference

### Controller-এ সঠিক Response পাঠানোর নিয়ম

```csharp
// ✅ সঠিক উপায়
return Ok(ResponseHelper.Success(data, "Data retrieved"));
return Ok(ResponseHelper.Created(newItem, "Created successfully"));
return Ok(ResponseHelper.NoContent<object>("No data found"));

// ✅ Error Response — Controller থেকে
return Unauthorized(ResponseHelper.Unauthorized("Invalid token"));
return BadRequest(ResponseHelper.BadRequest("Invalid input"));

// ✅ Exception throw করলে Middleware স্বয়ংক্রিয়ভাবে handle করবে
throw new GenericNotFoundException("Item not found");
throw new GenericBadRequestException("Invalid ID provided");
throw new UnauthorizedException("Access denied");
```

### Logging করার সঠিক উপায়

```csharp
// Constructor Injection
private readonly ILoggerManager _logger;

// Log করা
_logger.LogInfo("User logged in: " + userId);
_logger.LogWarn("Failed attempt for user: " + loginId);
_logger.LogError("Critical error occurred: " + ex.Message);
_logger.LogDebug("Debug info: " + details);
```

---

*এই ডকুমেন্ট সর্বশেষ আপডেট হয়েছে: মার্চ ২০২৬*
