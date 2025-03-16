using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.KendoGrid;

namespace bdDevCRM.ServicesContract.Core.SystemAdmin;

public interface IMenuService
{
  Task<IEnumerable<MenuDto>> SelectAllMenuByModuleId(int moduleId, bool trackChanges);
  Task<IEnumerable<MenuDto>> SelectMenuByUserPermission(int userId, bool trackChanges);
  Task<List<MenuDto>> GetParentMenuByMenu(int parentMenuId, bool trackChanges);

  GridEntity<MenuDto> MenuSummary(bool trackChanges, GridOptions options);
  MenuDto GetMenu(int MenuId, bool trackChanges);
  MenuDto CreateMenu(MenuDto Menu);
  IEnumerable<MenuDto> GetByIds(IEnumerable<int> ids, bool trackChanges);

  Task<IEnumerable<MenuDto>> GetMenusAsync(bool trackChanges);
  Task<MenuDto> GetMenuAsync(int MenuId, bool trackChanges);
  Task<IEnumerable<MenuDto>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
  Task<(IEnumerable<MenuDto> Menus, string ids)> CreateMenuCollectionAsync(IEnumerable<MenuDto> MenuCollection);
  Task<MenuDto> CreateMenuAsync(MenuDto entityForCreate);
  Task DeleteMenuAsync(int MenuId, bool trackChanges);
  Task UpdateMenuAsync(int MenuId, MenuDto MenuForUpdate, bool trackChanges);
}
