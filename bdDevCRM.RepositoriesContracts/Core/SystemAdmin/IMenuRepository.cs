using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

public interface IMenuRepository : IRepositoryBase<Menu>
{
  Task<IEnumerable<MenuRepositoryDto>> SelectAllMenuByModuleId(int moduleId ,bool trackChanges);
  Task<IEnumerable<MenuRepositoryDto>> SelectMenuByUserPermission(int userId, bool trackChanges);
  Task<List<MenuRepositoryDto>> GetParentMenuByMenu(int parentMenuId, bool trackChanges);










  IEnumerable<Menu> GetAllMenus(bool trackChanges);
  Menu GetMenu(int MenuId, bool trackChanges);
  void CreateMenu(Menu Menu);

  IEnumerable<Menu> GetByIds(IEnumerable<int> ids, bool trackChanges);


  Task<IEnumerable<Menu>> GetMenusAsync(bool trackChanges);
  Task<Menu> GetMenuAsync(int MenuId, bool trackChanges);
  Task<Menu?> MenuByMenuIdWithAdditionalCondition(int MenuId, string additionalCondition);
  void UpdateMenu(Menu Menu);
  void DeleteMenu(Menu Menu);
}
