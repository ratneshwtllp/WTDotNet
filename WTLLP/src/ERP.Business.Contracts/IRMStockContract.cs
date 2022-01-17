using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IRMStockContract
    {
        Task<List<ViewRMStock>> Get();
        Task<List<ViewRMStock>> Get(long id);
        Task<string> GetRMCategoryList();
        Task<string> GetRMSubCategoryList(long catid);
        Task<string> GetRMList(int scatid);
        Task<List<ViewRMStock>> GetViewRMCurrentStock(RMSearch rms);
        Task<List<ViewRMStockRackWise>> GetViewRMStockRackWise(long ritemid);
        Task<List<ViewRMStockRackWise>> GetViewRMStockRackWise(RMSearch RMS);
        Task<List<RMMovement>> GetRMMovement(RMSearch RMS);
        Task<List<RMMovement>> RMMovementPrint();
        Task<List<ViewRMLedger>> GetViewRMLedger(RMSearch rms);
        Task<double> GetViewRMLedgerOPOnDate(RMSearch rms);
        Task<string> GetStoreList();
        Task<string> GetRMList();

        Task<List<ViewRMStatus>> GetViewRMStatus(RMSearch rms);
        Task<string> CalculateRMStatus();
        Task<List<ViewWorkPlanBOMRuning>> GetViewWorkPlanBOMRuning(RMSearch rms);
        Task<List<ViewSaleOrderItemBalBOM>> GetViewSaleOrderItemBalBOM(RMSearch rms);
    }
}