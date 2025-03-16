using bdDevCRM.Api.ContentFormatter;
using bdDevCRM.LoggerSevice;
using bdDevCRM.Repositories;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.ServiceContract.Authentication;
using bdDevCRM.Services;
using bdDevCRM.ServicesContract;
using bdDevCRM.Sql.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace bdDevCRM.Api.Extensions;

public static class ServiceExtensions
{
  public static void ConfigureCors(this IServiceCollection services) => services.AddCors(options =>
  {
    options.AddPolicy("CorsPolicy", builder =>
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
  });

  public static void Configureiisintegration(this IServiceCollection services) => services.Configure<IISOptions>(options =>
  {
    options.AutomaticAuthentication = false;
  });

  public static void ConfigureLoggerService(this IServiceCollection services)
  {
    services.AddSingleton<ILoggerManager, LoggerManager>();
  }

  public static void ConfigureRepositoryManager(this IServiceCollection services) => services.AddScoped<IRepositoryManager, RepositoryManager>();

  public static void ConfigureServiceManager(this IServiceCollection services) => services.AddScoped<IServiceManager, ServiceManager>();

  public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) => services.AddDbContext<CRMContext>(options => options.UseSqlServer(configuration.GetConnectionString("DbLocation")));

  public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder) => builder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputFormatter()));

  public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddAuthentication(options =>
    {
      options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
      options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]))
      };

      //options.Events = new JwtBearerEvents
      //{
      //  OnTokenValidated = async context =>
      //  {
      //    try
      //    {

      //      var token = context.SecurityToken as JwtSecurityToken;
      //      if (token == null)
      //      {
      //        context.Fail("Invalid token format.");
      //        return;
      //      }

      //      var rawToken = token.RawData;
      //      if (string.IsNullOrEmpty(rawToken))
      //      {
      //        context.Fail("Token data is missing.");
      //        return;
      //      }

      //      var tokenBlacklistService = context.HttpContext.RequestServices.GetRequiredService<ITokenBlacklistService>();

      //      if (await tokenBlacklistService.IsTokenBlacklisted(rawToken))
      //      {
      //        context.Fail("Token is blacklisted.");
      //      }
      //    }
      //    catch (Exception ex)
      //    {
      //      context.Fail($"An error occurred during token validation: {ex.Message}");
      //    }
      //  }
      //};
    });
  }

  public static void ConfigureAuthorization(this IServiceCollection services)
  {
    services.AddAuthorization();
  }

  public static void ConfigureResponseCompression(this IServiceCollection services)
  {
    services.AddResponseCompression(options =>
      {
        options.EnableForHttps = true;
        options.Providers.Add<GzipCompressionProvider>();
        options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
            new[] { "application/json" }
        );
      }
    );
  }

  public static void ConfigureGzipCompression(this IServiceCollection services)
  {
    services.Configure<GzipCompressionProviderOptions>(options =>
    {
      options.Level = System.IO.Compression.CompressionLevel.Optimal;
    });
  }
}