using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.Shared.DataTransferObjects.DMS;

public class DmsdocumentFolderDto
{
  public int FolderId { get; set; }

  public int? ParentFolderId { get; set; }

  public string FolderName { get; set; } = null!;

  public int OwnerUserId { get; set; }

  public string ReferenceEntityType { get; set; } = null!;

  public string ReferenceEntityId { get; set; } = null!;

  //public virtual ICollection<DmsdocumentFolder> InverseParentFolder { get; set; } = new List<DmsdocumentFolder>();

  //public virtual DmsdocumentFolder? ParentFolder { get; set; }
}