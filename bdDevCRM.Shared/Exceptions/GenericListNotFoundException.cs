using bdDevCRM.Shared.Exceptions.BaseException;

namespace bdDevCRM.Shared.Exceptions;

//public class GenericListNotFoundException : Exception
//{
//  public GenericListNotFoundException(string entityName) : base($"No data found for {entityName}.") { }
//}

public sealed class GenericListNotFoundException(string entityName) : NotFoundException($"No data found for {entityName}.")
{
}
