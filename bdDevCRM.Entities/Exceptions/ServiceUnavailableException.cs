namespace bdDevCRM.Entities.Exceptions;

public abstract class ServiceUnavailableException : Exception
{
  public ServiceUnavailableException(string message) : base(message)
  {

  }
}
