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


namespace ERP.Business
{
    public class Bank : IBankContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public Bank(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<List<BankDetails>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "Bank");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<BankDetails>>(content);
            return result;
        }
    
        public async Task<BankDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "Bank/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BankDetails>(content);
            return result;
        }

        public async Task<List<BankDetails>> GetBankList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "Bank/GetBankList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<BankDetails>>(content);
            return result;
        }

        public async Task<int> GetNewBankId()
        {
            var response = await client.GetAsync(apiServiceUrl + "Bank/GetNewBankId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<int> CheckDuplicate(string serach)
        {
            var response = await client.GetAsync(apiServiceUrl + "Bank/CheckDuplicate/?value=" + serach.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<string> Post(BankDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Bank", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(BankDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Bank", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "Bank/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }
    }
}
