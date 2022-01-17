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
    public class SampleChangeRequest : ISampleChangeRequestContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public SampleChangeRequest(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<List<Domain.Entities.Sample_Request>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "SampleChangeRequest");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Domain.Entities.Sample_Request>>(content);
            return result;
        }

        public async Task<ViewSample_Request> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "SampleChangeRequest/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ViewSample_Request>(content);
            return result;
        }

        public async Task<List<ViewSample_Request>> GetSampleChangeRequestList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "SampleChangeRequest/GetSampleChangeRequestList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewSample_Request>>(content);
            return result;
        }

        public async Task<List<ViewSample_Request>> GetChangesCompleteList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "SampleChangeRequest/GetChangesCompleteList/?search=" + search);
            var content=await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewSample_Request>>(content);
            return result;
        }

        public async Task<int> GetNewSampleChangeRequestId()
        {
            var response = await client.GetAsync(apiServiceUrl + "SampleChangeRequest/GetNewSampleChangeRequestId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<int> CheckDuplicate(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "SampleChangeRequest/CheckDuplicate/?value=" + search.ToString());
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

        public async Task<string> GetChangeRequestStatus(long Request_Id)
        {
            var response = await client.GetAsync(apiServiceUrl + "SampleChangeRequest/GetChangeRequestStatus/" + Request_Id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> Post(Domain.Entities.Sample_Request value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "SampleChangeRequest", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(Domain.Entities.Sample_Request value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "SampleChangeRequest", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "SampleChangeRequest/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }
    }
}
