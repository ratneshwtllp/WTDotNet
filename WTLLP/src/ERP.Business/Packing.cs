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
    public class Packing : IPackingContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;
        string reportServiceUrl;

        public Packing(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
            this.reportServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ReportServiceUrl");
        }

        public async Task<List<PackingMaster>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "packing");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<PackingMaster>>(content);
            return result;
        }

        public async Task<PackingMaster> Get(long packingid)
        {
            var response = await client.GetAsync(apiServiceUrl + "packing/" + packingid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<PackingMaster>(content);
            return result;
        }


        public async Task<List<ViewPackingMaster>> Viewpacking(PackingSearch PS)  
        {
            var myContent = JsonConvert.SerializeObject(PS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "packing/Viewpacking", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewPackingMaster>>(content);
            return result;
        }


        public async Task<long> GetNewPackingID()
        {
            var response = await client.GetAsync(apiServiceUrl + "packing/GetNewid");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<long> GetNewPackingChildID()
        {
            var response = await client.GetAsync(apiServiceUrl + "packing/GetNewChildid");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<long> GetNewPackingSerial()
        {
            var response = await client.GetAsync(apiServiceUrl + "packing/GetNewSerial");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> GetNewPackingNO()
        {
            var response = await client.GetAsync(apiServiceUrl + "packing/GetNewNo");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetBuyerCodeList()
        {
            var response = await client.GetAsync(apiServiceUrl + "packing/GetBuyerCodeList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetOrderNoList(long buyerid)
        {
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetOrderNoList/" + buyerid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetDimensionList()
        {
            var response = await client.GetAsync(apiServiceUrl + "carton/GetDimensionList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewPackingChild>> GetPackingChild(long packingid)
        {
            var response = await client.GetAsync(apiServiceUrl + "packing/GetChild/" + packingid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewPackingChild>>(content);
            return result;
        }

        public async Task<List<ViewOrderItemsForPacking>> GetOrderItemsForPacking(long orderid)
        {
            var response = await client.GetAsync(apiServiceUrl + "saleorder/GetItems/" + orderid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewOrderItemsForPacking>>(content);
            return result;
        }

        public async Task<string> Post(PackingMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "packing", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(PackingMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "packing", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> ChangeStatus(long packingid)
        {
            var response = await client.GetAsync(apiServiceUrl + "packing/ChangeStatus/" + packingid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        //public async Task<string> CancelPacking(long packingid)
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "packing/Cancel/" + packingid);
        //    var content = await response.Content.ReadAsStringAsync();
        //    return content;
        //}

        //public async Task<string> DeletePacking(long packingid)
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "packing/Delete/" + packingid);
        //    var content = await response.Content.ReadAsStringAsync();
        //    return content;
        //}

        public async Task<long> GetNewCartonNo()
        {
            var response = await client.GetAsync(apiServiceUrl + "packing/GetNewCartonNo");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<List<ViewShoesPacking>> GetShoesPackingRecord(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "packing/GetShoesPackingRecord/" + id);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewShoesPacking>>(content);
            return result;
        }

        public async Task<ViewProductSize> GetProductDetails(string barcode)
        {
            var response = await client.GetAsync(apiServiceUrl + "productsize/GetProductDetails/" + barcode);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewProductSize>(content);
            return result;
        }

        public async Task<ViewSaleOrderChild> GetBarcodeDetails(long orderid, string barcode)
        {
            var response = await client.GetAsync(apiServiceUrl + "packing/GetBarcodeDetails/" + orderid + "/" + barcode);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewSaleOrderChild>(content);
            return result;
        }

        public async Task<long> GetNewShoesCartonMasterId()
        {
            var response = await client.GetAsync(apiServiceUrl + "packing/GetNewShoesCartonMasterId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<long> GetNewShoesCartonChildId()
        {
            var response = await client.GetAsync(apiServiceUrl + "packing/GetNewShoesCartonChildId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> PostShoesCarton(ShoesCartonMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "packing/PostShoesCarton", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ViewShoesCartonChild>> GetShoesCartonList(PackingSearch search)
        {
            var myContent = JsonConvert.SerializeObject(search);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "packing/GetShoesCartonList", byteContent);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewShoesCartonChild>>(content);
            return result;
        }
         
        public async Task<List<ViewShoesCartonChild>> GetShoesCartonListForPacking(PackingSearch search)
        {
            var myContent = JsonConvert.SerializeObject(search);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "packing/GetShoesCartonListForPacking", byteContent);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewShoesCartonChild>>(content);
            return result;
        }

        public async Task<List<ViewShoesPackingChild>> GetShoesPackingList(PackingSearch search)
        {
            var myContent = JsonConvert.SerializeObject(search);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "packing/GetShoesPackingList", byteContent);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewShoesPackingChild>>(content);
            return result;
        }
        public async Task<List<ViewProductionPacking >> ProductionPackingPrint(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "packing/ProductionPackingPrint/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProductionPacking>>(content);
            return result;
        }
        public async Task<List<ViewPackingChild >> PackingSlipPrint(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "packing/PackingSlipPrint/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewPackingChild>>(content);
            return result;
        }

        public async Task<string> PostBuyerCartonStickerTemplate(BuyerCartonStickerTemplate value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "packing/PostBuyerCartonStickerTemplate", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }


        public async Task<BuyerCartonStickerTemplate> GetBuyerCartonSticker(long buyerid)
        {
            var response = await client.GetAsync(apiServiceUrl + "packing/GetBuyerCartonSticker/" + buyerid.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<BuyerCartonStickerTemplate>(content);
            return result;
        }

        public async Task<string> PutBarcodeFromPacking(ProductMultipleSize value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "product/PutBarcodeFromPacking", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
    }
}
