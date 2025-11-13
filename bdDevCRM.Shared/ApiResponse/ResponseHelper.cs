namespace bdDevCRM.Shared.ApiResponse;


public static class ResponseHelper
{
  // Success responses
  public static ApiResponse<T> Success<T>(T data, string message = null)
  {
    return new ApiResponse<T>(data, 200, message ?? "Operation completed successfully");
  }

  public static ApiResponse<T> Created<T>(T data, string message = null)
  {
    return new ApiResponse<T>(data, 201, message ?? "Resource created successfully");
  }

  public static ApiResponse<T> Updated<T>(T data, string message = null)
  {
    return new ApiResponse<T>(data, 200, message ?? "Resource updated successfully");
  }


  public static ApiResponse Success(string message = null)
  {
    return new ApiResponse(200, message ?? "Operation completed successfully");
  }

  public static ApiResponse<T> NoContent<T>(string message = null)
  {
    return new ApiResponse<T>(204, message ?? "No content available");
  }

  // Error responses
  public static ApiException BadRequest(string message = null, string details = null)
  {
    return new ApiException(400, message, details)
    {
      ErrorType = "BadRequest"
    };
  }

  public static ApiException Unauthorized(string message = null)
  {
    return new ApiException(401, message)
    {
      ErrorType = "Unauthorized"
    };
  }

  public static ApiException Forbidden(string message = null)
  {
    return new ApiException(403, message)
    {
      ErrorType = "Forbidden"
    };
  }

  public static ApiException NotFound(string message = null)
  {
    return new ApiException(404, message)
    {
      ErrorType = "NotFound"
    };
  }

  public static ApiException Conflict(string message = null)
  {
    return new ApiException(409, message)
    {
      ErrorType = "Conflict"
    };
  }

  public static ApiException ValidationError(string message = null, Dictionary<string, string[]> errors = null)
  {
    return new ApiException(422, message ?? "Validation failed")
    {
      ErrorType = "ValidationError",
      ValidationErrors = errors
    };
  }

  public static ApiException InternalServerError(string message = null, string details = null)
  {
    return new ApiException(500, message, details)
    {
      ErrorType = "InternalServerError"
    };
  }
}


