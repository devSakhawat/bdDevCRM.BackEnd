using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Shared.DataTransferObjects.DMS;

public class DmsdocumentAccessLogDto
{
  public long LogId { get; set; }

  public int DocumentId { get; set; }

  public int AccessedByUserId { get; set; }

  public DateTime AccessDateTime { get; set; }

  public string Action { get; set; } = null!;

  //public virtual Dmsdocument Document { get; set; } = null!;
}
