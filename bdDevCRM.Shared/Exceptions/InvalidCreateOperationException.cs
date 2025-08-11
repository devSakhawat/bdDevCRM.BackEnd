using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Utilities.Exceptions;

public class InvalidCreateOperationException : Exception
{
  public int StatusCode { get; } = (int)HttpStatusCode.BadRequest;

  public InvalidCreateOperationException() : base("Failed to create record due to invalid operation.")
  {
  }

  public InvalidCreateOperationException(string message) : base(message)
  {
  }
}