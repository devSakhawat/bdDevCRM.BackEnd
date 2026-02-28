# API Response Format Specification

## Standard Response Structure

All API endpoints return responses in a standardized format for consistency and ease of integration.

### Success Response

```json
{
  "statusCode": 200,
  "success": true,
  "message": "Operation completed successfully",
  "version": "1.0",
  "timestamp": "2026-02-28T17:00:00Z",
  "data": {
    // Response payload
  },
  "pagination": {
    // Only for paginated responses
    "currentPage": 1,
    "pageSize": 20,
    "totalCount": 100,
    "totalPages": 5,
    "hasNextPage": true,
    "hasPreviousPage": false,
    "startIndex": 0,
    "endIndex": 19
  },
  "links": [
    {
      "rel": "self",
      "href": "/api/resource",
      "method": "GET",
      "description": "Current resource"
    }
  ],
  "correlationId": "abc123-def456-ghi789"
}
```

### Error Response

```json
{
  "statusCode": 404,
  "success": false,
  "message": "Resource not found",
  "version": "1.0",
  "timestamp": "2026-02-28T17:00:00Z",
  "data": null,
  "error": {
    "code": "NOT_FOUND",
    "type": "NotFoundException",
    "details": "User with ID 123 was not found",
    "validationErrors": {
      "field1": ["Error message 1", "Error message 2"]
    },
    "stackTrace": "... (only in development mode)",
    "additionalData": {
      "requestedId": 123
    }
  },
  "correlationId": "abc123-def456-ghi789"
}
```

## Response Fields

| Field | Type | Description | Always Present |
|-------|------|-------------|----------------|
| `statusCode` | number | HTTP status code (200, 404, etc.) | ✅ Yes |
| `success` | boolean | Indicates if request was successful | ✅ Yes |
| `message` | string | Human-readable message | ✅ Yes |
| `version` | string | API version | ✅ Yes |
| `timestamp` | string (ISO 8601) | Response timestamp (UTC) | ✅ Yes |
| `data` | any | Response payload (null for errors) | ✅ Yes |
| `error` | object | Error details (null for success) | ❌ Errors only |
| `pagination` | object | Pagination metadata | ❌ Paginated lists only |
| `links` | array | HATEOAS navigation links | ❌ Optional |
| `correlationId` | string | Request tracing ID | ✅ Yes |

## HTTP Status Codes

| Status Code | Meaning | Example |
|-------------|---------|---------|
| 200 | OK | Successful GET, PUT, PATCH |
| 201 | Created | Successful POST |
| 204 | No Content | Successful DELETE |
| 400 | Bad Request | Invalid input data |
| 401 | Unauthorized | Missing or invalid authentication |
| 403 | Forbidden | Insufficient permissions |
| 404 | Not Found | Resource doesn't exist |
| 409 | Conflict | Duplicate resource |
| 422 | Unprocessable Entity | Validation failed |
| 500 | Internal Server Error | Server-side error |
| 503 | Service Unavailable | Service temporarily down |

## Error Codes

Common error codes returned in the `error.code` field:

| Error Code | Description |
|------------|-------------|
| `BAD_REQUEST` | Invalid request data |
| `UNAUTHORIZED` | Authentication required |
| `FORBIDDEN` | Access denied |
| `NOT_FOUND` | Resource not found |
| `CONFLICT` | Resource already exists |
| `VALIDATION_ERROR` | Input validation failed |
| `INTERNAL_ERROR` | Server error |
| `DATABASE_ERROR` | Database operation failed |
| `TOKEN_EXPIRED` | Authentication token expired |
| `INVALID_TOKEN` | Authentication token invalid |

## Pagination

Paginated endpoints include a `pagination` object with the following fields:

```json
{
  "pagination": {
    "currentPage": 2,        // Current page number (1-based)
    "pageSize": 20,          // Items per page
    "totalCount": 150,       // Total items across all pages
    "totalPages": 8,         // Total number of pages
    "hasNextPage": true,     // Whether next page exists
    "hasPreviousPage": true, // Whether previous page exists
    "startIndex": 20,        // Index of first item on page (0-based)
    "endIndex": 39           // Index of last item on page (0-based)
  }
}
```

### Pagination Query Parameters

```
GET /api/users?page=2&pageSize=20
```

| Parameter | Type | Default | Description |
|-----------|------|---------|-------------|
| `page` | number | 1 | Page number (1-based) |
| `pageSize` | number | 20 | Number of items per page |

## HATEOAS Links

Responses may include `links` array for API discoverability:

