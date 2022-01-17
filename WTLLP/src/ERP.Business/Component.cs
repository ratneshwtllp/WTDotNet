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
    public class Component : IComponent 
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public Component(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<int> Delete(int id)
        {

            var response = await client.DeleteAsync(apiServiceUrl + "Component/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<int> GetNewId()
        {
            var response = await client.GetAsync(apiServiceUrl + "Component/GetNewId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<ViewComponentDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "Component/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewComponentDetails>(content);
            return result;
        }

        public async Task<string> Post(ComponentDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(apiServiceUrl + "Component", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return ("Record save");
        }

        public async Task<string> Put(ComponentDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PutAsync(apiServiceUrl + "Component", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return ("Record Update");
        }

        public async Task<List<ComponentDetails>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "Component");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ComponentDetails>>(content);
            return result;
        }

        public async Task<List<ViewComponentDetails >> GetComponentList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "Component/GetComponentList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewComponentDetails>>(content);
            return result;
        }

        public async Task<int> CheckDuplicate(string search)
        {
   
               var response = await client.GetAsync(apiServiceUrl + "Component/CheckDuplicate/?value=" + search);
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content); 
        }
        public async Task<string>ComponentGroupList()
        {
            var response = await client.GetAsync(apiServiceUrl + "ComponentGroup/ComponentGroupList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

    }
}
