using System.Net;

namespace bdDevCRM.Shared.Exceptions.BaseException;

/// <summary>
/// Exception for conflict errors (HTTP 409)
/// </summary>
[Serializable]
public abstract class ConflictException : BaseCustomException
{
    /// <summary>
    /// HTTP status code for conflict
    /// </summary>
    public override int StatusCode => (int)HttpStatusCode.Conflict; // 409

    /// <summary>
    /// Error code for conflict
    /// </summary>
    public override string ErrorCode => "CONFLICT";

    protected ConflictException(string message) : base(message) { }

    protected ConflictException(string message, Exception innerException)
        : base(message, innerException) { }
}
