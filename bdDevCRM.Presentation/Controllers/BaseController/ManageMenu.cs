using bdDevCRM.Entities.Entities.System;
using bdDevCRM.ServicesContract;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Shared.Exceptions.BaseException;
using bdDevCRM.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;

namespace bdDevCRM.Presentation.Controllers.BaseController;

public static class ManageMenu
{
  // Async + optional IServiceManager (DI or parameter)
  public static async Task<MenuDto> GetAsync(ControllerBase filterContext, IServiceManager? serviceManager = null)
  {
    try
    {
      if (filterContext is null)
        throw new GenericBadRequestException("Invalid controller context.");

      serviceManager ??= filterContext.HttpContext.RequestServices.GetRequiredService<IServiceManager>();

      var userIdClaim = filterContext.HttpContext.User.FindFirst("UserId")?.Value;
      if (string.IsNullOrWhiteSpace(userIdClaim))
        throw new GenericUnauthorizedException("User authentication required.");

      if (!int.TryParse(userIdClaim, out var userId) || userId <= 0)
        throw new GenericBadRequestException("Invalid user ID format.");

      var currentUser = serviceManager.GetCache<UsersDto>(userId);
      if (currentUser == null)
        throw new GenericUnauthorizedException("User session expired.");

      var request = filterContext.HttpContext.Request;
      var apiPath = $"{request.PathBase}{request.Path}{request.QueryString}";
      var fullURL = $"{request.Scheme}://{request.Host}{apiPath}";
      var rawUrl = ".." + apiPath;

      var menu = await serviceManager.Groups.CheckMenuPermission(rawUrl, currentUser);
      var menuMinimal = new MenuDto
      {
        MenuPath = rawUrl,
        StatusCode = 204,
        ReponseMessage = "No menu permission found"
      };

      return await Task.FromResult(menu ?? menuMinimal);
    }
    catch (GenericBadRequestException) { throw; }
    catch (GenericUnauthorizedException) { throw; }
    catch (Exception ex)
    {
      throw new BadRequestException(ex.Message);
    }
  }

  // Check menu permission by MenuName for current user (permission aware via path) path from menu name controller.
  public static async Task<MenuDto> CheckByMenuName( ControllerBase filterContext, string menuName, IServiceManager? serviceManager = null)
  {
    try
    {
      if (filterContext is null)
        throw new GenericBadRequestException("Invalid controller context.");

      serviceManager ??= filterContext.HttpContext.RequestServices.GetRequiredService<IServiceManager>();

      var userIdClaim = filterContext.HttpContext.User.FindFirst("UserId")?.Value;
      if (string.IsNullOrWhiteSpace(userIdClaim))
        throw new GenericUnauthorizedException("User authentication required.");

      if (!int.TryParse(userIdClaim, out var userId) || userId <= 0)
        throw new GenericBadRequestException("Invalid user ID format.");

      var currentUser = serviceManager.GetCache<UsersDto>(userId);
      if (currentUser == null)
        throw new GenericUnauthorizedException("User session expired.");

      if (!MenuConstant.TryGetPath(menuName, out var menuPath))
        throw new GenericBadRequestException("Invalid menu name.");

      var rawUrl = $"..{menuPath}"; // only if your Menu.MenuPath uses this pattern

      var menu = await serviceManager.Groups.CheckMenuPermission(rawUrl, currentUser);
      var menuMinimal = new MenuDto
      {
        MenuPath = rawUrl,
        StatusCode = 204,
        ReponseMessage = "No menu permission found"
      };

      return await Task.FromResult(menu ?? menuMinimal);
    }
    catch (GenericBadRequestException) { throw; }
    catch (GenericUnauthorizedException) { throw; }
    catch (Exception ex)
    {
      throw new BadRequestException(ex.Message);
    }
  }

  //   // Get MenuId by MenuName using database for menu
  public static async Task<MenuDto?> GetMenuByNameAsync(ControllerBase filterContext, string menuName, IServiceManager? serviceManager = null)
  {
    try
    {
      if (filterContext is null)
        throw new GenericBadRequestException("Invalid controller context.");
      if (string.IsNullOrWhiteSpace(menuName))
        throw new GenericBadRequestException("Menu name is required.");

      var userIdClaim = filterContext.HttpContext.User.FindFirst("UserId")?.Value;
      if (string.IsNullOrWhiteSpace(userIdClaim))
        throw new GenericUnauthorizedException("User authentication required.");

      if (!int.TryParse(userIdClaim, out var userId) || userId <= 0)
        throw new GenericBadRequestException("Invalid user ID format.");

      var currentUser = serviceManager.GetCache<UsersDto>(userId);
      if (currentUser == null)
        throw new GenericUnauthorizedException("User session expired.");

      var menus = await serviceManager.Menus.GetMenusByMenuName(menuName, false);
      var menu = menus.FirstOrDefault(m => string.Equals(m.MenuName.Trim().ToLower(), menuName.Trim().ToLower(), StringComparison.OrdinalIgnoreCase));
      return menu;
    }
    catch (GenericBadRequestException) { throw; }
    catch (Exception ex)
    {
      throw new BadRequestException(ex.Message);
    }
  }

  // Get MenuId by MenuName for current user (permission aware via path)
  public static async Task<int?> GetMenuIdByNameForCurrentUserAsync(ControllerBase filterContext, string menuName, IServiceManager? serviceManager = null)
  {
    try
    {
      if (filterContext is null)
        throw new GenericBadRequestException("Invalid controller context.");
      if (string.IsNullOrWhiteSpace(menuName))
        throw new GenericBadRequestException("Menu name is required.");

      serviceManager ??= filterContext.HttpContext.RequestServices.GetRequiredService<IServiceManager>();

      var userIdClaim = filterContext.HttpContext.User.FindFirst("UserId")?.Value;
      if (string.IsNullOrWhiteSpace(userIdClaim))
        throw new GenericUnauthorizedException("User authentication required.");
      if (!int.TryParse(userIdClaim, out var userId) || userId <= 0)
        throw new GenericBadRequestException("Invalid user ID format.");

      var currentUser = serviceManager.GetCache<UsersDto>(userId);
      if (currentUser == null)
        throw new GenericUnauthorizedException("User session expired.");

      if (!MenuConstant.TryGetPath(menuName, out var menuPath))
        throw new GenericBadRequestException("Invalid menu name.");

      var rawUrl = $"..{menuPath}";
      var menuDto = await serviceManager.Groups.CheckMenuPermission(rawUrl, currentUser);
      return menuDto?.MenuId;
    }
    catch (GenericBadRequestException) { throw; }
    catch (GenericUnauthorizedException) { throw; }
    catch (Exception ex)
    {
      throw new BadRequestException(ex.Message);
    }
  }
}




