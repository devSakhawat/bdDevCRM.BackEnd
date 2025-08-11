using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;

public class WfActionDto
{
  public int WfActionId { get; set; }

  public int WfStateId { get; set; }

  public string? ActionName { get; set; } = null!;
  public string? StateName { get; set; } = null!;

  public int? NextStateId { get; set; }

  public int? EmailAlert { get; set; }

  public int? SmsAlert { get; set; }

  public int? AcSortOrder { get; set; }

  public string? NextStateName { get; set; }

  public int? IsDefaultStart { get; set; }
  public int? IsClosed { get; set; }
}
