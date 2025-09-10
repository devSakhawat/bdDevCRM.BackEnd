using bdDevCRM.Shared.Exceptions.BaseException;

namespace bdDevCRM.Shared.Exceptions;

public sealed class GenericUnauthorizedException(string message) : UnauthorizedException(message)
{
}