
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IMoveRMStockContract
    {
        Task<long> GetNewMoveRMStockID();
        Task<string> GetRMCategoryList();
        Task<string> GetRMSubCategoryList(long catid);
        Task<string> GetRMList(int scatid);
        Task<string> GetSupplierList(long ritemid);
        Task<ViewRItemShow> GetRItemShow(long ritemid);
        Task<string> GetRackList();
        Task<int> CheckDuplicate(long ritemid, long suppid, long rackid);
        Task<string> Post(Move_RMStock value);
        Task<int> Delete(int movermstockId);
        Task<string> GetRackNoList(long suppid, long rmid);
        Task<List<ViewMoveRMStock>> GetViewMoveRMStock(string search);
        Task<List<ViewMoveRMStock>> GetViewMoveRMStock(StoreSearch obj);
        Task<ViewMoveRMStock> Get(int id);
        Task<double> GetRMRackStock(long ritemid, long suppid, long rackid, string lotno);

        Task<string> GetMonthList();
        Task<string> GetYearList();
        Task<string> GetSessionYearList();
        Task<string> GetSupplierList();
        Task<string> GetContractorList();
        Task<double> GetRMRackStockFromBatchLotNo(long rowidlotid);
        Task<string> GetBatch_LotNoFromRM_Supp_Rack(long ritemid, long suppid, long rackid);

        Task<string> GetRMList();

    }
}