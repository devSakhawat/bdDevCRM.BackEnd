namespace bdDevCRM.Entities.Exceptions;

public abstract class ForbiddenAccessException : Exception
{
  protected ForbiddenAccessException(string message) : base(message)
  {

  }
}
