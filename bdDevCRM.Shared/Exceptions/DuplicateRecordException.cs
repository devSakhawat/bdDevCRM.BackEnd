using bdDevCRM.Utilities.Exceptions.BaseException;

namespace bdDevCRM.Utilities.Exceptions;

//[Serializable]
//public class DuplicateRecordException : Exception
//{
//  public DuplicateRecordException()
//  {
//  }

//  public DuplicateRecordException(string? message) : base(message)
//  {
//  }

//  public DuplicateRecordException(string? message, Exception? innerException) : base(message, innerException)
//  {
//  }
//}

[Serializable]
public class DuplicateRecordException : ConflictException
{
  public DuplicateRecordException() : base("You are trying to update using the same data. No changes were made.")
  {
  }
  public DuplicateRecordException(string propertyName) : base($"A record with the same {propertyName} already exists.")
  {
  }
  public DuplicateRecordException(string modelName, string propertyName) : base($"A record with the same {propertyName} already exists in the {modelName}.")
  {
  }
}

