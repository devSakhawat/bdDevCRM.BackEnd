using System.Globalization;

namespace bdDevs.Security;

public class bdDevsLicense
{
	private string SerialNo;

	private string CompanyName;

	private string LicenseKey;

	private int NoOfUser;

	private int NoOfRepository;

	private DateTime ExpiryDate;

	private string Module;

	public bdDevsLicense()
	{
		Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
		SerialNo = ".BN92QP1.CN70166133000F.BFEBFBFF00020655BN92QP12661";
		CompanyName = " Karim & Karim";
		LicenseKey = "AZ12-LN12-WMG2-011E";
		NoOfUser = 100;
		NoOfRepository = 5;
		ExpiryDate = DateTime.Parse("31/12/2027", new CultureInfo("en-GB"));
		//Module = "HR Record;Conveyance;Movement;Attendance;Leave;Payroll";
		Module = "CRM";
	}

	public string GetCompanyName()
	{
		return CompanyName;
	}

	public string GetLicenseKey()
	{
		return LicenseKey;
	}

	public int GetNumberOfUser()
	{
		return NoOfUser;
	}

	public int GetNumberOfRepository()
	{
		return NoOfRepository;
	}

	public DateTime GetExpiryDate()
	{
		return ExpiryDate;
	}

	public bool ValidateActivation(string SerialNo, string LicenseKey)
	{
		if (SerialNo == this.SerialNo && LicenseKey == this.LicenseKey)
		{
			return true;
		}

		return false;
	}

	public bool ValidateNoOfUser(int NoOfUser)
	{
		if (NoOfUser >= this.NoOfUser)
		{
			return false;
		}

		return true;
	}

	public bool ValidateNoOfRepository(int NoOfRepository)
	{
		if (NoOfRepository >= this.NoOfRepository)
		{
			return false;
		}

		return true;
	}

	public bool ValidateExpiryDate()
	{
		if (DateTime.Today > ExpiryDate)
		{
			return false;
		}

		return true;
	}

	public bool ValidateModulePermission(string ModuleName)
	{
		string[] array = Module.Split(';');
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].ToLower() == ModuleName.ToLower())
			{
				return true;
			}
		}

		return false;
	}
}
