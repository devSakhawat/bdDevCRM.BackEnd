using System.Diagnostics;

namespace bdDevCRM.Api.Middleware.Shared;

/// <summary>
/// For storing shared data across all middleware.
/// Will be stored in HttpContext.Items["PipelineContext"].
/// 
/// ✅ Conflict #2 resolved: Single Stopwatch
/// ✅ Conflict #3 resolved: Request body will be read only once
/// ✅ Conflict #4 resolved: Correlation ID will be resolved only once
/// </summary>
public class RequestPipelineContext
{
  // ── HttpContext.Items Key ──
  public const string ItemsKey = "PipelineContext";

  // ── Correlation ID (Set once, read by all) ──
  public string CorrelationId { get; set; } = default!;

  // ── Single Stopwatch (Started once, read by all) ──
  public Stopwatch Stopwatch { get; } = Stopwatch.StartNew();

  // ── Cached Request Body (Read once, used by all) ──
  /// <summary>
  /// null means not yet read.
  /// empty string means body was empty.
  /// </summary>
  public string? CachedRequestBody { get; set; }
  public bool IsRequestBodyCaptured { get; set; }

  // ── Request Start Time ──
  public DateTime RequestStartTimeUtc { get; } = DateTime.UtcNow;

  // ── Helper: Extract PipelineContext from Context ──
  public static RequestPipelineContext GetOrCreate(HttpContext context)
  {
    if (context.Items.TryGetValue(ItemsKey, out var existing) && existing is RequestPipelineContext ctx)
    {
      return ctx;
    }

    var newCtx = new RequestPipelineContext();
    context.Items[ItemsKey] = newCtx;
    return newCtx;
  }

  /// <summary>
  /// For read-only access — returns null if not exists
  /// </summary>
  public static RequestPipelineContext? Get(HttpContext context)
  {
    if (context.Items.TryGetValue(ItemsKey, out var existing) && existing is RequestPipelineContext ctx)
    {
      return ctx;
    }
    return null;
  }
}