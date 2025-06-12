using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class OfficeTime
{
    public int OfficeTimeId { get; set; }

    public int? ShiftId { get; set; }

    public string? SatFrom { get; set; }

    public string? SatTo { get; set; }

    public string? SunFrom { get; set; }

    public string? SunTo { get; set; }

    public string? MonFrom { get; set; }

    public string? MonTo { get; set; }

    public string? TueFrom { get; set; }

    public string? TueTo { get; set; }

    public string? WedFrom { get; set; }

    public string? WedTo { get; set; }

    public string? ThuFrom { get; set; }

    public string? ThuTo { get; set; }

    public string? FriFrom { get; set; }

    public string? FriTo { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public int? AdditionalDayOffSat { get; set; }

    public int? AdditionalDayOffSun { get; set; }

    public int? AdditionalDayOffMon { get; set; }

    public int? AdditionalDayOffTue { get; set; }

    public int? AdditionalDayOffWed { get; set; }

    public int? AdditionalDayOffThu { get; set; }

    public int? AdditionalDayOffFri { get; set; }

    public decimal? SatBuiltInOtHour { get; set; }

    public decimal? SunBuiltInOtHour { get; set; }

    public decimal? MonBuiltInOtHour { get; set; }

    public decimal? TueBuiltInOtHour { get; set; }

    public decimal? WedBuiltInOtHour { get; set; }

    public decimal? ThuBuiltInOtHour { get; set; }

    public decimal? FriBuiltInOtHour { get; set; }

    public virtual Shift? Shift { get; set; }
}
