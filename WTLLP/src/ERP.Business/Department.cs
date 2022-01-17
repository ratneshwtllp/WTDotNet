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
    public class Department : IDepartmentContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public Department(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "Department/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<DepartmentDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "Department/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<DepartmentDetails>(content);
            return result;
        }

        public async Task<List<DepartmentDetails>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "Department");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<DepartmentDetails>>(content);
            return result;
        }

        public async Task<List<DepartmentDetails>> GetDepartmentList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "Department/GetDepartmentList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<DepartmentDetails>>(content);
            return result;
        }

        public async Task<int> GetNewDepartmentId()
        {
            var response = await client.GetAsync(apiServiceUrl + "Department/GetNewDepartmentId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<string> Post(DepartmentDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Department", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(DepartmentDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Department", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
        public async Task<int> CheckDuplicate(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "Department/CheckDuplicate/?value=" + search.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }
    }
}
