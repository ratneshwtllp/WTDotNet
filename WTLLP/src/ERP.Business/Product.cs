using ERP.Business.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.Domain.Entities;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace ERP.Business
{
    public class Product : IProductContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public Product(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<List<ProductDetails>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "product");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ProductDetails>>(content);
            return result;
        }

        public async Task<ProductDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ProductDetails>(content);
            return result;
        }

        public async Task<string> GetProCategoryList()
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductCategory/GetProCategoryList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<long> GetProCategoryIdByBuyerCode(string val)
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductCategory/GetProCategoryIdByBuyerCode/?val=" + val);
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> GetProSubCategoryList(long pcategoryId)
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductSubCategory/GetProSubCategoryList/" + pcategoryId.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetMultiLevelSubCat(long catid)
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductSubCategory/GetMultiLevelSubCat/" + catid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetBuyerList()
        {
            var response = await client.GetAsync(apiServiceUrl + "buyer/GetBuyerNameList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetBuyerCodeList()
        {
            var response = await client.GetAsync(apiServiceUrl + "buyer/GetBuyerCodeList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetIssuedByList(int buyerId)
        {
            var response = await client.GetAsync(apiServiceUrl + "IssuedBy/GetIssuedByList/" + buyerId.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetColorList()
        {
            var response = await client.GetAsync(apiServiceUrl + "color/ColorList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetFinishList()
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetFinishList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetDesignerList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Designer/DesignerList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetGroupList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Group/GroupList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetProductSizeList()
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetProductSizeList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetStickerList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Sticker/GetStickerList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetTrimmingList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Trimming/TrimmingList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetbackingList()
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetbackingList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetLiningList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Lining/LiningList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetLiningColorList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Color/ColorList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetCareLabelList()
        {
            var response = await client.GetAsync(apiServiceUrl + "CareLabel/GetCareLabelList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetEmbossmentList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Embossment/EmbossmentList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetWaxList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Wax/WaxList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetWashList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Wash/WashList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetQualityList()
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetQualityList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetHangtagsList()
        {
            var response = await client.GetAsync(apiServiceUrl + "HandTag/GetHangtagsList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetStitchingList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Stitching/StitchingList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetLabelList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Label/LabelList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ProductDetails>> GetProductList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetProductList/" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ProductDetails>>(content);
            return result;
        }



        public async Task<ViewProductShow> ViewProduct(long fitemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/ViewProduct/" + fitemid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewProductShow>(content);
            return result;
        }

        public async Task<long> GetNewProductId()
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetNewProductId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<long> GetNewProductMultipleColorId()
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetNewProductMultipleColorId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<long> GetNewProductMultipleSizeId()
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetNewProductMultipleSizeId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<List<ProductMultipleColor>> GetProductMultiColorRecord(long fitemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetProductMultiColorRecord/" + fitemid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ProductMultipleColor>>(content);
            return result;
        }

        public async Task<List<ProductMultipleSize>> GetProductMultiSizeRecord(long fitemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetProductMultiSizeRecord/" + fitemid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ProductMultipleSize>>(content);
            return result;
        }

        public async Task<string> GetProductImagePath(int fitemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetProductImagePath/" + fitemid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<int> CheckDuplicate(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/CheckDuplicate/?value=" + search.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<string> Post(ProductDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "product", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(ProductDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "product", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> ChangeSubCat(SearchModel value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "product/ChangeSubCat", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "product/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        //Move PRoduct
        //GetRMImagePath it is also used in Move Product and defined above.

        public async Task<string> GetProductCodeList(long subcatId)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetProductCodeList/" + subcatId);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> PutMoveProduct(ProductDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "moveproduct", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> GetStyleList()
        {
            var response = await client.GetAsync(apiServiceUrl + "style/GetStyleList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetRMList(long rmscatid)
        {
            var response = await client.GetAsync(apiServiceUrl + "RM/GetRMList/" + rmscatid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<long> GetNumericItemCode(long pcatid, long pscatid)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetNumericItemCode/" + pcatid + "/" + pscatid);
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<int> CheckBarcode(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/CheckBarcode/?value=" + search.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<int> CheckProductCode(string Code, long FitemId)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/CheckProductCode/"+ FitemId + "?Code=" + Code.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        //product file
        public async Task<long> GetNewId()
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetNewId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> ProductFileName(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/ProductFileName/" + id);
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToString(content);
        }

        public async Task<string> PostProductFile(ProductFile value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "product/PostProductFile", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> TypeList()
        {
            var response = await client.GetAsync(apiServiceUrl + "product/TypeList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ColorDetails>> GetMultiColorList()
        {
            var response = await client.GetAsync(apiServiceUrl + "color");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ColorDetails>>(content);
            return result;
        }

        public async Task<List<ProductSizeDetails>> GetMultiSizeList()
        {
            var response = await client.GetAsync(apiServiceUrl + "productsize");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ProductSizeDetails>>(content);
            return result;
        }

        public async Task<string> RawMaterialCatList()
        {
            var response = await client.GetAsync(apiServiceUrl + "rmcategory/GetList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> RawMaterialSCatList(long rmcatid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rmsubcategory/GetList/" + rmcatid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> RawMaterialList(long rmscatid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetRMList/" + rmscatid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetUnitList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Unit/UnitList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetWidthDetails()
        {
            var response = await client.GetAsync(apiServiceUrl + "Width/GetWidthDetails");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetConstructionList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Construction/GetListConstruction");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetGenderList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Gender/GetListGender");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetHeelList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Heel/GetListHeel");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetGSMList()
        {
            var response = await client.GetAsync(apiServiceUrl + "GSM/GetListGSM");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<long> GetNewSampleRMId()
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetNewSampleRMId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> PostSampleRM(List<SampleRM> value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "product/PostSampleRM", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> PutSampleRM(List<SampleRM> value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "product/PutSampleRM", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ViewSampleRM>> GetSampleRMRecord(long fitemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetSampleRMRecord/" + fitemid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewSampleRM>>(content);
            return result;
        }

        public async Task<List<ViewProductShow>> GetProductForPrint(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "Product/GetProductForPrint/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProductShow>>(content);
            return result;
        }

        public async Task<List<ViewDieFitemDetails>> GetProductDiePrint(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "Product/GetProductDiePrint/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewDieFitemDetails>>(content);
            return result;
        }

        public async Task<List<ViewProductWithoutPhoto>> ProductWithoutPhoto()
        {
            var response = await client.GetAsync(apiServiceUrl + "Product/ProductWithoutPhoto");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProductWithoutPhoto>>(content);
            return result;
        }

        public async Task<List<ProductionProcessDetails>> SearchProcessChekedList(SearchModel obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Product/SearchProcessList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ProductionProcessDetails>>(content);
            return result;
        }

        public async Task<long> GetNewProductProcessId()
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetNewProductProcessId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> PutProductProcess(List<ProductProcess> value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "product/PutProductProcess", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> RemoveProductProcess(List<ProductProcess> value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "product/RemoveProductProcess", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ViewProductProcessDetails>> GetViewProductProcessList(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "Product/GetViewProductProcessList/" + id);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProductProcessDetails>>(content);
            return result;
        }

        public async Task<int> GetNewProductProcessSNo(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetNewProductProcessSNo/" + id);
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt32(content);
        }

        public async Task<string> GetSoleList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Sole/SoleList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetZipperList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Zipper/ZipperList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }


        public async Task<List<ViewSampleRM>> GetSampleRMPrint(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "Product/GetSampleRMPrint/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewSampleRM>>(content);
            return result;
        }

        //public async Task<List<ViewProductShow>> GetViewProduct(string search)
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "product/GetViewProduct/?search=" + search);
        //    var content = await response.Content.ReadAsStringAsync();
        //    var result = JsonConvert.DeserializeObject<List<ViewProductShow>>(content);
        //    return result;
        //}

        //public async Task<List<ViewProductShow>> GetViewProduct(string search, long FItemCategory_ID)
        //{
        //    var responsecat = await client.GetAsync(apiServiceUrl + "product/GetViewProductSearch/?search=" + search + "&id=" + FItemCategory_ID);
        //    var contentcat = await responsecat.Content.ReadAsStringAsync();
        //    var resultcat = JsonConvert.DeserializeObject<List<ViewProductShow>>(contentcat);
        //    return resultcat;
        //}

        public async Task<List<ViewProductShow>> ProductList(ProductSearch PS)
        {
            var myContent = JsonConvert.SerializeObject(PS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Product/ProductList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProductShow>>(content);
            return result;
        }

        public async Task<List<ViewProductProcessDetails>> ProductProcessList(ProductSearch PS)
        {
            var myContent = JsonConvert.SerializeObject(PS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Product/ProductProcessList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProductProcessDetails>>(content);
            return result;
        }

        public async Task<List<ViewProductFile>> GetProductFileRecord(long fitemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetProductFileRecord/" + fitemid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProductFile>>(content);
            return result;
        }
          
        public async Task<string> ProductDocumentTypeList(long fitemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "DocumentType/ProductDocumentTypeList/" + fitemid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<int> DeleteDocument(long id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "product/DeleteDocument/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<string> GetProductList(long subcategoryid)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetProductList/" + subcategoryid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> PostCreateSubitems(ProductSearch value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "product/PostCreateSubitems", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        #region Product Process Pictures
        public async Task<long> GetNewProductProcessPicturesId()
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetNewProductProcessPicturesId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> PostProductProcessPictures(List<ProductProcessPictures> value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "product/PostProductProcessPictures", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<ViewProductProcessDetails> GetProductProcessDetails(long fitemid, int processid)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetProductProcessDetails/" + fitemid + "/" + processid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewProductProcessDetails>(content);
            return result;
        }

        public async Task<List<ViewProductProcessPictures>> GetProductProcessPicsRecord(long fitemid, int processid)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetProductProcessPicsRecord/" + fitemid + "/" + processid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProductProcessPictures>>(content);
            return result;
        }

        public async Task<ProductProcessPictures> GetProductProcessPicsRecord(long productprocesspicturesid)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetProductProcessPicsRecord/" + productprocesspicturesid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ProductProcessPictures>(content);
            return result;
        }

        public async Task<int> DeleteProductProcessPictures(long productprocesspicturesid)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "product/DeleteProductProcessPictures/" + productprocesspicturesid);
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }
        #endregion

        public async Task<List<ViewProductDetails>> SearchproductcodeList(SearchModel obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Product/SearchproductcodeList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProductDetails>>(content);
            return result;
        }


        //++++++++++++++++++++++++
        public async Task<List<ViewComponentDetails>> SearchComponentList(SearchModel obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Component/SearchComponentList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewComponentDetails>>(content);
            return result;
        }

        public async Task<long> GetNewProductComponentId()
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetNewProductComponentId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }
        public async Task<int> GetNewProductComponentSNo(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetNewProductComponentSNo/" + id);
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt32(content);
        }

        public async Task<string> PutProductComponent(List<ProductComponent> value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "product/PutProductComponent", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ViewProductComponent>> GetViewProductComponentList(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "Product/GetViewProductComponentList/" + id);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProductComponent>>(content);
            return result;
        }

        public async Task<string> RemoveProductComponent(List<ProductComponent> value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "product/RemoveProductComponent", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        #region ProductQCPoint

        public async Task<int> CheckDuplicateProductQCPoint(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/CheckDuplicateProductQCPoint/?value=" + search.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<long> GetNewProductQCPointId()
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetNewProductQCPointId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> PostProductQCPoint(ProductQCPointMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "product/PostProductQCPoint", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ProductQCPointMaster>> GetProductQCPointList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "Product/GetProductQCPointList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ProductQCPointMaster>>(content);
            return result;
        }

        public async Task<ProductQCPointMaster> GetProductQCPoint(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "Product/GetProductQCPoint/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ProductQCPointMaster>(content);
            return result;
        }

        public async Task<string> PutProductQCPoint(ProductQCPointMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "product/PutProductQCPoint", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<int> DeleteProductQCPoint(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "product/DeleteProductQCPoint/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }
        
        public async Task<List<ProductQCPointMaster>> SearchQCPointChekedList()
        {
            var response = await client.GetAsync(apiServiceUrl + "product/SearchQCPointChekedList");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ProductQCPointMaster>>(content);
            return result;
        }

        public async Task<List<ViewProductQC>> GetExQCPointChekedList(long FitemId)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetExQCPointChekedList/" + FitemId);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProductQC>>(content);
            return result;
        }

        public async Task<string> PutProductQC(List<ProductQC> value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "product/PutProductQC", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> RemoveProductQC(List<ProductQC> value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "product/RemoveProductQC", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<long> GetNewQCId()
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetNewQCId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> ProductQCPointList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Product/ProductQCPointList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewProductQC>> ProductQCList(ProductSearch PS)
        {
            var myContent = JsonConvert.SerializeObject(PS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Product/ProductQCList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProductQC>>(content);
            return result;
        }

        public async Task<List<ViewProductQC>> GetProductQCPrint(long FitemId)
        {
            var response = await client.GetAsync(apiServiceUrl + "Product/GetProductQCPrint/" + FitemId.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProductQC>>(content);
            return result;
        }

        #endregion
    }
}
