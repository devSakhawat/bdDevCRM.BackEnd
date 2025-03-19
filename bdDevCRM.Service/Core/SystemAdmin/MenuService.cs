using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Exceptions;
using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.ServicesContract.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.KendoGrid;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace bdDevCRM.Services.Core.SystemAdmin;


internal sealed class MenuService : IMenuService
{
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;
  private readonly IConfiguration _configuration;

  public MenuService(IRepositoryManager repository, ILoggerManager logger, IConfiguration configuration)
  {
    _repository = repository;
    _logger = logger;
    _configuration = configuration;
  }

  public async Task<IEnumerable<MenuDto>> SelectAllMenuByModuleId(int moduleId, bool trackChanges)
  {
    var menuRepositoryDtos = await _repository.Menus.SelectAllMenuByModuleId(moduleId, trackChanges);
    if(menuRepositoryDtos.Count() == 0) throw new GenericListNotFoundException("Menu");
    var menuDtos = MyMapper.JsonCloneIEnumerableToList<MenuRepositoryDto, MenuDto>(menuRepositoryDtos);
    return menuDtos;
  }

  public async Task<IEnumerable<MenuDto>> SelectMenuByUserPermission(int userid, bool trackChanges)
  {
    var stopwatch = System.Diagnostics.Stopwatch.StartNew();
    var menuRepositoryDtos = await _repository.Menus.SelectMenuByUserPermission(userid, trackChanges);
    stopwatch.Stop();

    _logger.LogInfo($"Menu query execution time: {stopwatch.ElapsedMilliseconds}ms for user {userid}");

    if (!menuRepositoryDtos.Any()) throw new GenericListNotFoundException("Menu");

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

  public async Task<GridEntity<MenuDto>> MenuSummary(bool trackChanges, GridOptions options)
  {
    var menus = await _repository.Menus.MenuSummary(trackChanges);
    var menusDto = MyMapper.JsonCloneIEnumerableToList<MenuRepositoryDto, MenuDto>(menus);
    //var modules = _repository.Modules.GetAllModules(trackChanges).ToList();
    var gridData = GridResult<MenuDto>.Data(menusDto, options);
    return gridData;
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

  public async Task UpdateMenuAsync(int MenuId, MenuDto MenuForUpdate, bool trackChanges)
  {
    Expression<Func<Menu, bool>> expression = e => e.MenuId == MenuId;
    bool exists = await _repository.Menus.ExistsAsync(expression);
    if (!exists) throw new GenericNotFoundException("Menu", "MenuId", MenuId.ToString());

    Menu Menu = MyMapper.JsonClone<MenuDto, Menu>(MenuForUpdate);
    _repository.Menus.UpdateMenu(Menu);
    await _repository.SaveAsync();
  }

  public async Task DeleteMenuAsync(int MenuId, bool trackChanges)
  {
    var Menu = await _repository.Menus.GetByIdWithNotFoundException(MenuId);
    _logger.LogWarn($"Menu with Id: {MenuId} is about to be deleted from the database.");
    _repository.Menus.DeleteMenu(Menu);
    await _repository.SaveAsync();

    // i think the method should return int value.
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

}
