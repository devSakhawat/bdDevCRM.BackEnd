namespace bdDevCRM.Utilities.Constants;


public static class MessageConstants
{
  public const string GenericError = "Something went wrong, please try after sometimes! If you are experiencing similar frequently, please report it to helpdesk.";
  public const string RequiredFieldError = "Required!";
  public const string PasswordLengthError = "Password must have atleast 5 characters!";
  public const string GenderNotSelectedError = "Gender was not selected!";
  public const string UnofficialEmailAddressError = "Only IHM’s official email address is allowed!";
  public const string UnderAgedUserError = "User must be equal or above 18 years old!";
  public const string InvalidDOBError = "Date of birth cannot be a future date!";
  public const string ConfirmedPasswordNotMatchedError = "Confirmed password did not matched!";
  public const string DuplicateUserAccountError = "The email address is associated with another user account!";
  public const string DuplicateError = "Duplicate data found!";
  public const string DependencyError = "This record cannot be deleted. It is already in use.";
  public const string NoMatchFoundError = "No match found!";
  public const string InvalidParameterError = "Invalid parameter(s)!";
  public const string UnauthorizedAttemptOfRecordUpdateError = "Unauthorized attempt of updating record!";
  public const string IfNotAlphabet = "Input string is not correct format!";
  public const string IfNotInteger = "Input string is not correct format!";
  public const string IfNotSelected = "Choose an option!";
  public const string IfNotEmailAddress = "Email is not correct formation!";
  public const string IfNotCountryCode = "Invalid counrty code!";
  public const string IfFutureDateSelected = "Please select valid date. Your can't select future date!";

  public const string RecordSaved = "Record successfully saved";
  public const string RecordDeleted = "Record successfully deleted";
  public const string IfDeleteReffereceRecord = "Foreign key refference in other table";
  public const string IfInvalidUserPassword = "Invalid Username or password";
  public const string RecordUpdated = "Record updated successfully";
  public const string SaveFailed = "Record save failed";
  public const string UpdateFailed = "Record updated failed";
  public const string DeleteFailed = "Record deleted failed";
  public const string ModelStateInvalid = "Model state invalid. Fillup all the field value.";
  public const string MappingFailed = "Mapping Failed!";
  public const string NoRecordError = "No record found!";
  public const string UnauthorizedAttemptOfRecordDeleteError = "Unauthorized attempt of deleting record!";
  public const string UnauthorizedAttemptOfRecordInsert = "Unauthorized attempt of inserting record!";
  public static string ExceptionError = "";
}

public static class OperationMessage
{
  public const string Success = "Success";
}
