using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Shared.DataTransferObjects.DMS;

public class DmsdocumentTagDto
{
  public int TagId { get; set; }

  public string Name { get; set; } = null!;

  //public virtual ICollection<Dmsdocument> Document { get; set; } = new List<Dmsdocument>();
}
