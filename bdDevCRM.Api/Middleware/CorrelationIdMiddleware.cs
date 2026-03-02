using bdDevCRM.Api.Middleware.Shared;
using Serilog.Context;
using System.Diagnostics;

namespace bdDevCRM.Api.Middleware;

/// <summary>
/// Sets the Correlation ID and initializes PipelineContext.
/// 
/// ✅ Conflict #4 resolved: Resolve once here — everyone else reads from here
/// ✅ Conflict #2 resolved: PipelineContext.Stopwatch starts here
/// </summary>
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
    // ✅ Create PipelineContext (Stopwatch starts here)
    var pipelineCtx = RequestPipelineContext.GetOrCreate(context);

    // Resolve Correlation ID (priority order)
    var incoming = context.Request.Headers[CorrelationIdHeader].FirstOrDefault();
    var activityId = Activity.Current?.Id;
    var correlationId = incoming
                        ?? activityId
                        ?? context.TraceIdentifier
                        ?? Guid.NewGuid().ToString();

    // ✅ Set once — everyone reads from here
    pipelineCtx.CorrelationId = correlationId;

    // Backward compatibility: Keep old context.Items key as well
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

    // Add correlation ID to response header
    context.Response.OnStarting(() =>
    {
      context.Response.Headers[CorrelationIdHeader] = correlationId;
      return Task.CompletedTask;
    });

    // Push to Serilog LogContext
    using (LogContext.PushProperty("CorrelationId", correlationId))
    {
      await _next(context);
    }
  }
}