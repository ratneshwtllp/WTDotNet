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
    public class Title : ITitleContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public Title(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<List<Domain.Entities.HR_TitleDetails>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "Title");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Domain.Entities.HR_TitleDetails>>(content);
            return result;
        }
    
        public async Task<HR_TitleDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "Title/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<HR_TitleDetails>(content);
            return result;
        }

        public async Task<List<HR_TitleDetails>> GetTitleList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "Title/GetTitleList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<HR_TitleDetails>>(content);
            return result;
        }

        public async Task<int> GetNewTitleId()
        {
            var response = await client.GetAsync(apiServiceUrl + "Title/GetNewTitleId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<int> CheckDuplicate(string serach)
        {
            var response = await client.GetAsync(apiServiceUrl + "Title/CheckDuplicate/?value=" + serach.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<string> Post(Domain.Entities.HR_TitleDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Title", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(Domain.Entities.HR_TitleDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Title", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "Title/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }
    }
}
