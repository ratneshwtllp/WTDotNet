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
    public class Supplier : ISupplierContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public Supplier(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<List<SupplierDetails>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "supplier");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<SupplierDetails>>(content);
            return result;
        }

        public async Task<SupplierDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "supplier/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<SupplierDetails>(content);
            return result;
        }

        public async Task<string> GetCountryList()
        {
            var response = await client.GetAsync(apiServiceUrl + "supplier/GetCountryList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetStateList(int Countryid)
        {
            var response = await client.GetAsync(apiServiceUrl + "supplier/GetStateList/" + Countryid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetCityList(int Stateid)
        {
            var response = await client.GetAsync(apiServiceUrl + "supplier/GetCityList/" + Stateid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetCurrencyList()
        {
            var response = await client.GetAsync(apiServiceUrl + "supplier/GetCurrencyList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewSupplierDetails>> GetSupplierList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "supplier/GetSupplierList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewSupplierDetails>>(content);
            return result;
        }

        public async Task<int> GetNewSupplierId()
        {
            var response = await client.GetAsync(apiServiceUrl + "supplier/GetNewSupplierId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<int> CheckDuplicate(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "supplier/CheckDuplicate/?value=" + search.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<string> Post(SupplierDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "supplier", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(SupplierDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "supplier", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "supplier/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<List<ViewSupplierBank>> GetSupplierBank(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "supplier/GetSupplierBank/" + id);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewSupplierBank>>(content);
            return result;
        }

        public async Task<string> PostSupplierBank(SupplierBankDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(apiServiceUrl + "supplier/PostSupplierBank", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
        public async Task<int> GetNewSupplierBankId()
        {
            var response = await client.GetAsync(apiServiceUrl + "supplier/GetNewSupplierBankId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }
        public async Task<int> DeleteSupplierBank(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "supplier/DeleteSupplierBank/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<string> GetBankNameList()
        {
            var response = await client.GetAsync(apiServiceUrl + "bank/GetBankNameList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<List<ViewSupplierDetails>> GetSupplierForPrint(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "supplier/GetSupplierForPrint/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewSupplierDetails>>(content);
            return result;
        }
    }
}
