using bdDevCRM.Entities.Exceptions.BaseException;

namespace bdDevCRM.Entities.Exceptions.Company;

public sealed class CompanyNotFoundException : NotFoundException
{
  public CompanyNotFoundException(int companyId) : base($"The company with id: {companyId} doesn't exist in the database.")
  {
  }
}
