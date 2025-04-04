using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;

public class WfActionDto
{
  public int WfactionId { get; set; }

  public int WfstateId { get; set; }

  public string ActionName { get; set; } = null!;

  public int NextStateId { get; set; }

  public int? EmailAlert { get; set; }

  public int? SmsAlert { get; set; }

  public int? AcSortOrder { get; set; }
}
