using bdDevCRM.Shared.Exceptions.BaseException;

namespace bdDevCRM.Shared.Exceptions;

public sealed class AccessDeniedException : ForbiddenAccessException
{
  public AccessDeniedException() : base("Access is denied due to insufficient permissions.")
  {

  }
}