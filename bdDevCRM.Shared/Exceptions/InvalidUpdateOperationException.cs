using bdDevCRM.Shared.Exceptions.BaseException;

namespace bdDevCRM.Shared.Exceptions;

/// <summary>
/// Exception thrown when an update operation is invalid
/// </summary>
[Serializable]
public class InvalidUpdateOperationException : BadRequestException
{
  /// <summary>
  /// Creates a new InvalidUpdateOperationException with the specified message
  /// </summary>
  /// <param name="message">Error message</param>
  public InvalidUpdateOperationException(string message)
      : base(message)
  {
  }

  /// <summary>
  /// Creates a new InvalidUpdateOperationException with the specified message and inner exception
  /// </summary>
  /// <param name="message">Error message</param>
  /// <param name="innerException">Inner exception</param>
  public InvalidUpdateOperationException(string message, Exception innerException)
      : base(message, innerException)
  {
  }

  /// <summary>
  /// Creates a new InvalidUpdateOperationException with a default message
  /// </summary>
  public InvalidUpdateOperationException()
      : base("Invalid update operation.")
  {
  }
}