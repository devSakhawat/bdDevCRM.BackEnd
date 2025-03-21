namespace bdDevCRM.Api.ApiResponseError;

public class ApiResponse
{
  public ApiResponse(int statusCode, string message = null)
  {
    StatusCode = statusCode;
    Message = message ?? GetDefaultMessageForStatusCode(statusCode);
  }

  public int StatusCode { get; set; }
  public string Message { get; set; }

  private string GetDefaultMessageForStatusCode(int statusCode)
  {
    return statusCode switch
    {
      400 => "A bad requet, you have made!",
      401 => "Authorized, you are not!",
      404 => "Resource found, it was not!",
      405 => "Invalid url",
      409 => "Duplicate data found!",
      500 => "Internal server error!",
      _ => null
    };
  }
}