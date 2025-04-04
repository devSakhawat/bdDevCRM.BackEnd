using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
{
  public MenuRepository(CRMContext context) : base(context) { }

  private const string SELECT_ALL_MENU_BY_MODULEID_QUERY = "Select * from Menu where ModuleId = {0} order by SorOrder,menuName asc";

  private const string SELECT_MENU_BY_USERS_PERMISSION_QUERY =
            "SELECT DISTINCT Menu.MenuId,Menu.ModuleId, GroupMember.UserId, GroupPermission.PermissionTableName, Menu.MenuName, Menu.MenuPath, Menu.ParentMenu,SORORDER,ToDo FROM GroupMember INNER JOIN Groups ON GroupMember.GroupId = Groups.GroupId INNER JOIN GroupPermission ON Groups.GroupId = GroupPermission.GroupId INNER JOIN Menu ON GroupPermission.ReferenceID = Menu.MenuId WHERE (GroupMember.UserId ={0}) AND (GroupPermission.PermissionTableName = 'Menu') order by Sororder, Menu.MenuName";

  public async Task<IEnumerable<MenuRepositoryDto>> SelectAllMenuByModuleId(int moduleId, bool trackChanges)
  {
    //string query = string.Format(SELECT_ALL_MENU_BY_MODULEID_QUERY, moduleId);
    //IEnumerable<MenuRepositoryDto> menuRepositoryDto = await GetListOfDataByQuery<MenuRepositoryDto>(query);

    string query = string.Format(SELECT_ALL_MENU_BY_MODULEID_QUERY, moduleId);

    IEnumerable<MenuRepositoryDto> menuRepositoryDto = await ExecuteListQuery<MenuRepositoryDto>(query, null);
    return menuRepositoryDto.AsQueryable();
  }

  public async Task<IEnumerable<MenuRepositoryDto>> SelectMenuByUserPermission(int userId, bool trackChanges)
  {
    string query = string.Format(SELECT_MENU_BY_USERS_PERMISSION_QUERY, userId);
    IEnumerable<MenuRepositoryDto> menuRepositoryDto = await ExecuteListQuery<MenuRepositoryDto>(query, null);

    return menuRepositoryDto.AsQueryable();
  }

  public async Task<List<MenuRepositoryDto>> GetParentMenuByMenu(int parentMenuId, bool trackChanges)
  {
    string menusByUserPermissionQuery = $"SELECT * FROM Menu WHERE MenuID = {parentMenuId}";
    IEnumerable<MenuRepositoryDto> menusDto = await ExecuteListQuery<MenuRepositoryDto>(menusByUserPermissionQuery, null);
    return menusDto.ToList();
  }

  public async Task<List<MenuRepositoryDto>> MenuSummary(bool trackChanges)
  {
    string menuSummaryQuery = $"Select MenuId,Menu.ModuleId, MenuName, MenuPath, ISNULL(ParentMenu, 0) as ParentMenu ,ModuleName,ToDo,SORORDER\r\n,(Select MenuName from Menu mn where mn.MenuId = menu.ParentMenu) as ParentMenuName \r\nfrom Menu \r\nleft outer join Module on module.ModuleId = menu.ModuleId\r\norder by ModuleName asc,ParentMenu asc, MenuName";
    IEnumerable<MenuRepositoryDto> menusDto = await ExecuteListQuery<MenuRepositoryDto>(menuSummaryQuery, null);
    return menusDto.ToList();
  }

  public async Task<IEnumerable<Menu>> GetMenus(bool trackChanges) => await ListAsync(x => x.MenuName ,trackChanges);

  //public async Task<IEnumerable<Menu>> GetAllMenus(bool trackChanges) => await ListAsync(trackChanges).OrderBy(c => c.MenuName).ToList();


  public IEnumerable<Menu> GetByIds(IEnumerable<int> ids, bool trackChanges)
    => GetListByIds(c => ids.Contains(c.MenuId), trackChanges).ToList();


  public Menu? GetMenu(int MenuId, bool trackChanges) => FirstOrDefault(c => c.MenuId.Equals(MenuId), trackChanges);
      

  // Get all Menus
  public async Task<IEnumerable<Menu>> GetMenusAsync(bool trackChanges)
    => await ListAsync(x => x.MenuId, trackChanges);

  // Get a single Menu by ID
  public async Task<Menu> GetMenuAsync(int MenuId, bool trackChanges) => await FirstOrDefaultAsync(c => c.MenuId.Equals(MenuId), trackChanges);

  public async Task<Menu?> MenuByMenuIdWithAdditionalCondition(int MenuId, string additionalCondition)
  {
    var query = string.Format("Select * from Menu where MenuId = {0} {1}", MenuId, additionalCondition);
    Menu? objMenu = await ExecuteSingleData<Menu>(query);
    return objMenu;
  }

  public async Task<IEnumerable<Menu>> MenusByModuleId(int moduleId, bool trackChanges)
    => await ListByConditionAsync(c => c.ModuleId.Equals(moduleId), orderBy:x => x.ModuleId, trackChanges: trackChanges);

  // Add a new Menu
  public void CreateMenu(Menu Menu) => Create(Menu);

  // Update an existing Menu
  public void UpdateMenu(Menu Menu) => UpdateByState(Menu);

  // Delete a Menu by ID
  public void DeleteMenu(Menu Menu) => Delete(Menu);

}
