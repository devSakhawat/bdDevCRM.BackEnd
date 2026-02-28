# API Response Standardization - Migration Guide

## Overview

This guide explains the changes made to standardize API responses across all endpoints and how to migrate existing controllers to use the new response format.

## What Changed?

### 1. **Unified Response Format** ✅
All API responses now use a consistent structure with the following properties:
- `statusCode`: HTTP status code
- `success`: Boolean indicating success/failure
- `message`: Human-readable message
- `version`: API version (default: "1.0")
- `timestamp`: Response timestamp (UTC)
- `data`: Response payload (null for errors)
- `error`: Error details (null for success)
- `pagination`: Pagination metadata (for list responses)
- `links`: HATEOAS links for resource discovery
- `correlationId`: Request tracing ID

### 2. **Removed Duplicate Classes** ✅
Deleted the entire `bdDevCRM.Api/ApiResponseError/` directory which contained commented-out duplicate response classes.

### 3. **API Versioning** ✅
All responses now include a `version` field (configurable in `appsettings.json`):
```json
{
  "version": "1.0",
  "statusCode": 200,
  "success": true,
  ...
}
```

### 4. **Pagination Metadata** ✅
List endpoints can now include comprehensive pagination information:
```json
{
  "data": [...],
  "pagination": {
    "currentPage": 1,
    "pageSize": 20,
    "totalCount": 150,
    "totalPages": 8,
    "hasNextPage": true,
    "hasPreviousPage": false,
    "startIndex": 0,
    "endIndex": 19
  }
}
```

### 5. **HATEOAS Links** ✅
Responses include navigation links for API discoverability:
```json
{
  "links": [
    { "rel": "self", "href": "/api/users?page=1", "method": "GET" },
    { "rel": "next", "href": "/api/users?page=2", "method": "GET" },
    { "rel": "last", "href": "/api/users?page=8", "method": "GET" }
  ]
}
```

### 6. **Caching Headers** ✅
All GET responses now include appropriate caching headers:
- `Cache-Control`: Defines caching strategy
- `ETag`: Entity tag for conditional requests
- `Last-Modified`: Timestamp of last modification

### 7. **Content Negotiation** ✅
API now supports multiple response formats:
- **JSON** (default): `Accept: application/json`
- **XML**: `Accept: application/xml`
- **CSV**: `Accept: text/csv`

### 8. **Standardized Error Handling** ✅
All errors now use `StandardExceptionMiddleware` with consistent structure:
```json
{
  "statusCode": 404,
  "success": false,
  "message": "Resource not found",
  "version": "1.0",
  "correlationId": "abc123...",
  "error": {
    "code": "NOT_FOUND",
    "type": "NotFoundException",
    "details": "User with ID 123 was not found",
    "validationErrors": null
  }
}
```

### 9. **Structured Logging** ✅
All requests/responses are now logged with structured data including:
- Correlation ID for request tracing
- Request/response headers
- Request/response body (configurable)
- Execution duration
- User information

### 10. **Rate Limiting Headers** ✅
All responses include rate limit information:
- `X-RateLimit-Limit`: Maximum requests allowed
- `X-RateLimit-Remaining`: Remaining requests
- `X-RateLimit-Reset`: Timestamp when limit resets
- `X-RateLimit-Window`: Time window in seconds
- `X-RateLimit-Policy`: Policy type (standard, strict, etc.)

## Migration Steps

### Step 1: Update Controller Success Responses

**Before:**
```csharp
[HttpGet]
public async Task<IActionResult> GetUsers()
{
    var users = await _serviceManager.Users.GetAllAsync();
    return Ok(ResponseHelper.Success(users, "Users retrieved successfully"));
}
```

**After (Recommended):**
```csharp
[HttpGet]
public async Task<IActionResult> GetUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
{
    var (users, totalCount) = await _serviceManager.Users.GetPagedAsync(page, pageSize);

    // Generate HATEOAS links
    var links = StandardResponseHelper.GeneratePaginationLinks(
        $"{Request.Scheme}://{Request.Host}/api/users",
        page,
        (int)Math.Ceiling(totalCount / (double)pageSize),
        pageSize
    );

    return Ok(StandardResponseHelper.SuccessWithPagination(
        users,
        page,
        pageSize,
        totalCount,
        "Users retrieved successfully",
        links: links
    ));
}
```

### Step 2: Update Controller Error Responses

Controllers no longer need manual error handling - exceptions are caught by `StandardExceptionMiddleware`:

**Before:**
```csharp
[HttpGet("{id}")]
public async Task<IActionResult> GetUser(int id)
{
    if (id <= 0)
        return BadRequest(ResponseHelper.BadRequest("Invalid user ID"));

    var user = await _serviceManager.Users.GetByIdAsync(id);
    if (user == null)
        return NotFound(ResponseHelper.NotFound("User not found"));

    return Ok(ResponseHelper.Success(user));
}
```

**After:**
```csharp
[HttpGet("{id}")]
public async Task<IActionResult> GetUser(int id)
{
    if (id <= 0)
        throw new GenericBadRequestException("Invalid user ID");

    var user = await _serviceManager.Users.GetByIdAsync(id);
    if (user == null)
        throw new GenericNotFoundException($"User with ID {id} not found");

    return Ok(StandardResponseHelper.Success(user, "User retrieved successfully"));
}
```

### Step 3: Update POST Endpoints

