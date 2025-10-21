using System;

namespace bdDevCRM.Shared.Exceptions.BaseException;

/// <summary>
/// Exception for bad request errors (HTTP 400)
/// </summary>
[Serializable]
public class BadRequestException : Exception
{
    /// <summary>
    /// HTTP status code for bad request
    /// </summary>
    public int StatusCode { get; } = 400;

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