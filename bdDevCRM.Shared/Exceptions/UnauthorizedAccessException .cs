using bdDevCRM.Shared.Exceptions.BaseException;

namespace bdDevCRM.Shared.Exceptions;

public class UnauthorizedAccessCRMException(string message) : UnauthorizedException(message)
{
}
