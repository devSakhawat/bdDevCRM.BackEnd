using bdDevCRM.Entities.Entities.System;

using bdDevCRM.RepositoriesContracts;
using bdDevCRM.RepositoryDtos;
using bdDevCRM.RepositoryDtos.Core.SystemAdmin;
using bdDevCRM.ServiceContract.Authentication;
using bdDevCRM.Shared.DataTransferObjects.Authentication;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Utilities.Common;
using bdDevCRM.Utilities.OthersLibrary;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace bdDevCRM.Service.Authentication;

public class AuthenticationService : IAuthenticationService
{

  private readonly IConfiguration _configuration;
  private readonly IRepositoryManager _repository;
  private readonly ILoggerManager _logger;

  public AuthenticationService(IRepositoryManager repository,ILoggerManager logger, IConfiguration configuration) 
  {
    _configuration = configuration; 
    _repository = repository;
    _logger = logger;
  }

  public bool ValidateUser(UserForAuthenticationDto model)
  {
    var userInfo =  _repository.Users.GetUserByLoginId(model.LoginId.Trim(), false);
    if (userInfo == null)
    {
      _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong user name or password.");
      return false;
    }

    var currentPassword = userInfo.Password;
    string dycryptPass = EncryptDecryptHelper.Decrypt(currentPassword);

    if (model.Password != dycryptPass)
    {
      _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong user name or password.");
      return false;
    }

    return true;
  }

