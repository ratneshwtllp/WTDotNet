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
    public class ReceiveWP : IReceiveWPContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;
        string reportServiceUrl;

        public ReceiveWP(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
            this.reportServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ReportServiceUrl");
        }

        public async Task<List<ReceiveWPMaster>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "receivewp");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ReceiveWPMaster>>(content);
            return result;
        }

        public async Task<ReceiveWPMaster> Get(long issueid)
        {
            var response = await client.GetAsync(apiServiceUrl + "receivewp/" + issueid.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ReceiveWPMaster>(content);
            return result;
        }

        public async Task<List<ViewReceiveWP>> GetList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "receivewp/GetList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewReceiveWP>>(content);
            return result;
        }

        public async Task<long> GetNewReceiveWPID()
        {
            var response = await client.GetAsync(apiServiceUrl + "receivewp/GetNewReceiveWPID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> GetPlanNoList()
        {
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetPlanNoList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewWorkPlanChild>> GetWorkPlanDetails(long planid)
        {
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetWorkPlanDetails/" + planid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewWorkPlanChild>>(content);
            return result;
        }

        public async Task<ViewWorkPlan> GetViewWorkPlan(long planid)
        {
            var response = await client.GetAsync(apiServiceUrl + "workplan/" + planid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewWorkPlan>(content);
            return result;
        }
         
        public async Task<string> Post(ReceiveWPMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "receivewp", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> PutIssueStatus(long issueid)
        {
            var response = await client.GetAsync(apiServiceUrl + "receivewp/PutIssueStatus/" + issueid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> DeleteReceiveWP(long receivewpid)
        {
            var response = await client.GetAsync(apiServiceUrl + "receivewp/Delete/" + receivewpid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
