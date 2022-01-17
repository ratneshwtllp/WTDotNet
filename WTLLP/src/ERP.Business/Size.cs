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
    public class Size :ISizeContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public Size(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "size/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<int> GetNewSizeId()
        {
            var response = await client.GetAsync(apiServiceUrl + "size/GetNewSizeId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<SizeDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "size/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<SizeDetails>(content);
            return result;
        }

        public async Task<string> Post(SizeDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "size", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(SizeDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "size", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<SizeDetails>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "size");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<SizeDetails>>(content);
            return result;
        }

        public async Task<List<SizeDetails>> GetSizeList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "size/GetSizeList/?search=" + search); 
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<SizeDetails>>(content);
            return result;
        }

        public async Task<int> CheckDuplicate(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "size/CheckDuplicate/?value=" + search);
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content); 
        }
    }
}
