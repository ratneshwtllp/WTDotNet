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
    public class Wash : IWashContract 
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public Wash(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<int> Delete(int id)
        {

            var response = await client.DeleteAsync(apiServiceUrl + "wash/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<int> GetNewWashId()
        {
            var response = await client.GetAsync(apiServiceUrl + "wash/GetNewWashId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<WashDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "wash/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<WashDetails >(content);
            return result;
        }

        public async Task<string> Post(WashDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "wash", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return ("Record save");
        }

        public async Task<string> Put(WashDetails  value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "wash", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return ("Record save");
        }

        public async Task<List<WashDetails >> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "wash");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<WashDetails >>(content);
            return result;
        }

        public async Task<List<WashDetails >> GetWashList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "wash/GetWashList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<WashDetails >>(content);
            return result;
        }

        public async Task<int> CheckDuplicate(string search)
        {
   
               var response = await client.GetAsync(apiServiceUrl + "wash/CheckDuplicate/?value=" + search);
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content); 
        }
    }
}
