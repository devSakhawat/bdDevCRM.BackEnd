using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class RevDistributionDetails
{
    public int RevDistributionId { get; set; }

    public int RevDistributionMasterId { get; set; }

    public int HrRecordId { get; set; }

    public decimal? IndividualPf { get; set; }

    public decimal? IndividualCpf { get; set; }

    public decimal? IndividualTotal { get; set; }

    public decimal? IndividualIncome { get; set; }

    public int? DesignationId { get; set; }
}
