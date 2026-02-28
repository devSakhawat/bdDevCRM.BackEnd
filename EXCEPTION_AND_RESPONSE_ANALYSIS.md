# Exception Handling এবং Response Mechanism বিশ্লেষণ ও Enterprise-Level সুপারিশ

## সূচিপত্র
1. [বর্তমান অবস্থা সংক্ষিপ্ত বিবরণ](#বর্তমান-অবস্থা-সংক্ষিপ্ত-বিবরণ)
2. [Exception Handling এর দুর্বল দিক](#exception-handling-এর-দুর্বল-দিক)
3. [Response Mechanism এর দুর্বল দিক](#response-mechanism-এর-দুর্বল-দিক)
4. [Enterprise-Level সুপারিশ](#enterprise-level-সুপারিশ)
5. [বাস্তবায়ন পরিকল্পনা](#বাস্তবায়ন-পরিকল্পনা)
6. [Code Examples](#code-examples)

---

## বর্তমান অবস্থা সংক্ষিপ্ত বিবরণ

### যা ভালো আছে ✅

আপনার প্রজেক্টে ইতিমধ্যে কিছু ভালো বিষয় বাস্তবায়িত:

1. **Global Exception Middleware** আছে
2. **Custom Exception Hierarchy** তৈরি করা হয়েছে
3. **Correlation ID** tracking আছে
4. **Environment-aware error messages** (Development vs Production)
5. **Database error sanitization** আছে
6. **Structured API Response** format আছে

### যা উন্নতি দরকার ⚠️

1. **Duplicate middleware files** (ExceptionMiddleware - Copy.cs, Copy (2).cs, Copy (3).cs)
2. **Inconsistent response formats**
3. **Missing validation error handling**
4. **No retry mechanism**
5. **Limited error categorization**
6. **No circuit breaker pattern**
7. **Insufficient logging details**
8. **No error metrics/monitoring**

---

## Exception Handling এর দুর্বল দিক

### 🔴 **সমস্যা ১: Duplicate Middleware Files**

**বর্তমান অবস্থা**:
```
bdDevCRM.Api/Middleware/
├── ExceptionMiddleware.cs
├── ExceptionMiddleware - Copy.cs
├── ExceptionMiddleware - Copy (2).cs
└── ExceptionMiddleware - Copy (3).cs
```

**সমস্যা**:
- ❌ কোনটি actual file তা বোঝা যাচ্ছে না
- ❌ Version control confusion
- ❌ Maintainability issue
- ❌ একাধিক version থাকলে bug fix করা কঠিন

**প্রভাব**:
- Code confusion
- Merge conflicts
- Bug fix একটা file-এ হলেও অন্যগুলোতে থেকে যায়

---

### 🔴 **সমস্যা ২: Exception Hierarchy Commented Out**

**বর্তমান অবস্থা**:
`BaseCustomException.cs` file এ **297 lines code commented out**

```csharp
// ✅ Better design (কিন্তু commented out)
//public abstract class BaseCustomException : Exception
//{
//    public abstract int StatusCode { get; }
//    public abstract string ErrorCode { get; }
//    public virtual string UserFriendlyMessage => Message;
//    public Dictionary<string, object> AdditionalData { get; } = new();
//}

// ❌ বর্তমানে এটি ব্যবহার হচ্ছে (Simpler but limited)
public class BadRequestException : Exception
{
    public int StatusCode { get; } = 400;
}
```

**সমস্যা**:
- ❌ **ErrorCode** property নেই - error tracking কঠিন
- ❌ **AdditionalData** নেই - context information pass করা যায় না
- ❌ **UserFriendlyMessage** vs **Message** separation নেই
- ❌ Abstract base class নেই - consistency enforce করা যায় না

**বর্তমানে কী হচ্ছে**:
```csharp
// Exception throw করার সময়
throw new GenericNotFoundException("User", "UserId", userId.ToString());

// কিন্তু কোথাও track করা যাচ্ছে না:
// - কোন module থেকে এসেছে?
// - User কে ছিল?
// - কোন action attempt করছিল?
// - Related entity IDs কি?
```

**প্রভাব**:
- Debugging কঠিন
- Error analysis করা যায় না
- User support দিতে সমস্যা
- Monitoring/alerting সঠিকভাবে setup করা যায় না

---

### 🔴 **সমস্যা ৩: Limited Error Context**

**বর্তমান exception শুধু message দেয়**:
```csharp
public class GenericNotFoundException : NotFoundException
{
    public GenericNotFoundException(string entityName, string propertyName, string value)
        : base($"The {entityName} with {propertyName}: {value} doesn't exist.")
    {
        // ❌ কোনো additional context নেই
    }
}
```

**Enterprise-level-এ যা দরকার**:
```csharp
// ✅ Rich context
var ex = new GenericNotFoundException("User", "UserId", "123");
ex.AdditionalData["RequestedBy"] = currentUser.Id;
ex.AdditionalData["Module"] = "UserManagement";
ex.AdditionalData["Action"] = "UpdateProfile";
ex.AdditionalData["Timestamp"] = DateTime.UtcNow;
ex.AdditionalData["ClientIp"] = context.Connection.RemoteIpAddress;
throw ex;
```

**কেন প্রয়োজন**:
- **Security audit**: কে কখন কোন resource access করতে চেয়েছিল
- **Usage analytics**: কোন features বেশি error দিচ্ছে
- **User support**: Customer support team-কে context দিতে
- **Root cause analysis**: Pattern খুঁজে বের করতে

---

### 🔴 **সমস্যা ৪: No Validation Error Structure**

**বর্তমান অবস্থা**:
Validation errors খুব basic:

```csharp
// ExceptionMiddleware.cs
ValidationException validation => CreateResponse(
    400,
    "One or more validation errors occurred.", // ❌ Generic message
    "Validation",
    correlationId
)
```

**সমস্যা**:
- ❌ কোন field-এ error তা জানা যায় না
- ❌ Multiple validation errors একসাথে return করা যায় না
- ❌ Frontend field-wise error দেখাতে পারে না
- ❌ Error code নেই - internationalization করা যায় না

**বর্তমানে Client কী পায়**:
```json
{
  "statusCode": 400,
  "message": "One or more validation errors occurred.",
  "errorType": "Validation"
}
```

**Enterprise-level-এ Client কী পাওয়া উচিত**:
```json
{
  "statusCode": 400,
  "message": "Validation failed",
  "errorType": "ValidationError",
  "errors": [
    {
      "field": "Email",
      "message": "Email is required",
      "errorCode": "REQUIRED_FIELD"
    },
    {
      "field": "Email",
      "message": "Email format is invalid",
      "errorCode": "INVALID_EMAIL_FORMAT"
    },
    {
      "field": "Password",
      "message": "Password must be at least 8 characters",
      "errorCode": "PASSWORD_TOO_SHORT"
    }
  ]
}
```

**প্রভাব**:
- Frontend UX খারাপ - user জানে না কী ভুল
- Mobile apps field-wise error highlighting করতে পারে না
- Multilingual support করা impossible

---

### 🔴 **সমস্যা ৫: No Retry Logic for Transient Errors**

**বর্তমান অবস্থা**:
Database connection error হলে সরাসরি 500 error return হয়:

```csharp
DbUpdateException dbUpdate => CreateResponse(
    500,
    SanitizeDatabaseErrorMessage(ex),
    "DatabaseError",
    correlationId
)
```

**সমস্যা**:
- ❌ Database temporarily down থাকলেও retry করে না
- ❌ Network glitch-এ permanent failure মনে হয়
- ❌ Rate limiting hit হলেও immediate failure
- ❌ Circuit breaker নেই - failed service-কে বারবার call করতে থাকে

**Transient errors যেগুলো retry করা উচিত**:
```
✅ Database connection timeout
✅ Network temporary unavailable
✅ HTTP 429 (Too Many Requests)
✅ HTTP 503 (Service Unavailable)
✅ SQL Server deadlock
✅ Redis connection lost
```

**Enterprise pattern**:
```csharp
// Polly library ব্যবহার করে
var retryPolicy = Policy
    .Handle<SqlException>(ex => IsTransient(ex))
    .Or<HttpRequestException>()
    .WaitAndRetryAsync(
        retryCount: 3,
        sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)), // Exponential backoff
        onRetry: (exception, timeSpan, retryCount, context) =>
        {
            _logger.LogWarning($"Retry {retryCount} after {timeSpan.TotalSeconds}s due to {exception.Message}");
        }
    );

var result = await retryPolicy.ExecuteAsync(async () =>
{
    return await _repository.GetUserAsync(userId);
});
```

**প্রভাব**:
- Temporary issues-এ service down মনে হয়
- User experience খারাপ
- Unnecessary error reports

---

### 🔴 **সমস্যা ৬: Poor Logging Context**

**বর্তমান logging**:
```csharp
_logger.LogError(ex, $"[{correlationId}] {ex.GetType().Name}: {ex.Message}");
_loggerManager.LogError($"[{correlationId}] {ex.GetType().Name}: {ex.Message}");
```

**সমস্যা**:
- ❌ Request context নেই (URL, Method, User)
- ❌ Performance metrics নেই (Duration)
- ❌ Structured logging নেই
- ❌ Log levels সঠিকভাবে ব্যবহার করা হয় নি

**বর্তমানে log এরকম**:
```
[Error] [guid-123] GenericNotFoundException: User with UserId: 456 not found
```

**Enterprise-level logging হওয়া উচিত**:
```json
{
  "timestamp": "2024-02-28T10:30:00Z",
  "level": "Error",
  "correlationId": "guid-123",
  "exceptionType": "GenericNotFoundException",
  "message": "User not found",
  "context": {
    "userId": 456,
    "requestedBy": 789,
    "httpMethod": "GET",
    "url": "/api/users/456",
    "userAgent": "Mozilla/5.0...",
    "clientIp": "192.168.1.100",
    "duration": 125,
    "module": "UserManagement",
    "action": "GetUserById"
  },
  "stackTrace": "...",
  "innerException": null
}
```

**কেন structured logging দরকার**:
- **Elasticsearch/Splunk-এ query** করা যায়
- **Dashboard/Metrics** তৈরি করা যায়
- **Alerting rules** লেখা যায়
- **Pattern detection** automated হয়

---

### 🔴 **সমস্যা ৭: No Circuit Breaker Pattern**

**বর্তমান অবস্থা**:
External service/database down থাকলেও বারবার call করতে থাকে

**সমস্যা scenario**:
```
1. Database connection issue শুরু হলো
2. প্রতিটি request database-এ connect করার চেষ্টা করে
3. প্রতিটি request 30 second timeout পর্যন্ত wait করে
4. 100 concurrent requests = 100 threads blocked
5. Server hang/crash হয়ে যায়
```

**Enterprise solution: Circuit Breaker**:
```
State 1: CLOSED (Normal) - সব requests যায়
   ↓ (Failure threshold: 5 errors in 10 seconds)
State 2: OPEN (Broken) - কোনো request যায় না, immediate fail
   ↓ (After 30 seconds)
State 3: HALF-OPEN (Testing) - 1টা request test করে
   ↓ (Success) → CLOSED | (Fail) → OPEN
```

**সুবিধা**:
- ✅ Failed service-কে "rest" করার সুযোগ দেয়
- ✅ Server resources protect করে
- ✅ Fast fail - user দ্রুত error পায়
- ✅ Cascading failures prevent করে

---

### 🔴 **সমস্যা ৮: Exception Handling in Loops**

**Code-এ এরকম pattern আছে**:
```csharp
foreach (var item in items)
{
    try
    {
        await ProcessItemAsync(item);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error processing item");
        // Continue - ❌ Silent failure
    }
}
```

**সমস্যা**:
- ❌ Partial success/failure track করা হয় না
- ❌ Caller জানে না কতগুলো failed
- ❌ Rollback mechanism নেই
- ❌ Failed items পরে retry করা যায় না

**Enterprise approach**:
```csharp
public class BatchProcessingResult<T>
{
    public int TotalItems { get; set; }
    public int SuccessCount { get; set; }
    public int FailedCount { get; set; }
    public List<ProcessingResult<T>> Results { get; set; }
    public TimeSpan Duration { get; set; }
}

public class ProcessingResult<T>
{
    public T Item { get; set; }
    public bool Success { get; set; }
    public string ErrorMessage { get; set; }
    public string ErrorCode { get; set; }
}

// Usage
var results = new List<ProcessingResult<Item>>();
var stopwatch = Stopwatch.StartNew();

foreach (var item in items)
{
    try
    {
        var result = await ProcessItemAsync(item);
        results.Add(new ProcessingResult<Item>
        {
            Success = true,
            Item = item
        });
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Error processing item {ItemId}", item.Id);
        results.Add(new ProcessingResult<Item>
        {
            Success = false,
            Item = item,
            ErrorMessage = ex.Message,
            ErrorCode = GetErrorCode(ex)
        });
    }
}

return new BatchProcessingResult<Item>
{
    TotalItems = items.Count,
    SuccessCount = results.Count(r => r.Success),
    FailedCount = results.Count(r => !r.Success),
    Results = results,
    Duration = stopwatch.Elapsed
};
```

---

### 🔴 **সমস্যা ৯: No Exception Metrics/Monitoring**

**বর্তমান অবস্থা**:
Exceptions শুধু log হয়, কিন্তু metrics track করা হয় না

**কী track করা উচিত**:
```
✅ Exception count by type
✅ Exception rate (per minute/hour)
✅ Top 10 most frequent exceptions
✅ Exceptions by endpoint
✅ Exceptions by user
✅ Mean time between failures (MTBF)
✅ Error rate trends
✅ P95/P99 error response times
```

**Enterprise monitoring setup**:
```csharp
public class ExceptionMetrics
{
    private readonly IMetricsCollector _metrics;

    public void RecordException(Exception ex, HttpContext context)
    {
        // Count by type
        _metrics.Increment($"exceptions.{ex.GetType().Name}");

        // Count by endpoint
        _metrics.Increment($"exceptions.endpoint.{context.Request.Path}");

        // Count by status code
        _metrics.Increment($"exceptions.status.{GetStatusCode(ex)}");

        // Track response time
        var duration = GetRequestDuration(context);
        _metrics.RecordTiming("exceptions.duration", duration);
    }
}
```

**Alerting rules**:
```yaml
# Alert when error rate > 5% for 5 minutes
- alert: HighErrorRate
  expr: rate(exceptions_total[5m]) / rate(requests_total[5m]) > 0.05
  for: 5m
  annotations:
    summary: "High error rate detected"

# Alert when specific error spikes
- alert: DatabaseConnectionErrors
  expr: increase(exceptions_DbUpdateException[5m]) > 10
  annotations:
    summary: "Database connection issues"
```

---

### 🔴 **সমস্যা ১০: No Graceful Degradation**

**বর্তমান অবস্থা**:
একটা dependency fail হলে পুরো request fail হয়

**Example scenario**:
```csharp
// User profile page load করার সময়
var user = await _userService.GetUserAsync(userId); // Essential
var recentActivities = await _activityService.GetRecentAsync(userId); // Nice to have
var recommendations = await _recommendationService.GetAsync(userId); // Nice to have

// যদি recommendation service down থাকে, পুরো page fail হয়ে যায় ❌
```

**Enterprise approach: Graceful degradation**:
```csharp
public async Task<UserProfileResponse> GetUserProfileAsync(int userId)
{
    var response = new UserProfileResponse();

    // Essential data - MUST succeed
    try
    {
        response.User = await _userService.GetUserAsync(userId);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Failed to load user {UserId}", userId);
        throw; // Re-throw - এটা ছাড়া page load করা যাবে না
    }

    // Optional data - CAN fail
    try
    {
        response.RecentActivities = await _activityService
            .GetRecentAsync(userId)
            .WithTimeout(TimeSpan.FromSeconds(2)); // Fast timeout
    }
    catch (Exception ex)
    {
        _logger.LogWarning(ex, "Failed to load activities for user {UserId}", userId);
        response.RecentActivities = new List<Activity>(); // Empty list
        response.Warnings.Add("Recent activities unavailable");
    }

    // Optional data with fallback
    try
    {
        response.Recommendations = await _recommendationService.GetAsync(userId);
    }
    catch (Exception ex)
    {
        _logger.LogWarning(ex, "Failed to load recommendations for user {UserId}", userId);
        response.Recommendations = await _cacheService.GetCachedRecommendations(userId); // Fallback
    }

    return response;
}
```

**সুবিধা**:
- ✅ Partial functionality available
- ✅ Better user experience
- ✅ System more resilient
- ✅ Clear warnings to user

---

## Response Mechanism এর দুর্বল দিক

### 🔴 **সমস্যা ১: Inconsistent Response Format**

**বর্তমান অবস্থা**:
দুটি আলাদা response class আছে:

```
bdDevCRM.Shared/ApiResponse/
├── ApiResponse.cs         (Generic wrapper)
└── ErrorResponse.cs       (Error specific)

bdDevCRM.Api/ApiResponseError/
└── ApiResponse.cs         (Duplicate?)
```

**Different response formats ব্যবহার হচ্ছে**:

**Success Response**:
```csharp
// ResponseHelper.Success() returns:
{
  "statusCode": 200,
  "message": "Operation completed successfully",
  "isSuccess": true,
  "timestamp": "2024-02-28T10:30:00Z",
  "data": { /* actual data */ }
}
```

**Error Response**:
```csharp
// ExceptionMiddleware returns:
{
  "statusCode": 404,
  "message": "User not found",
  "isSuccess": false,
  "timestamp": "2024-02-28T10:30:00Z",
  "errorType": "GenericNotFoundException",
  "correlationId": "guid-123",
  "details": "stack trace..."
}
```

**সমস্যা**:
- ❌ Success এবং Error response structure different
- ❌ `ErrorResponse.cs` file আছে কিন্তু ব্যবহার হয় না
- ❌ Frontend-এ two different parsers লাগবে
- ❌ `validationErrors` property কখনো populate হয় না

---

### 🔴 **সমস্যা ২: Limited HTTP Status Code Usage**

**বর্তমানে যা ব্যবহার হয়**:
```
✅ 200 OK
✅ 201 Created
✅ 204 No Content
✅ 400 Bad Request
✅ 401 Unauthorized
✅ 403 Forbidden
✅ 404 Not Found
✅ 409 Conflict
✅ 500 Internal Server Error
✅ 503 Service Unavailable
```

**যা ব্যবহার হয় না (কিন্তু Enterprise-level-এ দরকার)**:
```
❌ 202 Accepted (Async operations)
❌ 206 Partial Content (Pagination, large files)
❌ 207 Multi-Status (Batch operations)
❌ 304 Not Modified (Caching)
❌ 408 Request Timeout
❌ 422 Unprocessable Entity (Validation)
❌ 423 Locked (Resource locked)
❌ 429 Too Many Requests (Rate limiting)
❌ 502 Bad Gateway (Upstream service failed)
❌ 504 Gateway Timeout (Upstream timeout)
```

**Example scenarios**:

**Scenario 1: Long-running operation**
```csharp
// ❌ বর্তমানে - request block হয়ে থাকে
[HttpPost("generate-report")]
public async Task<IActionResult> GenerateReport(ReportRequest request)
{
    var report = await _reportService.GenerateAsync(request); // 2 minutes wait
    return Ok(ResponseHelper.Success(report));
}

// ✅ Enterprise approach - async with 202 Accepted
[HttpPost("generate-report")]
public async Task<IActionResult> GenerateReport(ReportRequest request)
{
    var jobId = await _reportService.QueueReportGenerationAsync(request);

    return Accepted(new
    {
        jobId,
        statusUrl = $"/api/reports/status/{jobId}",
        message = "Report generation started"
    });
}

[HttpGet("status/{jobId}")]
public async Task<IActionResult> GetReportStatus(string jobId)
{
    var status = await _reportService.GetJobStatusAsync(jobId);

    if (status.IsCompleted)
        return Ok(new { status = "completed", downloadUrl = status.DownloadUrl });

    return Ok(new { status = "processing", progress = status.Progress });
}
```

**Scenario 2: Rate limiting**
```csharp
// ✅ 429 Too Many Requests return করা উচিত
public class RateLimitMiddleware
{
    public async Task InvokeAsync(HttpContext context)
    {
        var rateLimiter = context.RequestServices.GetRequiredService<IRateLimiter>();

        if (!await rateLimiter.IsAllowedAsync(context))
        {
            context.Response.StatusCode = 429;
            context.Response.Headers.Add("Retry-After", "60");

            await context.Response.WriteAsJsonAsync(new
            {
                statusCode = 429,
                message = "Too many requests. Please try again later.",
                retryAfter = 60
            });

            return;
        }

        await _next(context);
    }
}
```

---

### 🔴 **সমস্যা ৩: No Response Caching Headers**

**বর্তমান অবস্থা**:
Response-এ caching headers নেই

```csharp
// Controllers থেকে response
return Ok(ResponseHelper.Success(countries));
```

**Response headers**:
```
HTTP/1.1 200 OK
Content-Type: application/json
Date: Thu, 28 Feb 2024 10:30:00 GMT

{ "data": [...] }
```

**সমস্যা**:
- ❌ Browser cache করতে পারে না
- ❌ CDN cache করতে পারে না
- ❌ Unnecessary database calls
- ❌ Bandwidth waste
- ❌ Slow user experience

**Enterprise approach**:
```csharp
// Static data (Countries, Currencies) - Long cache
[HttpGet("countries")]
[ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Any)] // 24 hours
public async Task<IActionResult> GetCountries()
{
    var countries = await _service.GetAllCountriesAsync();

    Response.Headers.Add("Cache-Control", "public, max-age=86400");
    Response.Headers.Add("ETag", GenerateETag(countries));

    return Ok(ResponseHelper.Success(countries));
}

// Dynamic data - Short cache with validation
[HttpGet("users/{id}")]
public async Task<IActionResult> GetUser(int id)
{
    var requestETag = Request.Headers["If-None-Match"];
    var user = await _service.GetUserAsync(id);
    var currentETag = GenerateETag(user);

    // Client has latest version
    if (requestETag == currentETag)
    {
        return StatusCode(304); // Not Modified - saves bandwidth
    }

    Response.Headers.Add("Cache-Control", "private, max-age=300"); // 5 minutes
    Response.Headers.Add("ETag", currentETag);

    return Ok(ResponseHelper.Success(user));
}
```

**সুবিধা**:
- ✅ Bandwidth savings: 60-80%
- ✅ Database load reduction: 70%
- ✅ Faster page loads
- ✅ Better scalability

---

### 🔴 **সমস্যা ৪: No Pagination Metadata**

**বর্তমান pagination response**:
```csharp
return Ok(ResponseHelper.Success(pagedData, "Data retrieved"));
```

**Response**:
```json
{
  "statusCode": 200,
  "message": "Data retrieved",
  "data": [
    { "id": 1, "name": "..." },
    { "id": 2, "name": "..." }
  ]
}
```

**সমস্যা**:
- ❌ Total count নেই - frontend জানে না কত page আছে
- ❌ HasNext/HasPrevious নেই
- ❌ Current page number নেই
- ❌ Links to next/previous page নেই (HATEOAS)

**Enterprise pagination response**:
```json
{
  "statusCode": 200,
  "message": "Data retrieved successfully",
  "data": [...],
  "pagination": {
    "currentPage": 2,
    "pageSize": 20,
    "totalPages": 15,
    "totalCount": 287,
    "hasPrevious": true,
    "hasNext": true,
    "links": {
      "first": "/api/users?page=1&pageSize=20",
      "previous": "/api/users?page=1&pageSize=20",
      "self": "/api/users?page=2&pageSize=20",
      "next": "/api/users?page=3&pageSize=20",
      "last": "/api/users?page=15&pageSize=20"
    }
  }
}
```

**Implementation**:
```csharp
public class PagedResponse<T> : ApiResponse<List<T>>
{
    public PaginationMetadata Pagination { get; set; }

    public PagedResponse(List<T> data, int page, int pageSize, int totalCount, string baseUrl)
        : base(data, 200)
    {
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        Pagination = new PaginationMetadata
        {
            CurrentPage = page,
            PageSize = pageSize,
            TotalPages = totalPages,
            TotalCount = totalCount,
            HasPrevious = page > 1,
            HasNext = page < totalPages,
            Links = new PaginationLinks
            {
                First = $"{baseUrl}?page=1&pageSize={pageSize}",
                Previous = page > 1 ? $"{baseUrl}?page={page - 1}&pageSize={pageSize}" : null,
                Self = $"{baseUrl}?page={page}&pageSize={pageSize}",
                Next = page < totalPages ? $"{baseUrl}?page={page + 1}&pageSize={pageSize}" : null,
                Last = $"{baseUrl}?page={totalPages}&pageSize={pageSize}"
            }
        };
    }
}

public class PaginationMetadata
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public bool HasPrevious { get; set; }
    public bool HasNext { get; set; }
    public PaginationLinks Links { get; set; }
}

public class PaginationLinks
{
    public string First { get; set; }
    public string Previous { get; set; }
    public string Self { get; set; }
    public string Next { get; set; }
    public string Last { get; set; }
}
```

---

### 🔴 **সমস্যা ৫: No Content Negotiation**

**বর্তমান অবস্থা**:
শুধু JSON support করে

**সমস্যা**:
- ❌ XML support নেই (কিছু legacy system XML চায়)
- ❌ CSV export করা যায় না
- ❌ Excel export করা যায় না
- ❌ PDF reports generate করা যায় না

**Enterprise support করা উচিত**:
```csharp
[HttpGet("users")]
public async Task<IActionResult> GetUsers(
    [FromQuery] string format = "json") // json, xml, csv, excel
{
    var users = await _service.GetAllUsersAsync();

    return format.ToLower() switch
    {
        "xml" => Ok(users) with ContentType("application/xml"),
        "csv" => File(GenerateCsv(users), "text/csv", "users.csv"),
        "excel" => File(GenerateExcel(users), "application/vnd.ms-excel", "users.xlsx"),
        _ => Ok(ResponseHelper.Success(users))
    };
}

// অথবা Accept header দিয়ে
[HttpGet("users")]
[Produces("application/json", "application/xml", "text/csv")]
public async Task<IActionResult> GetUsers()
{
    var users = await _service.GetAllUsersAsync();
    return Ok(users); // Framework automatically negotiates
}
```

---

### 🔴 **সমস্যা ৬: No API Versioning in Responses**

**বর্তমান response**:
```json
{
  "statusCode": 200,
  "message": "Success",
  "data": { }
}
```

**সমস্যা**:
- ❌ Client জানে না কোন API version থেকে response এসেছে
- ❌ Breaking changes track করা কঠিন
- ❌ Debugging difficult when multiple versions live

**Enterprise response**:
```json
{
  "statusCode": 200,
  "message": "Success",
  "apiVersion": "2.0",
  "data": { },
  "deprecationWarning": null,
  "links": {
    "self": "/api/v2/users/123",
    "v1": "/api/v1/users/123"
  }
}
```

**With deprecation warning**:
```json
{
  "statusCode": 200,
  "message": "Success",
  "apiVersion": "1.0",
  "deprecationWarning": {
    "message": "This API version will be deprecated on 2024-12-31",
    "newVersion": "2.0",
    "migrationGuide": "https://docs.api.com/migration/v1-to-v2"
  },
  "data": { }
}
```

---

### 🔴 **সমস্যা ৭: No Response Compression Strategy**

**বর্তমান অবস্থা**:
GZIP compression enabled আছে কিন্তু:

```csharp
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});
```

**সমস্যা**:
- ❌ Compression threshold set করা নেই
- ❌ Small responses-ও compress হচ্ছে (overhead)
- ❌ Brotli support নেই (better compression)
- ❌ Image/video compress করার চেষ্টা করছে (waste)

**Enterprise configuration**:
```csharp
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;

    // Brotli first, then GZIP
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();

    // Only compress these MIME types
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
    {
        "application/json",
        "application/xml",
        "text/plain",
        "text/html",
        "text/css",
        "text/javascript",
        "application/javascript"
    });
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Optimal;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Optimal;
});

// Middleware to skip small responses
app.Use(async (context, next) =>
{
    var originalBodyStream = context.Response.Body;
    using var responseBody = new MemoryStream();
    context.Response.Body = responseBody;

    await next();

    // Only compress if > 1KB
    if (responseBody.Length > 1024)
    {
        context.Response.Body = originalBodyStream;
        responseBody.Seek(0, SeekOrigin.Begin);
        await responseBody.CopyToAsync(originalBodyStream);
    }
    else
    {
        // Skip compression for small responses
        context.Response.Headers.Remove("Content-Encoding");
        context.Response.Body = originalBodyStream;
        responseBody.Seek(0, SeekOrigin.Begin);
        await responseBody.CopyToAsync(originalBodyStream);
    }
});
```

**Compression benefits**:
```
Original JSON: 1.2 MB
GZIP: 250 KB (79% reduction)
Brotli: 180 KB (85% reduction)
```

---

### 🔴 **সমস্যা ৮: No Batch Operation Response**

**বর্তমান অবস্থা**:
Batch operations-এ শুধু success/failure বলে:

```csharp
[HttpPost("batch-delete")]
public async Task<IActionResult> BatchDelete([FromBody] List<int> ids)
{
    await _service.DeleteManyAsync(ids);
    return Ok(ResponseHelper.Success("Deleted successfully"));
}
```

**সমস্যা**:
- ❌ কোনটা delete হয়েছে কোনটা হয় নি জানা যায় না
- ❌ Partial success-এর status unclear
- ❌ Error details পাওয়া যায় না

**Enterprise batch response**:
```json
{
  "statusCode": 207,
  "message": "Batch operation completed with some failures",
  "data": {
    "totalRequested": 10,
    "successCount": 7,
    "failedCount": 3,
    "duration": "1.5s",
    "results": [
      {
        "id": 1,
        "status": 200,
        "success": true,
        "message": "Deleted successfully"
      },
      {
        "id": 2,
        "status": 404,
        "success": false,
        "message": "User not found",
        "errorCode": "USER_NOT_FOUND"
      },
      {
        "id": 3,
        "status": 200,
        "success": true,
        "message": "Deleted successfully"
      }
    ]
  }
}
```

**Implementation**:
```csharp
public class BatchOperationResult<T>
{
    public int TotalRequested { get; set; }
    public int SuccessCount { get; set; }
    public int FailedCount { get; set; }
    public TimeSpan Duration { get; set; }
    public List<BatchItemResult<T>> Results { get; set; }
}

public class BatchItemResult<T>
{
    public T Id { get; set; }
    public int Status { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
    public string ErrorCode { get; set; }
    public Dictionary<string, object> Metadata { get; set; }
}

[HttpPost("batch-delete")]
public async Task<IActionResult> BatchDelete([FromBody] List<int> ids)
{
    var stopwatch = Stopwatch.StartNew();
    var results = new List<BatchItemResult<int>>();

    foreach (var id in ids)
    {
        try
        {
            await _service.DeleteAsync(id);
            results.Add(new BatchItemResult<int>
            {
                Id = id,
                Status = 200,
                Success = true,
                Message = "Deleted successfully"
            });
        }
        catch (NotFoundException)
        {
            results.Add(new BatchItemResult<int>
            {
                Id = id,
                Status = 404,
                Success = false,
                Message = "User not found",
                ErrorCode = "USER_NOT_FOUND"
            });
        }
        catch (Exception ex)
        {
            results.Add(new BatchItemResult<int>
            {
                Id = id,
                Status = 500,
                Success = false,
                Message = ex.Message,
                ErrorCode = "INTERNAL_ERROR"
            });
        }
    }

    var result = new BatchOperationResult<int>
    {
        TotalRequested = ids.Count,
        SuccessCount = results.Count(r => r.Success),
        FailedCount = results.Count(r => !r.Success),
        Duration = stopwatch.Elapsed,
        Results = results
    };

    // 207 Multi-Status if partial success/failure
    var statusCode = result.FailedCount > 0 && result.SuccessCount > 0 ? 207 : 200;

    return StatusCode(statusCode, result);
}
```

---

### 🔴 **সমস্যা ৯: No HATEOAS (Hypermedia)**

**বর্তমান response**:
```json
{
  "statusCode": 200,
  "data": {
    "id": 123,
    "name": "John Doe",
    "email": "john@example.com"
  }
}
```

**সমস্যা**:
- ❌ Client কে hardcode করতে হয় next actions
- ❌ API changes-এ client break হয়
- ❌ Discoverability নেই

**Enterprise HATEOAS response**:
```json
{
  "statusCode": 200,
  "data": {
    "id": 123,
    "name": "John Doe",
    "email": "john@example.com",
    "status": "active"
  },
  "_links": {
    "self": {
      "href": "/api/users/123",
      "method": "GET"
    },
    "update": {
      "href": "/api/users/123",
      "method": "PUT"
    },
    "delete": {
      "href": "/api/users/123",
      "method": "DELETE"
    },
    "deactivate": {
      "href": "/api/users/123/deactivate",
      "method": "POST"
    },
    "change-password": {
      "href": "/api/users/123/change-password",
      "method": "POST"
    },
    "applications": {
      "href": "/api/users/123/applications",
      "method": "GET"
    }
  }
}
```

**সুবিধা**:
- ✅ Self-documenting API
- ✅ Client loosely coupled
- ✅ Conditional links (based on permissions/state)
- ✅ Better API evolution

---

### 🔴 **সমস্যা ১০: No Response Time Tracking**

**বর্তমান response**:
```json
{
  "statusCode": 200,
  "timestamp": "2024-02-28T10:30:00Z",
  "data": { }
}
```

**সমস্যা**:
- ❌ Client জানে না request কত সময় নিয়েছে
- ❌ Performance monitoring client-side করা কঠিন
- ❌ Slow queries identify করা যায় না

**Enterprise response with timing**:
```json
{
  "statusCode": 200,
  "timestamp": "2024-02-28T10:30:00Z",
  "data": { },
  "performance": {
    "duration": 245,
    "breakdown": {
      "database": 180,
      "processing": 50,
      "serialization": 15
    }
  }
}
```

**Response headers**:
```
X-Response-Time: 245ms
X-Database-Time: 180ms
X-Cache-Hit: false
X-Rate-Limit-Remaining: 95
X-Request-Id: guid-123
```

**Implementation**:
```csharp
public class PerformanceTrackingMiddleware
{
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        // Track database time
        var dbStopwatch = Stopwatch.StartNew();
        // ... database calls ...
        var dbTime = dbStopwatch.ElapsedMilliseconds;

        await _next(context);

        stopwatch.Stop();

        // Add performance headers
        context.Response.Headers.Add("X-Response-Time", $"{stopwatch.ElapsedMilliseconds}ms");
        context.Response.Headers.Add("X-Database-Time", $"{dbTime}ms");
        context.Response.Headers.Add("X-Request-Id", context.TraceIdentifier);

        // Log slow requests
        if (stopwatch.ElapsedMilliseconds > 1000)
        {
            _logger.LogWarning(
                "Slow request detected: {Method} {Path} took {Duration}ms",
                context.Request.Method,
                context.Request.Path,
                stopwatch.ElapsedMilliseconds
            );
        }
    }
}
```

---

## Enterprise-Level সুপারিশ

### 🎯 **Phase 1: Foundation (Week 1-2)**

#### 1. Exception Hierarchy Reorganization

**Step 1: Implement proper base exception**

```csharp
// bdDevCRM.Shared/Exceptions/Base/BaseCustomException.cs
public abstract class BaseCustomException : Exception
{
    public abstract int StatusCode { get; }
    public abstract string ErrorCode { get; }

    public string UserFriendlyMessage { get; set; }
    public string CorrelationId { get; set; }
    public Dictionary<string, object> AdditionalData { get; } = new();
    public DateTime Timestamp { get; } = DateTime.UtcNow;

    protected BaseCustomException(string message) : base(message)
    {
        UserFriendlyMessage = message;
    }

    protected BaseCustomException(string message, Exception innerException)
        : base(message, innerException)
    {
        UserFriendlyMessage = message;
    }

    public BaseCustomException WithData(string key, object value)
    {
        AdditionalData[key] = value;
        return this;
    }

    public BaseCustomException WithCorrelationId(string correlationId)
    {
        CorrelationId = correlationId;
        return this;
    }
}
```

**Step 2: Create category exceptions**

```csharp
// Business Logic Exceptions (400-499)
public abstract class BusinessException : BaseCustomException
{
    public override int StatusCode => 400;
    protected BusinessException(string message) : base(message) { }
    protected BusinessException(string message, Exception innerException)
        : base(message, innerException) { }
}

// Domain Exceptions (422)
public abstract class DomainException : BaseCustomException
{
    public override int StatusCode => 422;
    protected DomainException(string message) : base(message) { }
}

// Infrastructure Exceptions (500+)
public abstract class InfrastructureException : BaseCustomException
{
    public override int StatusCode => 500;
    protected InfrastructureException(string message) : base(message) { }
}
```

**Step 3: Update existing exceptions**

```csharp
// Before
public class GenericNotFoundException : NotFoundException
{
    public GenericNotFoundException(string entityName, string propertyName, string value)
        : base($"The {entityName} with {propertyName}: {value} doesn't exist.")
    {
    }
}

// After
public class GenericNotFoundException : NotFoundException
{
    public override string ErrorCode => "RESOURCE_NOT_FOUND";

    public GenericNotFoundException(string entityName, string propertyName, string value)
        : base($"The {entityName} with {propertyName}: {value} doesn't exist in the database.")
    {
        AdditionalData["EntityName"] = entityName;
        AdditionalData["PropertyName"] = propertyName;
        AdditionalData["SearchValue"] = value;
    }
}

// Usage
throw new GenericNotFoundException("User", "UserId", userId.ToString())
    .WithCorrelationId(context.TraceIdentifier)
    .WithData("RequestedBy", currentUser.Id)
    .WithData("Action", "GetUserProfile");
```

---

#### 2. Unified Response Format

**Create single response wrapper**:

```csharp
// bdDevCRM.Shared/ApiResponse/ApiResponseV2.cs
public class ApiResponseV2<T>
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
    public DateTime Timestamp { get; set; }
    public string CorrelationId { get; set; }
    public string ApiVersion { get; set; }

    // Success data
    public T Data { get; set; }

    // Error data
    public ErrorDetails Error { get; set; }

    // Metadata
    public ResponseMetadata Metadata { get; set; }

    // Links (HATEOAS)
    public Dictionary<string, Link> Links { get; set; }

    public ApiResponseV2()
    {
        Timestamp = DateTime.UtcNow;
        ApiVersion = "2.0";
    }
}

public class ErrorDetails
{
    public string ErrorCode { get; set; }
    public string ErrorType { get; set; }
    public string StackTrace { get; set; } // Only in development
    public List<ValidationError> ValidationErrors { get; set; }
    public Dictionary<string, object> AdditionalData { get; set; }
}

public class ResponseMetadata
{
    public int? Duration { get; set; } // Response time in ms
    public PaginationMetadata Pagination { get; set; }
    public PerformanceMetrics Performance { get; set; }
}

public class Link
{
    public string Href { get; set; }
    public string Method { get; set; }
    public string Description { get; set; }
}

public class PerformanceMetrics
{
    public int DatabaseTime { get; set; }
    public int CacheTime { get; set; }
    public int ProcessingTime { get; set; }
    public bool CacheHit { get; set; }
}
```

**Response builder**:

```csharp
public static class ApiResponseBuilder
{
    // Success responses
    public static ApiResponseV2<T> Success<T>(
        T data,
        string message = "Operation completed successfully",
        int statusCode = 200)
    {
        return new ApiResponseV2<T>
        {
            StatusCode = statusCode,
            Message = message,
            Success = true,
            Data = data
        };
    }

    // Error responses
    public static ApiResponseV2<object> Error(
        int statusCode,
        string message,
        string errorCode,
        string errorType,
        string correlationId)
    {
        return new ApiResponseV2<object>
        {
            StatusCode = statusCode,
            Message = message,
            Success = false,
            CorrelationId = correlationId,
            Error = new ErrorDetails
            {
                ErrorCode = errorCode,
                ErrorType = errorType
            }
        };
    }

    // With pagination
    public static ApiResponseV2<List<T>> Paginated<T>(
        List<T> data,
        int page,
        int pageSize,
        int totalCount,
        string baseUrl)
    {
        var response = Success(data);

        response.Metadata = new ResponseMetadata
        {
            Pagination = new PaginationMetadata
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                TotalCount = totalCount
            }
        };

        // Add HATEOAS links
        response.Links = new Dictionary<string, Link>
        {
            ["self"] = new Link { Href = $"{baseUrl}?page={page}", Method = "GET" },
            ["next"] = page < response.Metadata.Pagination.TotalPages
                ? new Link { Href = $"{baseUrl}?page={page + 1}", Method = "GET" }
                : null
        };

        return response;
    }
}
```

---

#### 3. Enhanced Exception Middleware

```csharp
// bdDevCRM.Api/Middleware/EnhancedExceptionMiddleware.cs
public class EnhancedExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _env;
    private readonly ILogger<EnhancedExceptionMiddleware> _logger;
    private readonly IMetricsCollector _metrics;

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            await HandleExceptionAsync(context, ex, stopwatch.ElapsedMilliseconds);
        }
    }

    private async Task HandleExceptionAsync(
        HttpContext context,
        Exception ex,
        long duration)
    {
        var correlationId = context.TraceIdentifier;

        // Log with rich context
        LogException(ex, context, correlationId, duration);

        // Record metrics
        RecordExceptionMetrics(ex, context, duration);

        // Map to response
        var response = MapExceptionToResponse(ex, correlationId);

        // Set status code
        context.Response.StatusCode = response.StatusCode;
        context.Response.ContentType = "application/json";

        // Add custom headers
        context.Response.Headers.Add("X-Correlation-Id", correlationId);
        context.Response.Headers.Add("X-Error-Code", response.Error?.ErrorCode ?? "UNKNOWN");

        // Serialize and send
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = _env.IsDevelopment(),
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        await context.Response.WriteAsJsonAsync(response, options);
    }

    private void LogException(
        Exception ex,
        HttpContext context,
        string correlationId,
        long duration)
    {
        var logLevel = GetLogLevel(ex);

        using (_logger.BeginScope(new Dictionary<string, object>
        {
            ["CorrelationId"] = correlationId,
            ["UserId"] = GetUserId(context),
            ["HttpMethod"] = context.Request.Method,
            ["Path"] = context.Request.Path,
            ["QueryString"] = context.Request.QueryString.ToString(),
            ["UserAgent"] = context.Request.Headers["User-Agent"].ToString(),
            ["ClientIp"] = context.Connection.RemoteIpAddress?.ToString(),
            ["Duration"] = duration,
            ["ExceptionType"] = ex.GetType().Name
        }))
        {
            _logger.Log(logLevel, ex,
                "Exception occurred: {ExceptionType} - {Message}",
                ex.GetType().Name,
                ex.Message);
        }
    }

    private void RecordExceptionMetrics(Exception ex, HttpContext context, long duration)
    {
        _metrics.Increment($"exceptions.total");
        _metrics.Increment($"exceptions.type.{ex.GetType().Name}");
        _metrics.Increment($"exceptions.status.{GetStatusCode(ex)}");
        _metrics.Increment($"exceptions.endpoint.{context.Request.Path}");
        _metrics.RecordTiming("exceptions.duration", duration);
    }

    private ApiResponseV2<object> MapExceptionToResponse(Exception ex, string correlationId)
    {
        // Custom exceptions
        if (ex is BaseCustomException customEx)
        {
            var response = ApiResponseBuilder.Error(
                customEx.StatusCode,
                customEx.UserFriendlyMessage ?? customEx.Message,
                customEx.ErrorCode,
                ex.GetType().Name,
                correlationId
            );

            if (customEx.AdditionalData.Any())
            {
                response.Error.AdditionalData = customEx.AdditionalData;
            }

            if (_env.IsDevelopment())
            {
                response.Error.StackTrace = ex.StackTrace;
            }

            return response;
        }

        // Framework exceptions
        return ex switch
        {
            ValidationException validationEx => HandleValidationException(validationEx, correlationId),
            DbUpdateException dbEx => HandleDatabaseException(dbEx, correlationId),
            SecurityTokenException => HandleTokenException(ex, correlationId),
            _ => HandleGenericException(ex, correlationId)
        };
    }

    private ApiResponseV2<object> HandleValidationException(
        ValidationException ex,
        string correlationId)
    {
        var response = ApiResponseBuilder.Error(
            400,
            "Validation failed",
            "VALIDATION_ERROR",
            "ValidationException",
            correlationId
        );

        response.Error.ValidationErrors = new List<ValidationError>
        {
            new() { Field = "General", Message = ex.Message }
        };

        return response;
    }

    private ApiResponseV2<object> HandleDatabaseException(
        DbUpdateException ex,
        string correlationId)
    {
        var message = _env.IsDevelopment()
            ? ex.InnerException?.Message ?? ex.Message
            : "A database error occurred. Please try again.";

        return ApiResponseBuilder.Error(
            500,
            message,
            "DATABASE_ERROR",
            "DbUpdateException",
            correlationId
        );
    }

    private ApiResponseV2<object> HandleTokenException(
        Exception ex,
        string correlationId)
    {
        return ApiResponseBuilder.Error(
            401,
            "Your session has expired. Please login again.",
            "TOKEN_EXPIRED",
            ex.GetType().Name,
            correlationId
        );
    }

    private ApiResponseV2<object> HandleGenericException(
        Exception ex,
        string correlationId)
    {
        var message = _env.IsDevelopment()
            ? ex.Message
            : "An unexpected error occurred. Please try again later.";

        return ApiResponseBuilder.Error(
            500,
            message,
            "INTERNAL_ERROR",
            ex.GetType().Name,
            correlationId
        );
    }

    private LogLevel GetLogLevel(Exception ex) => ex switch
    {
        NotFoundException => LogLevel.Warning,
        BadRequestException => LogLevel.Warning,
        UnauthorizedException => LogLevel.Warning,
        ValidationException => LogLevel.Information,
        _ => LogLevel.Error
    };

    private int GetStatusCode(Exception ex) => ex switch
    {
        BaseCustomException customEx => customEx.StatusCode,
        ValidationException => 400,
        UnauthorizedAccessException => 401,
        KeyNotFoundException => 404,
        DbUpdateException => 500,
        _ => 500
    };

    private string GetUserId(HttpContext context)
    {
        return context.User?.FindFirst("userId")?.Value ?? "Anonymous";
    }
}
```

---

### 🎯 **Phase 2: Resilience (Week 3-4)**

#### 4. Implement Retry Policies with Polly

```csharp
// bdDevCRM.Service/Resilience/ResiliencePolicies.cs
public static class ResiliencePolicies
{
    // Database retry policy
    public static IAsyncPolicy<T> GetDatabaseRetryPolicy<T>(ILogger logger)
    {
        return Policy<T>
            .Handle<SqlException>(ex => IsTransientError(ex))
            .Or<TimeoutException>()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: attempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, attempt)), // Exponential backoff
                onRetry: (outcome, timespan, retryCount, context) =>
                {
                    logger.LogWarning(
                        "Database operation failed. Retry {RetryCount} after {Delay}s. Error: {Error}",
                        retryCount,
                        timespan.TotalSeconds,
                        outcome.Exception?.Message ?? outcome.Result?.ToString()
                    );
                }
            );
    }

    // HTTP retry policy
    public static IAsyncPolicy<HttpResponseMessage> GetHttpRetryPolicy(ILogger logger)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError() // 408, 5xx
            .Or<TimeoutException>()
            .OrResult(response => response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: (retryCount, response, context) =>
                {
                    // Check Retry-After header
                    if (response.Result?.Headers.RetryAfter?.Delta.HasValue == true)
                    {
                        return response.Result.Headers.RetryAfter.Delta.Value;
                    }
                    return TimeSpan.FromSeconds(Math.Pow(2, retryCount));
                },
                onRetryAsync: async (outcome, timespan, retryCount, context) =>
                {
                    logger.LogWarning(
                        "HTTP request failed. Retry {RetryCount} after {Delay}s. Status: {Status}",
                        retryCount,
                        timespan.TotalSeconds,
                        outcome.Result?.StatusCode
                    );
                    await Task.CompletedTask;
                }
            );
    }

    // Circuit breaker policy
    public static IAsyncPolicy GetCircuitBreakerPolicy(ILogger logger)
    {
        return Policy
            .Handle<Exception>()
            .CircuitBreakerAsync(
                exceptionsAllowedBeforeBreaking: 5,
                durationOfBreak: TimeSpan.FromSeconds(30),
                onBreak: (exception, duration) =>
                {
                    logger.LogError(
                        exception,
                        "Circuit breaker opened for {Duration}s due to {ExceptionType}: {Message}",
                        duration.TotalSeconds,
                        exception.GetType().Name,
                        exception.Message
                    );
                },
                onReset: () =>
                {
                    logger.LogInformation("Circuit breaker reset - service recovered");
                },
                onHalfOpen: () =>
                {
                    logger.LogInformation("Circuit breaker half-open - testing service");
                }
            );
    }

    // Timeout policy
    public static IAsyncPolicy GetTimeoutPolicy(TimeSpan timeout)
    {
        return Policy.TimeoutAsync(
            timeout,
            TimeoutStrategy.Pessimistic
        );
    }

    // Combined policy (wrap multiple policies)
    public static IAsyncPolicy GetCombinedPolicy(ILogger logger)
    {
        var timeout = GetTimeoutPolicy(TimeSpan.FromSeconds(10));
        var retry = GetDatabaseRetryPolicy<object>(logger);
        var circuitBreaker = GetCircuitBreakerPolicy(logger);

        return Policy.WrapAsync(timeout, retry, circuitBreaker);
    }

    private static bool IsTransientError(SqlException ex)
    {
        // SQL Server transient error codes
        int[] transientErrorCodes =
        {
            -2,    // Timeout
            -1,    // Connection broken
            1205,  // Deadlock
            4060,  // Cannot open database
            40197, // Service error
            40501, // Service busy
            40613, // Database unavailable
            49918, // Cannot process request
            49919, // Too many operations
            49920  // Too many operations
        };

        return transientErrorCodes.Contains(ex.Number);
    }
}

// Usage in repository
public class UsersRepository : RepositoryBase<Users>, IUsersRepository
{
    private readonly IAsyncPolicy _retryPolicy;
    private readonly ILogger<UsersRepository> _logger;

    public UsersRepository(
        RepositoryContext context,
        ILogger<UsersRepository> logger) : base(context)
    {
        _logger = logger;
        _retryPolicy = ResiliencePolicies.GetDatabaseRetryPolicy<Users>(logger);
    }

    public async Task<Users> GetByIdAsync(int userId)
    {
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            return await FindByCondition(u => u.UserId == userId)
                .FirstOrDefaultAsync();
        });
    }
}
```

---

#### 5. Implement FluentValidation

```csharp
// Install NuGet: FluentValidation.AspNetCore

// Validator
public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.LoginId)
            .NotEmpty().WithMessage("Login ID is required")
            .WithErrorCode("LOGINID_REQUIRED")
            .Length(3, 50).WithMessage("Login ID must be between 3 and 50 characters")
            .WithErrorCode("LOGINID_LENGTH")
            .Matches("^[a-zA-Z0-9_]*$").WithMessage("Login ID can only contain letters, numbers and underscores")
            .WithErrorCode("LOGINID_INVALID_FORMAT");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .WithErrorCode("EMAIL_REQUIRED")
            .EmailAddress().WithMessage("Email format is invalid")
            .WithErrorCode("EMAIL_INVALID_FORMAT");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .WithErrorCode("PASSWORD_REQUIRED")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .WithErrorCode("PASSWORD_TOO_SHORT")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .WithErrorCode("PASSWORD_NO_UPPERCASE")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .WithErrorCode("PASSWORD_NO_LOWERCASE")
            .Matches(@"\d").WithMessage("Password must contain at least one digit")
            .WithErrorCode("PASSWORD_NO_DIGIT")
            .Matches(@"[!@#$%^&*(),.?""{}|<>]").WithMessage("Password must contain at least one special character")
            .WithErrorCode("PASSWORD_NO_SPECIAL");

        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("Company must be selected")
            .WithErrorCode("COMPANY_REQUIRED");

        RuleFor(x => x.EmployeeId)
            .GreaterThan(0).WithMessage("Employee must be selected")
            .WithErrorCode("EMPLOYEE_REQUIRED")
            .When(x => x.CompanyId > 0); // Conditional validation
    }
}

// Register in Program.cs
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>();

// Custom validation error handler
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value.Errors.Count > 0)
            .SelectMany(e => e.Value.Errors.Select(er => new ValidationError
            {
                Field = e.Key,
                Message = er.ErrorMessage,
                ErrorCode = er.Exception?.Data["ErrorCode"]?.ToString() ?? "VALIDATION_ERROR"
            }))
            .ToList();

        var response = ApiResponseBuilder.Error(
            400,
            "Validation failed",
            "VALIDATION_ERROR",
            "ValidationException",
            context.HttpContext.TraceIdentifier
        );

        response.Error.ValidationErrors = errors;

        return new BadRequestObjectResult(response);
    };
});

// Controller usage - automatic validation
[HttpPost]
public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
{
    // No need for manual validation - FluentValidation does it automatically
    // If validation fails, returns 400 with structured errors

    var user = await _service.CreateUserAsync(request);
    return Ok(ApiResponseBuilder.Success(user, "User created successfully", 201));
}
```

---

### 🎯 **Phase 3: Monitoring (Week 5-6)**

#### 6. Exception Metrics and Alerting

```csharp
// bdDevCRM.Shared/Metrics/IMetricsCollector.cs
public interface IMetricsCollector
{
    void Increment(string metricName, int value = 1, Dictionary<string, string> tags = null);
    void RecordTiming(string metricName, long milliseconds, Dictionary<string, string> tags = null);
    void Gauge(string metricName, double value, Dictionary<string, string> tags = null);
}

// Implementation with Application Insights
public class ApplicationInsightsMetricsCollector : IMetricsCollector
{
    private readonly TelemetryClient _telemetry;

    public ApplicationInsightsMetricsCollector(TelemetryClient telemetry)
    {
        _telemetry = telemetry;
    }

    public void Increment(string metricName, int value = 1, Dictionary<string, string> tags = null)
    {
        _telemetry.GetMetric(metricName).TrackValue(value);

        if (tags != null)
        {
            _telemetry.TrackEvent(metricName, tags);
        }
    }

    public void RecordTiming(string metricName, long milliseconds, Dictionary<string, string> tags = null)
    {
        _telemetry.TrackDependency(
            metricName,
            metricName,
            DateTimeOffset.UtcNow.AddMilliseconds(-milliseconds),
            TimeSpan.FromMilliseconds(milliseconds),
            true
        );
    }

    public void Gauge(string metricName, double value, Dictionary<string, string> tags = null)
    {
        _telemetry.GetMetric(metricName).TrackValue(value);
    }
}

// Exception tracking service
public class ExceptionTrackingService
{
    private readonly IMetricsCollector _metrics;
    private readonly TelemetryClient _telemetry;

    public void TrackException(Exception ex, HttpContext context)
    {
        // Count metrics
        _metrics.Increment("exceptions.total");
        _metrics.Increment($"exceptions.type.{ex.GetType().Name}");
        _metrics.Increment($"exceptions.endpoint.{context.Request.Path}");

        // Track in Application Insights
        var properties = new Dictionary<string, string>
        {
            ["UserId"] = GetUserId(context),
            ["HttpMethod"] = context.Request.Method,
            ["Path"] = context.Request.Path,
            ["QueryString"] = context.Request.QueryString.ToString(),
            ["UserAgent"] = context.Request.Headers["User-Agent"],
            ["ClientIp"] = context.Connection.RemoteIpAddress?.ToString(),
            ["CorrelationId"] = context.TraceIdentifier
        };

        if (ex is BaseCustomException customEx)
        {
            foreach (var kvp in customEx.AdditionalData)
            {
                properties[$"Custom_{kvp.Key}"] = kvp.Value?.ToString();
            }
        }

        _telemetry.TrackException(ex, properties);
    }
}
```

**Application Insights Queries**:

```kusto
// Top 10 exceptions in last 24 hours
exceptions
| where timestamp > ago(24h)
| summarize count() by type
| top 10 by count_
| render barchart

// Error rate over time
requests
| where timestamp > ago(24h)
| summarize
    Total = count(),
    Errors = countif(success == false)
| extend ErrorRate = Errors * 100.0 / Total
| render timechart

// Exceptions by endpoint
exceptions
| where timestamp > ago(24h)
| extend endpoint = tostring(customDimensions.Path)
| summarize count() by endpoint
| order by count_ desc
| take 20

// Users with most errors
exceptions
| where timestamp > ago(24h)
| extend userId = tostring(customDimensions.UserId)
| summarize count() by userId
| order by count_ desc
| take 10

// Slow requests with errors
requests
| where timestamp > ago(24h)
| where success == false
| where duration > 1000
| project
    timestamp,
    url,
    duration,
    resultCode,
    operation_Id
| join kind=inner (
    exceptions
    | project operation_Id, type, outerMessage
) on operation_Id
| order by duration desc
```

---

#### 7. Structured Logging with Serilog

```csharp
// Replace NLog with Serilog

// Program.cs
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithEnvironmentName()
    .Enrich.WithProperty("Application", "bdDevCRM")
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
    .WriteTo.File(
        path: "logs/app-.log",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}",
        retainedFileCountLimit: 30)
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
    {
        IndexFormat = "bddevcrm-logs-{0:yyyy.MM}",
        AutoRegisterTemplate = true,
        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
        NumberOfShards = 2,
        NumberOfReplicas = 1
    })
    .WriteTo.ApplicationInsights(
        builder.Configuration["ApplicationInsights:InstrumentationKey"],
        TelemetryConverter.Traces)
    .CreateLogger();

builder.Host.UseSerilog();

// Usage in middleware
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        using (_logger.BeginScope(new Dictionary<string, object>
        {
            ["CorrelationId"] = context.TraceIdentifier,
            ["UserId"] = GetUserId(context),
            ["ClientIp"] = context.Connection.RemoteIpAddress?.ToString()
        }))
        {
            try
            {
                await _next(context);

                stopwatch.Stop();

                _logger.LogInformation(
                    "HTTP {Method} {Path} responded {StatusCode} in {Duration}ms",
                    context.Request.Method,
                    context.Request.Path,
                    context.Response.StatusCode,
                    stopwatch.ElapsedMilliseconds
                );
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                _logger.LogError(
                    ex,
                    "HTTP {Method} {Path} failed with {ExceptionType} in {Duration}ms",
                    context.Request.Method,
                    context.Request.Path,
                    ex.GetType().Name,
                    stopwatch.ElapsedMilliseconds
                );

                throw;
            }
        }
    }
}
```

**Elasticsearch queries**:

```json
// Find all errors from specific user
GET /bddevcrm-logs-*/_search
{
  "query": {
    "bool": {
      "must": [
        { "match": { "level": "Error" }},
        { "match": { "fields.UserId": "123" }}
      ]
    }
  }
}

// Error rate by endpoint
GET /bddevcrm-logs-*/_search
{
  "size": 0,
  "aggs": {
    "endpoints": {
      "terms": {
        "field": "fields.Path.keyword",
        "size": 20
      },
      "aggs": {
        "errors": {
          "filter": {
            "term": { "level": "Error" }
          }
        }
      }
    }
  }
}
```

---

## বাস্তবায়ন পরিকল্পনা

### 📅 6-Week Roadmap

#### **Week 1: Foundation**
```
✅ Day 1-2: Code cleanup
  - Delete duplicate middleware files
  - Remove commented code
  - Organize exception classes

✅ Day 3-4: Exception hierarchy
  - Implement BaseCustomException
  - Update all existing exceptions
  - Add ErrorCode property

✅ Day 5: Unified response format
  - Create ApiResponseV2 class
  - Update ResponseHelper
  - Test with sample endpoints
```

#### **Week 2: Validation & Error Handling**
```
✅ Day 1-2: FluentValidation setup
  - Install packages
  - Create validators for 10 DTOs
  - Configure automatic validation

✅ Day 3-4: Enhanced exception middleware
  - Rewrite middleware with rich logging
  - Add metrics collection
  - Test error scenarios

✅ Day 5: Validation error response
  - Structured validation errors
  - Field-wise error messages
  - Error code support
```

#### **Week 3: Resilience**
```
✅ Day 1-2: Polly setup
  - Install Polly
  - Create retry policies
  - Implement in repositories

✅ Day 3-4: Circuit breaker
  - Configure circuit breaker
  - Test fail scenarios
  - Add monitoring

✅ Day 5: Timeout policies
  - Add timeout for all external calls
  - Configure per-endpoint timeouts
```

#### **Week 4: Advanced Features**
```
✅ Day 1-2: HATEOAS implementation
  - Add _links to responses
  - Implement for key endpoints
  - Document patterns

✅ Day 3-4: Response caching
  - Add ETag support
  - Configure cache headers
  - Test caching behavior

✅ Day 5: Batch operations
  - Implement batch response format
  - Add 207 Multi-Status support
```

#### **Week 5: Monitoring**
```
✅ Day 1-2: Serilog migration
  - Replace NLog with Serilog
  - Configure Elasticsearch sink
  - Test structured logging

✅ Day 3-4: Metrics collection
  - Implement IMetricsCollector
  - Add exception metrics
  - Create Application Insights queries

✅ Day 5: Alerting rules
  - Configure alerts in Azure
  - Test alert triggers
```

#### **Week 6: Testing & Documentation**
```
✅ Day 1-2: Unit tests
  - Test exception handling
  - Test validation
  - Test response formats

✅ Day 3-4: Integration tests
  - Test error scenarios end-to-end
  - Test retry/circuit breaker
  - Load testing

✅ Day 5: Documentation
  - Update API documentation
  - Error code reference
  - Best practices guide
```

---

## Code Examples

### Complete Usage Example

```csharp
// Service layer
public class UserService : IUserService
{
    private readonly IRepositoryManager _repository;
    private readonly ILogger<UserService> _logger;
    private readonly IAsyncPolicy _retryPolicy;

    public async Task<UsersDTO> GetUserByIdAsync(int userId)
    {
        try
        {
            // Retry policy for transient errors
            var user = await _retryPolicy.ExecuteAsync(async () =>
            {
                return await _repository.Users.GetByIdAsync(userId);
            });

            if (user == null)
            {
                throw new GenericNotFoundException("User", "UserId", userId.ToString())
                    .WithData("RequestedBy", _currentUser.Id)
                    .WithData("Timestamp", DateTime.UtcNow);
            }

            return _mapper.Map<UsersDTO>(user);
        }
        catch (GenericNotFoundException)
        {
            // Re-throw domain exceptions
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching user {UserId}", userId);
            throw new InfrastructureException("Error fetching user data", ex);
        }
    }

    public async Task<UsersDTO> CreateUserAsync(CreateUserRequest request)
    {
        // Validation is automatic via FluentValidation

        // Business logic validation
        var exists = await _repository.Users.ExistsByLoginIdAsync(request.LoginId);
        if (exists)
        {
            throw new DuplicateRecordException("User", "LoginId")
                .WithData("LoginId", request.LoginId);
        }

        var user = _mapper.Map<Users>(request);
        user.CreatedDate = DateTime.UtcNow;
        user.StatusId = (int)RecordStatus.Active;

        await _repository.Users.CreateAsync(user);
        await _repository.SaveAsync();

        return _mapper.Map<UsersDTO>(user);
    }
}

// Controller
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("2.0")]
public class UsersController : BaseApiController
{
    private readonly IUserService _service;

    [HttpGet("{id}")]
    [ResponseCache(Duration = 300)] // 5 minutes
    [ProducesResponseType(typeof(ApiResponseV2<UsersDTO>), 200)]
    [ProducesResponseType(typeof(ApiResponseV2<object>), 404)]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _service.GetUserByIdAsync(id);

        var response = ApiResponseBuilder.Success(user, "User retrieved successfully");

        // Add HATEOAS links
        response.Links = new Dictionary<string, Link>
        {
            ["self"] = new Link { Href = $"/api/users/{id}", Method = "GET" },
            ["update"] = new Link { Href = $"/api/users/{id}", Method = "PUT" },
            ["delete"] = new Link { Href = $"/api/users/{id}", Method = "DELETE" },
            ["applications"] = new Link { Href = $"/api/users/{id}/applications", Method = "GET" }
        };

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseV2<UsersDTO>), 201)]
    [ProducesResponseType(typeof(ApiResponseV2<object>), 400)]
    [ProducesResponseType(typeof(ApiResponseV2<object>), 409)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
    {
        // FluentValidation runs automatically

        var user = await _service.CreateUserAsync(request);

        var response = ApiResponseBuilder.Success(user, "User created successfully", 201);

        // Add Location header
        Response.Headers.Add("Location", $"/api/users/{user.UserId}");

        return Created($"/api/users/{user.UserId}", response);
    }
}
```

---

## সারসংক্ষেপ

### 🔴 **Critical Issues (এখনই ঠিক করুন)**

1. ❌ Duplicate middleware files delete করুন
2. ❌ Exception hierarchy implement করুন (ErrorCode সহ)
3. ❌ Unified response format তৈরি করুন
4. ❌ FluentValidation যোগ করুন
5. ❌ Structured logging implement করুন

### 🟡 **Important (পরবর্তী 2-3 সপ্তাহে)**

6. ⚠️ Retry policies implement করুন
7. ⚠️ Circuit breaker যোগ করুন
8. ⚠️ Exception metrics tracking যোগ করুন
9. ⚠️ HATEOAS support যোগ করুন
10. ⚠️ Response caching implement করুন

### 🟢 **Nice to Have (পরবর্তী 1-2 মাসে)**

11. ✅ Batch operation support
12. ✅ Content negotiation (XML, CSV)
13. ✅ Advanced monitoring dashboards
14. ✅ Automated alerting
15. ✅ Load testing and optimization

---

## মূল সুবিধা

এই সব improvement করলে:

✅ **User Experience**:
- Clear error messages
- Field-wise validation errors
- Faster responses (caching)

✅ **Developer Experience**:
- Easy debugging (correlation IDs, rich logs)
- Consistent patterns
- Self-documenting APIs (HATEOAS)

✅ **Operations**:
- Better monitoring
- Automated alerts
- Faster issue resolution

✅ **Reliability**:
- Resilient to transient failures
- Graceful degradation
- Circuit breaker protection

✅ **Performance**:
- Response caching
- Compression
- Optimized database calls

✅ **Security**:
- No sensitive data exposure
- Rate limiting
- Proper error codes

---

যদি কোনো specific section নিয়ে আরো বিস্তারিত জানতে চান বা কোনো প্রশ্ন থাকে, জিজ্ঞাসা করুন!
