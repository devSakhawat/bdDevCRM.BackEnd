using Microsoft.AspNetCore.Http;

namespace bdDevCRM.Shared.ApiResponse;

/// <summary>
/// Helper class for creating standardized API responses
/// Provides factory methods for success and error responses with consistent structure
/// </summary>
public static class StandardResponseHelper
{
    private const string DefaultApiVersion = "1.0";

    #region Success Responses

    /// <summary>
    /// Creates a successful response with data (200 OK)
    /// </summary>
    public static StandardApiResponse<T> Success<T>(
        T data,
        string message = null,
        string version = DefaultApiVersion,
        List<ResourceLink> links = null)
    {
        return new StandardApiResponse<T>
        {
            StatusCode = StatusCodes.Status200OK,
            Success = true,
            Message = message ?? "Operation completed successfully",
            Version = version,
            Data = data,
            Links = links
        };
    }

    /// <summary>
    /// Creates a successful response with pagination (200 OK)
    /// </summary>
    public static StandardApiResponse<T> SuccessWithPagination<T>(
        T data,
        int currentPage,
        int pageSize,
        int totalCount,
        string message = null,
        string version = DefaultApiVersion,
        List<ResourceLink> links = null)
    {
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        var startIndex = (currentPage - 1) * pageSize;
        var endIndex = Math.Min(startIndex + pageSize - 1, totalCount - 1);

        return new StandardApiResponse<T>
        {
            StatusCode = StatusCodes.Status200OK,
            Success = true,
            Message = message ?? "Data retrieved successfully",
            Version = version,
            Data = data,
            Pagination = new PaginationMetadata
            {
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = totalPages,
                HasNextPage = currentPage < totalPages,
                HasPreviousPage = currentPage > 1,
                StartIndex = startIndex,
                EndIndex = endIndex
            },
            Links = links
        };
    }

    /// <summary>
    /// Creates a successful response without data (200 OK)
    /// </summary>
    public static StandardApiResponse Success(
        string message = null,
        string version = DefaultApiVersion)
    {
        return new StandardApiResponse
        {
            StatusCode = StatusCodes.Status200OK,
            Success = true,
            Message = message ?? "Operation completed successfully",
            Version = version
        };
    }

    /// <summary>
    /// Creates a resource created response (201 Created)
    /// </summary>
    public static StandardApiResponse<T> Created<T>(
        T data,
        string message = null,
        string version = DefaultApiVersion,
        List<ResourceLink> links = null)
    {
        return new StandardApiResponse<T>
        {
            StatusCode = StatusCodes.Status201Created,
            Success = true,
            Message = message ?? "Resource created successfully",
            Version = version,
            Data = data,
            Links = links
        };
    }

    /// <summary>
    /// Creates a resource updated response (200 OK)
    /// </summary>
    public static StandardApiResponse<T> Updated<T>(
        T data,
        string message = null,
        string version = DefaultApiVersion,
        List<ResourceLink> links = null)
    {
        return new StandardApiResponse<T>
        {
            StatusCode = StatusCodes.Status200OK,
            Success = true,
            Message = message ?? "Resource updated successfully",
            Version = version,
            Data = data,
            Links = links
        };
    }

    /// <summary>
    /// Creates a no content response (204 No Content)
    /// </summary>
    public static StandardApiResponse<T> NoContent<T>(
        string message = null,
        string version = DefaultApiVersion)
    {
        return new StandardApiResponse<T>
        {
            StatusCode = StatusCodes.Status204NoContent,
            Success = true,
            Message = message ?? "No content available",
            Version = version
        };
    }

    #endregion

    #region Error Responses

    /// <summary>
    /// Creates a bad request error response (400)
    /// </summary>
    public static StandardApiResponse<T> BadRequest<T>(
        string message = null,
        string errorCode = "BAD_REQUEST",
        string details = null,
        Dictionary<string, string[]> validationErrors = null,
        string version = DefaultApiVersion)
    {
        return new StandardApiResponse<T>
        {
            StatusCode = StatusCodes.Status400BadRequest,
            Success = false,
            Message = message ?? "Bad request",
            Version = version,
            Error = new ErrorDetails
            {
                Code = errorCode,
                Type = "BadRequest",
                Details = details,
                ValidationErrors = validationErrors
            }
        };
    }

    /// <summary>
    /// Creates an unauthorized error response (401)
    /// </summary>
    public static StandardApiResponse<T> Unauthorized<T>(
        string message = null,
        string errorCode = "UNAUTHORIZED",
        string details = null,
        string version = DefaultApiVersion)
    {
        return new StandardApiResponse<T>
        {
            StatusCode = StatusCodes.Status401Unauthorized,
            Success = false,
            Message = message ?? "Unauthorized access",
            Version = version,
            Error = new ErrorDetails
            {
                Code = errorCode,
                Type = "Unauthorized",
                Details = details
            }
        };
    }

    /// <summary>
    /// Creates a forbidden error response (403)
    /// </summary>
    public static StandardApiResponse<T> Forbidden<T>(
        string message = null,
        string errorCode = "FORBIDDEN",
        string details = null,
        string version = DefaultApiVersion)
    {
        return new StandardApiResponse<T>
        {
            StatusCode = StatusCodes.Status403Forbidden,
            Success = false,
            Message = message ?? "Access forbidden",
            Version = version,
            Error = new ErrorDetails
            {
                Code = errorCode,
                Type = "Forbidden",
                Details = details
            }
        };
    }

