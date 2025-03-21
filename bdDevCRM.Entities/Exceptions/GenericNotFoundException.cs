using bdDevCRM.Entities.Exceptions.BaseException;

namespace bdDevCRM.Entities.Exceptions;

public sealed class GenericNotFoundException : NotFoundException
{
  public GenericNotFoundException(string entityName, string propertyName, string key) : base($"{entityName} with {propertyName} {key} was not found.") { }
}


//public class GenericNotFoundException : Exception
//{
//  public GenericNotFoundException(string entityName, string propertyName, string key) : base($"{entityName} with {propertyName} {key} was not found.") { }
//}
