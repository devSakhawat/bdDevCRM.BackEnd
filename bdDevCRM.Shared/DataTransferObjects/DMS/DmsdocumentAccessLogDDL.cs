using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Shared.DataTransferObjects.DMS;

public class DmsdocumentAccessLogDDL
{
  public long LogId { get; set; }
  public int DocumentId { get; set; }
  public int AccessedByUserId { get; set; }
}
