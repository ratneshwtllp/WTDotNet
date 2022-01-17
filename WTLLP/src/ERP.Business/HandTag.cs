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
    public class HandTag : IHandTagContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public HandTag(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<string> BuyerList()
        {
            var response = await client.GetAsync(apiServiceUrl + "HandTag/BuyerList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<List<ViewHandTag>> GetHandTagDetailsList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "HandTag/GetHandTagDetailsList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewHandTag>>(content);
            return result;
        }
        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "HandTag/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<int> GetNewHandTagId()
        {
            var response = await client.GetAsync(apiServiceUrl + "HandTag/GetNewHandTagId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<HandTagDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "HandTag/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<HandTagDetails>(content);
            return result;
        }

        public async Task<string> Post(HandTagDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "HandTag", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(HandTagDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "HandTag", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ViewHandTag>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "HandTag");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewHandTag>>(content);
            return result;
        }

        public async Task<int> CheckDuplicate(string search,int BuyerId)
        {
            var response = await client.GetAsync(apiServiceUrl + "HandTag/CheckDuplicate/?value=" + search + "&BuyerId=" +BuyerId);
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content); 
        }
    }
}
