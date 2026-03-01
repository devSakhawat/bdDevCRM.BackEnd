using Serilog.Context;
using System.Diagnostics;

namespace bdDevCRM.Api.Middleware;

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
		// Prefer the incoming header, else try Activity, else generate new GUID
		var incoming = context.Request.Headers[CorrelationIdHeader].FirstOrDefault();
		var activityId = Activity.Current?.Id;
		var correlationId = incoming
							?? activityId
							?? context.TraceIdentifier
							?? Guid.NewGuid().ToString();

		// Normalize: ensure it's a string
		correlationId = correlationId.ToString();

		// Store for other middlewares to reuse
		context.Items["CorrelationId"] = correlationId;

		// Also set TraceIdentifier so that other libraries can use it
		context.TraceIdentifier = correlationId;

		// Optionally start or enrich Activity
		if (Activity.Current == null)
		{
			var activity = new Activity("http-request");
			activity.Start();
			Activity.Current = activity;
		}

		// Add to activity baggage for distributed tracing
		try
		{
			Activity.Current?.AddBaggage("CorrelationId", correlationId);
		}
		catch { /* swallow if Activity unsupported */ }

		// Add to response header
		context.Response.Headers[CorrelationIdHeader] = correlationId;

		// Add to Serilog log context (FromLogContext will pick it up)
		using (LogContext.PushProperty("CorrelationId", correlationId))
		{
			await _next(context);
		}
	}
}