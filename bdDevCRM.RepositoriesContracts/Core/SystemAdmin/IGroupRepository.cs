using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;

namespace bdDevCRM.RepositoriesContracts.Core.SystemAdmin;

public interface IGroupRepository : IRepositoryBase<Groups>
{
  Task<List<GroupsRepositoryDto>> GroupSummary(bool trackChanges);
  Task<IEnumerable<GroupPermissionRepositoryDto>> GroupPermisionsbyGroupId(int groupId);
  Task<IEnumerable<AccessControlRepositoryDto>> GetAccesses();



  //Task<IEnumerable<Menu>> GetMenus(bool trackChanges);

  //Menu? GetMenu(int MenuId, bool trackChanges);
  //Task<Menu> GetMenuAsync(int MenuId, bool trackChanges);
  //void CreateMenu(Menu Menu);

  //IEnumerable<Menu> GetByIds(IEnumerable<int> ids, bool trackChanges);


  //Task<IEnumerable<Menu>> GetMenusAsync(bool trackChanges);
  //Task<Menu?> MenuByMenuIdWithAdditionalCondition(int MenuId, string additionalCondition);
  //Task<IEnumerable<Menu>> MenusByModuleId(int moduleId, bool trackChanges);
  //void UpdateMenu(Menu Menu);
  //void DeleteMenu(Menu Menu);
}