    /// <summary>
    /// Creates a not found error response (404)
    /// </summary>
    public static StandardApiResponse<T> NotFound<T>(
        string message = null,
        string errorCode = "NOT_FOUND",
        string details = null,
        string version = DefaultApiVersion)
    {
        return new StandardApiResponse<T>
        {
            StatusCode = StatusCodes.Status404NotFound,
            Success = false,
            Message = message ?? "Resource not found",
            Version = version,
            Error = new ErrorDetails
            {
                Code = errorCode,
                Type = "NotFound",
                Details = details
            }
        };
    }

    /// <summary>
    /// Creates a conflict error response (409)
    /// </summary>
    public static StandardApiResponse<T> Conflict<T>(
        string message = null,
        string errorCode = "CONFLICT",
        string details = null,
        string version = DefaultApiVersion)
    {
        return new StandardApiResponse<T>
        {
            StatusCode = StatusCodes.Status409Conflict,
            Success = false,
            Message = message ?? "Resource conflict",
            Version = version,
            Error = new ErrorDetails
            {
                Code = errorCode,
                Type = "Conflict",
                Details = details
            }
        };
    }

    /// <summary>
    /// Creates a validation error response (422)
    /// </summary>
    public static StandardApiResponse<T> ValidationError<T>(
        string message = null,
        Dictionary<string, string[]> validationErrors = null,
        string errorCode = "VALIDATION_ERROR",
        string version = DefaultApiVersion)
    {
        return new StandardApiResponse<T>
        {
            StatusCode = StatusCodes.Status422UnprocessableEntity,
            Success = false,
            Message = message ?? "Validation failed",
            Version = version,
            Error = new ErrorDetails
            {
                Code = errorCode,
                Type = "ValidationError",
                ValidationErrors = validationErrors
            }
        };
    }

    /// <summary>
    /// Creates an internal server error response (500)
    /// </summary>
    public static StandardApiResponse<T> InternalServerError<T>(
        string message = null,
        string errorCode = "INTERNAL_ERROR",
        string details = null,
        string stackTrace = null,
        string version = DefaultApiVersion)
    {
        return new StandardApiResponse<T>
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            Success = false,
            Message = message ?? "Internal server error",
            Version = version,
            Error = new ErrorDetails
            {
                Code = errorCode,
                Type = "InternalServerError",
                Details = details,
                StackTrace = stackTrace
            }
        };
    }

    #endregion

    #region HATEOAS Link Helpers

    /// <summary>
    /// Creates a self link
    /// </summary>
    public static ResourceLink SelfLink(string href, string description = null)
    {
        return new ResourceLink
        {
            Rel = "self",
            Href = href,
            Method = "GET",
            Description = description ?? "Current resource"
        };
    }

    /// <summary>
    /// Creates a next page link
    /// </summary>
    public static ResourceLink NextLink(string href, string description = null)
    {
        return new ResourceLink
        {
            Rel = "next",
            Href = href,
            Method = "GET",
            Description = description ?? "Next page"
        };
    }

    /// <summary>
    /// Creates a previous page link
    /// </summary>
    public static ResourceLink PreviousLink(string href, string description = null)
    {
        return new ResourceLink
        {
            Rel = "prev",
            Href = href,
            Method = "GET",
            Description = description ?? "Previous page"
        };
    }

    /// <summary>
    /// Creates a first page link
    /// </summary>
    public static ResourceLink FirstLink(string href, string description = null)
    {
        return new ResourceLink
        {
            Rel = "first",
            Href = href,
            Method = "GET",
            Description = description ?? "First page"
        };
    }

    /// <summary>
    /// Creates a last page link
    /// </summary>
    public static ResourceLink LastLink(string href, string description = null)
    {
        return new ResourceLink
        {
            Rel = "last",
            Href = href,
            Method = "GET",
            Description = description ?? "Last page"
        };
    }

    /// <summary>
    /// Creates a related resource link
    /// </summary>
    public static ResourceLink RelatedLink(string rel, string href, string method = "GET", string description = null)
    {
        return new ResourceLink
        {
            Rel = rel,
            Href = href,
            Method = method,
            Description = description
        };
    }

    /// <summary>
    /// Generates pagination links for list endpoints
    /// </summary>
    public static List<ResourceLink> GeneratePaginationLinks(
        string baseUrl,
        int currentPage,
        int totalPages,
        int pageSize)
    {
        var links = new List<ResourceLink>
        {
            SelfLink($"{baseUrl}?page={currentPage}&pageSize={pageSize}")
        };

        if (currentPage > 1)
        {
            links.Add(FirstLink($"{baseUrl}?page=1&pageSize={pageSize}"));
            links.Add(PreviousLink($"{baseUrl}?page={currentPage - 1}&pageSize={pageSize}"));
        }

        if (currentPage < totalPages)
        {
            links.Add(NextLink($"{baseUrl}?page={currentPage + 1}&pageSize={pageSize}"));
            links.Add(LastLink($"{baseUrl}?page={totalPages}&pageSize={pageSize}"));
        }

        return links;
    }

    #endregion
}
