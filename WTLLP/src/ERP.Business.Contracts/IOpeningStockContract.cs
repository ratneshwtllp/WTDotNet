
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IOpeningStockContract
    {
        Task<List<ViewOpStockDetails>> Get();
        Task<OpeningStockDetails> Get(int id);
        Task<List<ViewOpStockDetails>> GetViewOpStockDetails(string search);
        Task<long> GetNewOpStockId();
        Task<string> GetRMCategoryList();
        Task<string> GetRMSubCategoryList(long catid);
        Task<string> GetRMList(int scatid);
        Task<string> GetSupplierList(long ritemid);
        Task<ViewRItemShow> GetRItemShow(long ritemid);
        Task<string> GetRackList(long storeid);
        Task<List<ViewOpStockDetails>> GetOpStockDetails(long ritemid);
        Task<int> CheckDuplicate(long ritemid, long suppid, long rackid);
        Task<string> Post(OpeningStockDetails value);
        Task<string> Put(OpeningStockDetails value);
        Task<int> Delete(int opstockid);
        Task<int> GetIsBatchOfRM();
        Task<string> GetStoreList();
        Task<string> GetRMList();
        Task<string> RMSuppRate(long rmid, long suppid);
    }
}