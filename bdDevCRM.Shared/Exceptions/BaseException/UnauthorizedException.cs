using System.Net;

namespace bdDevCRM.Shared.Exceptions.BaseException;

/// <summary>
/// Exception for unauthorized errors (HTTP 401)
/// </summary>
[Serializable]
public class UnauthorizedException : BaseCustomException
{
    /// <summary>
    /// HTTP status code for unauthorized
    /// </summary>
    public override int StatusCode => (int)HttpStatusCode.Unauthorized; // 401

    /// <summary>
    /// Error code for unauthorized
    /// </summary>
    public override string ErrorCode => "UNAUTHORIZED";

    public UnauthorizedException(string message) : base(message) { }

    public UnauthorizedException(string message, Exception innerException)
        : base(message, innerException) { }
}
