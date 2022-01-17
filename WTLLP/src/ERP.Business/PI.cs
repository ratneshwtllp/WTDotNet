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
    public class PI : IPIContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;
        string reportServiceUrl;

        public PI(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
            this.reportServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ReportServiceUrl");
        }

        public async Task<List<PIMaster>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "pi");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<PIMaster>>(content);
            return result;
        }

        public async Task<ViewPI> Get(long pimasterid)
        {
            var response = await client.GetAsync(apiServiceUrl + "pi/" + pimasterid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewPI>(content);
            return result;
        }

        public async Task<List<ViewPI>> Get(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "pi/Get/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewPI>>(content);
            return result;
        }

        public async Task<List<ViewPIChild>> GetChild(long pimasterid)
        {
            var response = await client.GetAsync(apiServiceUrl + "pi/GetChild/" + pimasterid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewPIChild>>(content);
            return result;
        }

        public async Task<long> GetNewPIMasterID()
        {
            var response = await client.GetAsync(apiServiceUrl + "pi/GetNewid");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<long> GetNewPIChildID()
        {
            var response = await client.GetAsync(apiServiceUrl + "pi/GetNewChildid");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content); ;
        }

        public async Task<long> GetNewPISerial()
        {
            var response = await client.GetAsync(apiServiceUrl + "pi/GetNewSerial");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> GetNewPINO()
        {
            var response = await client.GetAsync(apiServiceUrl + "pi/GetNewNo");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }


        public async Task<string> GetConsigneeNameList()
        {
            var response = await client.GetAsync(apiServiceUrl + "consignee/GetConsigneeNameList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> FOBCIFList()
        {
            var response = await client.GetAsync(apiServiceUrl + "transport/FOBCIFList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetBuyerNameList()
        {
            var response = await client.GetAsync(apiServiceUrl + "buyer/GetBuyerNameList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetOrderNoListForPI(long buyerid)
        {
            var response = await client.GetAsync(apiServiceUrl + "saleorder/GetOrderNoListForPI/" + buyerid);    //workplan
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetSingleOrderNo(long orderid)
        {
            var response = await client.GetAsync(apiServiceUrl + "saleorder/GetSingleOrderNo/" + orderid);    //workplan
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        

        public async Task<ViewBuyerDetails> GetBuyerDetails(long buyerid)
        {
            var response = await client.GetAsync(apiServiceUrl + "buyer/GetBuyerDetails/" + buyerid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewBuyerDetails>(content);
            return result;
        }

        public async Task<ConsigneeDetails> GetConsigneeRecord(long consigneeid)
        {
            var response = await client.GetAsync(apiServiceUrl + "consignee/" + consigneeid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ConsigneeDetails>(content);
            return result;
        }

        public async Task<List<ViewSaleOrderDetails>> GetOrderItems(long orderid)
        {
            var response = await client.GetAsync(apiServiceUrl + "saleorder/GetOrderItems/" + orderid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewSaleOrderDetails>>(content);
            return result;
        }

        public async Task<string> Post(PIMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "pi", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(PIMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "pi", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Cancel(long pimasterid)
        {
            var response = await client.GetAsync(apiServiceUrl + "pi/Cancel/" + pimasterid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> Delete(long pimasterid)
        {
            var response = await client.GetAsync(apiServiceUrl + "pi/Delete/" + pimasterid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewPIPrint>> GetPIPrint(long piid)
        {
            var response = await client.GetAsync(apiServiceUrl + "pi/GetPIPrint/" + piid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewPIPrint>>(content);
            return result;
        }

    }
}
