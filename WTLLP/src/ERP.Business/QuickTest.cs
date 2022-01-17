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
    public class QuickTest : IQuickTestContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public QuickTest(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "QuickTest/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<QuickTestDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "QuickTest/" + id.ToString());

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<QuickTestDetails>(content);
            return result;
        }

        public async Task<List<QuickTestDetails>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "QuickTest");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<QuickTestDetails>>(content);
            return result;
        }

        public async Task<List<QuickTestDetails>> GetQuickTestList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "QuickTest/GetQuickTestList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<QuickTestDetails>>(content);
            return result;
        }

        public async Task<int> GetNewQuickTestId()
        {
            var response = await client.GetAsync(apiServiceUrl + "QuickTest/GetNewQuickTestId");

            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<int> CheckDuplicate(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "QuickTest/CheckDuplicate/?value=" + search.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<string> Post(QuickTestDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(apiServiceUrl + "QuickTest", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(QuickTestDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PutAsync(apiServiceUrl + "QuickTest", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
    }
}
