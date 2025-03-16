using bdDevCRM.Entities.Entities;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
{
  public MenuRepository(CRMContext context) : base(context) { }

  private const string SELECT_ALL_MENU_BY_MODULEID = "Select * from Menu where ModuleId = {0} order by SorOrder,menuName asc";

  private const string SELECT_MENU_BY_USERS_PERMISION =
            "SELECT DISTINCT Menu.MenuId,Menu.ModuleId, GroupMember.UserId, GroupPermission.PermissionTableName, Menu.MenuName, Menu.MenuPath, Menu.ParentMenu,SORORDER,ToDo FROM GroupMember INNER JOIN Groups ON GroupMember.GroupId = Groups.GroupId INNER JOIN GroupPermission ON Groups.GroupId = GroupPermission.GroupId INNER JOIN Menu ON GroupPermission.ReferenceID = Menu.MenuId WHERE (GroupMember.UserId ={0}) AND (GroupPermission.PermissionTableName = 'Menu') order by Sororder, Menu.MenuName";



  public async Task<IEnumerable<MenuRepositoryDto>> SelectAllMenuByModuleId(int moduleId, bool trackChanges)
  {
    string quary = string.Format(SELECT_ALL_MENU_BY_MODULEID, moduleId);
    IEnumerable<MenuRepositoryDto> menuRepositoryDto = await GetListOfDataByQuery<MenuRepositoryDto>(quary);

    return menuRepositoryDto.AsQueryable();
  }

  public async Task<IEnumerable<MenuRepositoryDto>> SelectMenuByUserPermission(int userId, bool trackChanges)
  {
    string quary = string.Format(SELECT_MENU_BY_USERS_PERMISION, userId);
    IEnumerable<MenuRepositoryDto> menuRepositoryDto = await ExecuteOptimizedQuery<MenuRepositoryDto>(quary, null);

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



  public IEnumerable<Menu> GetAllMenus(bool trackChanges) => FindAll(trackChanges).OrderBy(c => c.MenuName).ToList();

  public Menu GetMenu(int MenuId, bool trackChanges) => FindByCondition(c => c.MenuId.Equals(MenuId), trackChanges).SingleOrDefault();

  public IEnumerable<Menu> GetByIds(IEnumerable<int> ids, bool trackChanges) => FindByCondition(x => ids.Contains(x.MenuId), trackChanges).ToList();


  // Get all Menus
  public async Task<IEnumerable<Menu>> GetMenusAsync(bool trackChanges) => await FindAll(trackChanges).OrderBy(c => c.MenuId).ToListAsync();

  // Get a single Menu by ID
  public async Task<Menu> GetMenuAsync(int MenuId, bool trackChanges) => await FindByCondition(c => c.MenuId.Equals(MenuId), trackChanges).FirstOrDefaultAsync();

  public async Task<Menu?> MenuByMenuIdWithAdditionalCondition(int MenuId, string additionalCondition)
  {
    var quary = string.Format("Select * from Menu where MenuId = {0} {1}", MenuId, additionalCondition);
    Menu? objMenu = await GetSingleGenericResultByQuery<Menu>(quary);
    return objMenu;
  }



  // Add a new Menu
  public void CreateMenu(Menu Menu) => Create(Menu);

  // Update an existing Menu
  public void UpdateMenu(Menu Menu) => UpdateAsync(Menu);

  // Delete a Menu by ID
  public void DeleteMenu(Menu Menu) => Delete(Menu);

}
