using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IRackMasterContract
    {
        Task<List<RackMaster>> Get();
        Task<RackMaster> Get(long rackid);

        Task<string> GetRMCategoryList();
        Task<List<ViewRackMaster>> GetRackMasterList(string search);
        Task<int> GetNewRackMasterId();
        Task<string> GetNewRackNo(int categoryId);
        Task<int> GetNewCategorySNo(long categoryId);
        Task<int> CheckDuplicate(string value);
        //Task<int> CheckDuplicateEdit(string value, long id);

        Task<string> Post(RackMaster value);
        Task<string> Put(RackMaster value);
        Task<int> Delete(long rackid);
        Task<string> GetStoreList();
    }
}