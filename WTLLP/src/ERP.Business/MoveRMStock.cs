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
    public class MoveRMStock : IMoveRMStockContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public MoveRMStock(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }
        
        public async Task<long> GetNewMoveRMStockID()
        {
            var response = await client.GetAsync(apiServiceUrl + "MoveRMstock/GetNewMoveRMStockID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
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
            var response = await client.GetAsync(apiServiceUrl + "MoveRMStock/GetSupplierList/" + ritemid.ToString());
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

        public async Task<int> CheckDuplicate(long ritemid, long suppid, long rackid)
        {
            var response = await client.GetAsync(apiServiceUrl + "openingstock/CheckDuplicate/" + ritemid + "/" + suppid + "/" + rackid);
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<string> Post(Move_RMStock value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "MoveRMStock", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<int> Delete(int movermstockId)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "MoveRMStock/Delete/" + movermstockId.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<string> GetRackNoList(long suppid, long rmid)
        {
            var response = await client.GetAsync(apiServiceUrl + "MoveRMStock/GetRackNoList/" + suppid + "/" + rmid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewMoveRMStock>> GetViewMoveRMStock(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "MoveRMStock/GetViewMoveRMStock/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewMoveRMStock>>(content);
            return result;
        }

        public async Task<List<ViewMoveRMStock>> GetViewMoveRMStock(StoreSearch obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "MoveRMStock/GetViewMoveRMStock", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewMoveRMStock>>(content);
            return result;
        }

        public async Task<ViewMoveRMStock> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "MoveRMStock/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ViewMoveRMStock>(content);
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

        public async Task<string> GetContractorList()
        {
            var response = await client.GetAsync(apiServiceUrl + "contractor/GetContractorList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetBatch_LotNoFromRM_Supp_Rack(long ritemid, long suppid, long rackid)
        {
            var response = await client.GetAsync(apiServiceUrl + "RMStock/GetBatch_LotNoFromRM_Supp_Rack/" + ritemid + "/" + suppid + "/" + rackid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<double> GetRMRackStockFromBatchLotNo(long rowidlotid)
        {
            var response = await client.GetAsync(apiServiceUrl + "RMStock/GetRMRackStockFromBatchLotNo/" + rowidlotid);
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToDouble(content); ;
        }

        public async Task<string> GetRMList()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetRMList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
