using bdDevCRM.Entities.CRMGrid.GRID;
using bdDevCRM.Shared.DataTransferObjects.Core.SystemAdmin;
using bdDevCRM.Shared.DataTransferObjects.CRM;

namespace bdDevCRM.ServiceContract.CRM;

public interface IStatementOfPurposeService
{
  Task<IEnumerable<StatementOfPurposeDto>> GetStatementOfPurposesDDLAsync(bool trackChanges = false);
  Task<IEnumerable<StatementOfPurposeDto>> GetActiveStatementOfPurposesAsync(bool trackChanges = false);
  Task<IEnumerable<StatementOfPurposeDto>> GetStatementOfPurposesAsync(bool trackChanges = false);
  Task<StatementOfPurposeDto> GetStatementOfPurposeAsync(int id, bool trackChanges = false);
  Task<StatementOfPurposeDto> GetStatementOfPurposeByApplicantIdAsync(int applicantId, bool trackChanges = false);
  Task<StatementOfPurposeDto> CreateNewRecordAsync(StatementOfPurposeDto dto, UsersDto currentUser);
  Task<string> UpdateRecordAsync(int key, StatementOfPurposeDto dto, bool trackChanges);
  Task<string> DeleteRecordAsync(int key, StatementOfPurposeDto dto);
  Task<GridEntity<StatementOfPurposeDto>> SummaryGrid(CRMGridOptions options);
}