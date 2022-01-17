using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ISampleIssueContract
    {
        Task<List<SampleIssueDetails>> Get();
        Task<ViewSampleIssue> Get(int id);
        Task<List<ViewSampleIssue>> GetSampleIssueList(string search);
        Task<int> GetNewSampleIssueId();
        Task<int> GetNewIssueNo();
        Task<string> GetProCategoryList();
        Task<string> GetProSubCategoryList(long pcategoryId);
        Task<string> GetProductList(long subcategoryid);
        Task<string> GetProductDetails(long fitemid); 
        Task<string> GetSampleTypeList();
        Task<string> GetContractorList();
        Task<string> GetIssueContractorList(long ContractorID);
        Task<int> CheckDuplicate(string value);
        Task<ViewSampleRoom> GetSampleRoomDetails(long fitemid);
        Task<string> Post(SampleIssueDetails value);
        Task<string> Put(SampleIssueDetails value);
        Task<int> Delete(int id);
    }
}