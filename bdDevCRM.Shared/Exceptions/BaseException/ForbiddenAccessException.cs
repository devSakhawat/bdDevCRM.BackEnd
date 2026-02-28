using System.Net;

namespace bdDevCRM.Shared.Exceptions.BaseException;

/// <summary>
/// Exception for forbidden access errors (HTTP 403)
/// </summary>
[Serializable]
public abstract class ForbiddenAccessException : BaseCustomException
{
    /// <summary>
    /// HTTP status code for forbidden access
    /// </summary>
    public override int StatusCode => (int)HttpStatusCode.Forbidden; // 403

    /// <summary>
    /// Error code for forbidden access
    /// </summary>
    public override string ErrorCode => "FORBIDDEN";

    protected ForbiddenAccessException(string message) : base(message) { }

    protected ForbiddenAccessException(string message, Exception innerException)
        : base(message, innerException) { }
}
