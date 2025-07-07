using bdDevCRM.Utilities.Exceptions.BaseException;

namespace bdDevCRM.Utilities.Exceptions;

//public class GenericListNotFoundException : Exception
//{
//  public GenericListNotFoundException(string entityName) : base($"No data found for {entityName}.") { }
//}

public sealed class GenericListNotFoundException(string entityName) : NotFoundException($"No data found for {entityName}.")
{
}
