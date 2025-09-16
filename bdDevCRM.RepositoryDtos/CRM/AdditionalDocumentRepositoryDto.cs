using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bdDevCRM.RepositoryDtos.CRM;

public class AdditionalDocumentRepositoryDto
{
  public int AdditionalDocumentId { get; set; }
  public int ApplicantId { get; set; } // Foreign Key
  public string? DocumentTitle { get; set; }
  public string? DocumentName { get; set; }
  public string? RecordType { get; set; }

  public string? AttachedDocument { get; set; }

  // Common fields
  public DateTime CreatedDate { get; set; }
  public int? CreatedBy { get; set; }
  public DateTime? UpdatedDate { get; set; }
  public int? UpdatedBy { get; set; }
}
