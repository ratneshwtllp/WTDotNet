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
    public class Buyer : IBuyerContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public Buyer(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<List<BuyerDetails>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "buyer");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<BuyerDetails>>(content);
            return result;
        }

        public async Task<BuyerDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "buyer/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<BuyerDetails>(content);
            return result;
        }

        public async Task<string> GetCountryList()
        {
            var response = await client.GetAsync(apiServiceUrl + "buyer/GetCountryList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetStateList(int Countryid)
        {
            var response = await client.GetAsync(apiServiceUrl + "buyer/GetStateList/" + Countryid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetCityList(int Stateid)
        {
            var response = await client.GetAsync(apiServiceUrl + "buyer/GetCityList/" + Stateid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewBuyerDetails>> GetBuyerList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "buyer/GetBuyerList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewBuyerDetails>>(content);
            return result;
        }

        public async Task<int> GetNewBuyerId()
        {
            var response = await client.GetAsync(apiServiceUrl + "buyer/GetNewBuyerId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<int> CheckDuplicate(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "buyer/CheckDuplicate/?value=" + search.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<string> Post(BuyerDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "buyer", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(BuyerDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "buyer", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "buyer/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<string> GetCurrencyList()
        {
            var response = await client.GetAsync(apiServiceUrl + "supplier/GetCurrencyList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ConsigneeDetails>> GetConsigneeName()
        {
            var response = await client.GetAsync(apiServiceUrl + "consignee/GetConsigneeName");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ConsigneeDetails>>(content);
            return result;
        }

        public async Task<string> GetBankNameList()
        {
            var response = await client.GetAsync(apiServiceUrl + "bank/GetBankNameList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<int> GetNewBuyerConsigneeId()
        {
            var response = await client.GetAsync(apiServiceUrl + "buyer/GetNewBuyerConsigneeId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<List<BuyerConsigneeDetails>> GetBuyerConsignee(int buyerid)
        {
            var response = await client.GetAsync(apiServiceUrl + "buyer/GetBuyerConsignee/" + buyerid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<BuyerConsigneeDetails>>(content);
            return result;
        }

        public async Task<List<ConsigneeDetails>> GetConsigneeList(int buyerid)
        {
            var response = await client.GetAsync(apiServiceUrl + "consignee/GetConsigneeList/" + buyerid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ConsigneeDetails>>(content);
            return result;
        }

        public async Task<List<ViewBuyerBank>> GetBuyerBank(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "buyer/GetBuyerBank/" + id);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewBuyerBank>>(content);
            return result;
        }

        public async Task<string> PostBuyerBank(BuyerBankDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "buyer/PostBuyerBank", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
        public async Task<int> GetNewBuyerBankId()
        {
            var response = await client.GetAsync(apiServiceUrl + "buyer/GetNewBuyerBankId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<int> DeleteBuyerBank(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "buyer/DeleteBuyerBank/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<List<ViewBuyerDetails>> GetBuyerDetails(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "buyer/GetBuyerDetails/" + id);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewBuyerDetails>>(content);
            return result;
        }

        public async Task<List<ViewBuyerConsignee>> BuyerConsigneePrint(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "buyer/BuyerConsigneePrint/" + id);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewBuyerConsignee>>(content);
            return result;
        }
    }
}
