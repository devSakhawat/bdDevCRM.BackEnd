using bdDevCRM.Entities.Exceptions.BaseException;

namespace bdDevCRM.Entities.Exceptions;

public sealed class AccessDeniedException : ForbiddenAccessException
{
  public AccessDeniedException() : base("Access is denied due to insufficient permissions.")
  {

  }
}