using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class ResignationMapping
{
    public int ResignationMappingId { get; set; }

    public int? ResignationMappingCompanyId { get; set; }

    public int? ResignationMappingDepartmentId { get; set; }

    public int? ResignationMappingAssignPersonHrrecordId { get; set; }

    public int? MappedBy { get; set; }

    public int? ModuleId { get; set; }

    public int? ResignationMappingDivisionId { get; set; }

    public int? ResignationMappingBranchId { get; set; }

    public int? ResignationMappingTeamId { get; set; }
}
