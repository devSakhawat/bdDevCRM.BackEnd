using bdDevCRM.Shared.Exceptions.BaseException;

namespace bdDevCRM.Shared.Exceptions;

public sealed class GenericNotFoundException(string entityName, string propertyName, string key) : NotFoundException($"{entityName} with {propertyName} {key} was not found.")
{
}


//public class GenericNotFoundException : Exception
//{
//  public GenericNotFoundException(string entityName, string propertyName, string key) : base($"{entityName} with {propertyName} {key} was not found.") { }
//}
