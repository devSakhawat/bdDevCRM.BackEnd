using bdDevCRM.Entities.Exceptions.BaseException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Entities.Exceptions;

public class UsernamePasswordMismatchException : BadRequestException
{
  public UsernamePasswordMismatchException() : base("The username or password is incorrect. Please try again.")
  {
  }

  public UsernamePasswordMismatchException(string message) : base(message)
  {
  }
}