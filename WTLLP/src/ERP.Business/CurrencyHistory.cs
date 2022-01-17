using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Business.Contracts;
using ERP.Domain.Entities;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using ERP.Helper;

namespace ERP.Business
{
    public class CurrencyHistory : ICurrencyHistory
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public CurrencyHistory(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<List<CurrencyHistoryDetails >> GetCurrencyHistory()
        {
            var response = await client.GetAsync(apiServiceUrl + "currency");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<CurrencyHistoryDetails>>(content);
            return result;
        }

        //public async Task<CurrencyHistoryDetails> Get(int id)
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "currency/" + id.ToString());
        //    var content = await response.Content.ReadAsStringAsync();

        //    var result = JsonConvert.DeserializeObject<CurrencyHistoryDetails>(content);
        //    return result;
        //}

        public async Task<int> GetNewCurrencyHistoryId()
        {
            var response = await client.GetAsync(apiServiceUrl + "currency/GetNewCurrencyId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        //public async Task<List<CurrencyHistoryDetails>> GetCurrencyHistoryList(string search)
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "currency/GetCurrencyList/?search=" + search);
        //    var content = await response.Content.ReadAsStringAsync();

        //    var result = JsonConvert.DeserializeObject<List<CurrencyHistoryDetails>>(content);
        //    return result;
        //}

        public async Task<string> Post(CurrencyHistoryDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "currency", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return ("Record save");
        }

        //public async Task<string> Put(CurrencyHistoryDetails value)
        //{
        //    var myContent = JsonConvert.SerializeObject(value);
        //    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        //    var byteContent = new ByteArrayContent(buffer);
        //    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //    var response = await client.PutAsync(apiServiceUrl + "currency", byteContent);
        //    var content = response.Content.ReadAsStringAsync();
        //    return ("Record update");
        //}
        
        //public async Task<int> Delete(int id)
        //{
        //    var response = await client.DeleteAsync(apiServiceUrl + "currency/Delete/" + id.ToString());
        //    var content = await response.Content.ReadAsStringAsync();

        //    return Convert.ToInt16(content);
        //}

        //public async Task<int> CheckDuplicate(string search)
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "currency/CheckDuplicate/?value=" + search.ToString());
        //    var content = await response.Content.ReadAsStringAsync();

        //    return Convert.ToInt16(content);
        //}
    }
}
