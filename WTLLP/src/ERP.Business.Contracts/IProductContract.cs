
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IProductContract
    {
        Task<string> GetProductList(long subcategoryid);

        Task<List<ProductDetails>> Get();
        Task<ProductDetails> Get(int id);
        Task<string> GetProCategoryList();
        Task<string> GetProSubCategoryList(long pcategoryId);
        Task<string> GetMultiLevelSubCat(long catid);
        Task<string> GetBuyerList();
        Task<string> GetBuyerCodeList();
        Task<long> GetProCategoryIdByBuyerCode(string val);
        Task<string> GetIssuedByList(int buyerId);
        Task<string> GetColorList();
        Task<string> GetFinishList();
        Task<string> GetDesignerList();
        Task<string> GetGroupList();
        Task<string> GetProductSizeList();
        Task<string> GetStickerList();
        Task<string> GetTrimmingList();
        Task<string> GetbackingList();
        Task<string> GetLiningList();
        Task<string> GetLiningColorList();
        Task<string> GetCareLabelList();
        Task<string> GetEmbossmentList();
        Task<string> GetWaxList();
        Task<string> GetWashList();
        Task<string> GetQualityList();
        Task<string> GetHangtagsList();
        Task<string> GetStitchingList();
        Task<string> GetLabelList();
        Task<string> TypeList();
        Task<string> GetUnitList(); 
        Task<string> GetWidthDetails();
        Task<string> GetConstructionList();
        Task<string> GetGenderList();
        Task<string> GetHeelList(); 
        Task<string> GetGSMList();

        Task<List<ColorDetails>> GetMultiColorList();
        Task<List<ProductSizeDetails>> GetMultiSizeList();
        Task<List<ProductDetails>> GetProductList(string search);
        Task<ViewProductShow> ViewProduct(long fitemid);
   
        Task<long> GetNewProductId();
        Task<long> GetNewProductMultipleColorId();
        Task<long> GetNewProductMultipleSizeId();
        Task<List<ProductMultipleColor>> GetProductMultiColorRecord(long fitemid);
        Task<List<ProductMultipleSize>> GetProductMultiSizeRecord(long fitemid);
        Task<string> GetProductImagePath(int fitemid);
        Task<int> CheckDuplicate(string value);

        Task<string> Post(ProductDetails value);
        Task<string> Put(ProductDetails value);
        Task<string> ChangeSubCat(SearchModel value);
        Task<int> Delete(int id);

        //Move PRoduct
        //GetProductImagePath  also used in Move Product
        //Put function is in MoveProductController API
        Task<string> GetProductCodeList(long subcatId);
        Task<string> PutMoveProduct(ProductDetails value);

        Task<string> GetStyleList();

        Task<string> GetRMList(long rmscatid);
        Task<long> GetNumericItemCode(long pcatid, long pscatid);
        Task<int> CheckBarcode(string value);
        Task<int> CheckProductCode(string Code, long FitemId);

        //sample Rm
        Task<string> RawMaterialCatList();
        Task<string> RawMaterialSCatList(long rmcatid);
        Task<string> RawMaterialList(long rmscatid);
        Task<long> GetNewSampleRMId();
        Task<string> PostSampleRM(List<SampleRM> value);
        Task<string> PutSampleRM(List<SampleRM> value);
        Task<List<ViewSampleRM>> GetSampleRMRecord(long fitemid);

        Task<List<ViewProductShow>> GetProductForPrint(long id);
        Task<List<ViewDieFitemDetails>> GetProductDiePrint(long id);
        Task<List<ViewProductWithoutPhoto>> ProductWithoutPhoto();

        //product process
        Task<long> GetNewProductProcessId();
        Task<int> GetNewProductProcessSNo(long id);
        Task<List<ProductionProcessDetails>> SearchProcessChekedList(SearchModel obj);
        Task<List<ViewProductProcessDetails>> GetViewProductProcessList(long id);
        Task<string> PutProductProcess(List<ProductProcess> value);
        Task<string> RemoveProductProcess(List<ProductProcess> value);

        Task<string> GetSoleList();
        Task<string> GetZipperList();
        Task<List<ViewSampleRM>> GetSampleRMPrint(long id);
        Task<List<ViewProductShow>> ProductList(ProductSearch PS);
        Task<List<ViewProductProcessDetails>> ProductProcessList(ProductSearch PS);
        Task<string> PostCreateSubitems(ProductSearch value);

        //product file
        Task<long> GetNewId();
        Task<List<ViewProductFile>> GetProductFileRecord(long id);
        Task<string> ProductFileName(long fitemid);
        Task<string> PostProductFile(ProductFile value);
        Task<string> ProductDocumentTypeList(long fitemid);
        Task<int> DeleteDocument(long id);

        //product process pictures
        Task<long> GetNewProductProcessPicturesId(); 
        Task<string> PostProductProcessPictures(List<ProductProcessPictures> value);
        Task<ViewProductProcessDetails> GetProductProcessDetails(long fitemid, int processid);
        Task<List<ViewProductProcessPictures>> GetProductProcessPicsRecord(long fitemid, int processid);
        Task<ProductProcessPictures> GetProductProcessPicsRecord(long productprocesspicturesid);
        Task<int> DeleteProductProcessPictures(long productprocesspicturesid);

        Task<List<ViewProductDetails>> SearchproductcodeList(SearchModel obj);


        Task<List<ViewComponentDetails>> SearchComponentList(SearchModel obj);
        Task<long> GetNewProductComponentId();
        Task<int> GetNewProductComponentSNo(long id);
        Task<string> PutProductComponent(List<ProductComponent> value);
        Task<List<ViewProductComponent>> GetViewProductComponentList(long id);
        Task<string> RemoveProductComponent(List<ProductComponent> value);

        #region ProductQCPoint
        Task<int> CheckDuplicateProductQCPoint(string value);
        Task<long> GetNewProductQCPointId();
        Task<string> PostProductQCPoint(ProductQCPointMaster value);
        Task<List<ProductQCPointMaster>> GetProductQCPointList(string search);
        Task<ProductQCPointMaster> GetProductQCPoint(int id);
        Task<string> PutProductQCPoint(ProductQCPointMaster value);
        Task<int> DeleteProductQCPoint(int id);
        Task<List<ProductQCPointMaster>> SearchQCPointChekedList();
        Task<List<ViewProductQC>> GetExQCPointChekedList(long FitemId);
        Task<string> PutProductQC(List<ProductQC> value);
        Task<string> RemoveProductQC(List<ProductQC> value);
        Task<long> GetNewQCId();
        Task<string> ProductQCPointList();
        Task<List<ViewProductQC>> ProductQCList(ProductSearch PS);
        Task<List<ViewProductQC>> GetProductQCPrint(long FitemId);
        #endregion
    }
}