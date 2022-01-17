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
    public class Die : IDieContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public Die(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<DieDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "Die/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<DieDetails>(content);
            return result;
        }

        public async Task<List<DieDetails>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "Die");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<DieDetails>>(content);
            return result;
        }

        public async Task<List<DieDetails>> GetDieList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "Die/GetDieList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<DieDetails>>(content);
            return result;
        }

        public async Task<List<ProductDetails>> GetFitemList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Die/GetFitemList");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ProductDetails>>(content);
            return result;
        }

        public async Task<List<ProductDetails>> SearchProductChekedList(SearchModel obj )
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Product/SearchProductList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ProductDetails>>(content);
            return result;
        }

        public async Task<string> SearchProductList(SearchModel obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Product/SearchProductList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }

        public async Task<List<DieFitem>> GetDieFitemList(long Dieid)
        {
            var response = await client.GetAsync(apiServiceUrl + "Die/GetDieFitemList/" + Dieid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<DieFitem>>(content);
            return result;
        }

        public async Task<int> GetNewDieId()
        {
            var response = await client.GetAsync(apiServiceUrl + "Die/GetNewDieId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<int> GetNewDieFitemId()
        {
            var response = await client.GetAsync(apiServiceUrl + "Die/GetNewDieFitemId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<int> CheckDuplicate(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "Die/CheckDuplicate/?value=" + search.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<string> Post(DieDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Die", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(DieDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Die", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> PutDieFitem(List<DieFitem> value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Die/PutDieFitem", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "Die/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<List<ViewDieFitemDetails>> GetViewDieFitemList(long dieid)
        {
            var response = await client.GetAsync(apiServiceUrl + "Die/GetViewDieFitemList/" + dieid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewDieFitemDetails>>(content);
            return result;
        }

        public async Task<string> RemoveDieFitem(List<DieFitem> value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Die/RemoveDieFitem", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }


    }
}
