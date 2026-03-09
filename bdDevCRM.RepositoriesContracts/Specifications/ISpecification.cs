using System.Linq.Expressions;

namespace bdDevCRM.RepositoriesContracts.Specifications;

/// <summary>
/// Enterprise Specification Pattern for complex query composition
/// Allows building reusable, testable, and composable query logic
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public interface ISpecification<T>
{
    /// <summary>
    /// Query criteria expression
    /// </summary>
    Expression<Func<T, bool>> Criteria { get; }

    /// <summary>
    /// Include expressions for eager loading
    /// </summary>
    List<Expression<Func<T, object>>> Includes { get; }

    /// <summary>
    /// Include string expressions for ThenInclude support
    /// </summary>
    List<string> IncludeStrings { get; }

    /// <summary>
    /// Order by expressions
    /// </summary>
    Expression<Func<T, object>>? OrderBy { get; }

    /// <summary>
    /// Order by descending expressions
    /// </summary>
    Expression<Func<T, object>>? OrderByDescending { get; }

    /// <summary>
    /// Pagination - number of items to take
    /// </summary>
    int Take { get; }

    /// <summary>
    /// Pagination - number of items to skip
    /// </summary>
    int Skip { get; }

    /// <summary>
    /// Enable change tracking
    /// </summary>
    bool IsPagingEnabled { get; }
}
