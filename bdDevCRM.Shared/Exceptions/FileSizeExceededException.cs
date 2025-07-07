using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Utilities.Exceptions;

public class FileSizeExceededException : Exception // Or a more specific exception like InvalidOperationException
{
  public FileSizeExceededException() { }

  public FileSizeExceededException(string message) : base(message) { }

  public FileSizeExceededException(string message, Exception innerException)
      : base(message, innerException) { }
}
