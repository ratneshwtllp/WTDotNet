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
    public class UserRight : IUserRightContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public UserRight(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<string> GetUserList()
        {
            var response = await client.GetAsync(apiServiceUrl + "UserRight/GetUserList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewUserRights>> GetmenuList(int menucatid)
        {
            var response = await client.GetAsync(apiServiceUrl + "UserRight/GetmenuList/" + menucatid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewUserRights>>(content);
            return result;
        }

        public async Task<string> GetMenuCatList()
        {
            var response = await client.GetAsync(apiServiceUrl + "UserRight/GetMenuCatList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<MenuDetails>> SearchMenuChekedList(int menucatid)
        {
            var myContent = JsonConvert.SerializeObject(menucatid);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.GetAsync(apiServiceUrl + "UserRight/SearchMenuList/" + menucatid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<MenuDetails>>(content);
            return result;
        }

        public async Task<int> GetNewId()
        {
            var response = await client.GetAsync(apiServiceUrl + "UserRight/GetNewId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<string> PostUserMenu(UserRights value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "UserRight", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ViewUserRights>> GetViewMenuList(int userid)
        {
            var response = await client.GetAsync(apiServiceUrl + "UserRight/GetViewMenuList/" + userid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewUserRights>>(content);
            return result;
        }

        public async Task<string> RemoveMenu(UserRights value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "UserRight/RemoveMenu", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
    }
}
