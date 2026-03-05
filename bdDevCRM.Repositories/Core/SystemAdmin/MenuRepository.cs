using bdDevCRM.Entities.Entities;
using bdDevCRM.Entities.Entities.System;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos.Core;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using Microsoft.EntityFrameworkCore;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

/// <summary>
/// Menu repository for data access operations.
/// Inherits from RepositoryBase for standard CRUD operations.
/// </summary>
public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
{
	public MenuRepository(CRMContext context) : base(context) { }

	private const string SELECT_ALL_MENU_BY_MODULEID_QUERY =
		"SELECT * FROM Menu WHERE ModuleId = {0} ORDER BY SorOrder, MenuName ASC";

	private const string SELECT_MENU_BY_USERS_PERMISSION_QUERY =
		"SELECT DISTINCT Menu.MenuId, Menu.ModuleId, GroupMember.UserId, GroupPermission.PermissionTableName, " +
		"Menu.MenuName, Menu.MenuPath, Menu.ParentMenu, SORORDER, ToDo " +
		"FROM GroupMember " +
		"INNER JOIN Groups ON GroupMember.GroupId = Groups.GroupId " +
		"INNER JOIN GroupPermission ON Groups.GroupId = GroupPermission.GroupId " +
		"INNER JOIN Menu ON GroupPermission.ReferenceID = Menu.MenuId " +
		"WHERE (GroupMember.UserId = {0}) AND (GroupPermission.PermissionTableName = 'Menu') " +
		"ORDER BY Sororder, Menu.MenuName";

	public async Task<IEnumerable<MenuRepositoryDto>> SelectAllMenuByModuleId(int moduleId, bool trackChanges)
	{
		string query = string.Format(SELECT_ALL_MENU_BY_MODULEID_QUERY, moduleId);
		IEnumerable<MenuRepositoryDto> menuRepositoryDto = await ExecuteListQuery<MenuRepositoryDto>(query, null);
		return menuRepositoryDto;
	}

	public async Task<IEnumerable<MenuRepositoryDto>> SelectMenuByUserPermission(int userId, bool trackChanges)
	{
		string query = string.Format(SELECT_MENU_BY_USERS_PERMISSION_QUERY, userId);
		IEnumerable<MenuRepositoryDto> menuRepositoryDto = await ExecuteListQuery<MenuRepositoryDto>(query, null);
		return menuRepositoryDto;
	}

	public async Task<List<MenuRepositoryDto>> GetParentMenuByMenu(int parentMenuId, bool trackChanges)
	{
		string query = $"SELECT * FROM Menu WHERE MenuID = {parentMenuId}";
		IEnumerable<MenuRepositoryDto> menusDto = await ExecuteListQuery<MenuRepositoryDto>(query, null);
		return menusDto.ToList();
	}

	public async Task<List<MenuRepositoryDto>> MenuSummary(bool trackChanges)
	{
		string query = "SELECT MenuId, Menu.ModuleId, MenuName, MenuPath, ISNULL(ParentMenu, 0) AS ParentMenu, " +
		               "ModuleName, ToDo, SORORDER, " +
		               "(SELECT MenuName FROM Menu mn WHERE mn.MenuId = menu.ParentMenu) AS ParentMenuName " +
		               "FROM Menu " +
		               "LEFT OUTER JOIN Module ON module.ModuleId = menu.ModuleId " +
		               "ORDER BY ModuleName ASC, ParentMenu ASC, MenuName";

		IEnumerable<MenuRepositoryDto> menusDto = await ExecuteListQuery<MenuRepositoryDto>(query, null);
		return menusDto.ToList();
	}

	// Get all menus with ordering
	public async Task<IEnumerable<Menu>> GetMenus(bool trackChanges) =>
		await ListAsync(x => x.MenuName, trackChanges);

	// Get menus by IDs
	public IEnumerable<Menu> GetByIds(IEnumerable<int> ids, bool trackChanges) =>
		GetListByIds(c => ids.Contains(c.MenuId), trackChanges).ToList();

	// Get single menu by ID
	public Menu? GetMenu(int MenuId, bool trackChanges) =>
		FirstOrDefault(c => c.MenuId.Equals(MenuId), trackChanges);

	// Get all menus async
	public async Task<IEnumerable<Menu>> GetMenusAsync(bool trackChanges) =>
		await ListAsync(x => x.MenuId, trackChanges);

	// Get single menu async
	public async Task<Menu> GetMenuAsync(int MenuId, bool trackChanges) =>
		await FirstOrDefaultAsync(c => c.MenuId.Equals(MenuId), trackChanges);

	// Get menu with additional SQL condition
	public async Task<Menu?> MenuByMenuIdWithAdditionalCondition(int MenuId, string additionalCondition)
	{
		var query = $"SELECT * FROM Menu WHERE MenuId = {MenuId} {additionalCondition}";
		Menu? objMenu = await ExecuteSingleData<Menu>(query);
		return objMenu;
	}

	// Get menus by module ID
	public async Task<IEnumerable<Menu>> MenusByModuleId(int moduleId, bool trackChanges) =>
		await ListByConditionAsync(c => c.ModuleId.Equals(moduleId), orderBy: x => x.ModuleId, trackChanges: trackChanges);

	// CRUD operations
	public void CreateMenu(Menu Menu) => Create(Menu);

	public void UpdateMenu(Menu Menu) => UpdateByState(Menu);

	public void DeleteMenu(Menu Menu) => Delete(Menu);
}