```json
{
  "links": [
    {
      "rel": "self",
      "href": "/api/users?page=2",
      "method": "GET",
      "description": "Current resource"
    },
    {
      "rel": "next",
      "href": "/api/users?page=3",
      "method": "GET",
      "description": "Next page"
    },
    {
      "rel": "prev",
      "href": "/api/users?page=1",
      "method": "GET",
      "description": "Previous page"
    },
    {
      "rel": "first",
      "href": "/api/users?page=1",
      "method": "GET",
      "description": "First page"
    },
    {
      "rel": "last",
      "href": "/api/users?page=8",
      "method": "GET",
      "description": "Last page"
    }
  ]
}
```

### Common Link Relations

| Relation | Description |
|----------|-------------|
| `self` | Current resource |
| `next` | Next page/resource |
| `prev` | Previous page/resource |
| `first` | First page |
| `last` | Last page |
| `create` | Create new resource |
| `update` | Update resource |
| `delete` | Delete resource |
| `related` | Related resource |

## HTTP Headers

### Request Headers

| Header | Description | Example |
|--------|-------------|---------|
| `Accept` | Desired response format | `application/json` |
| `Content-Type` | Request body format | `application/json` |
| `Authorization` | Bearer token | `Bearer eyJhbGc...` |
| `If-None-Match` | ETag for conditional request | `"abc123"` |

### Response Headers

| Header | Description | Example |
|--------|-------------|---------|
| `Content-Type` | Response format | `application/json; charset=utf-8` |
| `Cache-Control` | Caching strategy | `public, max-age=300` |
| `ETag` | Entity tag | `"abc123"` |
| `Last-Modified` | Last modification time | `Wed, 28 Feb 2026 17:00:00 GMT` |
| `X-RateLimit-Limit` | Rate limit maximum | `1000` |
| `X-RateLimit-Remaining` | Remaining requests | `950` |
| `X-RateLimit-Reset` | Reset timestamp | `1709143200` |
| `X-RateLimit-Window` | Rate limit window (seconds) | `3600` |
| `X-RateLimit-Policy` | Rate limit policy | `standard` |

## Content Negotiation

The API supports multiple response formats. Specify the desired format using the `Accept` header:

### JSON (Default)
```bash
curl -H "Accept: application/json" https://api.example.com/users
```

### XML
```bash
curl -H "Accept: application/xml" https://api.example.com/users
```

### CSV
```bash
curl -H "Accept: text/csv" https://api.example.com/users
```

## Caching

### Cache Strategy

The API implements intelligent caching based on endpoint type:

| Endpoint Type | Cache-Control | Max Age |
|---------------|---------------|---------|
| Static resources (.css, .js, images) | `public, max-age=31536000, immutable` | 1 year |
| Public data | `public, max-age=300, must-revalidate` | 5 minutes |
| User-specific data | `private, max-age=60, must-revalidate` | 1 minute |
| Dynamic data | `no-cache, must-revalidate` | No cache |

### Conditional Requests

Use `If-None-Match` header with ETag to avoid unnecessary data transfer:

```bash
# First request
curl -H "Accept: application/json" https://api.example.com/users
# Response includes: ETag: "abc123"

# Subsequent request
curl -H "Accept: application/json" -H 'If-None-Match: "abc123"' https://api.example.com/users
# Returns 304 Not Modified if data hasn't changed
```

## Rate Limiting

All API responses include rate limit information in headers:

```
X-RateLimit-Limit: 1000
X-RateLimit-Remaining: 950
X-RateLimit-Reset: 1709143200
X-RateLimit-Window: 3600
X-RateLimit-Policy: standard
```

### Rate Limit Policies

| Policy | Limit | Window | Endpoints |
|--------|-------|--------|-----------|
| `standard` | 1000 requests | 1 hour | Most endpoints |
| `strict` | 50 requests | 1 hour | Authentication endpoints |
| `resource-intensive` | 100 requests | 1 hour | Upload/import endpoints |

### Handling Rate Limits

When rate limit is exceeded, API returns:

```json
{
  "statusCode": 429,
  "success": false,
  "message": "Rate limit exceeded",
  "error": {
    "code": "RATE_LIMIT_EXCEEDED",
    "type": "RateLimitException",
    "details": "You have exceeded the rate limit. Please try again later."
  }
}
```

## Request Tracing

Every request/response includes a `correlationId` for tracing:

```json
{
  "correlationId": "abc123-def456-ghi789",
  ...
}
```

Use this ID when:
- Reporting issues
- Debugging errors
- Tracking requests across services

