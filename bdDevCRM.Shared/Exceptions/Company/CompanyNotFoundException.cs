

using bdDevCRM.Shared.Exceptions.BaseException;

namespace bdDevCRM.Shared.Exceptions.Company;

public sealed class CompanyNotFoundException(int companyId) : NotFoundException($"The company with id: {companyId} doesn't exist in the database.")
{
}
