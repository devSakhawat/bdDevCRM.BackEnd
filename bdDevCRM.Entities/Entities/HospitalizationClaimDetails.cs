using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class HospitalizationClaimDetails
{
    public int HospitalizationClaimDetalisId { get; set; }

    public int? HospitalizationClaimMasterId { get; set; }

    public int? HrRecordId { get; set; }

    public DateTime? TreatmentDate { get; set; }

    public int? RelationshipId { get; set; }

    public int? HospitalizationNomineeId { get; set; }

    public decimal? DoctorsFee { get; set; }

    public decimal? CostOfMedicine { get; set; }

    public decimal? Laboratory { get; set; }

    public decimal? Conveyance { get; set; }

    public decimal? Total { get; set; }
}