## Example Responses

### Successful GET (Single Item)

```json
{
  "statusCode": 200,
  "success": true,
  "message": "User retrieved successfully",
  "version": "1.0",
  "timestamp": "2026-02-28T17:00:00Z",
  "data": {
    "id": 123,
    "name": "John Doe",
    "email": "john@example.com"
  },
  "links": [
    {
      "rel": "self",
      "href": "/api/users/123",
      "method": "GET"
    },
    {
      "rel": "update",
      "href": "/api/users/123",
      "method": "PUT"
    },
    {
      "rel": "delete",
      "href": "/api/users/123",
      "method": "DELETE"
    }
  ],
  "correlationId": "abc123-def456-ghi789"
}
```

### Successful GET (Paginated List)

```json
{
  "statusCode": 200,
  "success": true,
  "message": "Users retrieved successfully",
  "version": "1.0",
  "timestamp": "2026-02-28T17:00:00Z",
  "data": [
    {
      "id": 1,
      "name": "John Doe",
      "email": "john@example.com"
    },
    {
      "id": 2,
      "name": "Jane Smith",
      "email": "jane@example.com"
    }
  ],
  "pagination": {
    "currentPage": 1,
    "pageSize": 20,
    "totalCount": 150,
    "totalPages": 8,
    "hasNextPage": true,
    "hasPreviousPage": false,
    "startIndex": 0,
    "endIndex": 19
  },
  "links": [
    {
      "rel": "self",
      "href": "/api/users?page=1&pageSize=20",
      "method": "GET"
    },
    {
      "rel": "next",
      "href": "/api/users?page=2&pageSize=20",
      "method": "GET"
    },
    {
      "rel": "last",
      "href": "/api/users?page=8&pageSize=20",
      "method": "GET"
    }
  ],
  "correlationId": "abc123-def456-ghi789"
}
```

### Successful POST

```json
{
  "statusCode": 201,
  "success": true,
  "message": "User created successfully",
  "version": "1.0",
  "timestamp": "2026-02-28T17:00:00Z",
  "data": {
    "id": 124,
    "name": "Alice Johnson",
    "email": "alice@example.com"
  },
  "links": [
    {
      "rel": "self",
      "href": "/api/users/124",
      "method": "GET"
    }
  ],
  "correlationId": "abc123-def456-ghi789"
}
```

### Validation Error

```json
{
  "statusCode": 422,
  "success": false,
  "message": "Validation failed",
  "version": "1.0",
  "timestamp": "2026-02-28T17:00:00Z",
  "data": null,
  "error": {
    "code": "VALIDATION_ERROR",
    "type": "ValidationError",
    "details": "One or more fields contain invalid data",
    "validationErrors": {
      "email": ["Email is required", "Email format is invalid"],
      "name": ["Name must be at least 3 characters"]
    }
  },
  "correlationId": "abc123-def456-ghi789"
}
```

### Not Found Error

```json
{
  "statusCode": 404,
  "success": false,
  "message": "Resource not found",
  "version": "1.0",
  "timestamp": "2026-02-28T17:00:00Z",
  "data": null,
  "error": {
    "code": "NOT_FOUND",
    "type": "NotFoundException",
    "details": "User with ID 999 was not found"
  },
  "correlationId": "abc123-def456-ghi789"
}
```

### Authentication Error

```json
{
  "statusCode": 401,
  "success": false,
  "message": "Authentication token has expired",
  "version": "1.0",
  "timestamp": "2026-02-28T17:00:00Z",
  "data": null,
  "error": {
    "code": "TOKEN_EXPIRED",
    "type": "SecurityTokenExpired",
    "details": "Your authentication token has expired. Please log in again."
  },
  "correlationId": "abc123-def456-ghi789"
}
```

## Best Practices

1. **Always check `success` field** before processing response data
2. **Use `correlationId`** for debugging and support requests
3. **Implement pagination** for list endpoints to improve performance
4. **Follow HATEOAS links** for API navigation instead of hardcoding URLs
5. **Respect rate limits** by monitoring `X-RateLimit-*` headers
6. **Use ETags** for conditional requests to reduce bandwidth
7. **Handle all documented status codes** in your client application
8. **Parse error details** from `error` object for user-friendly messages

## Version History

| Version | Release Date | Changes |
|---------|--------------|---------|
| 1.0 | 2026-02-28 | Initial standardized response format |

---

For implementation details and migration guide, see [MIGRATION_GUIDE.md](./MIGRATION_GUIDE.md).
