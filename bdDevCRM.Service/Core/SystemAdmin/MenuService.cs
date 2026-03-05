using bdDevCRM.Utilities.CRMGrid.GRID;
using bdDevCRM.Entities.Entities.System;

using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.Exceptions;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace bdDevCRM.Services.Core.SystemAdmin;


/// <summary>
/// Menu service implementing business logic for menu management.
/// Follows enterprise patterns with structured logging and exception handling.
/// </summary>
internal sealed class MenuService : IMenuService
{
    private readonly IRepositoryManager _repository;
    private readonly ILogger<MenuService> _logger;
    private readonly IConfiguration _configuration;

    public MenuService(IRepositoryManager repository, ILogger<MenuService> logger, IConfiguration configuration)
    {
        _repository = repository;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<IEnumerable<MenuDto>> SelectAllMenuByModuleId(int moduleId, bool trackChanges)
    {
        var menuRepositoryDtos = await _repository.Menus.SelectAllMenuByModuleId(moduleId, trackChanges);
        if (menuRepositoryDtos.Count() == 0) throw new GenericListNotFoundException("Menu");
        var menuDtos = MyMapper.JsonCloneIEnumerableToList<MenuRepositoryDto, MenuDto>(menuRepositoryDtos);
        return menuDtos;
    }

    public async Task<IEnumerable<MenuDto>> SelectMenuByUserPermission(int userid, bool trackChanges)
    {
        if (userid <= 0)
        {
            _logger.LogWarning("SelectMenuByUserPermission called with invalid userId: {UserId}", userid);
            return Enumerable.Empty<MenuDto>();
        }

        _logger.LogInformation("Fetching menu permissions for user {UserId}", userid);

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var menuRepositoryDtos = await _repository.Menus.SelectMenuByUserPermission(userid, trackChanges);
        stopwatch.Stop();

        _logger.LogInformation("Menu query execution time: {ElapsedMilliseconds}ms for user {UserId}", stopwatch.ElapsedMilliseconds, userid);

        if (!menuRepositoryDtos.Any())
        {
            _logger.LogWarning("No menu permissions found for userId: {UserId}", userid);
            return Enumerable.Empty<MenuDto>();
        }

        var menusDto = MyMapper.JsonCloneIEnumerableToList<MenuRepositoryDto, MenuDto>(menuRepositoryDtos);
        return menusDto;
    }

    public async Task<List<MenuDto>> GetParentMenuByMenu(int userid, bool trackChanges)
    {
        var menuRepositoryDtos = await _repository.Menus.GetParentMenuByMenu(userid, trackChanges);

        //if(menuRepositoryDtos.Count() == 0) throw new GenericListNotFoundException("Menu");
        var menusDto = MyMapper.JsonCloneIEnumerableToList<MenuRepositoryDto, MenuDto>(menuRepositoryDtos);
        return menusDto;
    }

    /// <summary>
    /// Menu crud
    /// </summary>
    /// <param name="trackChanges"></param>
    /// <param name="options"></param>
    /// <returns></returns>

    public async Task<GridEntity<MenuDto>> MenuSummary(bool trackChanges, CRMGridOptions options)
    {
        string menuSummaryQuery = $"Select MenuId,Menu.ModuleId, MenuName, MenuPath, ISNULL(ParentMenu, 0) as ParentMenu ,ModuleName,ToDo,SORORDER\r\n,(Select MenuName from Menu mn where mn.MenuId = menu.ParentMenu) as ParentMenuName ,IsActive \r\nfrom Menu \r\nleft outer join Module on module.ModuleId = menu.ModuleId";
        string orderBy = "ModuleName asc,ParentMenu asc, MenuName";

        var gridEntity = await _repository.Menus.GridData<MenuDto>(menuSummaryQuery, options, orderBy, "");

        return gridEntity;
    }

    public MenuDto GetMenu(int id, bool trackChanges)
    {
        var Menu = _repository.Menus.GetMenu(id, trackChanges);
        //Check if the Menu is null
        if (Menu == null) throw new GenericNotFoundException("Menu", "MenuId", id.ToString());

        var MenuDto = new MenuDto();
        MenuDto = MyMapper.JsonClone<Menu, MenuDto>(Menu);
        return MenuDto;
    }

    public MenuDto CreateMenu(MenuDto Menu)
    {
        Menu MenuEntity = MyMapper.JsonClone<MenuDto, Menu>(Menu);
        _repository.Menus.CreateMenu(MenuEntity);
        _repository.Save();

        var MenuToReturn = MyMapper.JsonClone<Menu, MenuDto>(MenuEntity);
        return MenuToReturn;
    }

    public IEnumerable<MenuDto> GetByIds(IEnumerable<int> ids, bool trackChanges)
    {
        if (ids is null)
            throw new IdParametersBadRequestException();
        var MenuEntities = _repository.Menus.GetByIds(ids, trackChanges);
        if (ids.Count() != MenuEntities.Count())
            throw new CollectionByIdsBadRequestException("Menus");
        var MenusToReturn = MyMapper.JsonCloneIEnumerableToList<Menu, MenuDto>(MenuEntities);
        return MenusToReturn;
    }

    public async Task<MenuDto> CreateMenuAsync(MenuDto entityForCreate)
    {
        Menu Menu = MyMapper.JsonClone<MenuDto, Menu>(entityForCreate);
        _repository.Menus.CreateMenu(Menu);
        await _repository.SaveAsync();
        return entityForCreate;
    }

    public async Task<MenuDto> CreateAsync(MenuDto modelDto)
    {
        if (modelDto == null)
            throw new NullModelBadRequestException(nameof(MenuDto));

        _logger.LogInformation("Creating new menu: {MenuName}", modelDto.MenuName);

        // Check for duplicate menu name
        bool menuExists = await _repository.Menus.ExistsAsync(
            m => m.MenuName.Trim().ToLower() == modelDto.MenuName.Trim().ToLower());

        if (menuExists)
            throw new DuplicateRecordException("Menu", "MenuName");

        // Map and create
        Menu entity = MyMapper.JsonClone<MenuDto, Menu>(modelDto);
        modelDto.MenuId = await _repository.Menus.CreateAndGetIdAsync(entity);
        await _repository.SaveAsync();

        _logger.LogInformation("Menu created successfully with ID: {MenuId}", modelDto.MenuId);

        return modelDto;
    }

    public async Task UpdateMenuAsync(int MenuId, MenuDto MenuForUpdate, bool trackChanges)
    {
        Expression<Func<Menu, bool>> expression = e => e.MenuId == MenuId;
        bool exists = await _repository.Menus.ExistsAsync(expression);
        if (!exists) throw new GenericNotFoundException("Menu", "MenuId", MenuId.ToString());

        Menu Menu = MyMapper.JsonClone<MenuDto, Menu>(MenuForUpdate);
        _repository.Menus.UpdateMenu(Menu);
        await _repository.SaveAsync();
    }

    public async Task DeleteMenuAsync(int menuId, bool trackChanges)
    {
        var Menu = await _repository.Menus.FirstOrDefaultAsync(x => x.MenuId.Equals(menuId));
        _logger.LogWarning("Menu with Id: {MenuId} is about to be deleted from the database.", menuId);
        if (Menu != null) _repository.Menus.DeleteMenu(Menu);
        await _repository.SaveAsync();
    }

    public async Task<IEnumerable<MenuDto>> GetMenusAsync(bool trackChanges)
    {
        IEnumerable<Menu> Menus = await _repository.Menus.GetMenusAsync(trackChanges);
        List<MenuDto> MenuRepositoryDtos = MyMapper.JsonCloneIEnumerableToList<Menu, MenuDto>(Menus);
        return MenuRepositoryDtos;
    }

    public async Task<MenuDto> GetMenuAsync(int MenuId, bool trackChanges)
    {
        if (MenuId <= 0) throw new ArgumentOutOfRangeException(nameof(MenuId), "Menu ID must be be zero or non-negative integer.");

        Menu Menu = await _repository.Menus.GetMenuAsync(MenuId, trackChanges);
        if (Menu == null) throw new GenericNotFoundException("Menu", "MenuId", MenuId.ToString());

        MenuDto MenuDto = MyMapper.JsonClone<Menu, MenuDto>(Menu);
        return MenuDto;
    }

    public Task<IEnumerable<MenuDto>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges)
    {
        throw new NotImplementedException();
    }

