using bdDevCRM.Entities.Entities.System;
using bdDevCRM.RepositoriesContracts.Core.SystemAdmin;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.Sql.Context;
using Microsoft.EntityFrameworkCore;

namespace bdDevCRM.Repositories.Core.SystemAdmin;

public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
{
	public MenuRepository(CRMContext context) : base(context) { }

	// ✅ SECURITY FIX: Using parameterized queries to prevent SQL Injection
	private const string SELECT_ALL_MENU_BY_MODULEID_QUERY =
		"SELECT * FROM Menu WHERE ModuleId = @ModuleId ORDER BY SorOrder, MenuName ASC";

	private const string SELECT_MENU_BY_USERS_PERMISSION_QUERY =
		@"SELECT DISTINCT
			Menu.MenuId, Menu.ModuleId, GroupMember.UserId,
			GroupPermission.PermissionTableName, Menu.MenuName,
			Menu.MenuPath, Menu.ParentMenu, SORORDER, ToDo
		FROM GroupMember
		INNER JOIN Groups ON GroupMember.GroupId = Groups.GroupId
		INNER JOIN GroupPermission ON Groups.GroupId = GroupPermission.GroupId
		INNER JOIN Menu ON GroupPermission.ReferenceID = Menu.MenuId
		WHERE (GroupMember.UserId = @UserId)
			AND (GroupPermission.PermissionTableName = 'Menu')
		ORDER BY Sororder, Menu.MenuName";

	/// <summary>
	/// Retrieves all menus for a specific module with parameterized query for security
	/// </summary>
	public async Task<IEnumerable<MenuRepositoryDto>> SelectAllMenuByModuleId(int moduleId, bool trackChanges)
	{
		var parameters = new[] { new SqlParameter("@ModuleId", moduleId) };
		IEnumerable<MenuRepositoryDto> menuRepositoryDto = await ExecuteListQuery<MenuRepositoryDto>(
			SELECT_ALL_MENU_BY_MODULEID_QUERY,
			parameters);
		return menuRepositoryDto.AsQueryable();
	}

	/// <summary>
	/// Retrieves menus based on user permissions with parameterized query for security
	/// </summary>
	public async Task<IEnumerable<MenuRepositoryDto>> SelectMenuByUserPermission(int userId, bool trackChanges)
	{
		var parameters = new[] { new SqlParameter("@UserId", userId) };
		IEnumerable<MenuRepositoryDto> menuRepositoryDto = await ExecuteListQuery<MenuRepositoryDto>(
			SELECT_MENU_BY_USERS_PERMISSION_QUERY,
			parameters);
		return menuRepositoryDto.AsQueryable();
	}

	/// <summary>
	/// Retrieves parent menu details with parameterized query for security
	/// </summary>
	public async Task<List<MenuRepositoryDto>> GetParentMenuByMenu(int parentMenuId, bool trackChanges)
	{
		const string query = "SELECT * FROM Menu WHERE MenuID = @MenuId";
		var parameters = new[] { new SqlParameter("@MenuId", parentMenuId) };
		IEnumerable<MenuRepositoryDto> menusDto = await ExecuteListQuery<MenuRepositoryDto>(query, parameters);
		return menusDto.ToList();
	}

	/// <summary>
	/// Retrieves menu summary with joins for display purposes (no user input - safe)
	/// </summary>
	public async Task<List<MenuRepositoryDto>> MenuSummary(bool trackChanges)
	{
		const string menuSummaryQuery = @"
			SELECT
				MenuId, Menu.ModuleId, MenuName, MenuPath,
				ISNULL(ParentMenu, 0) as ParentMenu, ModuleName, ToDo, SORORDER,
				(SELECT MenuName FROM Menu mn WHERE mn.MenuId = menu.ParentMenu) as ParentMenuName
			FROM Menu
			LEFT OUTER JOIN Module ON module.ModuleId = menu.ModuleId
			ORDER BY ModuleName ASC, ParentMenu ASC, MenuName";

		IEnumerable<MenuRepositoryDto> menusDto = await ExecuteListQuery<MenuRepositoryDto>(menuSummaryQuery, null);
		return menusDto.ToList();
	}

	public async Task<IEnumerable<Menu>> GetMenus(bool trackChanges) => await ListAsync(x => x.MenuName, trackChanges);

	//public async Task<IEnumerable<Menu>> GetAllMenus(bool trackChanges) => await ListAsync(trackChanges).OrderBy(c => c.MenuName).ToList();


	public IEnumerable<Menu> GetByIds(IEnumerable<int> ids, bool trackChanges)
		=> GetListByIds(c => ids.Contains(c.MenuId), trackChanges).ToList();


	public Menu? GetMenu(int MenuId, bool trackChanges) => FirstOrDefault(c => c.MenuId.Equals(MenuId), trackChanges);


	// Get all Menus
	public async Task<IEnumerable<Menu>> GetMenusAsync(bool trackChanges)
		=> await ListAsync(x => x.MenuId, trackChanges);

	// Get a single Menu by ID
	public async Task<Menu> GetMenuAsync(int MenuId, bool trackChanges) => await FirstOrDefaultAsync(c => c.MenuId.Equals(MenuId), trackChanges);

	/// <summary>
	/// ⚠️ WARNING: This method accepts raw SQL condition - use with caution
	/// Consider refactoring to use Expression<Func<T, bool>> instead
	/// </summary>
	[Obsolete("Use Expression-based query methods instead for better security")]
	public async Task<Menu?> MenuByMenuIdWithAdditionalCondition(int MenuId, string additionalCondition)
	{
		// Note: additionalCondition should be validated/sanitized by caller
		// This is a technical debt that should be refactored
		var query = $"SELECT * FROM Menu WHERE MenuId = @MenuId {additionalCondition}";
		var parameters = new[] { new SqlParameter("@MenuId", MenuId) };
		Menu? objMenu = await ExecuteSingleData<Menu>(query, parameters);
		return objMenu;
	}

	public async Task<IEnumerable<Menu>> MenusByModuleId(int moduleId, bool trackChanges)
		=> await ListByConditionAsync(c => c.ModuleId.Equals(moduleId), orderBy: x => x.ModuleId, trackChanges: trackChanges);

	// Add a new Menu
	public void CreateMenu(Menu Menu) => Create(Menu);

	// Update an existing Menu
	public void UpdateMenu(Menu Menu) => UpdateByState(Menu);

	// Delete a Menu by ID
	public void DeleteMenu(Menu Menu) => Delete(Menu);



}
