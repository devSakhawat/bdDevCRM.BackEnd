using bdDevCRM.Api;
using bdDevCRM.Api.ContentFormatter;
using bdDevCRM.Api.Extensions;
using bdDevCRM.Api.Middleware;
using bdDevCRM.Presentation;
using bdDevCRM.Presentation.ActionFIlters;
using bdDevCRM.Service.Authentication;
using bdDevCRM.ServiceContract.Authentication;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.ConfigureCors();
builder.Services.Configureiisintegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureResponseCompression();
builder.Services.ConfigureGzipCompression();


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
  options.SuppressModelStateInvalidFilter = true;
});


builder.Services.AddScoped<LogActionAttribute>();
builder.Services.AddScoped<EmptyObjectFilterAttribute>();
builder.Services.AddScoped<ValidateMediaTypeAttribute>();
//builder.Services.AddScoped<IDataShaper<EmployeeDto>, DataShaper<EmployeeDto>>();


// Configure controllers with Newtonsoft.Json support
builder.Services.AddControllers(config =>
{
  config.RespectBrowserAcceptHeader = true;
  config.ReturnHttpNotAcceptable = true;
  config.OutputFormatters.Add(new CsvOutputFormatter());

  // Add global validation filter for all controllers
  // to check if the Accept header is present with the required media type like "application/json" or "application/xml" or "text/csv" or "text/plain"
  //config.Filters.Add<ValidationFilterAttribute>();
})
.AddXmlDataContractSerializerFormatters()
.AddApplicationPart(typeof(PresentationReference).Assembly)
.AddNewtonsoftJson(options =>
{
  options.SerializerSettings.ContractResolver = new DefaultContractResolver
  {
    NamingStrategy = new DefaultNamingStrategy() // Use PascalCase
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

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Authentication/Authorization
builder.Services.ConfigureAuthentication(builder.Configuration);
builder.Services.ConfigureAuthorization();

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
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
  ForwardedHeaders = ForwardedHeaders.All
});
app.UseCors("CorsPolicy");

// Add these lines in this specific order
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