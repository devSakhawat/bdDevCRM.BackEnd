using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Shared.Exceptions;

public class RequestTimeoutException : Exception
{
  public RequestTimeoutException(string message) : base(message) { }
}
