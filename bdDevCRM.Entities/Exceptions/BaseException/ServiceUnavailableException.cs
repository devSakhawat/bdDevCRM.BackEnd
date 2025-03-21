namespace bdDevCRM.Entities.Exceptions.BaseException;

public abstract class ServiceUnavailableException : Exception
{
  public ServiceUnavailableException(string message) : base(message)
  {

  }
}
