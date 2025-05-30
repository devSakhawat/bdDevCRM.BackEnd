using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;

public class CurrencyDDL
{
  public int CurrencyId { get; set; }

  public string CurrencyName { get; set; } = null!;
}
