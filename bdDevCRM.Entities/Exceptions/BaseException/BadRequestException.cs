namespace bdDevCRM.Entities.Exceptions.BaseException;

public abstract class BadRequestException : Exception
{
  protected BadRequestException(string message) : base(message)
  {

  }
}