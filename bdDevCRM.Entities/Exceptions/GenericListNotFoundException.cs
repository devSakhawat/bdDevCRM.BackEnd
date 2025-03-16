namespace bdDevCRM.Entities.Exceptions;

//public class GenericListNotFoundException : Exception
//{
//  public GenericListNotFoundException(string entityName) : base($"No data found for {entityName}.") { }
//}

public sealed class GenericListNotFoundException : NotFoundException
{
  public GenericListNotFoundException(string entityName) : base($"No data found for {entityName}.") { }
}
