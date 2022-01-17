using ERP.Business.Contracts;
using ERP.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace ERP.Business
{
    public class HSN : IHSNContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public HSN(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<List<ViewHSNDetails>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "HSN");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewHSNDetails>>(content);
            return result;
        }

        public async Task<ViewHSNDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "HSN/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewHSNDetails>(content);
            return result;
        }

        public async Task<string> GetTaxList()
        {
            var response = await client.GetAsync(apiServiceUrl + "HSN/GetTaxList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewHSNDetails>> GetHSNList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "HSN/GetHSNList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewHSNDetails>>(content);
            return result;
        }

        public async Task<int> GetNewHSNId()
        {
            var response = await client.GetAsync(apiServiceUrl + "HSN/GetNewHSNId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<int> CheckDuplicate(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "HSN/CheckDuplicate/?value=" + search.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<string> Post(HSNDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "HSN", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(HSNDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "HSN", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "HSN/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }
        
    }
}
