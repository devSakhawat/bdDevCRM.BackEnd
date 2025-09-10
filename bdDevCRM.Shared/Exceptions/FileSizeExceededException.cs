using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bdDevCRM.Shared.Exceptions.BaseException;

namespace bdDevCRM.Shared.Exceptions;

public class FileSizeExceededException : BadRequestException
{
    public FileSizeExceededException(string fileName, double fileSizeMB, double maxSizeMB)
        : base($"File '{fileName}' size ({fileSizeMB:F2} MB) exceeds the maximum allowed size of {maxSizeMB} MB.")
    {
    }

    public FileSizeExceededException(double maxSizeMB)
        : base($"File size cannot exceed {maxSizeMB} MB.")
    {
    }
}
