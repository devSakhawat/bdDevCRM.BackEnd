using bdDevCRM.Shared.Exceptions.BaseException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Shared.Exceptions;

public class UnauthorizedAccessCRMException(string message) : UnauthorizedException(message)
{
}
