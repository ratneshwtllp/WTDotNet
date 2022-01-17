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
    public class RMStock : IRMStockContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public RMStock(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<List<ViewRMStock>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "rmstock");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRMStock>>(content);
            return result;
        }

        public async Task<List<ViewRMStock>> Get(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "rmstock/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRMStock>>(content);
            return result;
        }

        public async Task<string> GetRMCategoryList()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/RMCategory");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetRMSubCategoryList(long catid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/RMSubCategory/" + catid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetRMList(int scatid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetRMList/" + scatid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewRMStock>> GetViewRMCurrentStock(RMSearch RMS)
        {
            var myContent = JsonConvert.SerializeObject(RMS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "rmstock/GetViewRMCurrentStock", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRMStock>>(content);
            return result;
        }

        public async Task<List<ViewRMStockRackWise>> GetViewRMStockRackWise(long ritemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rmstock/GetViewRMStockRackWise/" + ritemid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRMStockRackWise>>(content);
            return result;
        }

        public async Task<List<RMMovement>> GetRMMovement(RMSearch RMS)
        {
            var myContent = JsonConvert.SerializeObject(RMS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "RMStock/GetRMMovement", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<RMMovement>>(content);
            return result;
        }

        public async Task<List<RMMovement>> RMMovementPrint()
        {
            RMSearch RMS = new RMSearch();
            var myContent = JsonConvert.SerializeObject(RMS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "RMStock/RMMovementPrint", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<RMMovement>>(content);
            return result;
        }

        public async Task<List<ViewRMLedger>> GetViewRMLedger(RMSearch RMS)
        {
            var myContent = JsonConvert.SerializeObject(RMS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "rmstock/GetViewRMLedger", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRMLedger>>(content);
            return result;
        }

        public async Task<double> GetViewRMLedgerOPOnDate(RMSearch RMS)
        {
            var myContent = JsonConvert.SerializeObject(RMS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "rmstock/GetViewRMLedgerOPOnDate", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = Convert.ToDouble(content);
            return result;
        }

        public async Task<List<ViewRMStockRackWise>> GetViewRMStockRackWise(RMSearch RMS)
        {
            var myContent = JsonConvert.SerializeObject(RMS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "RMStock/GetViewRMStockRackWise", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRMStockRackWise>>(content);
            return result;
        }

        public async Task<string> GetStoreList()
        {
            var response = await client.GetAsync(apiServiceUrl + "StoreMaster/GetStoreList/");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetRMList()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetRMList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewRMStatus>> GetViewRMStatus(RMSearch RMS)
        {
            var myContent = JsonConvert.SerializeObject(RMS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "rmstock/GetViewRMStatus", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRMStatus>>(content);
            Console.Write("Hello Status") ;
            return result;
        }

        public async Task<string> CalculateRMStatus()
        {
            var response = await client.GetAsync(apiServiceUrl + "rmstock/CalculateRMStatus");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }


        public async Task<List<ViewWorkPlanBOMRuning>> GetViewWorkPlanBOMRuning(RMSearch RMS)
        {
            var myContent = JsonConvert.SerializeObject(RMS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "rmstock/GetViewWorkPlanBOMRuning", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewWorkPlanBOMRuning>>(content);
            return result;
        }


        public async Task<List<ViewSaleOrderItemBalBOM>> GetViewSaleOrderItemBalBOM(RMSearch RMS)
        {
            var myContent = JsonConvert.SerializeObject(RMS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "rmstock/GetViewSaleOrderItemBalBOM", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewSaleOrderItemBalBOM>>(content);
            return result;
        }
    }
}
