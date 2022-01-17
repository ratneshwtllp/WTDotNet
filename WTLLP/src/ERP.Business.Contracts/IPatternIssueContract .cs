using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IPatternIssueContract
    {
        Task<List<PatternIssueDetails>> Get();
        Task<ViewPatternIssueDetails> Get(int id);
        Task<List<ViewPatternIssueDetails>> GetPatternIssueList(string search);
        Task<int> GetNewPatternIssueId();
        Task<int> GetNewPatternIssueNo();
        Task<string> GetProCategoryList();
        Task<string> GetProSubCategoryList(long pcategoryId);
        Task<string> GetProductList(long subcategoryid);
        Task<string> GetProductDetails(long fitemid); 
        Task<string> GetContractorList();
        Task<string> GetIssueContractorList(long ContractorID);
        Task<int> CheckDuplicate(string value);
        Task<ViewPatternRoomDetails> GetPatternRoomDetails(long fitemid);
        Task<string> Post(PatternIssueDetails value);
        Task<string> Put(PatternIssueDetails value);
        Task<int> Delete(int id);
    }
}