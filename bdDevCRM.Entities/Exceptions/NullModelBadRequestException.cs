namespace bdDevCRM.Entities.Exceptions;


public sealed class NullModelBadRequestException : BadRequestException
{
  public NullModelBadRequestException(string modelName) : base($"{modelName} model is null\"") { }
}
