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
    public class Menu : IMenuContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public Menu(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<string> MenuCategoryNameList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Menu/MenuCategoryNameList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<List<ViewMenu>> GetMenuDetailsList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "Menu/GetMenuDetailsList");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewMenu>>(content);
            return result;
        }
        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "Menu/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<int> GetNewMenuId()
        {
            var response = await client.GetAsync(apiServiceUrl + "Menu/GetNewMenuId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<MenuDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "Menu/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<MenuDetails>(content);
            return result;
        }

        public async Task<string> Post(MenuDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "Menu", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(MenuDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "Menu", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ViewMenu>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "Menu");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewMenu>>(content);
            return result;
        }

        public async Task<int> CheckDuplicate(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "Menu/CheckDuplicate/?value=" + search );
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content); 
        }

        public async Task<List<ViewMenu>> GetList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "Menu/GetList");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewMenu>>(content);
            return result;
        }

        public async Task<int> GetNewId()
        {
            var response = await client.GetAsync(apiServiceUrl + "Menu/GetNewId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

    }
}
