using ERP.Business.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ERP.Domain.Entities;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace ERP.Business
{
    public class User : IUserContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public User(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<List<UserDetails>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "user");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<UserDetails>>(content);
            return result;
        }

        public async Task<UserDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "user/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<UserDetails>(content);
            return result;
        }
         
        public async Task<ViewUserDetails> GetUserDetails(int userid)
        {
            var response = await client.GetAsync(apiServiceUrl + "user/GetUserDetails/" + userid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewUserDetails>(content);
            return result;
        }

        public async Task<int> GetId(string name, string psd)
        {
            var response = await client.GetAsync(apiServiceUrl + "user/GetId/?name=" + name +"&psd="+ psd);
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<List<ViewUserDetails>> GetUserList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "user/GetUserList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewUserDetails>>(content);
            return result;
        }

        public async Task<string> GetDepartmentList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Department/GetListDepartment");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<int> GetNewUserId()
        {
            var response = await client.GetAsync(apiServiceUrl + "user/GetNewUserId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<int> CheckDuplicate(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "user/CheckDuplicate/?value=" + search.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<string> Post(UserDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "user", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(UserDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "user", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "user/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }
        public async Task<string> GetUserTypeList()
        {
            var response = await client.GetAsync(apiServiceUrl + "user/GetUserTypeList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
