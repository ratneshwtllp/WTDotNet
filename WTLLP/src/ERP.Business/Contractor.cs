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
    public class Contractor : IContractorContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public Contractor(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<List<ContractorDetails>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "contractor");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ContractorDetails>>(content);
            return result;
        }

        public async Task<ContractorDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "contractor/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ContractorDetails>(content);
            return result;
        }

        public async Task<List<ViewContractorDetails>> GetContractorRecord(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "contractor/GetContractorRecord/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewContractorDetails>>(content);
            return result;
        }

        public async Task<ViewContractorDetails> GetContractorRecord(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "contractor/GetContractorRecord/" + id);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewContractorDetails>(content);
            return result;
        }

        public async Task<string> GetCompanyList()
        {
            var response = await client.GetAsync(apiServiceUrl + "contractor/GetCompanyList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetContractorList()
        {
            var response = await client.GetAsync(apiServiceUrl + "contractor/GetContractorList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        
        public async Task<long> GetNewContractorId()
        {
            var response = await client.GetAsync(apiServiceUrl + "contractor/GetNewContractorId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> Post(ContractorDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "contractor", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(ContractorDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "contractor", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
         
        public async Task<int> CheckDuplicate(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "contractor/CheckDuplicate/?value=" + search);
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content); 
        }

        public async Task<string> Delete(long id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "contractor/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }


    }
}
