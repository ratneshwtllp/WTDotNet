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
    public class Invoice : IInvoiceContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;
        string reportServiceUrl;

        public Invoice(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
            this.reportServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ReportServiceUrl");
        }

        public async Task<List<InvoiceMaster>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "invoice");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<InvoiceMaster>>(content);
            return result;
        }

        public async Task<ViewInvoice> Get(long invoiceid)
        {
            var response = await client.GetAsync(apiServiceUrl + "invoice/" + invoiceid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewInvoice>(content);
            return result;
        }

        public async Task<List<ViewInvoice>> Get(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "invoice/Get/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewInvoice>>(content);
            return result;
        }

        public async Task<List<ViewInvoiceChild>> GetChild(long invoiceid)
        {
            var response = await client.GetAsync(apiServiceUrl + "invoice/GetChild/" + invoiceid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewInvoiceChild>>(content);
            return result;
        }

        public async Task<long> GetNewInvoiceID()
        {
            var response = await client.GetAsync(apiServiceUrl + "invoice/GetNewid");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<long> GetNewInvoiceChildID()
        {
            var response = await client.GetAsync(apiServiceUrl + "invoice/GetNewChildid");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content); ;
        }

        public async Task<long> GetNewInvoiceSerial()
        {
            var response = await client.GetAsync(apiServiceUrl + "invoice/GetNewSerial");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> GetNewInvoiceNO()
        {
            var response = await client.GetAsync(apiServiceUrl + "invoice/GetNewNo");
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

        public async Task<string> GetPendingPackingNoList()
        {
            var response = await client.GetAsync(apiServiceUrl + "packing/GetPendingList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetSinglePackingNo(long packingid)
        {
            var response = await client.GetAsync(apiServiceUrl + "packing/GetSinglePackingNo/" + packingid);    //workplan
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<ConsigneeDetails> GetConsigneeRecord(long consigneeid)
        {
            var response = await client.GetAsync(apiServiceUrl + "consignee/" + consigneeid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ConsigneeDetails>(content);
            return result;
        }

        public async Task<List<ViewPackingWeight>> GetNoofCartonsByDimension(long pckid)
        {
            var response = await client.GetAsync(apiServiceUrl + "packingweight/GetNoofCartonsByDimension/" + pckid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewPackingWeight>>(content);
            return result;
        }

        public async Task<ViewPackingMaster> GetBuyerFromPacking(long pckid)
        {
            var response = await client.GetAsync(apiServiceUrl + "packing/GetBuyerFromPacking/" + pckid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewPackingMaster>(content);
            return result;
        }

        public async Task<List<ViewPackingChild>> GetRecordForInvoice(long pckid)
        {
            var response = await client.GetAsync(apiServiceUrl + "packing/GetRecordForInvoice/" + pckid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewPackingChild>>(content);
            return result;
        }

        public async Task<List<ViewPackingChild>> GetRecord(long pckid)
        {
            var response = await client.GetAsync(apiServiceUrl + "packing/GetRecord/" + pckid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewPackingChild>>(content);
            return result;
        }

        public async Task<List<PackingWeightDetails>> GetPackingWeightDetails(long packingid)
        {
            var response = await client.GetAsync(apiServiceUrl + "packingweight/Get/" + packingid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<PackingWeightDetails>>(content);
            return result;
        }

        public async Task<string> Post(InvoiceMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "invoice", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(InvoiceMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "invoice", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Cancel(long invoiceid)
        {
            var response = await client.GetAsync(apiServiceUrl + "invoice/Cancel/" + invoiceid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> Delete(long invoiceid)
        {
            var response = await client.GetAsync(apiServiceUrl + "invoice/Delete/" + invoiceid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewInvoicePrint>> GetInvoicePrint(long invoiceid)
        {
            var response = await client.GetAsync(apiServiceUrl + "invoice/GetInvoicePrint/" + invoiceid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewInvoicePrint>>(content);
            return result;
        }
        public async Task<List<ViewInvoice>> ViewInvoice(InvoiceSearch IS)
        {
            var myContent = JsonConvert.SerializeObject(IS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "invoice/ViewInvoice", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewInvoice>>(content);
            return result;
        }
        public async Task<string> GetBuyerList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Buyer/BuyerList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetMonthList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Month/GetMonthList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetYearList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Year/GetYearList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetFinalDestinationList()
        {
            var response = await client.GetAsync(apiServiceUrl + "invoice/GetFinalDestinationList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetCurrencyList()
        {
            var response = await client.GetAsync(apiServiceUrl + "invoice/GetCurrencyList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetSessionYearList()
        {
            var response = await client.GetAsync(apiServiceUrl + "SessionYear/GetSessionYearList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
    }
}
