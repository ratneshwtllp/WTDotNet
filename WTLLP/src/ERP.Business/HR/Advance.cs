using ERP.Business.Contracts;
using ERP.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
namespace ERP.Business
{
    public class Advance : IAdvanceContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public Advance(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<string> GetEmployeeList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Employee/EmployeeList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<List<HR_ViewAdvanceDetails >> GetAdvanceList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "Advance/GetAdvanceList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<HR_ViewAdvanceDetails>>(content);
            return result;
        }


        public async Task<string> Post(HR_AdvanceDetails  value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Advance", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(HR_AdvanceDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Advance", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "Advance/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }
        
    }
}
