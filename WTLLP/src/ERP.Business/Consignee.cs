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
    public class Consignee : IConsigneeContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public Consignee(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<List<ConsigneeDetails>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "consignee");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ConsigneeDetails>>(content);
            return result;
        }

        public async Task<ConsigneeDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "consignee/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ConsigneeDetails>(content);
            return result;
        }

        public async Task<string> GetCountryList()
        {
            var response = await client.GetAsync(apiServiceUrl + "consignee/GetCountryList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetStateList(int Countryid)
        {
            var response = await client.GetAsync(apiServiceUrl + "consignee/GetStateList/" + Countryid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetCityList(int Stateid)
        {
            var response = await client.GetAsync(apiServiceUrl + "consignee/GetCityList/" + Stateid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ConsigneeDetails>> GetConsigneeList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "consignee/GetConsigneeList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ConsigneeDetails>>(content);
            return result;
        }

        public async Task<int> GetNewConsigneeId()
        {
            var response = await client.GetAsync(apiServiceUrl + "consignee/GetNewConsigneeId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<int> CheckDuplicate(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "consignee/CheckDuplicate/?value=" + search.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<string> Post(ConsigneeDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "consignee", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(ConsigneeDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "consignee", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "consignee/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }
    }
}
