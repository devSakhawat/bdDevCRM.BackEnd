using bdDevCRM.Utilities.Exceptions.BaseException;

namespace bdDevCRM.Utilities.Exceptions.Company;

public sealed class CompanyNotFoundException(int companyId) : NotFoundException($"The company with id: {companyId} doesn't exist in the database.")
{
}
