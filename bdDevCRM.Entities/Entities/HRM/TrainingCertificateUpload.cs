using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TrainingCertificateUpload
{
    public int TrainingCertificateId { get; set; }

    public int? HrRecordId { get; set; }

    public string? EmployeeName { get; set; }

    public int? DepartmentId { get; set; }

    public int? BranchId { get; set; }

    public int? CompanyId { get; set; }

    public string? UploadFilePath { get; set; }

    public int? TrainingId { get; set; }
}
