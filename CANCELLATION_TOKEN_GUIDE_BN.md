# CancellationToken - বিস্তারিত গাইড (Bangla)

## সূচিপত্র
1. [CancellationToken কি?](#cancellationtoken-কি)
2. [কেন CancellationToken প্রয়োজন?](#কেন-cancellationtoken-প্রয়োজন)
3. [CancellationToken এর সুবিধা (Pros)](#cancellationtoken-এর-সুবিধা-pros)
4. [CancellationToken এর অসুবিধা (Cons)](#cancellationtoken-এর-অসুবিধা-cons)
5. [কিভাবে CancellationToken শুরু করতে হয়](#কিভাবে-cancellationtoken-শুরু-করতে-হয়)
6. [কোথায় CancellationToken implement করতে হয়](#কোথায়-cancellationtoken-implement-করতে-হয়)
7. [বর্তমান প্রোজেক্টে CancellationToken এর অবস্থা](#বর্তমান-প্রোজেক্টে-cancellationtoken-এর-অবস্থা)
8. [Implementation উদাহরণ](#implementation-উদাহরণ)
9. [Best Practices](#best-practices)
10. [Common Pitfalls এবং এড়ানোর উপায়](#common-pitfalls-এবং-এড়ানোর-উপায়)

---

## CancellationToken কি?

**CancellationToken** হলো .NET Framework এর একটি struct যা async operations কে gracefully cancel করার জন্য ব্যবহৃত হয়। এটি একটি signal mechanism যা running operation কে জানায় যে operation টি cancel করা উচিত।

### মূল Components:
```csharp
// 1. CancellationTokenSource - Token তৈরি করে এবং cancellation signal পাঠায়
CancellationTokenSource cts = new CancellationTokenSource();

// 2. CancellationToken - Operation এ pass করা হয় cancellation check করার জন্য
CancellationToken token = cts.Token;

// 3. Cancel করার জন্য
cts.Cancel();
```

---

## কেন CancellationToken প্রয়োজন?

### 1. **User Experience উন্নতি**
যখন user একটি request cancel করে (যেমন: browser এ back button click করে বা request timeout হয়), তখন server এ running operation টি বন্ধ করা প্রয়োজন।

**উদাহরণ:**
```
User একটি large report generate করছে (যা 5 মিনিট সময় নেয়)
↓
2 মিনিট পরে user page close করে দিলো
↓
❌ Without CancellationToken: Server 5 মিনিট পর্যন্ত operation চালাতে থাকবে (resource waste)
✅ With CancellationToken: Server অবিলম্বে operation বন্ধ করবে (resource save)
```

### 2. **Resource Optimization**
Database connections, memory, CPU cycles সব কিছু properly release করা যায়।

### 3. **Graceful Shutdown**
Application shutdown এর সময় pending operations কে gracefully বন্ধ করা যায়।

### 4. **Timeout Management**
Long-running operations কে timeout এর মাধ্যমে control করা যায়।

---

## CancellationToken এর সুবিধা (Pros)

### ✅ 1. **Resource সাশ্রয়**
```csharp
// Without CancellationToken
public async Task<List<Order>> GetOrdersAsync()
{
    // User cancel করলেও query চলতেই থাকবে
    return await _repository.ListAsync(); // 10 seconds
}

// With CancellationToken
public async Task<List<Order>> GetOrdersAsync(CancellationToken cancellationToken)
{
    // User cancel করলে query stop হয়ে যাবে
    return await _repository.ListAsync(cancellationToken); // Stopped immediately
}
```

**Result:**
- ❌ Without: 10 seconds database connection occupied
- ✅ With: 0.5 seconds (immediate cancellation)

### ✅ 2. **Better User Experience**
```csharp
[HttpGet]
public async Task<IActionResult> SearchProducts(string query, CancellationToken cancellationToken)
{
    try
    {
        // User browser close করলে automatically cancel হবে
        var results = await _service.SearchAsync(query, cancellationToken);
        return Ok(results);
    }
    catch (OperationCanceledException)
    {
        // User already left, no need to send response
        return NoContent();
    }
}
```

**বেনিফিট:**
- User wait করতে হয় না unnecessary operations এর জন্য
- Browser back/forward buttons responsive হয়

### ✅ 3. **Database Connection Pool Management**
```csharp
// যখন 100 users একসাথে request করে কিন্তু 50 জন cancel করে দেয়

// Without CancellationToken:
// - 100টি database connections লাগবে
// - Pool exhausted হতে পারে
// - Other users slow response পাবে

// With CancellationToken:
// - শুধু 50টি active connections থাকবে
// - Pool available থাকবে
// - All users fast response পাবে
```

### ✅ 4. **Graceful Application Shutdown**
```csharp
protected override async Task ExecuteAsync(CancellationToken stoppingToken)
{
    while (!stoppingToken.IsCancellationRequested)
    {
        try
        {
            await ProcessDataAsync(stoppingToken);
            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Background service stopping gracefully...");
            break;
        }
    }
}
```

### ✅ 5. **Timeout Control**
```csharp
public async Task<Data> GetDataWithTimeoutAsync()
{
    using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

    try
    {
        return await _repository.GetDataAsync(cts.Token);
    }
    catch (OperationCanceledException)
    {
        throw new TimeoutException("Operation took longer than 30 seconds");
    }
}
```

### ✅ 6. **Cascading Cancellation**
```csharp
public async Task<ComplexResult> ComplexOperationAsync(CancellationToken cancellationToken)
{
    // Step 1
    var data1 = await _service1.GetDataAsync(cancellationToken);

    // Step 2
    var data2 = await _service2.ProcessAsync(data1, cancellationToken);

    // Step 3
    var data3 = await _service3.FinalizeAsync(data2, cancellationToken);

    // যদি কোনো step এ cancel হয়, পরবর্তী steps execute হবে না
    return new ComplexResult { Data = data3 };
}
```

---

## CancellationToken এর অসুবিধা (Cons)

### ❌ 1. **Code Complexity বৃদ্ধি**
```csharp
// Simple Code (Without CancellationToken)
public async Task ProcessAsync()
{
    await DoWork1();
    await DoWork2();
    await DoWork3();
}

// Complex Code (With CancellationToken)
public async Task ProcessAsync(CancellationToken cancellationToken = default)
{
    await DoWork1(cancellationToken);
    cancellationToken.ThrowIfCancellationRequested();

    await DoWork2(cancellationToken);
    cancellationToken.ThrowIfCancellationRequested();

    await DoWork3(cancellationToken);
}
```

### ❌ 2. **Method Signature পরিবর্তন**
```csharp
// পুরো codebase এ method signatures update করতে হবে

// Before
Task<Data> GetDataAsync();
Task ProcessAsync();
Task SaveAsync();

// After
Task<Data> GetDataAsync(CancellationToken cancellationToken = default);
Task ProcessAsync(CancellationToken cancellationToken = default);
Task SaveAsync(CancellationToken cancellationToken = default);
```

### ❌ 3. **Testing Complexity**
```csharp
// Test without CancellationToken (Simple)
[Fact]
public async Task GetData_ReturnsData()
{
    var result = await _service.GetDataAsync();
    Assert.NotNull(result);
}

// Test with CancellationToken (Complex)
[Fact]
public async Task GetData_WithCancellation_ThrowsOperationCanceledException()
{
    // Arrange
    var cts = new CancellationTokenSource();
    cts.Cancel();

    // Act & Assert
    await Assert.ThrowsAsync<OperationCanceledException>(
        () => _service.GetDataAsync(cts.Token)
    );
}

[Fact]
public async Task GetData_WithTimeout_ThrowsOperationCanceledException()
{
    // Arrange
    var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(100));

    // Act & Assert
    await Assert.ThrowsAsync<OperationCanceledException>(
        () => _service.GetLongRunningDataAsync(cts.Token)
    );
}
```

### ❌ 4. **Exception Handling বৃদ্ধি**
```csharp
public async Task<IActionResult> GetDataAsync(CancellationToken cancellationToken)
{
    try
    {
        var data = await _service.GetDataAsync(cancellationToken);
        return Ok(data);
    }
    catch (OperationCanceledException)
    {
        // Client cancelled the request
        return NoContent();
    }
    catch (DbException ex)
    {
        // Database error
        return StatusCode(500, ex.Message);
    }
    catch (Exception ex)
    {
        // Other errors
        return StatusCode(500, ex.Message);
    }
}
```

### ❌ 5. **Breaking Changes**
```csharp
// Interface এ CancellationToken add করলে সব implementation update করতে হবে

// Old Interface
public interface IDataService
{
    Task<Data> GetDataAsync();
}

// New Interface (Breaking Change!)
public interface IDataService
{
    Task<Data> GetDataAsync(CancellationToken cancellationToken = default);
}

// এখন সব implementations update করতে হবে:
// - Implementation classes
// - Mock classes (for testing)
// - All calling code
```

### ❌ 6. **Performance Overhead (Minimal)**
```csharp
// প্রতিটি async operation এ cancellation check করার জন্য minimal overhead আছে

// Overhead প্রতি call: ~10-50 nanoseconds
// Usually negligible, but হাজার হাজার calls এ measurable হতে পারে
```

---

## কিভাবে CancellationToken শুরু করতে হয়

### পদ্ধতি 1: ASP.NET Core Controller থেকে (সবচেয়ে সহজ)

```csharp
[HttpGet]
public async Task<IActionResult> GetData(CancellationToken cancellationToken)
{
    // ASP.NET Core automatically provides CancellationToken
    // যখন client request cancel করে (browser close, timeout, etc.)

    var data = await _service.GetDataAsync(cancellationToken);
    return Ok(data);
}
```

**ASP.NET Core Automatic Features:**
- Browser connection lost → Automatic cancellation
- Request timeout → Automatic cancellation
- Application shutdown → Automatic cancellation

### পদ্ধতি 2: Manual CancellationTokenSource তৈরি

```csharp
public async Task ManualCancellationExample()
{
    // Create CancellationTokenSource
    using var cts = new CancellationTokenSource();

    // Get token
    var token = cts.Token;

    // Start async operation
    var task = LongRunningOperationAsync(token);

    // Cancel after some condition
    await Task.Delay(5000); // Wait 5 seconds
    cts.Cancel(); // Cancel the operation

    try
    {
        await task;
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("Operation was cancelled");
    }
}
```

### পদ্ধতি 3: Timeout সহ CancellationToken

```csharp
public async Task<Data> GetDataWithTimeoutAsync()
{
    // 30 seconds timeout
    using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));

    try
    {
        return await _repository.GetDataAsync(cts.Token);
    }
    catch (OperationCanceledException)
    {
        throw new TimeoutException("Operation exceeded 30 seconds timeout");
    }
}
```

### পদ্ধতি 4: Multiple CancellationTokens একসাথে (Linked Token)

```csharp
public async Task<Data> ComplexOperationAsync(CancellationToken userToken)
{
    // User cancellation + 1 minute timeout + shutdown token
    using var timeoutCts = new CancellationTokenSource(TimeSpan.FromMinutes(1));
    using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
        userToken,              // User cancelled
        timeoutCts.Token,       // Timeout
        _hostLifetime.ApplicationStopping  // App shutdown
    );

    return await _repository.GetDataAsync(linkedCts.Token);
}
```

### পদ্ধতি 5: Background Service এ

```csharp
public class MyBackgroundService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // stoppingToken automatically triggers when:
        // - Application is shutting down
        // - Service is being stopped

        while (!stoppingToken.IsCancellationRequested)
        {
            await ProcessDataAsync(stoppingToken);

            // Delay with cancellation support
            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}
```

---

## কোথায় CancellationToken implement করতে হয়

### 1. **Controller Layer (সবচেয়ে গুরুত্বপূর্ণ)**

```csharp
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetOrders(
        [FromQuery] int page,
        [FromQuery] int pageSize,
        CancellationToken cancellationToken) // ✅ Always add here
    {
        try
        {
            var orders = await _service.GetOrdersAsync(page, pageSize, cancellationToken);
            return Ok(orders);
        }
        catch (OperationCanceledException)
        {
            return NoContent(); // Client already gone
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(
        [FromBody] OrderDto orderDto,
        CancellationToken cancellationToken) // ✅ Add here too
    {
        var order = await _service.CreateOrderAsync(orderDto, cancellationToken);
        return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
    }
}
```

### 2. **Service Layer**

```csharp
public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetOrdersAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default); // ✅ Add with default

    Task<OrderDto> CreateOrderAsync(
        OrderDto orderDto,
        CancellationToken cancellationToken = default); // ✅ Add with default
}

public class OrderService : IOrderService
{
    public async Task<IEnumerable<OrderDto>> GetOrdersAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        // Pass to repository
        var orders = await _repository.ListAsync(cancellationToken);

        // Pass to other services if needed
        foreach (var order in orders)
        {
            await _enrichmentService.EnrichAsync(order, cancellationToken);
        }

        return orders;
    }
}
```

### 3. **Repository Layer**

```csharp
public interface IRepositoryBase<T> where T : class
{
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> ListAsync(CancellationToken cancellationToken = default);
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
}

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    public async Task<IEnumerable<T>> ListAsync(CancellationToken cancellationToken = default)
    {
        // Pass to EF Core
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
```

### 4. **Background Services**

```csharp
public class DataProcessingService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                // Process data with cancellation support
                await ProcessBatchAsync(stoppingToken);

                // Delay with cancellation support
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Service stopping...");
                break;
            }
        }
    }

    private async Task ProcessBatchAsync(CancellationToken cancellationToken)
    {
        var items = await _repository.GetPendingItemsAsync(cancellationToken);

        foreach (var item in items)
        {
            // Check cancellation between items
            cancellationToken.ThrowIfCancellationRequested();

            await ProcessItemAsync(item, cancellationToken);
        }
    }
}
```

### 5. **HTTP Calls (HttpClient)**

```csharp
public class ExternalApiService
{
    private readonly HttpClient _httpClient;

    public async Task<ApiResponse> CallExternalApiAsync(
        string endpoint,
        CancellationToken cancellationToken = default)
    {
        // HttpClient automatically supports CancellationToken
        var response = await _httpClient.GetAsync(endpoint, cancellationToken);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<ApiResponse>(content);
    }
}
```

### 6. **File Operations**

```csharp
public class FileService
{
    public async Task<string> ReadFileAsync(
        string path,
        CancellationToken cancellationToken = default)
    {
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
        using var reader = new StreamReader(stream);

        // ReadToEndAsync supports CancellationToken
        return await reader.ReadToEndAsync(cancellationToken);
    }

    public async Task WriteFileAsync(
        string path,
        string content,
        CancellationToken cancellationToken = default)
    {
        using var stream = new FileStream(path, FileMode.Create, FileAccess.Write);
        using var writer = new StreamWriter(stream);

        await writer.WriteAsync(content, cancellationToken);
    }
}
```

---

## বর্তমান প্রোজেক্টে CancellationToken এর অবস্থা

### ✅ যেখানে ইতিমধ্যে ব্যবহার হচ্ছে:

#### 1. **Background Services (Perfect Implementation)**
- `TokenCleanupBackgroundService.cs` ✅
- `AuditLogWriterService.cs` ✅
- `AuditLogQueue.cs` ✅

#### 2. **Database Interceptors (Good Implementation)**
- `SlowQueryLoggingInterceptor.cs` ✅
- `AuditSaveChangesInterceptor.cs` ✅

#### 3. **Global Exception Handler**
- `GlobalExceptionHandler.cs` ✅

#### 4. **Repository Layer (Refactored Version)**
- `IRepositoryBase.Refactored.cs` ✅ (সব async methods এ)
- `RepositoryBase.Refactored.cs` ✅ (সব async methods এ)

### ❌ যেখানে অনুপস্থিত (Implementation প্রয়োজন):

#### 1. **Controller Layer (0% Coverage)**
Example from your code:
```csharp
// Current (Without CancellationToken)
[HttpGet(RouteConstants.CRMApplicationStatus)]
public async Task<IActionResult> StatusByMenuNUserId()
{
    var res = await _serviceManager.WfState.GetWFStateByMenuNUserPermission(...);
    return Ok(ApiResponseHelper.Success(res, "..."));
}

// Should be (With CancellationToken)
[HttpGet(RouteConstants.CRMApplicationStatus)]
public async Task<IActionResult> StatusByMenuNUserId(CancellationToken cancellationToken)
{
    var res = await _serviceManager.WfState.GetWFStateByMenuNUserPermission(..., cancellationToken);
    return Ok(ApiResponseHelper.Success(res, "..."));
}
```

#### 2. **Service Layer (0% Coverage)**
Example from `CrmApplicationService.cs`:
```csharp
// Current (Line 27)
public async Task<GridEntity<CrmApplicationGridDto>> SummaryGrid(
    CRMGridOptions options,
    int statusId,
    UsersDto usersDto,
    MenuDto menuDto)
{
    IEnumerable<GroupPermissionRepositoryDto> returnResult =
        await _repository.Groups.GetAccessPermisionForCurrentUser(...);
    // ...
}

// Should be
public async Task<GridEntity<CrmApplicationGridDto>> SummaryGrid(
    CRMGridOptions options,
    int statusId,
    UsersDto usersDto,
    MenuDto menuDto,
    CancellationToken cancellationToken = default)
{
    IEnumerable<GroupPermissionRepositoryDto> returnResult =
        await _repository.Groups.GetAccessPermisionForCurrentUser(..., cancellationToken);
    // ...
}
```

#### 3. **Repository Layer (Legacy - Minimal Coverage)**
`IRepositoryBase.cs` এ শুধু 1টি method এ CancellationToken আছে:
```csharp
// Only this method has CancellationToken
Task<int> ExecuteDeleteAsync(
    Expression<Func<T, bool>> predicate,
    CancellationToken cancellationToken = default);

// But these don't have:
Task CreateAsync(T entity); // ❌
Task UpdateAsync(T entity); // ❌
Task<IEnumerable<T>> ListAsync(...); // ❌
Task<T> GetByIdAsync(int id, ...); // ❌
```

---

## Implementation উদাহরণ

### Example 1: Controller থেকে Repository পর্যন্ত Complete Flow

#### Step 1: Controller Layer
```csharp
[ApiController]
[Route("api/crm/applications")]
public class CRMApplicationController : BaseApiController
{
    [HttpPost("summary/{statusId}")]
    public async Task<IActionResult> SummaryGrid(
        [FromBody] CRMGridOptions options,
        [FromRoute] int statusId,
        CancellationToken cancellationToken) // ✅ Add CancellationToken parameter
    {
        try
        {
            // Get current user
            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                throw new GenericUnauthorizedException("User authentication required.");

            if (!int.TryParse(userIdClaim, out int userId))
                throw new GenericBadRequestException("Invalid user ID format.");

            UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
            if (currentUser == null)
                throw new GenericUnauthorizedException("User session expired.");

            if (!MenuConstant.TryGetPath("CRMApplication", out var menuPath))
                throw new GenericBadRequestException("Invalid menu name.");

            MenuDto menuDto = await _serviceManager.Groups.CheckMenuPermission(
                $"..{menuPath}",
                currentUser,
                cancellationToken); // ✅ Pass to service

            if (menuDto == null)
                throw new GenericBadRequestException("Menu not found for the current user.");

            // Call service with CancellationToken
            var summaryGrid = await _serviceManager.CrmApplications.SummaryGrid(
                options,
                statusId,
                currentUser,
                menuDto,
                cancellationToken); // ✅ Pass to service

            if (summaryGrid == null || !summaryGrid.Items.Any())
                return Ok(ApiResponseHelper.NoContent<GridEntity<CrmApplicationGridDto>>("No data found"));

            return Ok(ApiResponseHelper.Success(summaryGrid, "Data retrieved successfully"));
        }
        catch (OperationCanceledException)
        {
            // Client cancelled the request (browser closed, timeout, etc.)
            _logger.LogInformation("Request cancelled by client for SummaryGrid");
            return NoContent(); // Don't send response, client is gone
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SummaryGrid");
            throw;
        }
    }

    [HttpGet("{applicationId}")]
    public async Task<IActionResult> GetApplication(
        [FromRoute] int applicationId,
        CancellationToken cancellationToken) // ✅ Add CancellationToken
    {
        try
        {
            if (applicationId <= 0)
                throw new GenericBadRequestException("Invalid application ID.");

            var result = await _serviceManager.CrmApplications.GetApplication(
                applicationId,
                trackChanges: false,
                cancellationToken); // ✅ Pass to service

            if (result == null)
                return Ok(ApiResponseHelper.NoContent<GetApplicationDto>("Application not found"));

            return Ok(ApiResponseHelper.Success(result, "Application retrieved successfully"));
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("Request cancelled for GetApplication {ApplicationId}", applicationId);
            return NoContent();
        }
    }
}
```

#### Step 2: Service Layer
```csharp
public interface ICrmApplicationService
{
    Task<GridEntity<CrmApplicationGridDto>> SummaryGrid(
        CRMGridOptions options,
        int statusId,
        UsersDto usersDto,
        MenuDto menuDto,
        CancellationToken cancellationToken = default); // ✅ Add with default

    Task<GetApplicationDto> GetApplication(
        int applicationId,
        bool trackChanges,
        CancellationToken cancellationToken = default); // ✅ Add with default
}

public class CrmApplicationService : ICrmApplicationService
{
    private readonly IRepositoryManager _repository;
    private readonly ILogger<CrmApplicationService> _logger;

    public async Task<GridEntity<CrmApplicationGridDto>> SummaryGrid(
        CRMGridOptions options,
        int statusId,
        UsersDto usersDto,
        MenuDto menuDto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Check cancellation before heavy work
            cancellationToken.ThrowIfCancellationRequested();

            if (menuDto.MenuId != null && menuDto.MenuId != 0)
            {
                // Pass CancellationToken to repository
                IEnumerable<GroupPermissionRepositoryDto> returnResult =
                    await _repository.Groups.GetAccessPermisionForCurrentUser(
                        menuDto.ModuleId.Value,
                        usersDto.UserId.Value,
                        cancellationToken); // ✅ Pass to repository

                // Check cancellation after database call
                cancellationToken.ThrowIfCancellationRequested();

                IEnumerable<GroupPermissionDto> result =
                    MyMapper.JsonCloneIEnumerableToIEnumerable<GroupPermissionRepositoryDto, GroupPermissionDto>(returnResult);

                var isApprover = result.Any(groupPermission => groupPermission.ReferenceID == 4);
                var isRecomander = result.Any(groupPermission => groupPermission.ReferenceID == 3);
                var isHr = result.Any(groupPermission => groupPermission.ReferenceID == 22);
                var onlyApprovalData = result.Any(groupPermission => groupPermission.ReferenceID == 23);
            }

            string sql = @"SELECT ... FROM CrmApplication ...";
            string orderBy = " ApplicationId asc ";
            string condition = string.Empty;

            // Pass CancellationToken to GridData
            var gridResult = await _repository.CrmApplications.GridData<CrmApplicationGridDto>(
                sql,
                options,
                orderBy,
                condition,
                cancellationToken); // ✅ Pass to repository

            return gridResult;
        }
        catch (DataMappingException ex)
        {
            _logger.LogError($"Grid mapping error: {ex.Message}");
            throw new BadRequestException($"Grid data mapping error. {ex.Message}");
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("SummaryGrid operation cancelled");
            throw; // Re-throw to let controller handle it
        }
    }

    public async Task<GetApplicationDto> GetApplication(
        int applicationId,
        bool trackChanges,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Check cancellation
            cancellationToken.ThrowIfCancellationRequested();

            string query = @"SELECT ... FROM CrmApplication WHERE ca.ApplicationId = @ApplicationId";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@ApplicationId", applicationId)
            };

            // Pass CancellationToken to repository
            GetApplicationDto result = await _repository.CrmApplications.ExecuteSingleData<GetApplicationDto>(
                query,
                parameters,
                cancellationToken); // ✅ Pass to repository

            if (result == null)
            {
                _logger.LogWarning("No application found with ApplicationId: {ApplicationId}", applicationId);
                return new GetApplicationDto();
            }

            // Check cancellation before additional queries
            cancellationToken.ThrowIfCancellationRequested();

            // Load education histories
            var educationHistories = await _repository.CrmEducationHistories.EducationHistoryByApplicantId(
                result.ApplicantId,
                cancellationToken); // ✅ Pass to repository

            if (educationHistories != null && educationHistories.Any())
            {
                result.EducationHistories = MyMapper.JsonCloneIEnumerableToIEnumerable<
                    EducationHistoryRepositoryDto,
                    EducationHistoryDto>(educationHistories);
            }

            // Check cancellation
            cancellationToken.ThrowIfCancellationRequested();

            // Load work experiences
            var workExperiences = await _repository.CrmWorkExperiences.WorkExperiencesByApplicantId(
                result.ApplicantId,
                cancellationToken); // ✅ Pass to repository

            if (workExperiences != null && workExperiences.Any())
            {
                result.WorkExperienceHistories = MyMapper.JsonCloneIEnumerableToIEnumerable<
                    WorkExperienceHistoryRepositoryDto,
                    WorkExperienceHistoryDto>(workExperiences);
            }

            // Check cancellation
            cancellationToken.ThrowIfCancellationRequested();

            // Load references
            var references = await _repository.CrmApplicantReferences.ListByConditionAsync(
                expression: x => x.ApplicantId == result.ApplicantId,
                orderBy: x => x.ApplicantReferenceId,
                trackChanges: false,
                cancellationToken); // ✅ Pass to repository

            if (references != null && references.Any())
            {
                result.ApplicantReferences = MyMapper.JsonCloneIEnumerableToIEnumerable<
                    CrmApplicantReference,
                    ApplicantReferenceDto>(references);
            }

            return result;
        }
        catch (OperationCanceledException)
        {
            _logger.LogInformation("GetApplication operation cancelled for ApplicationId: {ApplicationId}", applicationId);
            throw;
        }
    }
}
```

#### Step 3: Repository Layer (Already done in Refactored version)
```csharp
public interface IRepositoryBase<T> where T : class
{
    Task<IEnumerable<T>> ListByConditionAsync(
        Expression<Func<T, bool>> expression,
        Expression<Func<T, object>> orderBy,
        bool trackChanges = true,
        CancellationToken cancellationToken = default); // ✅ Already added

    Task<T> FirstOrDefaultAsync(
        Expression<Func<T, bool>> expression,
        bool trackChanges = true,
        CancellationToken cancellationToken = default); // ✅ Already added

    Task<GridEntity<TGrid>> GridData<TGrid>(
        string sql,
        GridOptions options,
        string orderBy,
        string condition,
        CancellationToken cancellationToken = default) where TGrid : class; // ✅ Already added
}

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    public async Task<IEnumerable<T>> ListByConditionAsync(
        Expression<Func<T, bool>> expression,
        Expression<Func<T, object>> orderBy,
        bool trackChanges = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = trackChanges
            ? _dbSet.Where(expression)
            : _dbSet.Where(expression).AsNoTracking();

        if (orderBy != null)
            query = query.OrderBy(orderBy);

        // EF Core's ToListAsync accepts CancellationToken
        return await query.ToListAsync(cancellationToken); // ✅ Pass to EF Core
    }

    public async Task<T> FirstOrDefaultAsync(
        Expression<Func<T, bool>> expression,
        bool trackChanges = true,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = trackChanges
            ? _dbSet.Where(expression)
            : _dbSet.Where(expression).AsNoTracking();

        // EF Core's FirstOrDefaultAsync accepts CancellationToken
        return await query.FirstOrDefaultAsync(cancellationToken); // ✅ Pass to EF Core
    }
}
```

### Example 2: Long Running Operation with Progress Reporting

```csharp
public class ReportService
{
    public async Task<ReportResult> GenerateLargeReportAsync(
        ReportOptions options,
        IProgress<int> progress,
        CancellationToken cancellationToken = default)
    {
        var totalSteps = 10;
        var report = new ReportResult();

        for (int step = 1; step <= totalSteps; step++)
        {
            // Check cancellation before each step
            cancellationToken.ThrowIfCancellationRequested();

            // Perform step work
            await ProcessReportStepAsync(step, report, cancellationToken);

            // Report progress
            progress?.Report((step * 100) / totalSteps);

            // Small delay between steps (cancellable)
            await Task.Delay(1000, cancellationToken);
        }

        return report;
    }

    private async Task ProcessReportStepAsync(
        int step,
        ReportResult report,
        CancellationToken cancellationToken)
    {
        // Heavy database query
        var data = await _repository.GetReportDataAsync(step, cancellationToken);

        // Check cancellation after query
        cancellationToken.ThrowIfCancellationRequested();

        // Process data
        report.AddData(data);
    }
}

// Controller usage
[HttpPost("generate-report")]
public async Task<IActionResult> GenerateReport(
    [FromBody] ReportOptions options,
    CancellationToken cancellationToken)
{
    var progress = new Progress<int>(percent =>
    {
        // You could store this in cache/memory to show progress to user
        _cache.Set($"report-progress-{User.Identity.Name}", percent);
    });

    try
    {
        var report = await _reportService.GenerateLargeReportAsync(
            options,
            progress,
            cancellationToken);

        return Ok(report);
    }
    catch (OperationCanceledException)
    {
        _logger.LogInformation("Report generation cancelled by user");
        return StatusCode(499, "Report generation cancelled"); // Client Closed Request
    }
}
```

### Example 3: Batch Processing with Cancellation

```csharp
public class BatchProcessingService
{
    public async Task ProcessBatchAsync(
        IEnumerable<int> itemIds,
        CancellationToken cancellationToken = default)
    {
        var items = itemIds.ToList();
        var batchSize = 50;
        var totalBatches = (int)Math.Ceiling(items.Count / (double)batchSize);

        _logger.LogInformation("Starting batch processing: {TotalItems} items in {TotalBatches} batches",
            items.Count, totalBatches);

        for (int i = 0; i < totalBatches; i++)
        {
            // Check cancellation at start of each batch
            cancellationToken.ThrowIfCancellationRequested();

            var batch = items.Skip(i * batchSize).Take(batchSize);

            _logger.LogInformation("Processing batch {CurrentBatch}/{TotalBatches}", i + 1, totalBatches);

            // Process batch with cancellation support
            await ProcessBatchItemsAsync(batch, cancellationToken);

            // Save batch with cancellation support
            await _repository.SaveAsync(cancellationToken);

            _logger.LogInformation("Batch {CurrentBatch}/{TotalBatches} completed", i + 1, totalBatches);
        }
    }

    private async Task ProcessBatchItemsAsync(
        IEnumerable<int> itemIds,
        CancellationToken cancellationToken)
    {
        foreach (var itemId in itemIds)
        {
            // Check cancellation for each item
            cancellationToken.ThrowIfCancellationRequested();

            var item = await _repository.GetByIdAsync(itemId, cancellationToken);

            if (item != null)
            {
                await ProcessItemAsync(item, cancellationToken);
            }
        }
    }
}
```

---

## Best Practices

### ✅ 1. **সবসময় `default` parameter value ব্যবহার করুন**
```csharp
// ✅ Good
public async Task ProcessAsync(CancellationToken cancellationToken = default)
{
    // ...
}

// ❌ Bad - Caller কে সবসময় pass করতে হবে
public async Task ProcessAsync(CancellationToken cancellationToken)
{
    // ...
}
```

### ✅ 2. **Controller এ সবসময় CancellationToken parameter যোগ করুন**
```csharp
// ✅ Good - ASP.NET Core automatically provides it
[HttpGet]
public async Task<IActionResult> GetData(CancellationToken cancellationToken)
{
    // ...
}

// ❌ Bad - Missing cancellation support
[HttpGet]
public async Task<IActionResult> GetData()
{
    // ...
}
```

### ✅ 3. **Long-running operations এ periodically check করুন**
```csharp
// ✅ Good
public async Task ProcessManyItemsAsync(
    IEnumerable<Item> items,
    CancellationToken cancellationToken)
{
    foreach (var item in items)
    {
        // Check before processing each item
        cancellationToken.ThrowIfCancellationRequested();

        await ProcessItemAsync(item, cancellationToken);
    }
}

// ❌ Bad - Only checks at start
public async Task ProcessManyItemsAsync(
    IEnumerable<Item> items,
    CancellationToken cancellationToken)
{
    cancellationToken.ThrowIfCancellationRequested();

    foreach (var item in items)
    {
        await ProcessItemAsync(item); // No cancellation support
    }
}
```

### ✅ 4. **OperationCanceledException সঠিকভাবে handle করুন**
```csharp
// ✅ Good
try
{
    await ProcessAsync(cancellationToken);
}
catch (OperationCanceledException)
{
    // Expected cancellation, log and return gracefully
    _logger.LogInformation("Operation cancelled by user");
    return NoContent(); // Or appropriate response
}
catch (Exception ex)
{
    // Unexpected error, log and rethrow
    _logger.LogError(ex, "Unexpected error");
    throw;
}

// ❌ Bad - Treating cancellation as error
try
{
    await ProcessAsync(cancellationToken);
}
catch (Exception ex) // Catches everything including OperationCanceledException
{
    _logger.LogError(ex, "Error occurred"); // Logs cancellation as error
    throw;
}
```

### ✅ 5. **CancellationTokenSource সঠিকভাবে dispose করুন**
```csharp
// ✅ Good - Using statement ensures disposal
public async Task<Data> GetDataWithTimeoutAsync()
{
    using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
    return await _repository.GetDataAsync(cts.Token);
}

// ❌ Bad - Memory leak
public async Task<Data> GetDataWithTimeoutAsync()
{
    var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
    return await _repository.GetDataAsync(cts.Token);
    // cts never disposed!
}
```

### ✅ 6. **Token pass করার chain maintain করুন**
```csharp
// ✅ Good - Token flows through all layers
[HttpGet]
public async Task<IActionResult> GetData(CancellationToken cancellationToken)
{
    var data = await _service.GetDataAsync(cancellationToken); // → Service
    return Ok(data);
}

public async Task<Data> GetDataAsync(CancellationToken cancellationToken)
{
    return await _repository.GetDataAsync(cancellationToken); // → Repository
}

public async Task<Data> GetDataAsync(CancellationToken cancellationToken)
{
    return await _dbSet.FirstOrDefaultAsync(cancellationToken); // → EF Core
}

// ❌ Bad - Chain broken
[HttpGet]
public async Task<IActionResult> GetData(CancellationToken cancellationToken)
{
    var data = await _service.GetDataAsync(); // ❌ Token not passed
    return Ok(data);
}
```

### ✅ 7. **Multiple tokens combine করার জন্য CreateLinkedTokenSource ব্যবহার করুন**
```csharp
// ✅ Good - Multiple cancellation sources
public async Task<Data> ComplexOperationAsync(CancellationToken userToken)
{
    using var timeoutCts = new CancellationTokenSource(TimeSpan.FromMinutes(5));
    using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
        userToken,           // User cancelled
        timeoutCts.Token,    // Timeout
        _appLifetime.ApplicationStopping // App shutdown
    );

    return await _repository.GetDataAsync(linkedCts.Token);
}
```

### ✅ 8. **Background services এ সবসময় stoppingToken ব্যবহার করুন**
```csharp
// ✅ Good
protected override async Task ExecuteAsync(CancellationToken stoppingToken)
{
    while (!stoppingToken.IsCancellationRequested)
    {
        await ProcessDataAsync(stoppingToken);
        await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
    }
}

// ❌ Bad - Infinite loop without cancellation
protected override async Task ExecuteAsync(CancellationToken stoppingToken)
{
    while (true) // ❌ Never stops
    {
        await ProcessDataAsync();
        await Task.Delay(TimeSpan.FromMinutes(5));
    }
}
```

---

## Common Pitfalls এবং এড়ানোর উপায়

### ❌ Pitfall 1: CancellationToken pass না করা

```csharp
// ❌ Wrong
public async Task ProcessAsync(CancellationToken cancellationToken)
{
    var data = await _repository.GetDataAsync(); // Token not passed!
    await _service.ProcessAsync(data); // Token not passed!
}

// ✅ Correct
public async Task ProcessAsync(CancellationToken cancellationToken)
{
    var data = await _repository.GetDataAsync(cancellationToken);
    await _service.ProcessAsync(data, cancellationToken);
}
```

### ❌ Pitfall 2: OperationCanceledException কে error হিসেবে log করা

```csharp
// ❌ Wrong
try
{
    await ProcessAsync(cancellationToken);
}
catch (Exception ex)
{
    _logger.LogError(ex, "Error occurred"); // Logs cancellation as error!
    throw;
}

// ✅ Correct
try
{
    await ProcessAsync(cancellationToken);
}
catch (OperationCanceledException)
{
    _logger.LogInformation("Operation cancelled");
    throw;
}
catch (Exception ex)
{
    _logger.LogError(ex, "Error occurred");
    throw;
}
```

### ❌ Pitfall 3: CancellationTokenSource dispose না করা

```csharp
// ❌ Wrong - Memory leak
public async Task ProcessAsync()
{
    var cts = new CancellationTokenSource(TimeSpan.FromMinutes(5));
    await _repository.GetDataAsync(cts.Token);
    // cts never disposed!
}

// ✅ Correct
public async Task ProcessAsync()
{
    using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(5));
    await _repository.GetDataAsync(cts.Token);
} // cts automatically disposed
```

### ❌ Pitfall 4: Long loop এ cancellation check না করা

```csharp
// ❌ Wrong - Can't be cancelled mid-processing
public async Task ProcessItemsAsync(
    List<Item> items,
    CancellationToken cancellationToken)
{
    foreach (var item in items) // Loop runs to completion
    {
        await ProcessItemAsync(item);
    }
}

// ✅ Correct
public async Task ProcessItemsAsync(
    List<Item> items,
    CancellationToken cancellationToken)
{
    foreach (var item in items)
    {
        cancellationToken.ThrowIfCancellationRequested(); // Check each iteration
        await ProcessItemAsync(item, cancellationToken);
    }
}
```

### ❌ Pitfall 5: Synchronous code এ CancellationToken ignore করা

```csharp
// ❌ Wrong
public async Task ProcessAsync(CancellationToken cancellationToken)
{
    // Heavy synchronous work that can't be cancelled
    for (int i = 0; i < 1000000; i++)
    {
        HeavyComputation(i);
    }

    await SaveAsync(cancellationToken);
}

// ✅ Correct
public async Task ProcessAsync(CancellationToken cancellationToken)
{
    for (int i = 0; i < 1000000; i++)
    {
        if (i % 1000 == 0) // Check every 1000 iterations
        {
            cancellationToken.ThrowIfCancellationRequested();
        }

        HeavyComputation(i);
    }

    await SaveAsync(cancellationToken);
}
```

### ❌ Pitfall 6: Task.Delay without CancellationToken

```csharp
// ❌ Wrong - Delay can't be cancelled
protected override async Task ExecuteAsync(CancellationToken stoppingToken)
{
    while (!stoppingToken.IsCancellationRequested)
    {
        await ProcessAsync();
        await Task.Delay(TimeSpan.FromMinutes(5)); // ❌ Not cancellable
    }
}

// ✅ Correct
protected override async Task ExecuteAsync(CancellationToken stoppingToken)
{
    while (!stoppingToken.IsCancellationRequested)
    {
        await ProcessAsync(stoppingToken);
        await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken); // ✅ Cancellable
    }
}
```

### ❌ Pitfall 7: Token reuse করা

```csharp
// ❌ Wrong - Reusing cancelled token
var cts = new CancellationTokenSource();
cts.Cancel();

await Operation1Async(cts.Token); // Throws immediately
await Operation2Async(cts.Token); // Also throws immediately

// ✅ Correct - Create new token for each independent operation
using var cts1 = new CancellationTokenSource();
await Operation1Async(cts1.Token);

using var cts2 = new CancellationTokenSource();
await Operation2Async(cts2.Token);
```

---

## সংক্ষিপ্ত সারাংশ

### কখন CancellationToken ব্যবহার করবেন?
1. ✅ সব async API endpoints এ
2. ✅ সব async service methods এ
3. ✅ সব async repository methods এ
4. ✅ Background services এ
5. ✅ Long-running operations এ
6. ✅ Database queries এ
7. ✅ HTTP calls এ
8. ✅ File I/O operations এ

### কখন CancellationToken ব্যবহার নাও করতে পারেন?
1. ⚠️ Very short operations (< 100ms)
2. ⚠️ Pure computation without I/O
3. ⚠️ Already completed synchronous code

### Implementation Priority:
1. **High Priority:** Controllers, Services, Repositories (user-facing APIs)
2. **Medium Priority:** Background services, batch processes
3. **Low Priority:** Internal helper methods, very short operations

### Migration Strategy:
1. Start with **Controllers** (add parameter)
2. Update **Services** (add parameter with default)
3. Update **Repositories** (if not using refactored version)
4. Test thoroughly
5. Monitor for `OperationCanceledException` logs

---

## পরবর্তী পদক্ষেপ

আপনার প্রোজেক্টে CancellationToken implement করার জন্য:

1. **Controller Layer থেকে শুরু করুন** - সবচেয়ে বেশি impact
2. **Service Layer update করুন** - মধ্যম layer
3. **Repository Layer migrate করুন** - Refactored version use করুন
4. **Exception handling যোগ করুন** - `OperationCanceledException` catch করুন
5. **Testing করুন** - Cancellation scenarios test করুন
6. **Monitoring setup করুন** - Cancellation metrics track করুন

---

**তৈরি করা হয়েছে:** 2026-03-09
**Version:** 1.0
**প্রোজেক্ট:** bdDevCRM.BackEnd
