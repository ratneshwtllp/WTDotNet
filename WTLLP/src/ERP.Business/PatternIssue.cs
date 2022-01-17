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
    public class PatternIssue : IPatternIssueContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public PatternIssue(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<List<Domain.Entities.PatternIssueDetails>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "PatternIssue");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Domain.Entities.PatternIssueDetails>>(content);
            return result;
        }

        public async Task<ViewPatternIssueDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "PatternIssue/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ViewPatternIssueDetails>(content);
            return result;
        }

        public async Task<List<ViewPatternIssueDetails>> GetPatternIssueList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "PatternIssue/GetPatternIssueList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewPatternIssueDetails>>(content);
            return result;
        }

        public async Task<int> GetNewPatternIssueId()
        {
            var response = await client.GetAsync(apiServiceUrl + "PatternIssue/GetNewPatternIssueId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }
        public async Task<int> GetNewPatternIssueNo()
        {
            var response = await client.GetAsync(apiServiceUrl + "PatternIssue/GetNewPatternIssueNo");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }
        public async Task<int> CheckDuplicate(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "PatternIssue/CheckDuplicate/?value=" + search.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }
        
        public async Task<string> GetProCategoryList()
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetProCategoryList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetProSubCategoryList(long pcategoryId)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetProSubCategoryList/" + pcategoryId.ToString());
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
            var response = await client.GetAsync(apiServiceUrl + "product/" + fitemid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetContractorList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Contractor/GetContractorList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        
        public async Task<string> GetIssueContractorList(long ContractorID)
        {
            var response = await client.GetAsync(apiServiceUrl + "Contractor/GetContractorList/" + ContractorID.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<ViewPatternRoomDetails> GetPatternRoomDetails(long fitemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "PatternIssue/GetPatternRoomDetails/" + fitemid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewPatternRoomDetails>(content);
            return result;
        }

        public async Task<string> Post(Domain.Entities.PatternIssueDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "PatternIssue", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(Domain.Entities.PatternIssueDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "PatternIssue", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "PatternIssue/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }
    }
}
