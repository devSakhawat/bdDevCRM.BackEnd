using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Shared.DataTransferObjects.CRM;

public class CRMMonthDto
{
  public int MonthId { get; set; }

  public string CrmmonthName { get; set; } = null!;

  public string? MonthCode { get; set; }

  public bool? Status { get; set; }
}
