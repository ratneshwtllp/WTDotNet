using ERP.Business.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.Domain.Entities;
using System.Net.Http;
using ERP.Helper;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Newtonsoft.Json.Linq;


namespace ERP.Business
{
    public class RM : IRMContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public RM(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        //public async Task<int> Delete(int id)
        //{
        //    var response = await client.DeleteAsync(apiServiceUrl + "rm/Delete/" + id.ToString());
        //    var content = await response.Content.ReadAsStringAsync();

        //    return Convert.ToInt16(content);
        //}

        public async Task<string> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "rm/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }

        public async Task<List<RitemMaster>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<RitemMaster>>(content);
            return result;
        }

        public async Task<List<ViewRItemShow>> ViewRItem()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/ViewRItemShow");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRItemShow>>(content);
            return result;
        }

        public async Task<List<ViewRItemShow>> GetViewRItemShow(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetViewRItemShow/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRItemShow>>(content);
            return result;
        }

        public async Task<RitemMaster> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<RitemMaster>(content);
            return result;
        }

        public async Task<string> GetNewRMCode(int categoryId)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetNewRMCode/" + categoryId.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetRMImagePath(int ritemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetRMImagePath/" + ritemid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<long> GetNewRMId()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetNewRMId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<long> GetNewRMSuppId()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetNewRMSuppId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<long> GetNewRMCodeNumeric(int categoryId)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetNewRMCodeNumeric/" + categoryId.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public RitemMaster GetRawMaterial(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetSubCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<string> Post(RitemMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "rm", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> PutRM(RitemMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "RM/PutRM", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }


        public async Task<string> PutMoveRM(RitemMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "rm", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        //public void Post(RitemMaster value)
        //{
        //    var myContent = JsonConvert.SerializeObject(value);
        //    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        //    var byteContent = new ByteArrayContent(buffer);
        //    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //    var response = client.PostAsync(apiServiceUrl + "rm", byteContent).Result ;
        //    //var content = response.Content.ReadAsStringAsync();
        //    throw new NotImplementedException();
        //}

        public void Put(RitemMaster value)
        {
            throw new NotImplementedException();
        }

        public async Task<ViewRItemShow> RItemShow(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/RItemShow/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewRItemShow>(content);
            return result;
        }

        public async Task<string> GetCategoryList()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/RMCategory");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetSubCategoryList(int categoryId)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/RMSubCategory/" + categoryId.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetSubCategoryList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/SubCategoryList/" + search);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetRMList(int subcatId)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetRMList/" + subcatId.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetFinishMetalDetails()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/FinishMetalDetails");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetThicknessDetails()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/ThicknessDetails");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetColorDetails(string color)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/showcolor/" + color);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetShapeDetails(string shape)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/showshape/" + shape);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetSizeDetails(string size)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/showsize/" + size);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetMetalDetails(string metal)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/fillmetal/" + metal);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetStoneDetails(string stone)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/showstone/" + stone);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetUnitDetails(string unit)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/fillunit/" + unit);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetCartoonDetails(string cartoon)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/fillcartoon/" + cartoon);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetTanningDetails(string tanning)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/filltanning/" + tanning);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetQualityDetails(string quality)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/fillquality/" + quality);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetStructureDetails(string structure)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/fillstructure/" + structure);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetSelectionDetails(string selection)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/fillselection/" + selection);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetAdhesiveDetails(string adhesive)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/filladhesive/" + adhesive);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetClothDetails(string cloth)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/fillcloth/" + cloth);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetSupplierDetails()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/fillsupplier");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<SupplierDetails>> GetSupplierList()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetSupplierList");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<SupplierDetails>>(content);
            return result;
        }

        public async Task<List<RItemSupp>> GetRMSupplier(int RItemId)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetRMSupplier/" + RItemId);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<RItemSupp>>(content);
            return result;
        }

        public async Task<string> GetWidthDetails(string width)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/ShowWidth/" + width);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetGSMDetails()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/ShowGSM");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetListRMBrand(string rmb)
        {
            var response = await client.GetAsync(apiServiceUrl + "rmbrand/ListRMBrand/" + rmb);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewRItemShow>> GetRitemByCatid(long catid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetRitemByCatid/" + catid.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRItemShow>>(content);
            return result;
        }

        public async Task<string> PutRMCostPrice(List<RitemMaster> value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "rm/PutRMCostPrice", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> PutRMRate(RitemMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "rm/PutRMRate", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ViewRMSupplier>> RMSupplier()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/ViewRMSupplier");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRMSupplier>>(content);
            return result;
        }

        public async Task<List<ViewRMSupplier>> RMSupplier(long catid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/ViewRMSupplier/" + catid.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRMSupplier>>(content);
            return result;
        }

        public async Task<List<ViewRMSupplier>> RMSupplier(long rmcatid, long rmscatid, long ritemid, long supplierid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/ViewRMSupplier/" + rmcatid + "/" + rmscatid + "/" + ritemid + "/" + supplierid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRMSupplier>>(content);
            return result;
        }

        public async Task<List<ViewRItemShow>> GetRitemRecord(long rmcatid, long rmscatid, long ritemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetRitemRecord/" + rmcatid + "/" + rmscatid + "/" + ritemid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRItemShow>>(content);
            return result;
        }

        public async Task<string> GetSupplierNameList()
        {
            var response = await client.GetAsync(apiServiceUrl + "supplier/GetSupplierNameList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewShowCosting>> RMForProduct(long ritemid) //long rmcatid, long rmscatid, 
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/RMForProduct/" + ritemid); //rmcatid + "/" + rmscatid + "/" +
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewShowCosting>>(content);
            return result;
        }

        public async Task<List<ViewBelowMinimumLevel>> RMMinimumLevel()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/RMMinimumLevel");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewBelowMinimumLevel>>(content);
            return result;
        }

        public async Task<List<ViewUnusedRMList>> GetUnusedRmList()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetUnusedRmList");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewUnusedRMList>>(content);
            return result;
        }

        public async Task<List<ViewRItemShow>> RMList(RMSearch RMS)
        {
            var myContent = JsonConvert.SerializeObject(RMS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "rm/RMList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRItemShow>>(content);
            return result;
        }

        public async Task<string> GetHSNList()
        {
            var response = await client.GetAsync(apiServiceUrl + "hsn/HSNList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }


        public async Task<string> RMDocumentTypeList(long ritemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "DocumentType/RMDocumentTypeList/" + ritemid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> PostRMFile(RMFile value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "rm/PostRMFile", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
        public async Task<int> DeleteRMFile(long id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "rm/DeleteRMFile/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }
        public async Task<List<ViewRMFile>> GetRMFileRecord(long ritemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetRMFileRecord/" + ritemid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRMFile>>(content);
            return result;
        }

        public async Task<long> GetNewId()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetNewId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<ViewRMSubCategory> GetSubCategoryDetails(int subcatid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rmsubcategory/GetSubCategoryDetails/" + subcatid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewRMSubCategory>(content);
            return result;
        }

        public async Task<string> RMFileName(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/RMFileName/" + id);
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToString(content);
        }

        public async Task<string> PutRMReviewStatus(RitemMaster value)
        { 
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "rm/PutRMReviewStatus", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
        public async Task<string> GetRMList()
        {
            var response = await client.GetAsync(apiServiceUrl + "RM/GetRMList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewRMPropertiesMapping>> GetCatMapProperties(long catid)
        {
            var response = await client.GetAsync(apiServiceUrl + "RMProperties/GetCatMapProperties/" + catid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRMPropertiesMapping>>(content);
            return result;
        }

        public async Task<string> GetRMPropertiesValueList(long propertiesid)
        {
            var response = await client.GetAsync(apiServiceUrl + "RMProperties/GetRMPropertiesValueList/" + propertiesid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<long> GetNewRMChildId()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetNewRMChildId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<List<ViewRMChild>> GetRMChild(long rmid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetRMChild/" + rmid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRMChild>>(content);
            return result;
        }

        public async Task<List<QuickTestDetails>> GetQuickTestList()
        {
            var response = await client.GetAsync(apiServiceUrl + "QuickTest/GetQuickrTestList");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<QuickTestDetails>>(content);
            return result;
        }

        public async Task<long> GetNewRItemQuickTestId()
        {
            var response = await client.GetAsync(apiServiceUrl + "QuickTest/GetNewRItemQuickTestId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<List<RItemQuickTest>> GetExQuickTestList(long RItemId)
        {
            var response = await client.GetAsync(apiServiceUrl + "QuickTest/GetExQuickTestList/" + RItemId.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<RItemQuickTest>>(content);
            return result;
        }

        //public async Task<int> CheckRMCode(string code, long rmid)
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "rm/CheckRMCode/" + code + "/" + rmid.ToString());
        //    var content = await response.Content.ReadAsStringAsync();

        //    return Convert.ToInt16(content);
        //}

        public async Task<string> PutRMinProduct(List<CostingSearch> value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Costing/PutRMinProduct", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> PutRMinProductSizeWise(List<CostingSearch> value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Costing/PutRMinProductSizeWise", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        //++++++
        public async Task<long> GetNewRMPriceHistoryId()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetNewRMPriceHistoryId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> PostRMSupplierPriceHistory(List<RMSupplierPriceHistory> value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "rm/PostRMSupplierPriceHistory", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
        public async Task<List<ViewRMSupplierPriceHistory>> GetRMSupplierPriceHistory(long ritemid, long supplierid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetRMSupplierPriceHistory/" + ritemid.ToString() + "/" + supplierid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewRMSupplierPriceHistory>>(content);
            return result;
        }


        public async Task<List<ViewRMSupplierPriceHistory>> GetRMSupplierPriceHistoryList(RMSearch RMS)
        {
            var myContent = JsonConvert.SerializeObject(RMS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "rm/GetRMSupplierPriceHistoryList", byteContent);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewRMSupplierPriceHistory>>(content);
            return result;
        }
        public async Task<string> PutRMDetails(List<RitemMaster> value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "rm/PutRMDetails", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
       

        public async Task<List<ViewShowRMSizeCosting>> RMSizeForProduct(long ritemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/RMSizeForProduct/" + ritemid);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewShowRMSizeCosting>>(content);
            return result;
        }
        //---------
    }
}
