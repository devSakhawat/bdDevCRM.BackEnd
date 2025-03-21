namespace bdDevCRM.Entities.Exceptions.BaseException;

public abstract class ConflictException : Exception
{
  public ConflictException(string message) : base(message)
  {

  }
}