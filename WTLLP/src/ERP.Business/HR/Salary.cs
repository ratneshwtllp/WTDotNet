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
    public class Salary : ISalaryContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public Salary(IConfiguration configuration)
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
        public async Task<List<HR_ViewCalculateMonthlySalary>> SalaryList(AttandanceSearchModel obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Salary/SalaryList", byteContent);
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

        public async Task<List<HR_ViewEmployeeMonthlySalary>> EmployeeSalaryList(AttandanceSearchModel obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Salary/EmployeeSalaryList", byteContent);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<HR_ViewEmployeeMonthlySalary>>(content);
            return result;
        }
    }
}
