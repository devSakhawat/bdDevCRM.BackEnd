using bdDevCRM.Shared.Exceptions.BaseException;

namespace bdDevCRM.Shared.Exceptions;

/// <summary>
/// Exception thrown when a generic entity is not found in the database
/// </summary>
public sealed class GenericNotFoundException : NotFoundException
{
    public override string ErrorCode => "RESOURCE_NOT_FOUND";

    /// <summary>
    /// Creates a new GenericNotFoundException with entity context
    /// </summary>
    /// <param name="entityName">The name of the entity</param>
    /// <param name="propertyName">The property name used for search</param>
    /// <param name="key">The search key value</param>
    public GenericNotFoundException(string entityName, string propertyName, string key)
        : base($"{entityName} with {propertyName} {key} was not found.")
    {
        // Add contextual data for better error tracking
        this.WithData("EntityName", entityName)
            .WithData("PropertyName", propertyName)
            .WithData("SearchValue", key);
    }
}
