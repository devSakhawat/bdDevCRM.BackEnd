using System;
using System.Collections.Generic;

namespace bdDevCRM.Entities.Entities;

public partial class EmployeeTemp
{
    public int HrRecordId { get; set; }

    public string? FullName { get; set; }

    public string? FatherName { get; set; }

    public string? MotherName { get; set; }

    public string? SpouseName { get; set; }

    public string? Gender { get; set; }

    public string? Religion { get; set; }

    public string? Nationality { get; set; }

    public string? NationalId { get; set; }

    public string? PassportNo { get; set; }

    public DateTime? DateofBirth { get; set; }

    public string? PlaceofBirth { get; set; }

    public DateTime? DateofMarriage { get; set; }

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

    public string? BloodGroup { get; set; }

    public string? OriginalBirthDay { get; set; }

    public string? Profilepicture { get; set; }

    public string? Meritialstatus { get; set; }

    public string? Birthidentification { get; set; }

    public string? Placeofpassportissue { get; set; }

    public DateTime? Passportissuedate { get; set; }

    public DateTime? Passportexpiredate { get; set; }

    public string? Height { get; set; }

    public string? Weight { get; set; }

    public string? Hobby { get; set; }

    public string? Signature { get; set; }

    public string? Identificationmark { get; set; }

    public string? ShortName { get; set; }

    public int? IsAutistic { get; set; }

    public string? PresentPostCode { get; set; }

    public string? PermanentPostCode { get; set; }

    public string? EmployeeId { get; set; }

    public int? UserId { get; set; }
}
