
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IRMContract
    {
        //Task<int> Delete(int id);
        Task<string> Delete(int id);
        Task<List<RitemMaster>> Get();
        Task<List<ViewRItemShow>> ViewRItem();
        Task<List<ViewRItemShow>> GetViewRItemShow(string search);
        Task<RitemMaster> Get(int id);
        Task<string> GetNewRMCode(int categoryId);
        Task<string> GetRMImagePath(int ritemid);
        Task<long> GetNewRMId();
        Task<long> GetNewRMSuppId();
        Task<long> GetNewRMCodeNumeric(int categoryId);
        Task<string> GetCategoryList();
        Task<string> GetSubCategoryList(int categoryId);
        Task<string> GetSubCategoryList(string search);
        Task<string> GetRMList(int subcatId);
        Task<string> GetFinishMetalDetails();
        Task<string> GetThicknessDetails();
        Task<string> GetColorDetails(string color);
        Task<string> GetShapeDetails(string shape);
        Task<string> GetSizeDetails(string size);
        Task<string> GetMetalDetails(string metal);
        Task<string> GetStoneDetails(string stone);
        Task<string> GetUnitDetails(string unit);

        Task<string> GetCartoonDetails(string cartoon);
        Task<string> GetTanningDetails(string tanning);
        Task<string> GetQualityDetails(string quality);
        Task<string> GetStructureDetails(string structure);
        Task<string> GetSelectionDetails(string selection);
        Task<string> GetAdhesiveDetails(string adhesive);
        Task<string> GetClothDetails(string cloth);
        Task<string> GetSupplierDetails();
        Task<List<SupplierDetails>> GetSupplierList();
        Task<List<RItemSupp>> GetRMSupplier(int RItemId);
        Task<string> GetSupplierNameList();

        Task<string> Post(RitemMaster value);
        Task<string> PutRM(RitemMaster value);
        Task<string> PutMoveRM(RitemMaster value);
        void Put(RitemMaster value);

        Task<ViewRItemShow> RItemShow(int id);
        Task<List<ViewRMSupplier>> RMSupplier(long rmcatid, long rmscatid, long ritemid, long supplierid);
        Task<List<ViewRMSupplier>> RMSupplier(long catid);
        Task<List<ViewRMSupplier>> RMSupplier();

        Task<string> GetWidthDetails(string width);
        Task<string> GetGSMDetails();
        Task<string> GetListRMBrand(string rmb);

        Task<List<ViewRItemShow>> GetRitemByCatid(long catid);
        Task<string> PutRMRate(RitemMaster value);
        Task<string> PutRMCostPrice(List<RitemMaster> value);

        Task<List<ViewRItemShow>> GetRitemRecord(long rmcatid, long rmscatid, long ritemid);

        Task<List<ViewShowCosting>> RMForProduct(long ritemid);
        Task<List<ViewBelowMinimumLevel>> RMMinimumLevel();
        Task<List<ViewUnusedRMList>> GetUnusedRmList();
        Task<List<ViewRItemShow>> RMList(RMSearch RMS);
        Task<string> GetHSNList();

        Task<string> RMDocumentTypeList(long ritemid);
        Task<List<ViewRMFile>> GetRMFileRecord(long id);
        Task<long> GetNewId();//RMFile ID
        Task<string> PostRMFile(RMFile value);
        Task<int> DeleteRMFile(long id);
        Task<ViewRMSubCategory> GetSubCategoryDetails(int subcatid);
        Task<string> RMFileName(long Id);
        Task<string> PutRMReviewStatus(RitemMaster value);

        Task<string> GetRMList();

        Task<string> GetRMPropertiesValueList(long propertiesid);
        Task<List<ViewRMPropertiesMapping>> GetCatMapProperties(long catid);
        Task<List<ViewRMChild>> GetRMChild(long RItem_Id);

        Task<long> GetNewRMChildId();


        Task<List<QuickTestDetails>> GetQuickTestList();
        Task<long> GetNewRItemQuickTestId();
        Task<List<RItemQuickTest>> GetExQuickTestList(long RItemId);

        //Task<int> CheckRMCode(string code, long rmid);
        //Task<long> GetRMCatRMId(string Name);
        //Task<long> GetRMSubCatRMId(string Name, long catid);
        //Task<int> GetColorId(string ClName);
        //Task<int> GetSizeId(string Size);

        //Task<string> PutRMinProduct(CostingSearch value);
        Task<string> PutRMinProduct(List<CostingSearch> value);
        //Task<string> PutCostingSizeRM(CostingSearch value);
        Task<string> PutRMinProductSizeWise(List<CostingSearch> value);

        Task<long> GetNewRMPriceHistoryId();
        Task<string> PostRMSupplierPriceHistory(List<RMSupplierPriceHistory> value);
        Task<List<ViewRMSupplierPriceHistory>> GetRMSupplierPriceHistory(long ritemid, long supplierid);
        Task<List<ViewRMSupplierPriceHistory>> GetRMSupplierPriceHistoryList(RMSearch RMS);
        Task<string> PutRMDetails(List<RitemMaster> value);

        Task<List<ViewShowRMSizeCosting>> RMSizeForProduct(long ritemid);
    }
}