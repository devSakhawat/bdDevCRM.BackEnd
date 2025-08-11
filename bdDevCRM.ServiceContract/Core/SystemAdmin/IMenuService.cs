using bdDevCRM.Entities.CRMGrid;
using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;

namespace bdDevCRM.ServicesContract.Core.SystemAdmin;

public interface IMenuService
{
  Task<IEnumerable<MenuDto>> SelectAllMenuByModuleId(int moduleId, bool trackChanges);
  Task<IEnumerable<MenuDto>> SelectMenuByUserPermission(int userId, bool trackChanges);
  Task<List<MenuDto>> GetParentMenuByMenu(int parentMenuId, bool trackChanges);

  //Task<GridEntity<MenuDto>> MenuSummary(bool trackChanges, CRMGridOptions options);
  Task<GridEntity<MenuDto>> MenuSummary(bool trackChanges, CRMGridOptions options);



  //object MenuSummary(bool trackChanges, GridOptions options);
  MenuDto GetMenu(int MenuId, bool trackChanges);
  MenuDto CreateMenu(MenuDto Menu);
  IEnumerable<MenuDto> GetByIds(IEnumerable<int> ids, bool trackChanges);

  Task<IEnumerable<MenuDto>> GetMenusAsync(bool trackChanges);
  Task<MenuDto> GetMenuAsync(int MenuId, bool trackChanges);
  Task<IEnumerable<MenuDto>> GetByIdsAsync(IEnumerable<int> ids, bool trackChanges);
  Task<(IEnumerable<MenuDto> Menus, string ids)> CreateMenuCollectionAsync(IEnumerable<MenuDto> MenuCollection);
  Task<IEnumerable<MenuDto>> MenusByModuleId(int moduleId, bool trackChanges);


  Task<MenuDto> CreateMenuAsync(MenuDto entityForCreate);
  Task<MenuDto> CreateAsync(MenuDto modelDto);
  Task<MenuDto> UpdateAsync(int key, MenuDto modelDto);
  Task DeleteAsync(int key, MenuDto modelDto);

  Task DeleteMenuAsync(int MenuId, bool trackChanges);
  Task UpdateMenuAsync(int MenuId, MenuDto MenuForUpdate, bool trackChanges);



  Task<IEnumerable<MenuForDDLDto>> MenuForDDL();
}
