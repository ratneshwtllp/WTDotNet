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
    public class Lining : ILiningContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public Lining(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<int> Delete(int id)
        {

            var response = await client.DeleteAsync(apiServiceUrl + "lining/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<int> GetNewLiningId()
        {
            var response = await client.GetAsync(apiServiceUrl + "lining/GetNewLiningId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<LiningDetails > Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "lining/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<LiningDetails>(content);
            return result;
        }

        public async Task<string> Post(LiningDetails  value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "lining", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return ("Record save");
        }

        public async Task<string> Put(LiningDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "lining", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return ("Record save");
        }

        public async Task<List<LiningDetails>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "lining");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<LiningDetails>>(content);
            return result;
        }

        public async Task<List<LiningDetails>> GetLiningList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "lining/GetLiningList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<LiningDetails>>(content);
            return result;
        }

        public async Task<int> CheckDuplicate(string search)
        {
   
               var response = await client.GetAsync(apiServiceUrl + "lining/CheckDuplicate/?value=" + search);
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content); 
        }
    }
}
