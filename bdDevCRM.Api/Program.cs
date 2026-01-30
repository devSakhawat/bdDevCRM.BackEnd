using bdDevCRM.Api.BackgroundServices;
using bdDevCRM.Api.ContentFormatter;
using bdDevCRM.Api.Extensions;
using bdDevCRM.Api.Middleware;
using bdDevCRM.Presentation;
using bdDevCRM.Presentation.ActionFIlters;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.AddHttpContextAccessor();
builder.Services.ConfigureCors(builder.Configuration);
builder.Services.Configureiisintegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureResponseCompression();
builder.Services.ConfigureGzipCompression();
builder.Services.ConfigureFileLimit();
builder.Services.ConfigureCookiePolicy();


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
  options.SuppressModelStateInvalidFilter = true;
});


builder.Services.AddScoped<LogActionAttribute>();
builder.Services.AddScoped<EmptyObjectFilterAttribute>();
builder.Services.AddScoped<ValidateMediaTypeAttribute>();
//builder.Services.AddScoped<IDataShaper<EmployeeDto>, DataShaper<EmployeeDto>>();


// Configure controllers with Newtonsoft.Json support
//builder.Services.AddControllers(config =>
//{
//  config.RespectBrowserAcceptHeader = true;
//  config.ReturnHttpNotAcceptable = true;
//  config.OutputFormatters.Add(new CsvOutputFormatter());
//})
//.AddXmlDataContractSerializerFormatters()
//.AddApplicationPart(typeof(PresentationReference).Assembly)
//.AddNewtonsoftJson(options =>
//{
//  options.SerializerSettings.ContractResolver = new DefaultContractResolver
//  {
//    NamingStrategy = new DefaultNamingStrategy() // Use PascalCase
//  };
//});


builder.Services.AddControllers(config =>
{
  config.RespectBrowserAcceptHeader = true;
  config.ReturnHttpNotAcceptable = true;
  // CSV formatter remove করুন
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
// .AddCustomCSVFormatter();

// Register MemoryCache
builder.Services.AddMemoryCache();


// Add NewtonsoftJsonPatchInputFormatter
builder.Services.AddMvcCore(options =>
{
  var jsonPatchInputFormatter = GetJsonPatchInputFormatter();
  options.InputFormatters.Add(jsonPatchInputFormatter);
});

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Authentication/Authorization
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureAuthorization();

// Add background service for token cleanup
builder.Services.AddHostedService<TokenCleanupBackgroundService>();

var app = builder.Build();

//app.UseExceptionHandler(opt => { });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

// Enable compression middleware (must be at the top of the pipeline)
app.UseResponseCompression();

//if (app.Environment.IsProduction())
//{
//  app.UseHsts();
//}

app.UseHttpsRedirection();

//app.UseStaticFiles(new StaticFileOptions
//{
//  FileProvider = new PhysicalFileProvider(
//        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads")),
//  RequestPath = "/Uploads"
//});

//app.UseStaticFiles(new StaticFileOptions
//{
//  FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads")),
//  RequestPath = "/Uploads",
//  ServeUnknownFileTypes = true, // important for .pdf
//  DefaultContentType = "application/octet-stream"
//});




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
    // CORS headers properly set
    ctx.Context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
    ctx.Context.Response.Headers.Append("Access-Control-Allow-Methods", "GET, OPTIONS");
    ctx.Context.Response.Headers.Append("Access-Control-Allow-Headers", "Content-Type");
    ctx.Context.Response.Headers.Append("Cache-Control", "public, max-age=3600");
  }
});


// Add these lines in this specific order
app.UseCookiePolicy(); // Add cookie policy before authentication
app.UseAuthentication(); // Must come before authorization
//app.UseMiddleware<TokenBlacklistMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();

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