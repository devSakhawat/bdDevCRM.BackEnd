using System.Net;

namespace bdDevCRM.Shared.Exceptions.BaseException;

/// <summary>
/// Exception for service unavailable errors (HTTP 503)
/// </summary>
[Serializable]
public abstract class ServiceUnavailableException : BaseCustomException
{
    /// <summary>
    /// HTTP status code for service unavailable
    /// </summary>
    public override int StatusCode => (int)HttpStatusCode.ServiceUnavailable; // 503

    /// <summary>
    /// Error code for service unavailable
    /// </summary>
    public override string ErrorCode => "SERVICE_UNAVAILABLE";

    protected ServiceUnavailableException(string message) : base(message) { }

    protected ServiceUnavailableException(string message, Exception innerException)
        : base(message, innerException) { }
}
