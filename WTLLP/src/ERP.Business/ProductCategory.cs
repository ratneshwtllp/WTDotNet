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
    public class ProductCategory : IProductCategoryContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public ProductCategory(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "ProductCategory/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<int> GetNewProductCategoryId()
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductCategory/GetNewProductCategoryId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<ProductCategoryDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductCategory/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ProductCategoryDetails>(content);
            return result;
        }

        public async Task<string> Post(ProductCategoryDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "ProductCategory", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(ProductCategoryDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "ProductCategory", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ProductCategoryDetails>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductCategory");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ProductCategoryDetails>>(content);
            return result;
        }

        public async Task<List<ViewProductCategory >> GetProductCategoryList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductCategory/GetProductCategoryList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProductCategory>>(content);
            return result;
        }

        public async Task<int> CheckDuplicate(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductCategory/checkduplicate/?value=" + search);
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content); 
        }

        public async Task<string> GetBuyerList()
        {
            var response = await client.GetAsync(apiServiceUrl + "brand/GetBuyerList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetPositionList()
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductCategory/GetPositionList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetModifyPositionList()
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductCategory/GetModifyPositionList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
