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

// ============================================================
// P0: Load .env environment variables
// ============================================================
// .env File : ConnectionStrings__DbLocation=...
// ASP.NET Core: read as ConnectionStrings:DbLocation
// __ (double underscore) = : (colon) in configuration hierarchy
var envFilePath = Path.Combine(Directory.GetCurrentDirectory(), "..", ".env");
if (File.Exists(envFilePath))
{
	foreach (var line in File.ReadAllLines(envFilePath))
	{
		// Skip empty lines and comments
		if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith('#'))
			continue;

		var separatorIndex = line.IndexOf('=');
		if (separatorIndex <= 0) continue;

		var key = line[..separatorIndex].Trim();
		var value = line[(separatorIndex + 1)..].Trim();

		// Production এ OS environment variable will get priority 
		if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable(key)))
		{
			Environment.SetEnvironmentVariable(key, value);
		}
	}
}

// Environment variables configuration
builder.Configuration.AddEnvironmentVariables();

// ============================================================
// SERVICE REGISTRATION
// ============================================================





// Add services to the container
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureCors(builder.Configuration);
builder.Services.Configureiisintegration();
// Serilog
builder.Services.ConfigureSerilog(builder.Configuration, builder.Environment);
builder.Host.UseSerilog();

// Repository & Service
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureInterceptors();
builder.Services.ConfigureSqlContext(builder.Configuration);

// Compression
builder.Services.ConfigureResponseCompression();
builder.Services.ConfigureGzipCompression();

// File & Cookie
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

// API Behavior
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
	options.SuppressModelStateInvalidFilter = true;
});

// action attribute	
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
// NewtonsoftJson Patch
builder.Services.AddMvcCore(options =>
{
  var jsonPatchInputFormatter = GetJsonPatchInputFormatter();
  options.InputFormatters.Add(jsonPatchInputFormatter);
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureAddSwaggerGen();

// Authentication & Authorization
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureAuthorization();

// Background Services
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




// ============================================================
// MIDDLEWARE PIPELINE
// ============================================================



var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}


// 1️⃣ Exception handler — MUST be outermost
app.UseMiddleware<StandardExceptionMiddleware>();

// 2️⃣ Correlation ID + PipelineContext creation + Stopwatch start
app.UseMiddleware<CorrelationIdMiddleware>();

// 3️⃣ Performance monitoring (reads shared stopwatch)
app.UseMiddleware<PerformanceMonitoringMiddleware>();

// 4️⃣ Structured logging (reads shared body + stopwatch + Controller/Action name)
app.UseMiddleware<StructuredLoggingMiddleware>();

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

// Proper startup with shutdown handling
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
