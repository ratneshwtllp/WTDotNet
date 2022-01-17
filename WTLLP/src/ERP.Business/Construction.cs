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
    public class Construction : IConstructionContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public Construction(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<List<ConstructionDetails>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "Construction");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ConstructionDetails>>(content);
            return result;
        }

        public async Task<ConstructionDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "Construction/" + id);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ConstructionDetails>(content);
            return result;
        }

        //public async Task<string> GetBuyerList()
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "brand/GetBuyerList");
        //    var content = await response.Content.ReadAsStringAsync();
        //    return content;
        //}

        public async Task<List<ConstructionDetails>> GetConstructionList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "Construction/GetConstructionList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ConstructionDetails>>(content);
            return result;
        }

        public async Task<string> GetListConstruction()
        {
            var response = await client.GetAsync(apiServiceUrl + "Construction/GetListConstruction");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<int> GetNewConstructionId()
        {
            var response = await client.GetAsync(apiServiceUrl + "Construction/GetNewConstructionId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<int> CheckDuplicate(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "Construction/CheckDuplicate/?value=" + search.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<string> Post(ConstructionDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Construction", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(ConstructionDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Construction", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "Construction/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }
    }
}
