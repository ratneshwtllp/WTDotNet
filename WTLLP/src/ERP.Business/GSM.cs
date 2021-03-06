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
    public class GSM : IGSMContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public GSM(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "GSM/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<GSMDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "GSM/"+ id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<GSMDetails>(content);
            return result;
        }

        public async Task<List<GSMDetails>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "GSM");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<GSMDetails>>(content);
            return result;
        }

        public async Task<List<GSMDetails>> GetGSMList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "GSM/GetGSMList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<GSMDetails>>(content);
            return result;
        }

        public async Task<int> GetNewGSMId()
        {
            var response = await client.GetAsync(apiServiceUrl + "GSM/GetNewGSMId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        //public async Task<int> CheckDuplicate(string search)
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "GSM/CheckDuplicate/?value=" + search.ToString());
        //    var content = await response.Content.ReadAsStringAsync();

        //    return Convert.ToInt16(content);
        //}

        public async Task<int> CheckDuplicate(int search)
        {
            var response = await client.GetAsync(apiServiceUrl + "GSM/CheckDuplicate/" + search);
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<string> Post(GSMDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "GSM", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(GSMDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "GSM", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
    }
}
