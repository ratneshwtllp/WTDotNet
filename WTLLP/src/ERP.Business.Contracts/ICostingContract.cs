
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface ICostingContract
    {
        Task<ViewShowCosting> Get(long CostingID);
        
        Task<long> GetNewCostingID();
        Task<long> GetNewCostingRMID();
        Task<long> GetNewCostingElID();
        Task<long> GetNewCostingCurrencyId();
        Task<long> GetCostingId(long FitemId, int sizeid);

        Task<string> GetProCategoryList();
        Task<string> GetProSubCategoryList(long pcategoryId); 
        Task<string> GetProductList(long subcategoryid); // subcategoryid 0 for all product
        Task<string> GetProductDetails(long fitemid);

        Task<string> GetRMCategoryList();
        Task<string> GetRMSubCategoryList(long categoryId);
        Task<string> GetRMList(long subcatId);
        Task<string> GetRMList();
        Task<string> GetCostingRMList(long ritemid);
        
        Task<string> GetRMDetails(long ritemid);

        Task<string> PutMatReviewedStatus(CostingMaster value);
        Task<string> CostingElementCat();

        Task<string> Post(CostingMaster value);
        Task<string> Put(CostingMaster value);
        Task<string> PutCostingWithCurrentRMPrice(CostingMaster value);

        Task<string> GetSizeForCopy(long fitemid);
        Task<string> GetSizeForCasting(long fitemid);
        Task<string> GetSizeForAnlysis(long fitemid);

        Task<List<ViewCostingRM>> GetCostingRM(long id);
        Task<List<ViewCostingEL>> GetCostingEL(long id);

        Task<List<ViewShowCosting>> ViewShowCostingSearch(string search);
        Task<List<ViewShowCosting>> CostingList(CostingSearch CS);
       

        Task<string> GetOrderNoList();
        Task<List<ViewShowCosting>> ViewShowCosting();
        Task<List<ViewOrderBOM>> GetOrderBOM(long ordermasterid);
        Task<string> GetOrderNoList(long buyerid);
        Task<List<ViewOrderBOMSUM>> GetOrderBOMSum(long ordermasterid);
        Task<List<ViewItemBOM>> GetItemBOMDetails(long id);

        Task<string> DeleteMaterial(long costingid);

        Task<List<ViewRMSupplier>> GetSupplierRecord(long rmid);
        Task<string> SaveTempPOChild(List<TempPOChild> value);
        Task<string> DeleteTempPOChild();

        void Delete(int id);

        //workplan bom
        Task<string> GetPlanNoList();
        Task<List<ViewWorkPlanBOM>> GetWorkPlanRMDetails(long planid);

        //Reserve stock
        Task<List<ReserveStockMaster>> Get();
        Task<List<ReserveStockMaster>> GetList(string search);
        Task<long> GetNewReserveStockId();
        Task<string> PostReserveStock(ReserveStockMaster value);
        Task<string> DeleteReserveStock(long reserveid);
        Task<List<ViewReserveStock>> GetReserveReportList(string search);
        Task<string> PutReserveStatus(long reserveid);

        //CostingCurrencyDetails

        Task<List<ViewCostingCurrencyDetails>> GetCostCurrDetails();
        Task<List<ViewCostingCurrencyEdit>> GetCostCurrEdit(long costingid);

        //Task<List<ViewCostingRM>> GetCostForPrint(long id);
        Task<List<ViewCostingRMPrint>> GetCostForPrint(long id);
        Task<ViewCostingPendingReport> GetCostingPending(long id);
        Task<List<ViewCostingPendingReport>> GetViewCostingPendingReport(string search);
        Task<List<ViewCostingPendingReport>> CostingPendingReport(CostingSearch CS);

        Task<List<ViewCostingRM>> GetCostingRMIsCompFilter(long id, long ritemid);

        Task<string> GetCompGroupList();
        Task<string> GetComponentList();
        Task<string> GetComponentList(long id);
        Task<long> GetNewCostingRMComponentID();
        Task<List<ViewCostingRMComponent>> GetCostingRMComponent(long CostingId, long RitemId);
        Task<string> SaveComponent(CostingMaster value);
        Task<string> GetBuyerList();
         
        Task<string> PostNewMaterial(CostingMaster value); 
        Task<string> PutNewMaterial(CostingMaster value);
        Task<string> GetSupplier(long id);
        Task<List<ViewCostingRMComponent>> GetCostingRMComponent(long CostingId);
        Task<List<ViewCostingSizeRM>> GetViewCostingSizeRM(long CostingId);
        Task<List<ViewCostingSizeRM>> GetViewCostingSizeRM(long CostingId, int SizeId);
        Task<string> PutCompPhoto(CostingRMComponent value);

        
        Task<List<ViewShowCosting>> ViewShowMaterialSearch(CostingSearch CS);

        Task<string> ProcessListForAnlysis();

        Task<List<ViewOrderBOM>> OrderItemInBOM(CostingSearch CS);
        Task<List<ProductMultipleSize>> GetProductMultipleSize(long fitemid);

        Task<long> GetRMComboID();
        Task<long> GetNewRMComboChildID();
        Task<string> PostRMCombo(RMComboMaster value);
        Task<string> PutRMCombo(RMComboMaster value);
        Task<string> GetComboList();
        Task<long> GetRMComboId(long ComboID);
        Task<List<ViewRMCombo>> GetRMCombo(long comboid);
        //Task<long> GetNewCostingFitemID(long Fitemid);

        Task<long> GetNewCostingFitemID(long Fitemid);
        Task<string> SaveUseCombo(List<CostingMaster> value);

        Task<List<ViewCostingProductForRMCombo>> GetViewCostingProductForRMCombo(long BelongTo); 
        //Task<string> GetProductListOfCategory(long masterbelongto); // product of category
    }
}