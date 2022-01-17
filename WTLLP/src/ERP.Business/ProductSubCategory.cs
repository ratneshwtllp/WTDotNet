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
    public class ProductSubCategory : IProductSubCategoryContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public ProductSubCategory(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<string> ProductCategoryList()
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductSubCategory/GetProductCategory");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetMultiLevelSubCat()
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductSubCategory/GetMultiLevelSubCat");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewProduct >> GetProductSubCategoryList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductSubCategory/GetProductSubCategoryList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProduct>>(content);
            return result;
        }

        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "ProductSubCategory/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<long> GetNewProductSubCategoryId()
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductSubCategory/GetNewProductSubCategoryId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<ProductSubCategoryDetails> Get(int id)
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductSubCategory/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ProductSubCategoryDetails >(content);
            return result;
        }

        public async Task<string> MPost(ProductSubCategoryDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "ProductSubCategory/MPost", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> MPut(ProductSubCategoryDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "ProductSubCategory/MPut", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ProductSubCategoryDetails>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductSubCategory");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ProductSubCategoryDetails>>(content);
            return result;
        }

        public async Task<int> CheckDuplicate(string search,long ProductCategoryID)
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductSubCategory/CheckDuplicate/?value=" + search + "&ProductCategoryID=" + ProductCategoryID);
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content); 
        }

        public async Task<int> CheckDuplicate(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductSubCategory/CheckDuplicate/?value=" + search);
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<string> Post(ProductSubCategoryDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "ProductSubCategory", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(ProductSubCategoryDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "ProductSubCategory", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
    }
}
