namespace bdDevCRM.Entities.Exceptions.BaseException;

public sealed class AccessDeniedException : ForbiddenAccessException
{
  public AccessDeniedException() : base("Access is denied due to insufficient permissions.")
  {

  }
}