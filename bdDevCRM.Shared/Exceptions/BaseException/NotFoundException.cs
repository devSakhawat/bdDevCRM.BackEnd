using System.Net;

namespace bdDevCRM.Shared.Exceptions.BaseException;

/// <summary>
/// Exception for not found errors (HTTP 404)
/// </summary>
[Serializable]
public abstract class NotFoundException : BaseCustomException
{
    /// <summary>
    /// HTTP status code for not found
    /// </summary>
    public override int StatusCode => (int)HttpStatusCode.NotFound; // 404

    /// <summary>
    /// Error code for not found
    /// </summary>
    public override string ErrorCode => "NOT_FOUND";

    protected NotFoundException(string message) : base(message) { }

    protected NotFoundException(string message, Exception innerException)
        : base(message, innerException) { }
}
