using bdDevCRM.Entities.Exceptions.BaseException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Entities.Exceptions;

public class UnauthorizedAccessException : ForbiddenAccessException
{
  public UnauthorizedAccessException(string message) : base(message)
  {
  }
}