    public Task<(IEnumerable<MenuDto> Menus, string ids)> CreateMenuCollectionAsync(IEnumerable<MenuDto> MenuCollection)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<MenuDto>> MenusByModuleId(int moduleId, bool trackChanges)
    {
        if (moduleId < 0) throw new ArgumentOutOfRangeException(nameof(moduleId), "Module ID must be a positive integer.");

        IEnumerable<Menu> menus = await _repository.Menus.MenusByModuleId(moduleId, trackChanges);
        //if (menus.Count() = 0) throw new GenericNotFoundException("Menu", "MenuId", moduleId.ToString());
        if (menus.Count() == 0) return new List<MenuDto>();

        List<MenuDto> menusDto = MyMapper.JsonCloneIEnumerableToList<Menu, MenuDto>(menus);
        return menusDto;
    }

    public async Task<IEnumerable<MenuDto>> GetMenusByMenuName(string menuName, bool trackChanges = false)
    {
        if (string.IsNullOrWhiteSpace(menuName)) throw new GenericBadRequestException(menuName);

        IEnumerable<Menu> menus = await _repository.Menus.ListByConditionAsync(expression: x => x.MenuName.Trim().ToLower() == menuName.Trim().ToLower(), orderBy: x => x.MenuId, trackChanges: false);
        if (menus.Count() == 0) return new List<MenuDto>();

        List<MenuDto> menusDto = MyMapper.JsonCloneIEnumerableToList<Menu, MenuDto>(menus);
        return menusDto;
    }



