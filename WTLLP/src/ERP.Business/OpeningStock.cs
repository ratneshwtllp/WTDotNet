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
    public class OpeningStock : IOpeningStockContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public OpeningStock(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<List<ViewOpStockDetails>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "openingstock");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewOpStockDetails>>(content);
            return result;
        }

        public async Task<OpeningStockDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "openingstock/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<OpeningStockDetails>(content);
            return result;
        }

        public async Task<List<ViewOpStockDetails>> GetViewOpStockDetails(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "openingstock/GetViewOpStockDetails/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewOpStockDetails>>(content);
            return result;
        }

        public async Task<long> GetNewOpStockId()
        {
            var response = await client.GetAsync(apiServiceUrl + "openingstock/GetNewOpStockId");
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
            var response = await client.GetAsync(apiServiceUrl + "openingstock/GetSupplierList/" + ritemid.ToString());
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

        public async Task<string> GetRackList(long storeid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rackmaster/GetStoreRackList/" + storeid.ToString ());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewOpStockDetails>> GetOpStockDetails(long ritemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "openingstock/GetOpStockDetails/" + ritemid.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewOpStockDetails>>(content);
            return result;
        }

        //public async Task<List<ViewRackMaster>> GetRackMasterList(string search)
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "rackmaster/GetRackMasterList/" + search);
        //    var content = await response.Content.ReadAsStringAsync();

        //    var result = JsonConvert.DeserializeObject<List<ViewRackMaster>>(content);
        //    return result;
        //}

        //public async Task<string> GetNewRackNo(int categoryId)
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "rackmaster/GetNewRackNo/" + categoryId.ToString());
        //    var content = await response.Content.ReadAsStringAsync();
        //    return content;
        //}

        //public async Task<int> GetNewCategorySNo(long categoryId)
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "rackmaster/GetNewCategorySNo/" + categoryId.ToString());
        //    var content = await response.Content.ReadAsStringAsync();
        //    return Convert.ToInt32(content);
        //}

        public async Task<int> CheckDuplicate(long ritemid, long suppid, long rackid)
        {
            var response = await client.GetAsync(apiServiceUrl + "openingstock/CheckDuplicate/" + ritemid + "/" + suppid  + "/" + rackid);
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<string> Post(OpeningStockDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "openingstock", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(OpeningStockDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "openingstock", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<int> Delete(int opstockid)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "openingstock/Delete/" + opstockid.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<int> GetIsBatchOfRM()
        {
            var response = await client.GetAsync(apiServiceUrl + "FormSetting/GetIsBatchOfRM");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<string> GetStoreList()
        {
            var response = await client.GetAsync(apiServiceUrl + "StoreMaster/GetStoreList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetRMList()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetRMList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> RMSuppRate(long rmid, long suppid)
        {
            var response = await client.GetAsync(apiServiceUrl + "RM/GetRMSupplier/" + rmid + "/" + suppid);
            var content = await response.Content.ReadAsStringAsync();

            return (content);
        }

    }
}
