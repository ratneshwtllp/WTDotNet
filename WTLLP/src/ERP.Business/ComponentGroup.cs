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
    public class ComponentGroup : IComponentGroup 
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public ComponentGroup(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<int> Delete(int id)
        {

            var response = await client.DeleteAsync(apiServiceUrl + "ComponentGroup/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<int> GetNewId()
        {
            var response = await client.GetAsync(apiServiceUrl + "ComponentGroup/GetNewId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<ComponentGroupDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "ComponentGroup/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ComponentGroupDetails>(content);
            return result;
        }

        public async Task<string> Post(ComponentGroupDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "ComponentGroup", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return ("Record save");
        }

        public async Task<string> Put(ComponentGroupDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "ComponentGroup", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return ("Record save");
        }

        public async Task<List<ComponentGroupDetails>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "ComponentGroup");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ComponentGroupDetails>>(content);
            return result;
        }

        public async Task<List<ComponentGroupDetails>> GetComponentGroupList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "ComponentGroup/GetComponentGroupList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ComponentGroupDetails>>(content);
            return result;
        }

        public async Task<int> CheckDuplicate(string search)
        {
   
               var response = await client.GetAsync(apiServiceUrl + "ComponentGroup/CheckDuplicate/?value=" + search);
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content); 
        }
    }
}
