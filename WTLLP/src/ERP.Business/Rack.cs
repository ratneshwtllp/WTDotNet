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
    public class Rack : IRackMasterContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public Rack(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<List<RackMaster>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "rackmaster");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<RackMaster>>(content);
            return result;
        }

        public async Task<RackMaster> Get(long rackid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rackmaster/" + rackid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<RackMaster>(content);
            return result;
        }

        public async Task<string> GetRMCategoryList()
        {
            var response = await client.GetAsync(apiServiceUrl + "rackmaster/GetRMCategoryList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewRackMaster>> GetRackMasterList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "rackmaster/GetRackMasterList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRackMaster>>(content);
            return result;
        }

        public async Task<int> GetNewRackMasterId()
        {
            var response = await client.GetAsync(apiServiceUrl + "rackmaster/GetNewRackMasterId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<string> GetNewRackNo(int categoryId)
        {
            var response = await client.GetAsync(apiServiceUrl + "rackmaster/GetNewRackNo/" + categoryId.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<int> GetNewCategorySNo(long categoryId)
        {
            var response = await client.GetAsync(apiServiceUrl + "rackmaster/GetNewCategorySNo/" + categoryId.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt32(content);
        }

        public async Task<int> CheckDuplicate(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "rackmaster/CheckDuplicate/?value=" + search);
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        //public async Task<int> CheckDuplicateEdit(string value)
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "rackmaster/CheckDuplicateEdit/?value=" + value);
        //    var content = await response.Content.ReadAsStringAsync();

        //    return Convert.ToInt16(content);
        //}

        public async Task<string> Post(RackMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "rackmaster", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(RackMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "rackmaster", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<int> Delete(long rackid)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "rackmaster/Delete/" + rackid);
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<string> GetStoreList()
        {
            var response = await client.GetAsync(apiServiceUrl + "StoreMaster/GetStoreList/");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
