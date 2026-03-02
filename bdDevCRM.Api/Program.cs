using bdDevCRM.Api.BackgroundServices;
using bdDevCRM.Api.ContentFormatter;
using bdDevCRM.Api.Extensions;
using bdDevCRM.Api.Middleware;
using bdDevCRM.Presentation;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Service.Caching;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureCors(builder.Configuration);
builder.Services.Configureiisintegration();
//builder.Services.ConfigureLoggerService();
// Use Serilog
builder.Services.ConfigureSerilog(builder.Configuration, builder.Environment);
builder.Host.UseSerilog();

builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureInterceptors();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureResponseCompression();
builder.Services.ConfigureGzipCompression();
builder.Services.ConfigureFileLimit();
builder.Services.ConfigureCookiePolicy(builder.Environment);

// Configure distributed cache (Hybrid Redis + Memory)
builder.Services.ConfigureDistributedCache(builder.Configuration);
builder.Services.AddSingleton<IHybridCacheService, HybridCacheService>();
// Configure Application Insights
builder.Services.ConfigureApplicationInsights(builder.Configuration);

// Audit Log Queue + Background Writer
builder.Services.AddSingleton<AuditLogQueue>();
builder.Services.AddHostedService<AuditLogWriterService>();


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
  options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddScoped<EmptyObjectFilterAttribute>();
builder.Services.AddScoped<ValidateMediaTypeAttribute>();

builder.Services.AddControllers(config =>
{
  config.RespectBrowserAcceptHeader = true;
  config.ReturnHttpNotAcceptable = true;
  // Add custom CSV formatter for content negotiation (already exists in ContentFormatter)
  config.OutputFormatters.Add(new CsvOutputFormatter());
})
.AddXmlDataContractSerializerFormatters()
.AddApplicationPart(typeof(PresentationReference).Assembly)
.AddNewtonsoftJson(options =>
{
  options.SerializerSettings.ContractResolver = new DefaultContractResolver
  {
    NamingStrategy = new DefaultNamingStrategy()
  };
});

// Register MemoryCache
builder.Services.AddMemoryCache();

// Add NewtonsoftJsonPatchInputFormatter
//builder.Services.AddMvcCore(options =>
//{
//  options.InputFormatters.Add(GetJsonPatchInputFormatter());
//});
builder.Services.AddMvcCore(options =>
{
  var jsonPatchInputFormatter = GetJsonPatchInputFormatter();
  options.InputFormatters.Add(jsonPatchInputFormatter);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureAddSwaggerGen();

// Add Authentication/Authorization
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureAuthorization();

// Add background service for token cleanup
builder.Services.AddHostedService<TokenCleanupBackgroundService>();

// Add session support for audit middleware
builder.Services.AddSession(options =>
{
  options.IdleTimeout = TimeSpan.FromMinutes(30);
  options.Cookie.HttpOnly = true;
  options.Cookie.IsEssential = true;
});


//// Services
//builder.Services.AddHealthChecks()
//    .AddSqlServer( builder.Configuration.GetConnectionString("DbLocation")!, name: "database", timeout: TimeSpan.FromSeconds(5))
//    .AddCheck("audit-queue", () =>
//    {
//      var queue = app.Services.GetRequiredService<AuditLogQueue>();
//      return queue.Count < 9000
//          ? Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Healthy($"Queue: {queue.Count}")
//          : Microsoft.Extensions.Diagnostics.HealthChecks.HealthCheckResult.Degraded($"Queue nearly full: {queue.Count}");
//    });







var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

// 1 Exception handler — MUST be outermost
app.UseMiddleware<StandardExceptionMiddleware>();

// 2️ Correlation ID + PipelineContext creation + Stopwatch start
app.UseMiddleware<CorrelationIdMiddleware>();

// 3️ Performance monitoring (reads shared stopwatch)
app.UseMiddleware<PerformanceMonitoringMiddleware>();

// 4️ Structured logging (reads shared body + stopwatch)
app.UseMiddleware<StructuredLoggingMiddleware>();

// NEW: Enhanced audit middleware
if (builder.Configuration.GetValue<bool>("AuditLogging:EnableAuditMiddleware", true))
{
  app.UseMiddleware<EnhancedAuditMiddleware>();
}

// Enable compression middleware
// Infrastructure
app.UseResponseCompression();
app.UseHttpsRedirection();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
  ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors("CorsPolicy");

app.UseStaticFiles(new StaticFileOptions
{
  FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads")),
  RequestPath = "/Uploads",
  OnPrepareResponse = ctx =>
  {
    ctx.Context.Response.Headers.Append("Cache-Control", "public, max-age=3600");
  }
});

// Auth
// Session must be before authentication
app.UseSession();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

// 5️⃣ Audit (AFTER auth — needs context.User)
if (builder.Configuration.GetValue<bool>("AuditLogging:EnableAuditMiddleware", true))
{
  app.UseMiddleware<EnhancedAuditMiddleware>();
}

app.MapControllers();

// Startup with proper shutdown
try
{
  Log.Information("bdDevCRM Backend API started successfully");
  app.Run();
}
catch (Exception ex)
{
  Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
  Log.CloseAndFlush();
}


// Helper method to get NewtonsoftJsonPatchInputFormatter
NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() =>
    new ServiceCollection()
        .AddLogging()
        .AddMvc()
        .AddNewtonsoftJson()
        .Services.BuildServiceProvider()
        .GetRequiredService<IOptions<MvcOptions>>()
        .Value.InputFormatters
        .OfType<NewtonsoftJsonPatchInputFormatter>()
        .First();