  public string CreateToken(UserForAuthenticationDto user)
  {
    UsersRepositoryDto users = _repository.Users.GetUserByLoginIdAsync(user.LoginId, false);

    var signingCredentials = GetSigningCredentials();
    var claims = GetClaims(users);
    var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
    string token = string.Empty;
    try
    {
      token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
    catch (Exception ex)
    {
      throw new Exception(ex.Message, ex);
    }

    //return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    return token;
  }

  private SigningCredentials GetSigningCredentials()
  {
    var jwtSettings = _configuration.GetSection("Jwt");
    var secret = new SymmetricSecurityKey((jwtSettings.GetSection("SecretKey").Value != null) ? Encoding.UTF8.GetBytes(jwtSettings.GetSection("SecretKey").Value) : Encoding.UTF8.GetBytes("TechnoDeveloperThrillsIsLongEnough1234567890"));
    return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
  }

  private List<Claim> GetClaims(UsersRepositoryDto user)
  {
    var claims = new List<Claim> {
        new Claim(ClaimTypes.NameIdentifier, user.LoginId ?? ""),
        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserId.ToString() ?? ""),
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName.ToString()),
        new Claim(ClaimTypes.Name, user.UserName ?? ""),
        new Claim("CompanyId", user.CompanyId.ToString() ?? "0"),
        new Claim("HrrecordId", user.EmployeeId.ToString() ?? "0"),
        new Claim("UserId", user.UserId.ToString() ?? "0"),
        
        // You could add additional claims as needed
        // new Claim("IsActive", user.IsActive?.ToString() ?? "false"),
        // new Claim("EmployeeId", user.EmployeeId?.ToString() ?? "0"),
        
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };
    return claims;
  }

  private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
  {
    var jwtSettings = _configuration.GetSection("Jwt");

    var securityKey = new SymmetricSecurityKey((jwtSettings.GetSection("SecretKey").Value != null) ? Encoding.UTF8.GetBytes(jwtSettings.GetSection("SecretKey").Value) : Encoding.UTF8.GetBytes("TechnoDeveloperThrillsIsLongEnough1234567890"));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);



    var tokenOptions = new JwtSecurityToken
    (
    issuer: jwtSettings.GetSection("Issuer").Value,
    audience: jwtSettings.GetSection("Audience").Value,
    claims: claims,
    expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("ExpiryInMinutes").Value)),
    signingCredentials: credentials
    );
    return tokenOptions;
  }

  public string GenerateToken(Users user)
  {
    //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
    //var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var jwtSettings = _configuration.GetSection("Jwt");
    var securityKey = new SymmetricSecurityKey((jwtSettings.GetSection("SecretKey").Value != null) ? Encoding.UTF8.GetBytes(jwtSettings.GetSection("SecretKey").Value) : Encoding.UTF8.GetBytes("TechnoDeveloperThrillsIsLongEnough1234567890"));
    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    // These claims represent the user identity, but never include sensitive data like passwords
    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
        new Claim(JwtRegisteredClaimNames.UniqueName, user.LoginId ?? ""),
        new Claim(ClaimTypes.NameIdentifier, user.UserName ?? "0"),
        //new Claim(ClaimTypes.Name, user.EmployeeId ?? ""),
        new Claim("CompanyId", user.CompanyId?.ToString() ?? "0"),
        new Claim("HrrecordId", user.EmployeeId?.ToString() ?? "0"),
        
        // You could add additional claims as needed
        // new Claim("IsActive", user.IsActive?.ToString() ?? "false"),
        // new Claim("EmployeeId", user.EmployeeId?.ToString() ?? "0"),
        
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

    var token = new JwtSecurityToken(
        issuer: _configuration["Jwt:Issuer"],
        audience: _configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryInMinutes"])),
        signingCredentials: credentials
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
  }

  private UsersDto GetCurrentUser(string userData)
  {
    var data = userData.Split('^');
    var CurrentUser = new UsersDto();
    CurrentUser.UserId = Convert.ToInt32(data[1]);
    CurrentUser.LoginId = data[2];
    CurrentUser.Employee_Id = data[20];
    CurrentUser.UserName = data[3];
    CurrentUser.CompanyId = Convert.ToInt32(data[4]);
    CurrentUser.EmployeeId = Convert.ToInt32(data[5]);
    CurrentUser.CompanyName = data[6];
    CurrentUser.FullLogoPath = data[7];
    CurrentUser.LogHourEnable = Convert.ToBoolean(data[8]);
    if (data[0] == "CHANGESuccess")
    {
      CurrentUser.IsFirstLogin = "Yes";
    }
    else
    {
      CurrentUser.IsFirstLogin = "No";
    }
    CurrentUser.LicenseExpiryDate = Convert.ToDateTime(data[9]);
    CurrentUser.LicenseUserNo = Convert.ToInt32(data[10]);
    CurrentUser.FiscalYearStart = Convert.ToInt32(data[11]);
    CurrentUser.ShiftId = Convert.ToInt32(data[13]);
    CurrentUser.Theme = data[14];
    CurrentUser.BranchId = Convert.ToInt32(data[15]);
    CurrentUser.AccessParentCompany = Convert.ToInt32(data[16]);
    CurrentUser.ProfilePicture = Convert.ToString(data[17]);
    CurrentUser.Gender = Convert.ToInt32(data[18]);
    CurrentUser.DefaultDashboard = Convert.ToInt32(data[19]);
    CurrentUser.AssemblyInfoId = Convert.ToInt32(data[21]);

    return CurrentUser;
  }




  public Task<Users> GetUserById(int userId)
  {
    throw new NotImplementedException();
  }







  //public async Task<Users> AuthenticateAsync(string username, string password)
  //{
  //  if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
  //    return null;

  //  var user2 = await _repository.CustomAuthentication.Users.Include(u => u.Role)
  //      .SingleOrDefaultAsync(u => u.Username == username);

  //  if (user == null || !user.IsActive)
  //    return null;

  //  if (!_passwordHasher.VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
  //    return null;

  //  // Update last login date
  //  user.LastLoginDate = DateTime.UtcNow;
  //  await _context.SaveChangesAsync();

  //  return user;
  //}




  #region ValidateUserLogin from mvc

  //public async Task<bool> ValidateUserLogin(string loginId, string password, bool isRememberMe)
  //public async Task<string> ValidateUserLogin(UserForAuthenticationDto model)
  //{
  //  var res = "";
  //  var validateData = "";
  //  try
  //  {
  //    var replacements = new Dictionary<char, char> { //!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~
  //                  { '+', ' ' }, // Add more replacements as needed
  //     };

  //    string encryptedpwd = "";
  //    if (!CommonHelper.IsEncrypted(model.Password))
  //    {
  //      encryptedpwd = EncryptDecryptHelper.Encrypt(model.Password);
  //      string pwdc = CommonHelper.ReplaceMultipleSpecificSpecialCharacters(encryptedpwd, replacements);
  //      encryptedpwd = "enc_" + pwdc;
  //    }
  //    else
  //    {
  //      encryptedpwd = model.Password;
  //    }

  //    var rep = new Dictionary<char, char> { { ' ', '+' }, };

  //    if (CommonHelper.IsEncrypted(model.Password))
  //    {
  //      string sub = password.Substring("enc_".Length);
  //      string pwdc = CommonHelper.ReplaceMultipleSpecificSpecialCharacters(sub, rep);
  //      string dcrpwd = EncryptDecryptHelper.Decrypt(pwdc);
  //      password = dcrpwd;
  //    }

  //    AssemblyInfo objAssemblyInfo = await _repository.SystemSettings.GetAssemblyInfoResult();

  //    var isValid = false;

  //    //Checking Currect Password with Entering Password
  //    if (objAssemblyInfo.AssemblyInfoId == 12)
  //    {
  //      var userInfo = await _repository.Users.GetUserByLoginIdAsync(model.LoginId, false);
  //      var currentPassword = userInfo.Password;
  //      string dycryptPass = EncryptDecryptHelper.Decrypt(currentPassword);

  //      if (model.Password != dycryptPass)
  //      {
  //        return "Wrong Password! Please Enter Currect Password.";
  //      }
  //    }
  //    SystemSettingsDto systemSettingsDto = new SystemSettingsDto();
  //    Users objUser = _repository.Users.GetUserByLoginId(model.LoginId, false);
  //    UsersDto user = MyMapper.JsonClone<Users, UsersDto>(objUser);
  //    var currentDate = DateTime.Now;
  //    string message = "";

  //    var dayCount = 0;

  //    if (user == null)
  //    {
  //      message = "FAILED";
  //    }
  //    else
  //    {
  //      if (user.IsActive == false && isValid == false)
  //      {
  //        message = "INACTIVE";
  //      }
  //      else
  //      {
  //        if (user.IsExpired == true && isValid == false)
  //        {
  //          message = "EXPIRED";
  //        }
  //        else
  //        {

  //          var systemSettings = await _repository.SystemSettings.GetSystemSettingsDataByCompanyId((int)(user.CompanyId));
  //          if (systemSettings == null) throw new GenericNotFoundException("SystemSettings", "CompanyId", user.CompanyId.ToString());
  //          systemSettingsDto = MyMapper.JsonClone<SystemSettings, SystemSettingsDto>(systemSettings);

  //          var employmentInformation = await _repository.Employees.GetEmploymentByHrRecordId((int)user.EmployeeId);
  //          if (employmentInformation == null) throw new GenericNotFoundException("Employment", "user.EmployeeId", user.EmployeeId.ToString());

  //          if (user.EmployeeId != 1)
  //          {
  //            var status = await _repository.Employees.GetEmployeeCurrentStatusByHrRecordId((int)user.EmployeeId);
  //            if (status != null && status.StateName != "") return status.StateName;
  //          }

  //          var objCompany = _repository.Companies.GetCompany((int)user.CompanyId, false);
  //          if (objCompany == null) throw new GenericNotFoundException("Company", "CompanyId", user.CompanyId.ToString());


  //          if (objCompany.CompanyId > 0 && objCompany.IsActive == 0) return "CompanyInActive";

  //          var objEmployee = await _repository.Employees.GetEmployeeByHrRecordId((int)user.EmployeeId);


  //          user.CompanyName = objCompany.CompanyName;
  //          user.FiscalYearStart = objCompany.FiscalYearStart;
  //          user.FullLogoPath = objCompany.FullLogoPath;
  //          user.LogHourEnable = objEmployee.LogHourEnable;
  //          user.BranchId = employmentInformation.BranchId;
  //          user.ProfilePicture = objEmployee.ProfilePicture;
  //          user.Gender = objEmployee.Gender;
  //          user.Employee_Id = employmentInformation.EmployeeId;
  //          if (objAssemblyInfo.AssemblyInfoId > 0)
  //          {
  //            user.AssemblyInfoId = objAssemblyInfo.AssemblyInfoId;
  //          }

  //          if (isValid == true || ValidationHelper.ValidateLoginPassword(model.Password, user.Password, true))
  //          {
  //            IQueryable<PasswordHistoryRepositoryDto> passwordHistoryRepositoryDto = await _repository.Users.GetPasswordHistory(user.UserId, (int)systemSettings.OldPassUseRestriction);
  //            IEnumerable<PasswordHistoryDto> passwordHistoryDto = MyMapper.JsonCloneIEnumerableToList<PasswordHistoryRepositoryDto, PasswordHistoryDto>(passwordHistoryRepositoryDto);


  //            if (passwordHistoryDto.Count() == 0)
  //            {
  //              if (systemSettings.IsPasswordChange == 1)
  //              {
  //                TimeSpan span = currentDate.Subtract(user.CreatedDate); //00:00:00
  //                dayCount = span.Days;


  //                if (dayCount > systemSettings.PassExpiryDays)
  //                {
  //                  //update user as expired
  //                  user.IsExpired = true;

  //                  message = _repository.Users.UpdateUser(objUser, null);

  //                  if (message == "Success")
  //                  {
  //                    message = "EXPIRED";
  //                  }
  //                  return message;
  //                }
  //              }
  //            }
  //            else
  //            {
  //              TimeSpan span = currentDate.Subtract(passwordHistoryDto.ElementAt(0).PasswordChangeDate);
  //              dayCount = span.Days;
  //              if (systemSettings.IsPasswordChange == 1)
  //              {
  //                if (dayCount > systemSettings.PassExpiryDays)
  //                {
  //                  //update user as expired
  //                  user.IsExpired = true;

  //                  message = _repository.Users.UpdateUser(objUser, null);
  //                  if (message == "Success")
  //                  {
  //                    message = "EXPIRED";
  //                  }
  //                  return message;
  //                }
  //              }
  //            }
  //            if (systemSettings.ChangePassFirstLogin == true && user.LastLoginDate == DateTime.MinValue)
  //            {
  //              message = "CHANGE";
  //            }
  //            else
  //            {
  //              user.LastLoginDate = currentDate;
  //            }

  //            if (user.LastUpdateDate != DateTime.MinValue)
  //            {
  //              TimeSpan tSpan = currentDate.Subtract(user.LastUpdateDate);
  //              dayCount = tSpan.Days;
  //              if (systemSettings.IsPasswordChange == 1)
  //              {
  //                if (dayCount > systemSettings.ChangePassDays)
  //                {
  //                  message = "CHANGE";
  //                }
  //              }
  //            }
  //            //Update Failed Loging No = 0 of User table
  //            user.FailedLoginNo = 0;
  //            message = _repository.Users.UpdateUser(objUser, null);
  //          }
  //          else
  //          {
  //            user.FailedLoginNo += 1;

  //            //if Failed Login reached to "Wrong attempt to Lock" then deactivate user
  //            if (user.FailedLoginNo >= systemSettings.WrongAttemptNo)
  //            {
  //              //Deactivate User
  //              user.IsActive = false;
  //            }

  //            message = _repository.Users.UpdateUser(objUser, null);
  //            if (message == "Success")
  //            {
  //              message = "FAILED";
  //            }
  //            return message;
  //          }
  //        }
  //      }

  //      var theme = "";
  //      if (String.IsNullOrEmpty(user.Theme))
  //      {
  //        theme = systemSettingsDto.Theme;
  //      }
  //      else
  //      {
  //        theme = user.Theme;
  //      }

  //      //var license = new AzolutionLicense();
  //      //var expiryDate = license.GetExpiryDate();
  //      //var licUserNo = license.GetNumberOfUser();
  //      //  checkLeaveBalance(user);

  //      var sb = new StringBuilder();
  //      //sb.AppendFormat("{0}^{1}^{2}^{3}^{4}^{5}^{6}^{7}^{8}^{9}^{10}^{11}^{12}^{13}^{14}^{15}^{16}^{17}^{18}^{19}^{20}^{21}",
  //      //                            message, objUser.UserId, objUser.LoginId, objUser.UserName, objUser.CompanyId, objUser.EmployeeId, user.CompanyName,
  //      //                            user.FullLogoPath, user.LogHourEnable, expiryDate, licUserNo, user.FiscalYearStart, output, shiftId,
  //      //                            theme, user.BranchId, user.AccessParentCompany, user.ProfilePicture, user.Gender, user.DefaultDashboard, user.Employee_Id, user.AssemblyInfoId);
  //      sb.AppendFormat("{0}^{1}^{2}^{3}^{4}^{5}^{6}^{7}^{8}^{9}^{10}^{11}^{12}^{13}^{14}^{15}^{16}^{17}^{18}^{19}^{20}^{21}",
  //                                  message, objUser.UserId, objUser.LoginId, objUser.UserName, objUser.CompanyId, objUser.EmployeeId, user.CompanyName,
  //                                  user.FullLogoPath, user.LogHourEnable, "2025-09-31", "", user.FiscalYearStart, "", "",
  //                                  theme, user.BranchId, user.AccessParentCompany, user.ProfilePicture, user.Gender, user.DefaultDashboard, user.Employee_Id, user.AssemblyInfoId);
  //      //return sb.ToString();
  //      validateData = sb.ToString();
  //    }


  //    if ((validateData.Split('^')[0] == "Success") || (validateData.Split('^')[0] == "CHANGESHORT") ||
  //        (validateData.Split('^')[0] == "CHANGELEAVE") || (validateData.Split('^')[0] == "CHANGESuccess") ||
  //        (validateData.Split('^')[0] == "LATE") || (validateData.Split('^')[0] == "SHORT") || (validateData.Split('^')[0] == "LEAVE"))
  //    {
  //      var currentUser = GetCurrentUser(validateData);
  //      res = "Success"; //For Audit trail
  //  catch (Exception ex)
  //  {
  //    res = ex.Message;
  //    return res;
  //  }

  //  return validateData;
  //}
  #endregion ValidateUserLogin from mvc


}
