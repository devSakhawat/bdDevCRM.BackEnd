using bdDevCRM.Shared.Exceptions.BaseException;

namespace bdDevCRM.Shared.Exceptions;


public sealed class NullModelBadRequestException(string modelName) : BadRequestException($"{modelName} model is null\"")
{
}