    public async Task<MenuDto> UpdateAsync(int key, MenuDto modelDto)
    {
        if (modelDto == null)
            throw new NullModelBadRequestException(nameof(MenuDto));

        if (key != modelDto.MenuId)
            throw new IdMismatchBadRequestException(key.ToString(), nameof(MenuDto));

        _logger.LogInformation("Updating menu with ID: {MenuId}", key);

        // Check if menu exists
        Menu entity = await _repository.Menus.GetByIdAsync(m => m.MenuId == key, trackChanges: false);

        if (entity == null)
            throw new GenericNotFoundException("Menu", "MenuId", key.ToString());

        // Map and update
        entity = MyMapper.JsonClone<MenuDto, Menu>(modelDto);
        _repository.Menus.UpdateByState(entity);
        await _repository.SaveAsync();

        _logger.LogInformation("Menu updated successfully: {MenuId}", key);

        return MyMapper.JsonClone<Menu, MenuDto>(entity);
    }

    public async Task DeleteAsync(int key)
    {
        if (key <= 0)
            throw new IdParametersBadRequestException();

        _logger.LogInformation("Deleting menu with ID: {MenuId}", key);

        Menu entity = await _repository.Menus.GetByIdAsync(m => m.MenuId == key, trackChanges: false);

        if (entity == null)
            throw new GenericNotFoundException("Menu", "MenuId", key.ToString());

        // Physical delete
        await _repository.Menus.DeleteAsync(x => x.MenuId == key, trackChanges: false);
        await _repository.SaveAsync();

        _logger.LogInformation("Menu deleted successfully: {MenuId}", key);
    }

    public async Task<IEnumerable<MenuForDDLDto>> MenuForDDL()
    {
        // Corrected the initialization of the Menu object to use a constructor instead of a collection initializer.
        IEnumerable<Menu> menus = await _repository.Menus.ListWithSelectAsync(
            x => new Menu
            {
                MenuId = x.MenuId,
                MenuName = x.MenuName
            },
            orderBy: x => x.Sororder,
            trackChanges: false
        );

        if (menus.Count() == 0) return new List<MenuForDDLDto>();

        IEnumerable<MenuForDDLDto> menusForDDLDto = MyMapper.JsonCloneIEnumerableToList<Menu, MenuForDDLDto>(menus);
        return menusForDDLDto;
    }

}