**After:**
```csharp
[HttpPost]
public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
{
    var user = await _serviceManager.Users.CreateAsync(dto);

    var links = new List<ResourceLink>
    {
        StandardResponseHelper.SelfLink($"{Request.Scheme}://{Request.Host}/api/users/{user.Id}"),
        StandardResponseHelper.RelatedLink("update", $"{Request.Scheme}://{Request.Host}/api/users/{user.Id}", "PUT"),
        StandardResponseHelper.RelatedLink("delete", $"{Request.Scheme}://{Request.Host}/api/users/{user.Id}", "DELETE")
    };

    return Ok(StandardResponseHelper.Created(user, "User created successfully", links: links));
}
```

### Step 4: Configure Cache Headers (Optional)

Customize cache behavior in `CacheHeaderMiddleware.cs`:

```csharp
private bool IsPublicEndpoint(string path)
{
    // Add your public endpoints that can be cached aggressively
    var publicPaths = new[] { "/api/public", "/api/content", "/api/products" };
    return publicPaths.Any(p => path.StartsWith(p));
}
```

### Step 5: Configure Rate Limits (Optional)

Update rate limits in `appsettings.json`:

```json
{
  "RateLimit": {
    "DefaultLimit": 1000,
    "WindowSeconds": 3600,
    "AuthEndpointLimit": 50,
    "UploadEndpointLimit": 100
  }
}
```

## Configuration Options

### appsettings.json

```json
{
  "ApiSettings": {
    "Version": "1.0",
    "EnableHATEOAS": true,
    "EnablePaginationLinks": true
  },
  "Logging": {
    "StructuredLogging": {
      "Enabled": true,
      "LogRequestBody": true,
      "LogResponseBody": false,
      "MaxBodySize": 4096
    }
  },
  "RateLimit": {
    "DefaultLimit": 1000,
    "WindowSeconds": 3600,
    "AuthEndpointLimit": 50,
    "UploadEndpointLimit": 100
  }
}
```

## Response Helper Methods

### Success Responses
```csharp
// Simple success
StandardResponseHelper.Success(data, "Success message");

// Success with pagination
StandardResponseHelper.SuccessWithPagination(data, page, pageSize, totalCount, "Success message", links: links);

// Created (201)
StandardResponseHelper.Created(data, "Resource created", links: links);

// Updated (200)
StandardResponseHelper.Updated(data, "Resource updated", links: links);

// No content (204)
StandardResponseHelper.NoContent<T>("No content message");
```

### Error Responses (Use Exceptions Instead)
```csharp
// Bad Request (400)
throw new GenericBadRequestException("Invalid input");

// Unauthorized (401)
throw new GenericUnauthorizedException("Not authenticated");

// Forbidden (403)
throw new ForbiddenAccessException("Access denied");

// Not Found (404)
throw new GenericNotFoundException("Resource not found");

// Conflict (409)
throw new ConflictException("Resource already exists");

// Validation Error (422)
throw new ValidationException("Validation failed");
```

### HATEOAS Link Helpers
```csharp
// Self link
StandardResponseHelper.SelfLink("/api/users/1");

// Navigation links
StandardResponseHelper.NextLink("/api/users?page=2");
StandardResponseHelper.PreviousLink("/api/users?page=1");
StandardResponseHelper.FirstLink("/api/users?page=1");
StandardResponseHelper.LastLink("/api/users?page=10");

// Custom link
StandardResponseHelper.RelatedLink("delete", "/api/users/1", "DELETE", "Delete user");

// Auto-generate pagination links
StandardResponseHelper.GeneratePaginationLinks(baseUrl, currentPage, totalPages, pageSize);
```

## Testing Content Negotiation

### JSON (Default)
```bash
curl -H "Accept: application/json" http://localhost:5000/api/users
```

### XML
```bash
curl -H "Accept: application/xml" http://localhost:5000/api/users
```

### CSV
```bash
curl -H "Accept: text/csv" http://localhost:5000/api/users > users.csv
```

## Breaking Changes

1. **Response Structure**: Frontend applications need to update their response parsing to use the new structure:
   - Old: `response.Data` → New: `response.data`
   - Old: `response.StatusCode` → New: `response.statusCode`
   - Old: `response.IsSuccess` → New: `response.success`

2. **Error Structure**: Error responses now have nested `error` object:
   - Old: `response.ErrorType` → New: `response.error.type`
   - Old: `response.ValidationErrors` → New: `response.error.validationErrors`

3. **Pagination**: Pagination info moved to dedicated field:
   - Old: Part of response data → New: `response.pagination`

## Backward Compatibility

The old `ResponseHelper` class is still available for gradual migration. However, it's recommended to migrate to `StandardResponseHelper` for full feature support.

## Benefits

1. ✅ **Consistent Structure**: All responses follow the same format
2. ✅ **API Versioning**: Track breaking changes with version field
3. ✅ **Better Client Support**: Pagination metadata helps clients manage large datasets
4. ✅ **API Discoverability**: HATEOAS links help clients navigate the API
5. ✅ **Performance**: Cache headers reduce server load
6. ✅ **Flexibility**: Content negotiation supports multiple formats
7. ✅ **Monitoring**: Structured logging improves debugging
8. ✅ **Rate Limiting**: Clients can throttle requests appropriately
9. ✅ **Error Handling**: Consistent error format across all endpoints
10. ✅ **Tracing**: Correlation ID enables end-to-end request tracking

## Support

For questions or issues, please contact the development team or open an issue in the repository.
