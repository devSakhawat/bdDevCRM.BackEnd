using System.Security.Claims;
using System.Text.Json;
using System.Text;

namespace bdDevCRM.Api.Middleware;

// Custom middleware to set CreatedBy and UpdateBy fields
public class AuditMiddleware
{
  private readonly RequestDelegate _next;

  public AuditMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    // Get the userId from the User Claims
    var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (!string.IsNullOrEmpty(userIdClaim) && int.TryParse(userIdClaim, out var userId))
    {
      // Check if the request body is a valid JSON model and the HTTP method
      if (context.Request.Method == "POST" || context.Request.Method == "PUT")
      {
        context.Request.EnableBuffering();
        var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
        context.Request.Body.Position = 0;

        if (!string.IsNullOrEmpty(body))
        {
          // Deserialize into a generic dictionary to avoid model loss
          var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
          var modelDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(body, jsonOptions);

          if (modelDictionary != null)
          {
            // Identify if it is an update or create based on the presence of a non-zero Id value
            if (modelDictionary.TryGetValue("CreatedBy", out var createdByValue) && createdByValue is int createdBy && createdBy > 0)
            {
              // For updates, set only UpdateBy and UpdateDate
              modelDictionary["UpdateBy"] = userId;
              modelDictionary["UpdateDate"] = DateTimeOffset.UtcNow;
            }
            else
            {

              modelDictionary["CreatedBy"] = userId;
              modelDictionary["CreatedDate"] = DateTimeOffset.UtcNow;

            }

            // Serialize the updated dictionary back into a JSON string
            var updatedBody = JsonSerializer.Serialize(modelDictionary, jsonOptions);
            var updatedBytes = Encoding.UTF8.GetBytes(updatedBody);
            context.Request.Body = new MemoryStream(updatedBytes);
            context.Request.Body.Seek(0, SeekOrigin.Begin);
          }
        }
      }
    }

    // Continue to the next middleware
    await _next(context);
  }

}
