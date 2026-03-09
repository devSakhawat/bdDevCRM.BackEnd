using bdDevCRM.Entities.Entities.System;
using bdDevCRM.RepositoriesContracts.Specifications;

namespace bdDevCRM.Repositories.Specifications;

/// <summary>
/// Specification for filtering menus by module
/// Example usage: var spec = new MenusByModuleSpecification(moduleId);
/// </summary>
public class MenusByModuleSpecification : BaseSpecification<Menu>
{
    public MenusByModuleSpecification(int moduleId)
        : base(m => m.ModuleId == moduleId)
    {
        ApplyOrderBy(m => m.Sororder);
        AddInclude(m => m.Module);
    }
}

/// <summary>
/// Specification for filtering active menus only
/// </summary>
public class ActiveMenusSpecification : BaseSpecification<Menu>
{
    public ActiveMenusSpecification()
        : base(m => m.IsActive == 1)
    {
        ApplyOrderBy(m => m.MenuName);
    }
}

/// <summary>
/// Specification for parent menus (menus without parent)
/// </summary>
public class ParentMenusOnlySpecification : BaseSpecification<Menu>
{
    public ParentMenusOnlySpecification()
        : base(m => m.ParentMenu == null || m.ParentMenu == 0)
    {
        ApplyOrderBy(m => m.Sororder);
    }
}

/// <summary>
/// Specification for child menus of a specific parent
/// </summary>
public class ChildMenusByParentSpecification : BaseSpecification<Menu>
{
    public ChildMenusByParentSpecification(int parentMenuId)
        : base(m => m.ParentMenu == parentMenuId)
    {
        ApplyOrderBy(m => m.Sororder);
    }
}

/// <summary>
/// Specification for searching menus by name pattern
/// </summary>
public class MenusByNamePatternSpecification : BaseSpecification<Menu>
{
    public MenusByNamePatternSpecification(string namePattern)
        : base(m => m.MenuName.Contains(namePattern))
    {
        ApplyOrderBy(m => m.MenuName);
    }
}

/// <summary>
/// Complex specification combining multiple criteria with pagination
/// Example: Active menus from specific module with pagination
/// </summary>
public class ActiveMenusByModuleWithPagingSpecification : BaseSpecification<Menu>
{
    public ActiveMenusByModuleWithPagingSpecification(
        int moduleId,
        int pageIndex,
        int pageSize)
        : base(m => m.ModuleId == moduleId && m.IsActive == 1)
    {
        ApplyOrderBy(m => m.Sororder);
        ApplyPaging(pageIndex * pageSize, pageSize);
        AddInclude(m => m.Module);
    }
}
