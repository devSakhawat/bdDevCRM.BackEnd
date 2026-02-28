using bdDevCRM.Api.BackgroundServices;
using bdDevCRM.Api.ContentFormatter;
using bdDevCRM.Api.Extensions;
using bdDevCRM.Api.Middleware;
using bdDevCRM.Presentation;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Service.Caching;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Events;

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.File(
        path: "logs/app-.log",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
        retainedFileCountLimit: 30)
    .CreateLogger();

try
{
    Log.Information("Starting bdDevCRM Backend API");

    var builder = WebApplication.CreateBuilder(args);

    // Use Serilog
    builder.Host.UseSerilog();

    // Add services to the container
    builder.Services.AddHttpContextAccessor();
    builder.Services.ConfigureCors(builder.Configuration);
    builder.Services.Configureiisintegration();
    builder.Services.ConfigureLoggerService();
    builder.Services.ConfigureRepositoryManager();
    builder.Services.ConfigureServiceManager();
    builder.Services.ConfigureInterceptors();
    builder.Services.ConfigureSqlContext(builder.Configuration);
    builder.Services.ConfigureResponseCompression();
    builder.Services.ConfigureGzipCompression();
    builder.Services.ConfigureFileLimit();
    builder.Services.ConfigureCookiePolicy(builder.Environment);

    // NEW: Configure distributed cache (Hybrid Redis + Memory)
    builder.Services.ConfigureDistributedCache(builder.Configuration);
    builder.Services.AddSingleton<IHybridCacheService, HybridCacheService>();

    // NEW: Configure Application Insights
    builder.Services.ConfigureApplicationInsights(builder.Configuration);

    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

    builder.Services.AddScoped<LogActionAttribute>();
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

    var app = builder.Build();

    // Configure the HTTP request pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Exception handling middleware (must be first)
    // Use StandardExceptionMiddleware for consistent response format
    app.UseMiddleware<StandardExceptionMiddleware>();

    // NEW: Structured logging middleware
    app.UseMiddleware<StructuredLoggingMiddleware>();

    // NEW: Rate limit header middleware
    app.UseMiddleware<RateLimitHeaderMiddleware>();

    // NEW: Cache header middleware
    app.UseMiddleware<CacheHeaderMiddleware>();

    // NEW: Performance monitoring middleware
    app.UseMiddleware<PerformanceMonitoringMiddleware>();

    // NEW: Enhanced audit middleware
    if (builder.Configuration.GetValue<bool>("AuditLogging:EnableAuditMiddleware", true))
    {
        app.UseMiddleware<EnhancedAuditMiddleware>();
    }

    // Enable compression middleware
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

    // Session must be before authentication
    app.UseSession();

    // Authentication and Authorization
    app.UseCookiePolicy();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

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
