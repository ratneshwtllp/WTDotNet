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


namespace ERP.Business
{
    public class BRMProperties : IRMPropertiesContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public BRMProperties(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }
      
        public async Task<int> GetNewRMPId()
        {
            var response = await client.GetAsync(apiServiceUrl + "RMProperties/GetNewRPId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<string> Post(RMProperties value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "RMProperties", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
        public async Task<string> Put(RMProperties value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "RMProperties", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
       
        public async Task<List<RMProperties>> RMPropertiesList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "RMProperties/RMPropertiesList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<RMProperties>>(content);
            return result;
        }
        public async Task<RMProperties> GetValue(long fitemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "RMProperties/GetValueRMPro/" + fitemid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<RMProperties>(content);
            return result;
        }
        public async Task<int> Delete(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "RMProperties/Delete/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }
        //For RMPValue
        public async Task<string> GetRMPValueList()
        {
            var response = await client.GetAsync(apiServiceUrl + "RMProperties/GetRMPValueList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<int> GetNewRMPValueId()
        {
            var response = await client.GetAsync(apiServiceUrl + "RMProperties/GetNewRPValueId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<string> PostValue(RMPropertiesValue value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "RMProperties/PostValue", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }       
        public async Task<string> PutValue(RMPropertiesValue value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PutAsync(apiServiceUrl + "RMProperties/PutValue", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
        public async Task<List<ViewRMPropertiesDetails>> RMProValueList(RMProSearch PS)
        {
            var myContent = JsonConvert.SerializeObject(PS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "RMProperties/RMProValueList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRMPropertiesDetails>>(content);
            return result;
        }
        public async Task<List<ViewRMPropertiesDetails>> RMProValueList(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "RMProperties/RMProValueList/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewRMPropertiesDetails>>(content);
            return result;
        }

        public async Task<RMPropertiesValue> GetRMValue(long fitemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "RMProperties/GetValueRPro/" + fitemid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<RMPropertiesValue>(content);
            return result;
        }
        public async Task<int> DeleteVal(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "RMProperties/DeleteValue/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        //For Mapping
        public async Task<string> GetCategoryList()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/RMCategory");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<RMProperties>> GetRMproList()
        {
            var response = await client.GetAsync(apiServiceUrl + "RMProperties/GetRMProList");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<RMProperties>>(content);
            return result;
        }

        public async Task<long> GetNewMapId()
        {
            var response = await client.GetAsync(apiServiceUrl + "RMProperties/GetNewMapId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<long> GetNewRMProId()
        {
            var response = await client.GetAsync(apiServiceUrl + "RMProperties/GetNewRMProId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }
        public async Task<string> PostMapping(RMPropertiesMapping value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "RMProperties/PostMapping", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ViewRMPropertiesMapping>> RMMappingList(RMProSearch RMS)
        {
            var myContent = JsonConvert.SerializeObject(RMS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "RMProperties/RMMappingList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRMPropertiesMapping>>(content);
            return result;
        }

        public async Task<int> DeleteMapping(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "RMProperties/DeleteMapping/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<RMPropertiesMapping> GetMapping(long fitemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "RMProperties/GetMapping/" + fitemid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<RMPropertiesMapping>(content);
            return result;
        }
        public async Task<List<RMPropertiesMapping>> GetMapList()
        {
            var response = await client.GetAsync(apiServiceUrl + "RMProperties/GetMapList");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<RMPropertiesMapping>>(content);
            return result;
        }

        public async Task<List<RMPropertiesMapping >> GetRMMapProperties(long catid)
        {
            var response = await client.GetAsync(apiServiceUrl + "RMProperties/GetRMMapProperties/" + catid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<RMPropertiesMapping>>(content);
            return result;
        }

        public async Task<int> DeleteRMmapping(long  id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "RMProperties/DeleteRMmapping/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }
      
        public async Task<int> CheckRMPValue(long id,string value,long rmpvalueid)
        {
            var response = await client.GetAsync(apiServiceUrl + "RMProperties/CheckRMPValue/" + id+"/"+ value+"/"+rmpvalueid.ToString ());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<int> CheckRMP( string value, long rmpid)
        {
            var response = await client.GetAsync(apiServiceUrl + "RMProperties/CheckRMP/" + value + "/" + rmpid.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }
    }
}
