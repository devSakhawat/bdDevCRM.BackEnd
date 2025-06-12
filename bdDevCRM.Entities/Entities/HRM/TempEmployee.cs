using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class TempEmployee
{
    public int HrrecordId { get; set; }

    public string? FullName { get; set; }

    public string? FatherName { get; set; }

    public string? MotherName { get; set; }

    public string? SpouseName { get; set; }

    public string? Gender { get; set; }

    public string? ReligionId { get; set; }

    public string? Nationality { get; set; }

    public string? NationalId { get; set; }

    public string? PassportNo { get; set; }

    public DateOnly? DateofBirth { get; set; }

    public string? PlaceofBirth { get; set; }

    public DateOnly? DateofMarriage { get; set; }

    public string? PresentAddress { get; set; }

    public string? Thana { get; set; }

    public string? District { get; set; }

    public string? PermanentAddress { get; set; }

    public string? PermanentAddressThana { get; set; }

    public string? PermanentAddressDistrict { get; set; }

    public string? HomePhone { get; set; }

    public string? MobileNo { get; set; }

    public string? PersonalEmail { get; set; }

    public string? InternetMessenger { get; set; }

    public string? InternetProfileLink { get; set; }

    public string? AdditionalInfo { get; set; }

    public int? AdditionalDayOf { get; set; }

    public int? DayOfType { get; set; }

    public int? AppliedDayOfWeek { get; set; }

    public int? NumberofDays { get; set; }

    public int? CasualLeaveNo { get; set; }

    public int? MedicalLeaveNo { get; set; }

    public int? AnualLeaveNo { get; set; }

    public int? ShortLeaveNo { get; set; }

    public string? BloodGroup { get; set; }

    public int? StateId { get; set; }

    public string? OriginalBirthDay { get; set; }

    public int? UserId { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public int? ApproverId { get; set; }

    public DateTime? ApproveDate { get; set; }

    public bool? LogHourEnable { get; set; }

    public string? Profilepicture { get; set; }

    public string? Meritialstatus { get; set; }

    public string? Birthidentification { get; set; }

    public string? Placeofpassportissue { get; set; }

    public DateOnly? Passportissuedate { get; set; }

    public DateOnly? Passportexpiredate { get; set; }

    public string? Height { get; set; }

    public string? Weight { get; set; }

    public string? Hobby { get; set; }

    public string? Signature { get; set; }

    public string? Identificationmark { get; set; }

    public int? Taxexamption { get; set; }

    public int? Investmentamount { get; set; }

    public string? ShortName { get; set; }

    public int? IsAutistic { get; set; }

    public string? PresentPostCode { get; set; }

    public string? PermanentPostCode { get; set; }

    public string? Refempid { get; set; }

    public string? MobileNo1 { get; set; }
}
