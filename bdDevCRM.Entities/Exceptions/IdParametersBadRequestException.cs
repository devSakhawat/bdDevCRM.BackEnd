namespace bdDevCRM.Entities.Exceptions;

public sealed class IdParametersBadRequestException : BadRequestException
{
  //public IdParametersBadRequestException(string message) : base("Parameter ids is null")
  public IdParametersBadRequestException() : base("Parameter ids is null")
  {
  }
}
