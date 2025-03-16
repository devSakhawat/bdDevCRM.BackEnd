using bdDevCRM.ServiceContract.Authentication;

namespace bdDevCRM.Api.Extensions;

public class TokenBlacklistMiddleware
{
  private readonly RequestDelegate _next;
  private readonly ITokenBlacklistService _tokenBlacklistService;

  public TokenBlacklistMiddleware(RequestDelegate next, ITokenBlacklistService tokenBlacklistService)
  {
    _next = next;
    _tokenBlacklistService = tokenBlacklistService;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    if (context.Request.Headers.TryGetValue("Authorization", out var authHeader))
    {
      var token = authHeader.ToString().Replace("Bearer ", "");
      if (!string.IsNullOrEmpty(token))
      {
        try
        {
          if (await _tokenBlacklistService.IsTokenBlacklisted(token))
          {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Token is blacklisted.");
            return;
          }
        }
        catch (Exception ex)
        {
          context.Response.StatusCode = StatusCodes.Status500InternalServerError;
          await context.Response.WriteAsync("An error occurred while validating the token.");
          return;
        }
      }
    }

    await _next(context);
  }
}
