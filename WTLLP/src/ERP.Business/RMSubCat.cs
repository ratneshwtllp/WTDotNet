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
    public class RMSubCat : IRMSubCategoryContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public RMSubCat(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<string> GetCategoryList()
        {
            var response = await client.GetAsync(apiServiceUrl + "RMSubCategory/RMCategory");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetHSNList()
        {
            var response = await client.GetAsync(apiServiceUrl + "hsn/HSNList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "RMSubCategory/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<int> GetNewRMSubCategoryId()
        {
            var response = await client.GetAsync(apiServiceUrl + "RMSubCategory/GetNewRMSubCategoryId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<RMSubCategory> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "RMSubCategory/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<RMSubCategory>(content);
            return result;
        }

        public async Task<string> Post(RMSubCategory value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "RMSubCategory", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(RMSubCategory value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "RMSubCategory", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<RMSubCategory>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "RMSubCategory");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<RMSubCategory>>(content);
            return result;
        }

        public async Task<List<ViewRMSubCategory>> GetRMSubCategoryList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "RMSubCategory/GetRMSubCategoryList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRMSubCategory>>(content);
            return result;
        }

        public async Task<int> CheckDuplicate(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "RMSubCategory/CheckDuplicate/?value=" + search);
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content); 
        }
    }
}
