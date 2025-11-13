//namespace bdDevCRM.Api.ApiResponseError;


//public class ApiResponse<T> : ApiResponse
//{
//  public ApiResponse(T data, int statusCode = 200, string message = null) : base(statusCode, message)
//  {
//    Data = data;
//  }

//  public ApiResponse(int statusCode, string message = null) : base(statusCode, message)
//  {
//    Data = default(T);
//  }

//  public T Data { get; set; }
//}

//public class ApiResponse
//{
//  public ApiResponse(int statusCode, string message = null)
//  {
//    StatusCode = statusCode;
//    Message = message ?? GetDefaultMessageForStatusCode(statusCode);
//    IsSuccess = statusCode >= 200 && statusCode < 300;
//    Timestamp = DateTime.UtcNow;
//  }

//  public int StatusCode { get; set; }
//  public string Message { get; set; }
//  public bool IsSuccess { get; set; }
//  public DateTime Timestamp { get; set; }

//  private string GetDefaultMessageForStatusCode(int statusCode)
//  {
//    return statusCode switch
//    {
//      200 => "Request completed successfully",
//      201 => "Resource created successfully",
//      204 => "No content available",
//      400 => "A bad request, you have made!",
//      401 => "Authorized, you are not!",
//      403 => "Access forbidden!",
//      404 => "Resource found, it was not!",
//      405 => "Invalid url",
//      409 => "Duplicate data found!",
//      422 => "Validation failed!",
//      500 => "Internal server error!",
//      503 => "Service unavailable!",
//      _ => "Unknown status"
//    };
//  }
//}





//public class ApiResponse
//{
//  public ApiResponse(int statusCode, string message = null)
//  {
//    StatusCode = statusCode;
//    Message = message ?? GetDefaultMessageForStatusCode(statusCode);
//  }

//  public int StatusCode { get; set; }
//  public string Message { get; set; }

//  private string GetDefaultMessageForStatusCode(int statusCode)
//  {
//    return statusCode switch
//    {
//      400 => "A bad requet, you have made!",
//      401 => "Authorized, you are not!",
//      404 => "Resource found, it was not!",
//      405 => "Invalid url",
//      409 => "Duplicate data found!",
//      500 => "Internal server error!",
//      _ => null
//    };
//  }
//}

//public class ApiResponse<T> : ApiResponse
//{
//  public ApiResponse(int statusCode, T data, string message = null) : base(statusCode, message)
//  {
//    Data = data;
//  }

//  public T Data { get; set; }
//}
