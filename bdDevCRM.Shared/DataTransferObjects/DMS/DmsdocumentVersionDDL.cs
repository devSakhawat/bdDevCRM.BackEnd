using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Shared.DataTransferObjects.DMS;

public class DmsdocumentVersionDDL
{
  public int VersionId { get; set; }
  public int DocumentId { get; set; }
  public int VersionNumber { get; set; }
}
