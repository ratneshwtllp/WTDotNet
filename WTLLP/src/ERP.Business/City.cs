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
    public class City : ICityContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public City(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<List<ViewCityDetails>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "city");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewCityDetails>>(content);
            return result;
        }

        public async Task<ViewCityDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "city/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewCityDetails>(content);
            return result;
        }

        public async Task<string> GetCountryList()
        {
            var response = await client.GetAsync(apiServiceUrl + "city/GetCountryList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetStateList(int Countryid)
        {
            var response = await client.GetAsync(apiServiceUrl + "city/GetStateList/" + Countryid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        
        public async Task<List<ViewCityDetails>> GetCityList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "city/GetCityList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewCityDetails>>(content);
            return result;
        }

        public async Task<int> GetNewCityId()
        {
            var response = await client.GetAsync(apiServiceUrl + "city/GetNewCityId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<int> CheckDuplicate(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "city/CheckDuplicate/?value=" + search.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<string> Post(CityDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "city", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(CityDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "city", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "city/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }
        
    }
}
