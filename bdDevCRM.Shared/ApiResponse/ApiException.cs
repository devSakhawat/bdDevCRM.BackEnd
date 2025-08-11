using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Shared.ApiResponse;

public class ApiException : ApiResponse
{
  public ApiException(int statusCode, string message = null, string details = null) : base(statusCode, message)
  {
    ErrorType = "UnknownError";
    Details = details;
  }
  public string ErrorType { get; set; }
  public Dictionary<string, string[]> ValidationErrors { get; set; }
  public string CorrelationId { get; set; }

  public string Details { get; set; }
}