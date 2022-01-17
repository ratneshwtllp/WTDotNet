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
    public class UserAsSupervisor : IUserAsSupervisorContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public UserAsSupervisor(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<string> GetUserList()
        {
            var response = await client.GetAsync(apiServiceUrl + "UserAsSupervisor/GetUserList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
       
        public async Task<List<ContractorDetails>> GetContractorList()
        {
            var response = await client.GetAsync(apiServiceUrl + "UserAsSupervisor/GetContractorList");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ContractorDetails>>(content);
            return result;
        }
       

        public async Task<int> GetNewId()
        {
            var response = await client.GetAsync(apiServiceUrl + "UserAsSupervisor/GetNewId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<string> Post(List<UserAsSupervisorDetails> value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "UserAsSupervisor", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
      
        public async Task<List<UserAsSupervisorDetails>> GetUserSupervisor(int userid)
        {
            var response = await client.GetAsync(apiServiceUrl + "UserAsSupervisor/GetUserSupervisor/" + userid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<UserAsSupervisorDetails>>(content);
            return result;
        }
    }
}
