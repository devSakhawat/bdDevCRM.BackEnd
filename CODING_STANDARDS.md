# bdDevCRM Backend - Enterprise Coding Standards

This document defines the standardized coding patterns for Controllers → Services → Repositories in the bdDevCRM backend system.

## Table of Contents
- [Controller Layer Standards](#controller-layer-standards)
- [Service Layer Standards](#service-layer-standards)
- [Repository Layer Standards](#repository-layer-standards)
- [Common Patterns](#common-patterns)

---

## Controller Layer Standards

### Basic Structure

```csharp
[AuthorizeUser]  // Always apply at class level for authenticated endpoints
public class XyzController : BaseApiController
{
    private readonly IMemoryCache _cache;  // Optional, if caching is needed

    public XyzController(IServiceManager serviceManager, IMemoryCache cache)
        : base(serviceManager)
    {
        _cache = cache;
    }
}
```

### Authentication & Authorization

#### ✅ CORRECT - Use Inherited Properties
```csharp
[HttpGet("{id:int}")]
public async Task<IActionResult> GetById(int id)
{
    // Use inherited properties from BaseApiController
    var userId = CurrentUserId;          // Get current user ID
    var currentUser = CurrentUser;       // Get full user object

    // Business logic...
}
```

#### ❌ INCORRECT - Never Use Direct Claim Extraction
```csharp
// NEVER DO THIS
var userIdClaim = User.FindFirst("UserId")?.Value;
var userId = Convert.ToInt32(userIdClaim);
UsersDto currentUser = _serviceManager.GetCache<UsersDto>(userId);
```

### API Response Pattern

#### ✅ CORRECT - Always Use ApiResponseHelper

```csharp
// GET - Retrieve data
[HttpGet("{id:int}")]
public async Task<IActionResult> GetById(int id)
{
    var result = await _serviceManager.Xyz.GetByIdAsync(id, false);
    return Ok(ApiResponseHelper.Success(result, "Data retrieved successfully"));
}

// POST - Create new resource
[HttpPost]
[ServiceFilter(typeof(EmptyObjectFilterAttribute))]
public async Task<IActionResult> Create([FromBody] XyzDto dto)
{
    var result = await _serviceManager.Xyz.CreateAsync(dto);
    return Ok(ApiResponseHelper.Created(result, "Resource created successfully"));
}

// PUT - Update existing resource
[HttpPut("{id:int}")]
[ServiceFilter(typeof(EmptyObjectFilterAttribute))]
public async Task<IActionResult> Update([FromRoute] int id, [FromBody] XyzDto dto)
{
    var result = await _serviceManager.Xyz.UpdateAsync(id, dto);
    return Ok(ApiResponseHelper.Updated(result, "Resource updated successfully"));
}

// DELETE - Remove resource
[HttpDelete("{id:int}")]
public async Task<IActionResult> Delete([FromRoute] int id)
{
    await _serviceManager.Xyz.DeleteAsync(id);
    return Ok(ApiResponseHelper.NoContent<object>("Resource deleted successfully"));
}

// Empty/No results
[HttpGet]
public async Task<IActionResult> GetAll()
{
    var results = await _serviceManager.Xyz.GetAllAsync(false);

    if (!results.Any())
        return Ok(ApiResponseHelper.Success(Enumerable.Empty<XyzDto>(), "No data found"));

    return Ok(ApiResponseHelper.Success(results, "Data retrieved successfully"));
}
```

#### ❌ INCORRECT - Never Return Raw Objects
```csharp
// NEVER DO THIS
return BadRequest(new { Message = "Invalid request", ErrorCode = "BAD_REQUEST" });
return Unauthorized("User not found");
return Ok(result);  // Without ApiResponseHelper
```

### Error Handling Pattern

#### ✅ CORRECT - Throw Exceptions (Let Middleware Handle)

```csharp
[HttpGet("{id:int}")]
public async Task<IActionResult> GetById(int id)
{
    // Validate parameters - throw exceptions
    if (id <= 0)
        throw new IdParametersBadRequestException();

    // Call service - let it throw exceptions if needed
    var result = await _serviceManager.Xyz.GetByIdAsync(id, false);

    // Return success response
    return Ok(ApiResponseHelper.Success(result, "Data retrieved successfully"));
}
```

#### ❌ INCORRECT - Manual Error Handling in Controllers
```csharp
// NEVER DO THIS
[HttpGet("{id:int}")]
public async Task<IActionResult> GetById(int id)
{
    if (id <= 0)
        return BadRequest(ApiResponseHelper.BadRequest<XyzDto>("Invalid ID"));

    try
    {
        var result = await _serviceManager.Xyz.GetByIdAsync(id, false);

        if (result == null)
            return NotFound(ApiResponseHelper.NotFound<XyzDto>("Not found"));

        return Ok(ApiResponseHelper.Success(result, "Success"));
    }
    catch (Exception ex)
    {
        return StatusCode(500, ApiResponseHelper.InternalServerError<XyzDto>(ex.Message));
    }
}
```

### Action Filter Usage

#### ✅ CORRECT - Trust Action Filters

```csharp
[HttpPost]
[ServiceFilter(typeof(EmptyObjectFilterAttribute))]  // Validates body automatically
public async Task<IActionResult> Create([FromBody] XyzDto dto)
{
    // No need for null check - filter handles it
    // No need for ModelState check - filter handles it

    var result = await _serviceManager.Xyz.CreateAsync(dto);
    return Ok(ApiResponseHelper.Created(result, "Created successfully"));
}
```

#### ❌ INCORRECT - Redundant Validation
```csharp
// NEVER DO THIS
[HttpPost]
[ServiceFilter(typeof(EmptyObjectFilterAttribute))]
public async Task<IActionResult> Create([FromBody] XyzDto dto)
{
    // Redundant - filter already checks this
    if (dto == null)
        throw new NullModelBadRequestException("DTO cannot be null");

    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    // ...
}
```

### Pagination Pattern

```csharp
[HttpPost("summary")]
public async Task<IActionResult> GetSummary([FromBody] CRMGridOptions options)
{
    var result = await _serviceManager.Xyz.GetSummaryAsync(false, options);

    if (result == null || !result.Items.Any())
        return Ok(ApiResponseHelper.Success(new GridEntity<XyzDto>(), "No data found"));

    // Calculate pagination
    int totalPages = (int)Math.Ceiling(result.TotalCount / (double)options.pageSize);
    var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}";
    var links = ApiResponseHelper.GeneratePaginationLinks(
        baseUrl, options.page, totalPages, options.pageSize);

    return Ok(ApiResponseHelper.SuccessWithPagination(
        data: result,
        currentPage: options.page,
        pageSize: options.pageSize,
        totalCount: result.TotalCount,
        message: "Data retrieved successfully",
        links: links
    ));
}
```

---

## Service Layer Standards

### Basic Structure

```csharp
internal sealed class XyzService : IXyzService
{
    private readonly IRepositoryManager _repository;
    private readonly ILogger<XyzService> _logger;
    private readonly IConfiguration _configuration;

    // ✅ Use traditional constructor (more explicit for enterprise)
    public XyzService(
        IRepositoryManager repository,
        ILogger<XyzService> logger,
        IConfiguration configuration)
    {
        _repository = repository;
        _logger = logger;
        _configuration = configuration;
    }
}
```

### Async Method Patterns

#### ✅ CORRECT - Async with Proper Naming

```csharp
public async Task<XyzDto> GetByIdAsync(int id, bool trackChanges)
{
    // Log entry point
    _logger.LogInformation("Getting Xyz with ID: {Id}", id);

    // Repository call
    var entity = await _repository.Xyz.GetByIdAsync(id, trackChanges);

    // Business validation - throw custom exceptions
    if (entity == null)
        throw new GenericNotFoundException($"Xyz with ID {id} not found");

    // Mapping
    var dto = MyMapper.JsonClone<Xyz, XyzDto>(entity);

    return dto;
}
```

### Create/Update Patterns

#### ✅ CORRECT - Create Pattern

```csharp
public async Task<XyzDto> CreateAsync(XyzDto dto)
{
    // Business validation
    if (dto == null)
        throw new NullModelBadRequestException(nameof(XyzDto));

    // Check for duplicates
    bool exists = await _repository.Xyz.ExistsAsync(
        x => x.Name.Trim().ToLower() == dto.Name.Trim().ToLower());

    if (exists)
        throw new DuplicateRecordException("Xyz", "Name");

    // Mapping
    var entity = MyMapper.JsonClone<XyzDto, Xyz>(dto);
    entity.CreatedDate = DateTime.Now;

    // Repository operations
    dto.XyzId = await _repository.Xyz.CreateAndGetIdAsync(entity);
    await _repository.SaveAsync();

    return dto;
}
```

#### ✅ CORRECT - Update Pattern

```csharp
public async Task<XyzDto> UpdateAsync(int id, XyzDto dto)
{
    // Validation
    if (dto == null)
        throw new NullModelBadRequestException(nameof(XyzDto));

    if (id != dto.XyzId)
        throw new IdMismatchBadRequestException(id.ToString(), nameof(XyzDto));

    // Check existence
    var entity = await _repository.Xyz.GetByIdAsync(
        x => x.XyzId == id, trackChanges: false);

    if (entity == null)
        throw new GenericNotFoundException("Xyz", "XyzId", id.ToString());

    // Mapping and update
    entity = MyMapper.JsonClone<XyzDto, Xyz>(dto);
    entity.LastUpdatedDate = DateTime.Now;

    _repository.Xyz.UpdateByState(entity);
    await _repository.SaveAsync();

    return MyMapper.JsonClone<Xyz, XyzDto>(entity);
}
```

### Delete Pattern

```csharp
public async Task DeleteAsync(int id)
{
    if (id <= 0)
        throw new IdParametersBadRequestException();

    var entity = await _repository.Xyz.GetByIdAsync(
        x => x.XyzId == id, trackChanges: false);

    if (entity == null)
        throw new GenericNotFoundException("Xyz", "XyzId", id.ToString());

    // Physical delete
    await _repository.Xyz.DeleteAsync(x => x.XyzId == id, trackChanges: false);
    await _repository.SaveAsync();

    // OR Soft delete
    // entity.IsActive = false;
    // _repository.Xyz.UpdateByState(entity);
    // await _repository.SaveAsync();
}
```

### Logging Best Practices

```csharp
public async Task<XyzDto> GetByIdAsync(int id, bool trackChanges)
{
    // ✅ Log with structured parameters
    _logger.LogInformation("Getting Xyz with ID: {Id}", id);

    var entity = await _repository.Xyz.GetByIdAsync(id, trackChanges);

    if (entity == null)
    {
        // ✅ Log warnings for business logic issues
        _logger.LogWarning("Xyz with ID {Id} not found", id);
        throw new GenericNotFoundException($"Xyz with ID {id} not found");
    }

    return MyMapper.JsonClone<Xyz, XyzDto>(entity);
}
```

---

## Repository Layer Standards

### Basic Structure

```csharp
public class XyzRepository : RepositoryBase<Xyz>, IXyzRepository
{
    public XyzRepository(CRMContext context) : base(context) { }
}
```

### Async Method Patterns

#### ✅ CORRECT - Prefer Async Methods

```csharp
// Simple get by ID
public async Task<Xyz?> GetByIdAsync(int id, bool trackChanges) =>
    await FindByCondition(x => x.XyzId == id, trackChanges)
        .FirstOrDefaultAsync();

// Get all with ordering
public async Task<IEnumerable<Xyz>> GetAllAsync(bool trackChanges) =>
    await FindAll(trackChanges)
        .OrderBy(x => x.Name)
        .ToListAsync();

// Complex query with includes
public async Task<Xyz?> GetWithDetailsAsync(int id, bool trackChanges) =>
    await FindByCondition(x => x.XyzId == id, trackChanges)
        .Include(x => x.RelatedEntity)
        .ThenInclude(x => x.NestedEntity)
        .FirstOrDefaultAsync();
```

### CRUD Operations

```csharp
// Create - use inherited method
public void Create(Xyz entity) => Create(entity);

// Update - use UpdateByState for detached entities
public void Update(Xyz entity) => UpdateByState(entity);

// Delete - use Delete method
public void Delete(Xyz entity) => Delete(entity);
```

### Raw SQL Queries (When Needed)

```csharp
public async Task<IEnumerable<XyzDto>> GetCustomDataAsync(int param)
{
    string query = $"SELECT * FROM Xyz WHERE SomeColumn = {param}";
    IEnumerable<XyzDto> result = await ExecuteListQuery<XyzDto>(query, null);
    return result;
}
```

---

## Common Patterns

### Exception Usage

```csharp
// Parameter validation
if (id <= 0)
    throw new IdParametersBadRequestException();

// Null model check
if (dto == null)
    throw new NullModelBadRequestException(nameof(XyzDto));

// ID mismatch
if (id != dto.XyzId)
    throw new IdMismatchBadRequestException(id.ToString(), nameof(XyzDto));

// Not found
if (entity == null)
    throw new GenericNotFoundException("Xyz", "XyzId", id.ToString());

// Duplicate record
if (exists)
    throw new DuplicateRecordException("Xyz", "Name");

// Unauthorized
if (currentUser == null)
    throw new GenericUnauthorizedException("User authentication required");

// Invalid operation
if (result.Id <= 0)
    throw new InvalidCreateOperationException("Failed to create record");
```

### Data Mapping

```csharp
// Single object
var dto = MyMapper.JsonClone<Entity, Dto>(entity);

// Collection
var dtos = MyMapper.JsonCloneIEnumerableToList<Entity, Dto>(entities);
```

### Async/Await Guidelines

1. **Always** use `async`/`await` for I/O operations
2. **Never** use `.Result` or `.Wait()` (causes deadlocks)
3. **Always** add `Async` suffix to async method names
4. **Prefer** `Task<T>` over `void` for async methods

### Naming Conventions

- Controllers: `XyzController`
- Services: `XyzService` (internal sealed)
- Repositories: `XyzRepository` (public)
- DTOs: `XyzDto`
- Entities: `Xyz`
- Async methods: `GetByIdAsync()`, `CreateAsync()`, etc.

---

## Summary Checklist

### Controllers
- [ ] `[AuthorizeUser]` applied at class level
- [ ] Use `CurrentUser` / `CurrentUserId` properties
- [ ] All responses use `ApiResponseHelper`
- [ ] No manual error handling (throw exceptions)
- [ ] Trust action filters (no redundant validation)
- [ ] Async method signatures (`Task<IActionResult>`)

### Services
- [ ] Traditional constructor pattern
- [ ] Structured logging with ILogger
- [ ] Async methods with proper naming
- [ ] Business validation with custom exceptions
- [ ] Proper data mapping with MyMapper
- [ ] Transaction management when needed

### Repositories
- [ ] Inherit from `RepositoryBase<T>`
- [ ] Async methods preferred
- [ ] Use inherited CRUD methods
- [ ] Raw SQL only when necessary
- [ ] Simple, focused methods

---

**Last Updated**: 2026-03-05
**Version**: 1.0
