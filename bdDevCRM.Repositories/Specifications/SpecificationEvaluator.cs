using bdDevCRM.RepositoriesContracts.Specifications;
using Microsoft.EntityFrameworkCore;

namespace bdDevCRM.Repositories.Specifications;

/// <summary>
/// Evaluates specifications and applies them to IQueryable
/// Used by repositories to build queries from specifications
/// </summary>
public static class SpecificationEvaluator<T> where T : class
{
    /// <summary>
    /// Applies specification to queryable
    /// </summary>
    /// <param name="inputQuery">Base query</param>
    /// <param name="specification">Specification to apply</param>
    /// <returns>Query with specification applied</returns>
    public static IQueryable<T> GetQuery(
        IQueryable<T> inputQuery,
        ISpecification<T> specification)
    {
        var query = inputQuery;

        // Apply criteria (WHERE clause)
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        // Apply includes (eager loading)
        query = specification.Includes.Aggregate(
            query,
            (current, include) => current.Include(include));

        // Apply include strings (for complex includes)
        query = specification.IncludeStrings.Aggregate(
            query,
            (current, include) => current.Include(include));

        // Apply ordering
        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending != null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        // Apply paging (must be after ordering)
        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }

        return query;
    }
}
