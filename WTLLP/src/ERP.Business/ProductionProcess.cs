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
    public class ProductionProcess : IProductionProcessContract 
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public ProductionProcess(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<int> Delete(int id)
        {

            var response = await client.DeleteAsync(apiServiceUrl + "ProductionProcess/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<int> GetNewProductionProcessId()
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductionProcess/GetNewProductionProcessId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<ProductionProcessDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductionProcess/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ProductionProcessDetails >(content);
            return result;
        }

        public async Task<string> Post(ProductionProcessDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "ProductionProcess", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return ("Record save");
        }

        public async Task<string> Put(ProductionProcessDetails  value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "ProductionProcess", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return ("Record save");
        }

        public async Task<List<ProductionProcessDetails >> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductionProcess");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ProductionProcessDetails >>(content);
            return result;
        }

        public async Task<List<ProductionProcessDetails >> GetProductionProcessList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductionProcess/GetProductionProcessList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ProductionProcessDetails >>(content);
            return result;
        }

        public async Task<int> CheckDuplicate(string search)
        {
   
               var response = await client.GetAsync(apiServiceUrl + "ProductionProcess/CheckDuplicate/?value=" + search);
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content); 
        }
    }
}
