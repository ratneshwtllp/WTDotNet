using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ISampleChangeRequestContract
    {
        Task<List<Sample_Request>> Get();
        Task<ViewSample_Request> Get(int id);
        Task<List<ViewSample_Request>> GetSampleChangeRequestList(string search);
        Task<string> GetProCategoryList();
        Task<string> GetProSubCategoryList(long pcategoryId);
        Task<string> GetProductList(long subcategoryid);
        Task<string> GetProductDetails(long fitemid); 
        Task<int> GetNewSampleChangeRequestId();
        Task<int> CheckDuplicate(string value);
        Task<string> Post(Sample_Request value);
        Task<string> Put(Sample_Request value);
        Task<int> Delete(int id);
        Task<string> GetChangeRequestStatus(long Request_Id);
        Task<List<ViewSample_Request>> GetChangesCompleteList(string search);
    }
}