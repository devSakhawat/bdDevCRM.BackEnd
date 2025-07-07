using bdDevCRM.Utilities.Exceptions.BaseException;

namespace bdDevCRM.Utilities.Exceptions;


public sealed class NullModelBadRequestException(string modelName) : BadRequestException($"{modelName} model is null\"")
{
}
