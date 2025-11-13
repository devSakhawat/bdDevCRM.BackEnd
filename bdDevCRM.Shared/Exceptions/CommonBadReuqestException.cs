using bdDevCRM.Shared.Exceptions.BaseException;

namespace bdDevCRM.Shared.Exceptions;

public class CommonBadReuqestException(string message) : BadRequestException(message)
{
}
// pass only your message