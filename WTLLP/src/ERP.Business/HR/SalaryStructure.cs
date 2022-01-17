using ERP.Business.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.Domain.Entities;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace ERP.Business
{
    public class SalaryStructure : ISalaryStructureContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;
        string reportServiceUrl;
        public SalaryStructure(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
            this.reportServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ReportServiceUrl");
        }

        public async Task<string> Post(HR_SalaryStructureMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "SalaryStructure", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> GetDepartmentofEmployee()
        {
            var response = await client.GetAsync(apiServiceUrl + "SalaryStructure/GetDepartmentofEmployee/");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetEmployeeList(long departmentid)
        {
            var response = await client.GetAsync(apiServiceUrl + "SalaryStructure/GetEmployeeList/" + departmentid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetAllowncesList()
        {
            var response = await client.GetAsync(apiServiceUrl + "SalaryStructure/GetAllowncesList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<HR_ViewSalaryStructure> GetEmpDetails(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "SalaryStructure/GetEmpDetails/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<HR_ViewSalaryStructure>(content);
            return result;
        }

        public async Task<List<HR_ViewSalaryStructure>> GetChildDetails(long EmployeeId)
        {
            var response = await client.GetAsync(apiServiceUrl + "SalaryStructure/GetChildDetails/" + EmployeeId.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<HR_ViewSalaryStructure>>(content);
            return result;
        }

        public async Task<int> GetNewSalStrId()
        {
            var response = await client.GetAsync(apiServiceUrl + "SalaryStructure/GetNewSalStrId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<long> GetNewSalStrChildId()
        {
            var response = await client.GetAsync(apiServiceUrl + "SalaryStructure/GetNewSalStrChildId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content); ;
        }
    }
}
