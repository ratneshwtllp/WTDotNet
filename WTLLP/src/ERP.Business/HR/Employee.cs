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
    public class Employee : IEmployeeContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public Employee(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<List<HR_EmployeeDetails>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "Employee");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<HR_EmployeeDetails>>(content);
            return result;
        }

        public async Task<HR_ViewEmployeeDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "Employee/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<HR_ViewEmployeeDetails>(content);
            return result;
        }

        public async Task<List<HR_ViewEmployeeDetails>> GetEmployeeList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "Employee/GetEmployeeList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<HR_ViewEmployeeDetails>>(content);
            return result;
        }

        public async Task<int> GetNewEmployeeId()
        {
            var response = await client.GetAsync(apiServiceUrl + "Employee/GetNewEmployeeId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<int> CheckDuplicate(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "Employee/CheckDuplicate/?value=" + search.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<string> Post(HR_EmployeeDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Employee", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(HR_EmployeeDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Employee", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "Employee/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<string> TitleList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Title/TitleList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> DepartmentList()
        {
            var response = await client.GetAsync(apiServiceUrl + "HRDepartment/DepartmentList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> DesignationList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Designation/DesignationList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GradeList()
        {
            var response = await client.GetAsync(apiServiceUrl + "HRGrade/GradeList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> BankList()
        {
            var response = await client.GetAsync(apiServiceUrl + "HRBank/BankList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetGenderList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Employee/GetGenderList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetMaritalStatusList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Employee/GetMaritalStatusList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetEmployeeImagePath(long EmployeeId)
        {
            var response = await client.GetAsync(apiServiceUrl + "Employee/GetEmployeeImagePath/" + EmployeeId.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<HR_EmployeeFile>> GetEmployeeFileRecord(long employeeid)
        {
            var response = await client.GetAsync(apiServiceUrl + "Employee/GetEmployeeFileRecord/" + employeeid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<HR_EmployeeFile>>(content);
            return result;
        }

        public async Task<long> GetNewEMPFileId()
        {
            var response = await client.GetAsync(apiServiceUrl + "Employee/GetNewEMPFileId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> PostEmployeeFile(List<HR_EmployeeFile> value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Employee/PostEmployeeFile", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
        public async Task<string> GetEmployeeType()
        {
            var response = await client.GetAsync(apiServiceUrl + "Employee/GetEmployeeType");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

    }
}
