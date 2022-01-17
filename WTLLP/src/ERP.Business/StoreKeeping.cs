using ERP.Business.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.Domain.Entities;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ERP.Business
{
    public class StoreKeeping : IStoreKeepingContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;
        string reportServiceUrl;

        public StoreKeeping(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
            this.reportServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ReportServiceUrl");
        }

        public async Task<List<StoreKeepingMaster>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "storekeeping");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<StoreKeepingMaster>>(content);
            return result;
        }

        public async Task<StoreKeepingMaster> Get(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "storekeeping/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<StoreKeepingMaster>(content);
            return result;
        }

        //public async Task<List<ViewGRNMaster>> GetViewGRNMaster(string search) //, int cancelstatus)
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "storekeeping/GetViewGRNMaster/?search=" + search); //+ "&cancelstatus=" + cancelstatus);
        //    var content = await response.Content.ReadAsStringAsync();

        //    var result = JsonConvert.DeserializeObject<List<ViewGRNMaster>>(content);
        //    return result;
        //}

        //public async Task<List<ViewGRNMaster>> GetViewGRNMasterForCancelList() //int cancelstatus)
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "storekeeping/GetViewGRNMasterForCancelList"); 
        //    var content = await response.Content.ReadAsStringAsync();

        //    var result = JsonConvert.DeserializeObject<List<ViewGRNMaster>>(content);
        //    return result;
        //}

        public async Task<long> GetNewStoreKeepingID()
        {
            var response = await client.GetAsync(apiServiceUrl + "storekeeping/GetNewStoreKeepingID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<long> GetNewStoreKeepingChildID()
        {
            var response = await client.GetAsync(apiServiceUrl + "storekeeping/GetNewStoreKeepingChildID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<long> GetNewStoreKeepingSerial()
        {
            var response = await client.GetAsync(apiServiceUrl + "storekeeping/GetNewStoreKeepingSerial");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<string> GetNewStoreKeepingNO()
        {
            var response = await client.GetAsync(apiServiceUrl + "storekeeping/GetNewStoreKeepingNO");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
         
        public async Task<string> GetGRNList()
        {
            var response = await client.GetAsync(apiServiceUrl + "storekeeping/GetGRNList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetStoreList()
        {
            var response = await client.GetAsync(apiServiceUrl + "RackMaster/GetStoreList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetRackNoList(long suppid, long rmid)
        {
            var response = await client.GetAsync(apiServiceUrl + "RackMaster/GetRackNoList/" + suppid + "/" + rmid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetStoreRackList(long storeid)
        {
            var response = await client.GetAsync(apiServiceUrl + "RackMaster/GetStoreRackList/" + storeid );
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewGRNForStoreKeeping>> GetViewGRNForStoreKeeping(long grnid)
        {
            var response = await client.GetAsync(apiServiceUrl + "StoreKeeping/GetViewGRNForStoreKeeping/" + grnid.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewGRNForStoreKeeping>>(content);
            return result;
        }

        public async Task<ViewGRNMaster> GetViewGRNMaster(long grnid)
        {
            var response = await client.GetAsync(apiServiceUrl + "GRN/GetViewGRNMaster/" + grnid.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewGRNMaster>(content);
            return result;
        }

        public async Task<string> Post(StoreKeepingMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "storekeeping", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(StoreKeepingMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "storekeeping", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> PutStoreKeepingStatus(long grnid)
        {
            var response = await client.GetAsync(apiServiceUrl + "grn/PutStoreKeepingStatus/" + grnid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> Delete(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "storekeeping/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewStoreKeeping>> GetStoreKeepingList(StoreSearch obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "StoreKeeping/GetStoreKeepingList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewStoreKeeping>>(content);
            return result;
        }

        public async Task<string> GetYearList()
        {
            var response = await client.GetAsync(apiServiceUrl + "year/GetYearList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetSessionYearList()
        {
            var response = await client.GetAsync(apiServiceUrl + "sessionyear/GetSessionYearList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetSupplierList()
        {
            var response = await client.GetAsync(apiServiceUrl + "supplier/GetSupplier");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetMonthList()
        {
            var response = await client.GetAsync(apiServiceUrl + "month/GetMonthList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetNewRTSGRNNo()
        {
            var response = await client.GetAsync(apiServiceUrl + "StoreKeeping/GetNewRTSGRNNo");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> PostRTSGRN(RTSGRNMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "StoreKeeping/PostRTSGRN", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ViewRTSGRN>> GetRTSGRNList(StoreSearch obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "StoreKeeping/GetRTSGRNList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRTSGRN>>(content);
            return result;
        }

        public async Task<string> GetShortSessionYearList()
        {
            var response = await client.GetAsync(apiServiceUrl + "sessionyear/GetShortSessionYearList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

    }
}
