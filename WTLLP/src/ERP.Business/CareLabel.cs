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
    public class CareLabel : ICareLabelContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public CareLabel(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<string> BuyerList()
        {
            var response = await client.GetAsync(apiServiceUrl + "CareLabel/BuyerList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<List<ViewCarelabel  >> GetCareLabelDetailsList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "CareLabel/GetCareLabelDetailsList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewCarelabel>>(content);
            return result;
        }
        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "CareLabel/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<int> GetNewCarelabelId()
        {
            var response = await client.GetAsync(apiServiceUrl + "CareLabel/GetNewCarelabelId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<CareLabelDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "CareLabel/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<CareLabelDetails>(content);
            return result;
        }

        public async Task<string> Post(CareLabelDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "CareLabel", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(CareLabelDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "CareLabel", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ViewCarelabel >> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "CareLabel");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewCarelabel>>(content);
            return result;
        }

        public async Task<int> CheckDuplicate(string search,int BuyerId)
        {
            var response = await client.GetAsync(apiServiceUrl + "CareLabel/CheckDuplicate/?value=" + search + "&BuyerId=" +BuyerId);
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content); 
        }
    }
}
