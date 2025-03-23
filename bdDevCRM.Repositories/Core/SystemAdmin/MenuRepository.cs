using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
{
  public MenuRepository(CRMContext context) : base(context) { }

  private const string SELECT_ALL_MENU_BY_MODULEID_QUERY = "Select * from Menu where ModuleId = {0} order by SorOrder,menuName asc";

  private const string SELECT_MENU_BY_USERS_PERMISSION_QUERY =
            "SELECT DISTINCT Menu.MenuId,Menu.ModuleId, GroupMember.UserId, GroupPermission.PermissionTableName, Menu.MenuName, Menu.MenuPath, Menu.ParentMenu,SORORDER,ToDo FROM GroupMember INNER JOIN Groups ON GroupMember.GroupId = Groups.GroupId INNER JOIN GroupPermission ON Groups.GroupId = GroupPermission.GroupId INNER JOIN Menu ON GroupPermission.ReferenceID = Menu.MenuId WHERE (GroupMember.UserId ={0}) AND (GroupPermission.PermissionTableName = 'Menu') order by Sororder, Menu.MenuName";

  public async Task<IEnumerable<MenuRepositoryDto>> SelectAllMenuByModuleId(int moduleId, bool trackChanges)
  {
    string query = string.Format(SELECT_ALL_MENU_BY_MODULEID_QUERY, moduleId);
    IEnumerable<MenuRepositoryDto> menuRepositoryDto = await GetListOfDataByQuery<MenuRepositoryDto>(query);

    return menuRepositoryDto.AsQueryable();
  }

  public async Task<IEnumerable<MenuRepositoryDto>> SelectMenuByUserPermission(int userId, bool trackChanges)
  {
    string query = string.Format(SELECT_MENU_BY_USERS_PERMISSION_QUERY, userId);
    IEnumerable<MenuRepositoryDto> menuRepositoryDto = await ExecuteOptimizedQuery<MenuRepositoryDto>(query, null);

    return menuRepositoryDto.AsQueryable();
  }

  public async Task<List<MenuRepositoryDto>> GetParentMenuByMenu(int parentMenuId, bool trackChanges)
  {
    // Define the query to fetch menu data
    string menusByUserPermissionQuery = $"SELECT * FROM Menu WHERE MenuID = {parentMenuId}";
    //IEnumerable<MenuRepositoryDto> menusDto = await GetGenericResultByQuery<MenuRepositoryDto>(menusByUserPermissionQuery);
    List<MenuRepositoryDto> menusDto = await ExecuteQueryAsync<MenuRepositoryDto>(menusByUserPermissionQuery);
    return menusDto;
  }

  public async Task<List<MenuRepositoryDto>> MenuSummary(bool trackChanges)
  {
    string menuSummaryQuery = $"Select MenuId,Menu.ModuleId, MenuName, MenuPath, ISNULL(ParentMenu, 0) as ParentMenu ,ModuleName,ToDo,SORORDER\r\n,(Select MenuName from Menu mn where mn.MenuId = menu.ParentMenu) as ParentMenuName \r\nfrom Menu \r\nleft outer join Module on module.ModuleId = menu.ModuleId\r\norder by ModuleName asc,ParentMenu asc, MenuName";
    List<MenuRepositoryDto> menusDto = await ExecuteQueryAsync<MenuRepositoryDto>(menuSummaryQuery);
    return menusDto;
  }

  public IEnumerable<Menu> GetAllMenus(bool trackChanges) => FindAll(trackChanges).OrderBy(c => c.MenuName).ToList();

  public Menu GetMenu(int MenuId, bool trackChanges) => FindByCondition(c => c.MenuId.Equals(MenuId), trackChanges).SingleOrDefault();

  public IEnumerable<Menu> GetByIds(IEnumerable<int> ids, bool trackChanges) => FindByCondition(x => ids.Contains(x.MenuId), trackChanges).ToList();

  // Get all Menus
  public async Task<IEnumerable<Menu>> GetMenusAsync(bool trackChanges) => await FindAll(trackChanges).OrderBy(c => c.MenuId).ToListAsync();

  // Get a single Menu by ID
  public async Task<Menu> GetMenuAsync(int MenuId, bool trackChanges) => await FindByCondition(c => c.MenuId.Equals(MenuId), trackChanges).FirstOrDefaultAsync();

  public async Task<Menu?> MenuByMenuIdWithAdditionalCondition(int MenuId, string additionalCondition)
  {
    var query = string.Format("Select * from Menu where MenuId = {0} {1}", MenuId, additionalCondition);
    Menu? objMenu = await GetSingleGenericResultByQuery<Menu>(query);
    return objMenu;
  }

  public async Task<IEnumerable<Menu>> MenusByModuleId(int moduleId, bool trackChanges) => await FindByConditionAsync(c => c.ModuleId.Equals(moduleId));

  // Add a new Menu
  public void CreateMenu(Menu Menu) => Create(Menu);

  // Update an existing Menu
  public void UpdateMenu(Menu Menu) => UpdateAsync(Menu);

  // Delete a Menu by ID
  public void DeleteMenu(Menu Menu) => Delete(Menu);
}
