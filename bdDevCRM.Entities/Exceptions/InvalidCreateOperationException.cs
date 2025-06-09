using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Entities.Exceptions;

public class InvalidCreateOperationException : Exception
{
  public InvalidCreateOperationException() : base("Invalid operation for creating a new record.")
  {
  }

  public InvalidCreateOperationException(string message) : base(message)
  {
  }
}