
namespace bdDevCRM.Shared.Exceptions;


[Serializable]
public class InvalidUpdateOperationException : Exception
{
  public InvalidUpdateOperationException()
  {
  }

  public InvalidUpdateOperationException(string? message) : base(message)
  {
  }

  public InvalidUpdateOperationException(string? message, Exception? innerException) : base(message, innerException)
  {
  }
}
