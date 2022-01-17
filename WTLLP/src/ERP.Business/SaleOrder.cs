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
    public class SaleOrder : ISaleOrderContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;

        public SaleOrder(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
        }

        public async Task<ViewSaleOrder> Get(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "SaleOrder/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ViewSaleOrder>(content);
            return result;
        }

        public async Task<List<ViewSaleOrder>> ViewSaleOrder()
        {
            var response = await client.GetAsync(apiServiceUrl + "SaleOrder");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewSaleOrder>>(content);
            return result;
        }

        public async Task<List<ViewSaleOrder>> GetViewSaleOrder(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "SaleOrder/GetViewSaleOrder/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewSaleOrder>>(content);
            return result;
        }

        public async Task<List<ViewSaleOrderDetails>> GetSaleOrderChild(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "SaleOrder/GetSaleOrderChild/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewSaleOrderDetails>>(content);
            return result;
        }

        public async Task<long> GetNewOrderMasterID()
        {
            var response = await client.GetAsync(apiServiceUrl + "SaleOrder/NewOrderMasterID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content); ;
        }
        public async Task<long> GetOrderChildID()
        {
            var response = await client.GetAsync(apiServiceUrl + "SaleOrder/NewOrderChildID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content); ;
        }


        public async Task<string> GetProCategoryList()
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductCategory/GetProCategoryList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetProSubCategoryList(long pcategoryId)
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductSubCategory/GetProSubCategoryList/" + pcategoryId.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetProSubCategoryFromBuyer(long buyerid)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetProSubCategoryFromBuyer/" + buyerid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetProductList(long subcategoryid)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetMainProductCodeList/" + subcategoryid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetFItemCodeList(long subcategoryid)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetFItemCodeList/" + subcategoryid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetPartyFItemList(long Partyid)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetPartyFItemListWithPartyCode/" + Partyid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetProductDetails(long fitemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/" + fitemid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> BuyerList()
        {
            var response = await client.GetAsync(apiServiceUrl + "SaleOrder/Fillbuyer");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetFitemDetails(long fitemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "SaleOrder/FItemShow/" + fitemid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<SaleOrderMaster> CheckDuplicateOrder(string OrderNo)
        {
            var response = await client.GetAsync(apiServiceUrl + "SaleOrder/CheckDuplicateOrder/" + OrderNo.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SaleOrderMaster>(content);
            return result;
        }

        public async Task<string> Post(SaleOrderMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "SaleOrder", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(SaleOrderMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "SaleOrder", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
        public void UpdateOrder(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CheckDuplicate(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "SaleOrder/CheckDuplicate/?value=" + search);
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }
        public async Task<string> GetIssuedByList(int buyerId)
        {
            var response = await client.GetAsync(apiServiceUrl + "IssuedBy/GetIssuedByList/" + buyerId.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> TransportList()
        {
            var response = await client.GetAsync(apiServiceUrl + "transport/TransportList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> FOBCIFList()
        {
            var response = await client.GetAsync(apiServiceUrl + "transport/FOBCIFList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> PutOrderStatus(long OrderMasterID)
        {
            var response = await client.GetAsync(apiServiceUrl + "SaleOrder/PutOrderStatus/" + OrderMasterID.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<long> GetNewOrderShippingId()
        {
            var response = await client.GetAsync(apiServiceUrl + "SaleOrder/GetNewOrderShippingId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<ViewSaleOrder> GetShippingDetails(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "SaleOrder/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ViewSaleOrder>(content);
            return result;
        }

        public async Task<string> Post(OrderShippingDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "OrderShipping", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Put(OrderShippingDetails value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "OrderShipping", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ViewSaleOrderDetails>> GetOrderItems(long orderid)
        {
            var response = await client.GetAsync(apiServiceUrl + "saleorder/GetOrderItems/" + orderid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewSaleOrderDetails>>(content);
            return result;
        }
        public async Task<List<ViewSaleOrderDetails>> GetViewSaleOrder(OrderSearch SO)
        {
            var myContent = JsonConvert.SerializeObject(SO);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "saleorder/SaleOrderList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewSaleOrderDetails>>(content);
            return result;
        }
        public async Task<string> GetBuyerCodeList()
        {
            var response = await client.GetAsync(apiServiceUrl + "buyer/GetBuyerCodeList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetMultiLevelSubCat(long catid)
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductSubCategory/GetMultiLevelSubCat/" + catid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetSizeList(long fitemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "saleorder/GetSizeList/" + fitemid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewSaleOrderDetails>> GetSaleOrderPrint(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "saleorder/GetSaleOrderPrint/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewSaleOrderDetails>>(content);
            return result;
        }

        public async Task<string> PutRequestCancelStatus(SaleOrderMaster so)
        {
            var myContent = JsonConvert.SerializeObject(so);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "saleorder/PutRequestCancelStatus", byteContent);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }


        public async Task<string> PutRequestUndoStatus(SaleOrderMaster so)
        {
            var myContent = JsonConvert.SerializeObject(so);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "saleorder/PutRequestUndoStatus", byteContent);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> PostShoesSale(SaleOrderMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "SaleOrder/PostShoesSale", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> PutShoesSale(SaleOrderMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "SaleOrder/PutShoesSale", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<SaleOrderChild>> GetOrderProductList(long ordermasterid)
        {
            var response = await client.GetAsync(apiServiceUrl + "saleorder/GetOrderProductList/" + ordermasterid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<SaleOrderChild>>(content);
            return result;
        }

        public async Task<List<ViewSaleOrderDetails>> GetOrderItemsDetail(long ordermasterid, long fitemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "saleorder/GetOrderItemsDetail/" + ordermasterid + "/" + fitemid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewSaleOrderDetails>>(content);
            return result;
        }

        public async Task<List<ViewProductSize>> GetProductSizeList(long fitemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "saleorder/GetProductSizeList/" + fitemid.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProductSize>>(content);
            return result;
        }

        public async Task<double> GetFitemSizePrice(long fitemid, int sizeid)
        {
            var response = await client.GetAsync(apiServiceUrl + "SaleOrder/GetFitemSizePrice/" + fitemid + "/" + sizeid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToDouble(content);
        }

        #region OrderStatus
        public async Task<string> GetOrderNoList(long buyerid)
        {
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetOrderNoList/" + buyerid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewSaleOrderDetails>> GetOrderItemsDetails(long orderid)
        {
            var response = await client.GetAsync(apiServiceUrl + "saleorder/GetOrderItemsDetails/" + orderid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewSaleOrderDetails>>(content);
            return result;
        }

        public async Task<List<ViewSaleOrderStatus>> GetOrderStatus(long orderid, long orderchildid)
        {
            var response = await client.GetAsync(apiServiceUrl + "saleorder/GetOrderStatus/" + orderid + "/" + orderchildid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewSaleOrderStatus>>(content);
            return result;
        }
        #endregion

        #region SaleOrderCloseFile
        //public async Task<string> GetOrderNoList(long buyerid)
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "SaleOrder/GetBuyerOrderNoList/" + buyerid);
        //    var content = await response.Content.ReadAsStringAsync();
        //    return content;
        //}
        public async Task<List<ViewSaleOrderDetails>> GetSaleOrderDetails(long orderid)
        {
            var response = await client.GetAsync(apiServiceUrl + "saleorder/GetSaleOrderDetails/" + orderid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewSaleOrderDetails>>(content);
            return result;
        }

        public async Task<string> PostOrdeSheetClose(OrderSheetClose value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "SaleOrder/PostOrdeSheetClose", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ViewSaleOrderSheetClose>> GetViewSaleOrderSheetClose(OrderSearch SO)
        {
            var myContent = JsonConvert.SerializeObject(SO);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "saleorder/GetViewSaleOrderSheetClose", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewSaleOrderSheetClose>>(content);
            return result;
        }

        public async Task<int> DeleteOrderSheetClose(int id)
        {
            var response = await client.DeleteAsync(apiServiceUrl + "saleorder/DeleteOrderSheetClose/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            return Convert.ToInt16(content);
        }

        public async Task<string> GetOrderNoListForClose(long buyerid)
        {
            var response = await client.GetAsync(apiServiceUrl + "SaleOrder/GetOrderNoListForClose/" + buyerid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        #endregion
    }
}
