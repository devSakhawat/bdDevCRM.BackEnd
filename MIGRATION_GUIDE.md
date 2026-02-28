# API Response Standardization - Migration Guide (Updated)

## Overview

This guide explains the changes made to standardize API responses while maintaining compatibility with the existing Kendo Grid implementation.

## What Changed?

### 1. **Unified Response Format for Non-Grid Endpoints** ✅
Standard API responses (non-grid) now use a consistent structure with:
- `statusCode`: HTTP status code
- `success`: Boolean indicating success/failure
- `message`: Human-readable message
- `version`: API version (default: "1.0")
- `timestamp`: Response timestamp (UTC)
- `data`: Response payload
- `error`: Error details (null for success)
- `correlationId`: Request tracing ID

**Note**: Grid endpoints continue using `GridEntity<T>` for Kendo Grid compatibility.

### 2. **Removed Duplicate Classes** ✅
Deleted the entire `bdDevCRM.Api/ApiResponseError/` directory which contained commented-out duplicate response classes.

### 3. **API Versioning** ✅
Non-grid responses include a `version` field (configurable in `appsettings.json`):
```json
{
  "version": "1.0",
  "statusCode": 200,
  "success": true,
  ...
}
```

### 4. **Kendo Grid Pagination (No Changes)** ✅
**Your existing Kendo Grid pagination continues to work as-is:**
```csharp
// Existing pattern - NO CHANGES NEEDED
public async Task<IActionResult> UserSummary([FromBody] CRMGridOptions options)
{
    var summaryGrid = await _serviceManager.Users.UsersSummary(..., options, ...);

    if (summaryGrid == null || !summaryGrid.Items.Any())
        return Ok(ResponseHelper.NoContent<GridEntity<UsersDto>>("No data found"));

    return Ok(ResponseHelper.Success(summaryGrid, "Data retrieved successfully"));
}
```

The existing `GridEntity<T>` structure remains unchanged:
```csharp
public class GridEntity<T>
{
    public IList<T> Items { get; set; }
    public int TotalCount { get; set; }
    public IList<GridColumns> Columnses { get; set; }
}
```

### 5. **HATEOAS Links (Optional, Disabled by Default)** ✅
HATEOAS links are available but **disabled by default** to avoid conflicts. Enable only if needed:
```json
{
  "ApiSettings": {
    "EnableHATEOAS": false  // Disabled by default
  }
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
All errors now use `StandardExceptionMiddleware` with consistent structure.

### 9. **Structured Logging** ✅
All requests/responses are logged with structured data including correlation IDs.

### 10. **Rate Limiting** ❌ **REMOVED**
Rate limiting has been removed as it was not needed for this project.

## What You DON'T Need to Change

### Kendo Grid Endpoints (Keep As-Is)
```csharp
// ✅ This pattern continues to work - NO CHANGES NEEDED
[HttpPost(RouteConstants.UserSummary)]
public async Task<IActionResult> UserSummary([FromBody] CRMGridOptions options, [FromQuery] int companyId)
{
    var summaryGrid = await _serviceManager.Users.UsersSummary(companyId, false, options, currentUser);

    if (summaryGrid == null || !summaryGrid.Items.Any())
        return Ok(ResponseHelper.NoContent<GridEntity<UsersDto>>("No data found"));

    return Ok(ResponseHelper.Success(summaryGrid, "Data retrieved successfully"));
}
```

### CRMGridOptions (No Changes)
```csharp
// ✅ Your existing CRMGridOptions works as-is
public class CRMGridOptions
{
    public int skip { get; set; }
    public int take { get; set; }
    public int page { get; set; }
    public int pageSize { get; set; }
    public List<CRMFilter.GridSort> sort { get; set; }
    public CRMFilter.GridFilters filter { get; set; }
}
```

## What You CAN Optionally Change

### Non-Grid Endpoints (Optional Enhancement)
For single-item or simple list responses (non-grid), you can optionally use StandardApiResponse:

**Before (still works):**
```csharp
[HttpGet("{id}")]
public async Task<IActionResult> GetUser(int id)
{
    var user = await _serviceManager.Users.GetByIdAsync(id);
    return Ok(ResponseHelper.Success(user, "User retrieved"));
}
```

**After (optional, for enhanced structure):**
```csharp
[HttpGet("{id}")]
public async Task<IActionResult> GetUser(int id)
{
    var user = await _serviceManager.Users.GetByIdAsync(id);
    return Ok(StandardResponseHelper.Success(user, "User retrieved successfully"));
}
```

## Configuration Options

### appsettings.json

```json
{
  "ApiSettings": {
    "Version": "1.0",
    "EnableHATEOAS": false,        // Disabled - not needed for Kendo Grid
    "EnablePaginationLinks": false  // Disabled - using GridEntity instead
  },
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

## Key Features (No Breaking Changes)

1. ✅ **Existing Kendo Grid implementation unchanged**
2. ✅ **GridEntity<T> continues to work**
3. ✅ **CRMGridOptions continues to work**
4. ✅ **StandardApiResponse available for non-grid endpoints**
5. ✅ **Standardized error handling**
6. ✅ **Cache headers for performance**
7. ✅ **Content negotiation (CSV/XML)**
8. ✅ **Structured logging**
9. ✅ **API versioning**
10. ❌ **Rate limiting removed** (not needed)

## Testing Content Negotiation

### JSON (Default)
```bash
curl -H "Accept: application/json" http://localhost:5000/api/users/summary
```

### XML
```bash
curl -H "Accept: application/xml" http://localhost:5000/api/users/summary
```

### CSV
```bash
curl -H "Accept: text/csv" http://localhost:5000/api/users/summary > users.csv
```

## Benefits

1. ✅ **No breaking changes** - Existing code continues to work
2. ✅ **Kendo Grid unchanged** - Your pagination already works
3. ✅ **Consistent error format** - All errors structured the same way
4. ✅ **API Versioning** - Track versions easily
5. ✅ **Performance** - Cache headers reduce load
6. ✅ **Flexibility** - CSV/XML export when needed
7. ✅ **Monitoring** - Structured logging improves debugging
8. ✅ **Tracing** - Correlation IDs for tracking

## What Was Removed

1. ❌ **RateLimitHeaderMiddleware** - Not needed for this project
2. ❌ **Rate limiting configuration** - Removed from appsettings.json
3. ❌ **Forced pagination metadata** - Using existing GridEntity instead

## Support

For questions or issues, please contact the development team.
