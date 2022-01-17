using ERP.Business.Contracts;
using ERP.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ERP.Business
{
    public class Attandance : IAttandanceContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public Attandance(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        //public async Task<int> Delete(int id)
        //{
        //    var response = await client.DeleteAsync(apiServiceUrl + "adhesive/Delete/" + id.ToString());
        //    var content = await response.Content.ReadAsStringAsync();

        //    return Convert.ToInt16(content);
        //}

        public async Task<List<HR_ViewAttandance>> List(AttandanceSearchModel obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Attandance/List", byteContent);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<HR_ViewAttandance>>(content);
            return result;
        }

        public async Task<List<HR_ViewEmployeeDetails>> GetEmployeeList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "Employee/GetEmployeeList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<HR_ViewEmployeeDetails>>(content);
            return result;
        }

        public async Task<string> Post(List<HR_AttandanceDetails> value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Attandance", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> GetEmployeeList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Employee/EmployeeList");
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }

        public async Task<string> GetAttandanceStatusList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Attandance/AttandanceStatusList");
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }
        //public async Task<string> Put(AdhesiveDetails value)
        //{
        //    var myContent = JsonConvert.SerializeObject(value);
        //    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
        //    var byteContent = new ByteArrayContent(buffer);
        //    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //    var response = await client.PutAsync(apiServiceUrl + "adhesive", byteContent);
        //    var content = response.Content.ReadAsStringAsync();
        //    return (content.Result);
        //}

        //public async Task<List<AdhesiveDetails>> Get()
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "adhesive");
        //    var content = await response.Content.ReadAsStringAsync();

        //    var result = JsonConvert.DeserializeObject<List<AdhesiveDetails>>(content);
        //    return result;
        //}



        //public async Task<int> CheckDuplicate(string search)
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "adhesive/CheckDuplicate/?value=" + search.ToString());
        //    var content = await response.Content.ReadAsStringAsync();

        //    return Convert.ToInt16(content); 
        //}
        public async Task<List<HR_ViewCalculateMonthlySalary>> SalaryList(AttandanceSearchModel obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Attandance/SalaryList", byteContent);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<HR_ViewCalculateMonthlySalary>>(content);
            return result;
        }
        public async Task<string> GetMonthList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Master/GetMonthList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetYearList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Master/GetYearList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetDepartmentList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Department/DepartmentList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
