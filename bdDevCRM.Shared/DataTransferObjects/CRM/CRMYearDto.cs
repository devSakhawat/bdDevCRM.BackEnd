using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Shared.DataTransferObjects.CRM;

public class CRMYearDto
{
  public int YearId { get; set; }

  public string YearName { get; set; } = null!;

  public string? YearCode { get; set; }

  public bool? Status { get; set; }
}
