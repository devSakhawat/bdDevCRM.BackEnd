using Serilog.Context;

namespace bdDevCRM.Api.Middleware
{
	public class CorrelationIdMiddleware
	{
		private readonly RequestDelegate _next;
		private const string CorrelationIdHeader = "X-Correlation-ID";

		public CorrelationIdMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			// Get or generate correlation ID
			var correlationId = context.Request.Headers[CorrelationIdHeader].FirstOrDefault()
				?? Guid.NewGuid().ToString();

			// Add to response headers
			context.Response.Headers.Append(CorrelationIdHeader, correlationId);

			// Add to Serilog log context
			using (LogContext.PushProperty("CorrelationId", correlationId))
			{
				await _next(context);
			}
		}
	}
}