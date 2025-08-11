using bdDevCRM.Utilities.Exceptions.BaseException;

namespace bdDevCRM.Utilities.Exceptions;

public sealed class AccessDeniedException : ForbiddenAccessException
{
  public AccessDeniedException() : base("Access is denied due to insufficient permissions.")
  {

  }
}