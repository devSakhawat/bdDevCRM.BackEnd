namespace bdDevCRM.Api.ApiResponseError;

public class ApiException : ApiResponse
{
  public ApiException(int statusCode, string message = null, string details = null) : base(statusCode, message)
  {
    //Details = details;
  }

  //public string Details { get; set; }

  // New properties
  public string CorrelationId { get; set; }
  public string ErrorType { get; set; }
  public DateTime Timestamp { get; set; }
}

