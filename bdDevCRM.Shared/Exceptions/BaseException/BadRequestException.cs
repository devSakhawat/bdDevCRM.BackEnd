using System;
using System.Net;

namespace bdDevCRM.Shared.Exceptions.BaseException;

/// <summary>
/// Exception for bad request errors (HTTP 400)
/// </summary>
[Serializable]
public class BadRequestException : BaseCustomException
{
    /// <summary>
    /// HTTP status code for bad request
    /// </summary>
    public override int StatusCode => (int)HttpStatusCode.BadRequest; // 400

    /// <summary>
    /// Error code for bad request
    /// </summary>
    public override string ErrorCode => "BAD_REQUEST";

    /// <summary>
    /// Creates a new BadRequestException with the specified message
    /// </summary>
    /// <param name="message">Error message</param>
    public BadRequestException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Creates a new BadRequestException with the specified message and inner exception
    /// </summary>
    /// <param name="message">Error message</param>
    /// <param name="innerException">Inner exception</param>
    public BadRequestException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Creates a new BadRequestException with a default message
    /// </summary>
    public BadRequestException()
        : base("Bad request.")
    {
    }
}
