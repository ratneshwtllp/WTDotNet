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
    public class AdjustmentRM : IAdjustmentRMContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public AdjustmentRM(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<long> GetNewAdjustmentRMID()
        {
            var response = await client.GetAsync(apiServiceUrl + "AdjustmentRM/GetNewAdjustmentRMID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> GetRMCategoryList()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/RMCategory");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetRMSubCategoryList(long catid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/RMSubCategory/" + catid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetRMList(int scatid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetRMList/" + scatid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetSupplierList(long ritemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "AdjustmentRM/GetSupplierList/" + ritemid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<ViewRItemShow> GetRItemShow(long ritemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/RItemShow/" + ritemid.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewRItemShow>(content);
            return result;
        }

        public async Task<string> GetRackList()
        {
            var response = await client.GetAsync(apiServiceUrl + "rackmaster/GetList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> Post(AdjustmentRMDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "AdjustmentRM", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Delete(int adjustmentrmId)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "AdjustmentRM/Delete/" + adjustmentrmId.ToString());
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> GetRackNoList(long suppid, long rmid)
        {
            var response = await client.GetAsync(apiServiceUrl + "AdjustmentRM/GetRackNoList/" + suppid + "/" + rmid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetBatch_LotNoFromRM_Supp_Rack(long ritemid, long suppid, long rackid)
        {
            var response = await client.GetAsync(apiServiceUrl + "RMStock/GetBatch_LotNoFromRM_Supp_Rack/" + ritemid + "/" + suppid + "/" + rackid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        //public async Task<double> GetRMRackStockFromBatchLotNo(long rowidlotid)
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "RMStock/GetRMRackStockFromBatchLotNo/" + rowidlotid);
        //    var content = await response.Content.ReadAsStringAsync();
        //    return Convert.ToDouble(content); ;
        //}
         
        public async Task<List<ViewAdjustmentRM>> GetViewAdjustmentRM(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "AdjustmentRM/GetViewAdjustmentRM/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewAdjustmentRM>>(content);
            return result;
        }

        public async Task<List<ViewAdjustmentRM>> GetViewAdjustmentRM(StoreSearch obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "AdjustmentRM/GetViewAdjustmentRM", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewAdjustmentRM>>(content);
            return result;
        }

        public async Task<double> GetRMRackStock(long ritemid, long suppid, long rackid, string lotno)
        {
            var response = await client.GetAsync(apiServiceUrl + "RMStock/GetRMRackStock/" + ritemid + "/" + suppid + "/" + rackid + "?lotno=" + lotno.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToDouble(content); ;
        }

        public async Task<string> GetSessionYearList()
        {
            var response = await client.GetAsync(apiServiceUrl + "sessionyear/GetSessionYearList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetYearList()
        {
            var response = await client.GetAsync(apiServiceUrl + "year/GetYearList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetMonthList()
        {
            var response = await client.GetAsync(apiServiceUrl + "month/GetMonthList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetSupplierList()
        {
            var response = await client.GetAsync(apiServiceUrl + "supplier/GetSupplier");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetRMList()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetRMList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
