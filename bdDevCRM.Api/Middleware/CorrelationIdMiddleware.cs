using bdDevCRM.Api.Middleware.Shared;
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
		// make PipelineContext (Stopwatch starts here)
		var pipelineCtx = RequestPipelineContext.GetOrCreate(context);

		// Correlation ID resolve (priority order)
		var incoming = context.Request.Headers[CorrelationIdHeader].FirstOrDefault();
		var activityId = Activity.Current?.Id;
		var correlationId = incoming
							?? activityId
							?? context.TraceIdentifier
							?? Guid.NewGuid().ToString();

		// set Shared context everywhere (HTTP, custom, backward compatibility)
		pipelineCtx.CorrelationId = correlationId;

		// Backward compatibility
		context.Items["CorrelationId"] = correlationId;
		context.TraceIdentifier = correlationId;

		// Activity enrichment
		if (Activity.Current == null)
		{
			var activity = new Activity("http-request");
			activity.Start();
			Activity.Current = activity;
		}

		try
		{
			Activity.Current?.AddBaggage("CorrelationId", correlationId);
		}
		catch { /* swallow if Activity unsupported */ }

		// OnStarting response header set (safe)
		context.Response.OnStarting(() =>
		{
			context.Response.Headers[CorrelationIdHeader] = correlationId;
			return Task.CompletedTask;
		});

		// Push Serilog LogContext
		using (LogContext.PushProperty("CorrelationId", correlationId))
		{
			await _next(context);
		}
	}
}