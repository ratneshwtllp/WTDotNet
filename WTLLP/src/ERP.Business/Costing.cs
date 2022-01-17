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
    public class Costing : ICostingContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public Costing(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<ViewShowCosting> Get(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ViewShowCosting>(content);
            return result;
        }

        public async Task<long> GetNewCostingID()
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetNewCostingID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<long> GetNewCostingRMID()
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetNewCostingRMID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content); ;
        }
        public async Task<long> GetNewCostingElID()
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetNewCostingElID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }
        public async Task<string> GetProCategoryList()
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductCategory/GetProCategoryList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetProSubCategoryList(long pcategoryId)
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductSubCategory/GetProSubCategoryList/" + pcategoryId.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetProductList(long subcategoryid)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetProductList/" + subcategoryid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetProductDetails(long fitemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/ViewProduct/" + fitemid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetRMCategoryList()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/RMCategory");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetRMSubCategoryList(long categoryId)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/RMSubCategory/" + categoryId.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetRMList(long subcatId)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetRMList/" + subcatId.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetRMList()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetRMList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetCostingRMList(long ritemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetCostingRMList/" + ritemid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetRMDetails(long ritemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/RItemShow/" + ritemid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> PutMatReviewedStatus(CostingMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Costing/PutMatReviewedStatus", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> CostingElementCat()
        {
            var response = await client.GetAsync(apiServiceUrl + "CostingElement/CostingElementCat");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewShowCosting>> ViewShowCostingSearch(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/ViewShowCostingSearch/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewShowCosting>>(content);
            return result;
        }

        public async Task<List<ViewShowCosting>> CostingList(CostingSearch CS)
        {
            var myContent = JsonConvert.SerializeObject(CS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Costing/CostingList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewShowCosting>>(content);
            return result;
        }

        public async Task<List<ViewShowCosting>> ViewShowCosting()
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/ViewShowCosting");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewShowCosting>>(content);
            return result;
        }

        public async Task<List<ViewCostingRM>> GetCostingRM(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetCostingRM/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewCostingRM>>(content);
            return result;
        }

        public async Task<List<ViewCostingEL>> GetCostingEL(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetCostingEL/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewCostingEL>>(content);
            return result;
        }

        public async Task<string> Post(CostingMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Costing", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(CostingMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Costing", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> PutCostingWithCurrentRMPrice(CostingMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Costing/PutCostingWithCurrentRMPrice", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<long> GetCostingId(long fitemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetCostingId/" + fitemid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> GetMaterialProductList(long belongto)
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetMaterialProductList/" + belongto.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }


        public async Task<string> GetOrderNoList()
        {
            var response = await client.GetAsync(apiServiceUrl + "saleorder/GetOrderNoList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetOrderNoList(long buyerid)
        {
            var response = await client.GetAsync(apiServiceUrl + "saleorder/GetOrderNoList/" + buyerid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewOrderBOM>> GetOrderBOM(long ordermasterid)
        {
            var response = await client.GetAsync(apiServiceUrl + "costing/GetOrderBOM/" + ordermasterid.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewOrderBOM>>(content);
            return result;
        }

        public async Task<List<ViewOrderBOMSUM>> GetOrderBOMSum(long ordermasterid)
        {
            var response = await client.GetAsync(apiServiceUrl + "costing/GetOrderBOMSum/" + ordermasterid.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewOrderBOMSUM>>(content);
            return result;
        }

        public async Task<string> DeleteMaterial(long costingid)
        {
            var response = await client.GetAsync(apiServiceUrl + "costing/DeleteMaterial/" + costingid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewItemBOM>> GetItemBOMDetails(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetItemBOMDetails/" + id);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewItemBOM>>(content);
            return result;
        }

        public async Task<List<ViewRMSupplier>> GetSupplierRecord(long rmid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetSupplierRecord/" + rmid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRMSupplier>>(content);
            return result;
        }

        public async Task<string> DeleteTempPOChild()
        {
            var response = await client.GetAsync(apiServiceUrl + "costing/DeleteTempPOChild");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> SaveTempPOChild(List<TempPOChild> value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "costing/SaveTempPOChild", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetPlanNoList()
        {
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetPlanNoList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewWorkPlanBOM>> GetWorkPlanRMDetails(long planid)
        {
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetWorkPlanRMDetails/" + planid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewWorkPlanBOM>>(content);
            return result;
        }

        //Reserve Stock
        public async Task<List<ReserveStockMaster>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "reservestock");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ReserveStockMaster>>(content);
            return result;
        }
        //public async Task<ReserveStockMaster> Get(long reserveid)
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "reservestock/" + reserveid);
        //    var content = await response.Content.ReadAsStringAsync();

        //    var result = JsonConvert.DeserializeObject<ReserveStockMaster>(content);
        //    return result;
        //}
        public async Task<List<ReserveStockMaster>> GetList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "reservestock/GetList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ReserveStockMaster>>(content);
            return result;
        }
        public async Task<long> GetNewReserveStockId()
        {
            var response = await client.GetAsync(apiServiceUrl + "reservestock/GetNewReserveStockId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }
        public async Task<string> PostReserveStock(ReserveStockMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "reservestock", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
        //public async Task<string> PutReserveStock(ReserveStockMaster value)
        //{
        //    var myContent = JsonConvert.SerializeObject(value);
        //    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        //    var byteContent = new ByteArrayContent(buffer);
        //    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //    var response = await client.PutAsync(apiServiceUrl + "rackmaster", byteContent);
        //    var content = response.Content.ReadAsStringAsync();
        //    return (content.Result);
        //}
        public async Task<string> DeleteReserveStock(long reserveid)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "reservestock/Delete/" + reserveid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewReserveStock>> GetReserveReportList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "reservestock/GetList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewReserveStock>>(content);
            return result;
        }

        public async Task<string> PutReserveStatus(long reserveid)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "reservestock/PutReserveStatus/" + reserveid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        //CostingCurrencyDetails
        public async Task<long> GetNewCostingCurrencyId()
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetNewCostingCurrencyId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<List<ViewCostingCurrencyDetails>> GetCostCurrDetails()
        {
            var response = await client.GetAsync(apiServiceUrl + "costing/GetCostCurrDetails");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewCostingCurrencyDetails>>(content);
            return result;
        }

        public async Task<List<ViewCostingCurrencyEdit>> GetCostCurrEdit(long costingid)
        {
            var response = await client.GetAsync(apiServiceUrl + "costing/GetCostCurrEdit/" + costingid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewCostingCurrencyEdit>>(content);
            return result;
        }

        //public async Task<List<ViewCostingRM>> GetCostForPrint(long id)
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "Costing/GetCostForPrint/" + id.ToString());
        //    var content = await response.Content.ReadAsStringAsync();

        //    var result = JsonConvert.DeserializeObject<List<ViewCostingRM>>(content);
        //    return result;
        //}

        public async Task<List<ViewCostingRMPrint>> GetCostForPrint(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetCostForPrint/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewCostingRMPrint>>(content);
            return result;
        }

        public async Task<ViewCostingPendingReport> GetCostingPending(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetCostingPending/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewCostingPendingReport>(content);
            return result;
        }

        public async Task<List<ViewCostingPendingReport>> GetViewCostingPendingReport(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetViewCostingPendingReport/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewCostingPendingReport>>(content);
            return result;
        }

        public async Task<List<ViewCostingPendingReport>> CostingPendingReport(CostingSearch CS)
        {
            var myContent = JsonConvert.SerializeObject(CS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Costing/CostingPendingReport", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewCostingPendingReport>>(content);
            return result;
        }

        public async Task<string> GetSizeForCasting(long fitemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetSizeForCasting/" + fitemid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetSizeForAnlysis(long fitemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetSizeForAnlysis/" + fitemid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<long> GetCostingId(long FitemId, int sizeid)
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetCostingId/" + FitemId + "/" + sizeid);
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> GetSizeForCopy(long fitemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetSizeForCopy/" + fitemid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewCostingRM>> GetCostingRMIsCompFilter(long id, long ritemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetCostingRMIsCompFilter/" + id + "/" + ritemid);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewCostingRM>>(content);
            return result;
        }


        public async Task<string> GetCompGroupList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetCompGroupList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetComponentList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetComponentList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetComponentList(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetComponentList/" + id);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<long> GetNewCostingRMComponentID()
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetNewCostingRMComponentID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<List<ViewCostingRMComponent>> GetCostingRMComponent(long CostingId, long RitemId)
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetCostingRMComponent/" + CostingId + "/" + RitemId);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewCostingRMComponent>>(content);
            return result;
        }

        public async Task<string> SaveComponent(CostingMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Costing/SaveComponent", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> GetBuyerList()
        {
            var response = await client.GetAsync(apiServiceUrl + "brand/GetBuyerList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> PostNewMaterial(CostingMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Costing/PostNewMaterial", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> PutNewMaterial(CostingMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Costing/PutNewMaterial", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> GetSupplier(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "supplier/GetSupplier/" + id);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewCostingRMComponent>> GetCostingRMComponent(long CostingId)
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetCostingRMComponent/" + CostingId);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewCostingRMComponent>>(content);
            return result;
        }

        public async Task<List<ViewCostingSizeRM>> GetViewCostingSizeRM(long CostingId)
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetViewCostingSizeRM/" + CostingId);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewCostingSizeRM>>(content);
            return result;
        }

        public async Task<List<ViewCostingSizeRM>> GetViewCostingSizeRM(long CostingId, int SizeId)
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetViewCostingSizeRM/" + CostingId +"/"+ SizeId);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewCostingSizeRM>>(content);
            return result;
        }

        public async Task<string> PutCompPhoto(CostingRMComponent value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Costing/PutCompPhoto", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ViewShowCosting>> ViewShowMaterialSearch(CostingSearch CS)
        {
            var myContent = JsonConvert.SerializeObject(CS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Costing/ViewShowMaterialSearch", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewShowCosting>>(content);
            return result;
        }

        public async Task<string> ProcessListForAnlysis()
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductionProcess/ProcessListForAnlysis");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewOrderBOM>> OrderItemInBOM(CostingSearch CS)
        {
            var myContent = JsonConvert.SerializeObject(CS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Costing/OrderItemInBOM", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewOrderBOM>>(content);
            return result;
        }

        public async Task<List<ProductMultipleSize>> GetProductMultipleSize(long fitemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "Product/GetProductMultiSizeRecord/" + fitemid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ProductMultipleSize>>(content);
            return result;
        }

        public async Task<List<ViewCostingProductForRMCombo>> GetProductListOfCategory(long MasterBelongTo)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetProductListOfCategory/" + MasterBelongTo.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewCostingProductForRMCombo>>(content);
            return result;
        }

        #region RMCOmbo
        public async Task<long> GetRMComboID()
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetRMComboID");
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt64(content);
        }

        public async Task<long> GetNewRMComboChildID()
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetNewRMComboChildID");
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt64(content);
        }

        public async Task<string> PostRMCombo(RMComboMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Costing/PostRMCombo", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> PutRMCombo(RMComboMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Costing/PutRMCombo", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> GetComboList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetComboList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<long> GetRMComboId(long ComboID)
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetRMComboId/" + ComboID);
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<List<ViewRMCombo>> GetRMCombo(long comboid)
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetRMCombo/" + comboid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRMCombo>>(content);
            return result;
        }

        #endregion

        #region UseCombo
        public async Task<List<ViewCostingProductForRMCombo>> GetViewCostingProductForRMCombo(long BelongTo)
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetViewCostingProductForRMCombo/" + BelongTo);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewCostingProductForRMCombo>>(content);
            return result;
        }

        public async Task<long> GetNewCostingFitemID(long Fitemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetNewCostingFitemID/" + Fitemid);
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> SaveUseCombo(List<CostingMaster> value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Costing/SaveUseCombo", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
        #endregion
    }
}
