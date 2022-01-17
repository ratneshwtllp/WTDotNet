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
    public class Holiday : IHolidayContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public Holiday(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }
      
        public async Task<List<HR_HolidayMaster>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "Holiday");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<HR_HolidayMaster>>(content);
            return result;
        }
    
        public async Task<HR_HolidayMaster> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "Holiday/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<HR_HolidayMaster>(content);
            return result;
        }

        public async Task<List<HR_HolidayMaster>> GetHolidayList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "Holiday/GetHolidayList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<HR_HolidayMaster>>(content);
            return result;
        }
       
        public async Task<int> GetNewHolidayId()
        {
            var response = await client.GetAsync(apiServiceUrl + "Holiday/GetNewHolidayId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<int> CheckDuplicate(DateTime serach)
        {
            var response = await client.GetAsync(apiServiceUrl + "Holiday/CheckDuplicate/?value=" + serach);
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<string> Post(HR_HolidayMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Holiday", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(HR_HolidayMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Holiday", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "Holiday/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }
    }
}
