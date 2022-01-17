
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IAdjustmentRMContract
    {
        Task<long> GetNewAdjustmentRMID();
        Task<string> GetRMCategoryList();
        Task<string> GetRMSubCategoryList(long catid);
        Task<string> GetRMList(int scatid);
        Task<string> GetSupplierList(long ritemid);
        Task<ViewRItemShow> GetRItemShow(long ritemid);
        Task<string> GetRackList();
        Task<string> Post(AdjustmentRMDetails value);
        Task<string> Delete(int adjustmentrmId);
        Task<string> GetRackNoList(long suppid, long rmid);
        Task<string> GetBatch_LotNoFromRM_Supp_Rack(long ritemid, long suppid, long rackid);
        //Task<double> GetRMRackStockFromBatchLotNo(long rowidlotid);
        Task<List<ViewAdjustmentRM>> GetViewAdjustmentRM(string search);
        Task<List<ViewAdjustmentRM>> GetViewAdjustmentRM(StoreSearch obj);

        Task<double> GetRMRackStock(long ritemid, long suppid, long rackid, string lotno);

        Task<string> GetMonthList();
        Task<string> GetYearList();
        Task<string> GetSessionYearList();
        Task<string> GetSupplierList();
        Task<string> GetRMList();
    }
}