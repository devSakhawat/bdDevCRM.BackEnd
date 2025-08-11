using bdDevCRM.Utilities.Exceptions.BaseException;

namespace bdDevCRM.Utilities.Exceptions;

public class CommonBadReuqestException(string message) : BadRequestException(message)
{
}
// pass only your message