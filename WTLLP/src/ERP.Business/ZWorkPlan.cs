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
    public class ZWorkPlan : IZWorkPlanContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;
        string reportServiceUrl;

        public ZWorkPlan(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
            this.reportServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ReportServiceUrl");
        }

        public async Task<List<ViewWorkPlan>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "zworkplan");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewWorkPlan>>(content);
            return result;
        }

        public async Task<ViewWorkPlan> Get(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "zworkplan/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewWorkPlan>(content);
            return result;
        }

        public async Task<long> GetNewPlanID()
        {
            var response = await client.GetAsync(apiServiceUrl + "zworkplan/GetNewPlanID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<long> GetNewPlanChildID()
        {
            var response = await client.GetAsync(apiServiceUrl + "zworkplan/GetNewPlanChildID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content); ;
        }

        public async Task<string> GetNewPlanNO()
        {
            var response = await client.GetAsync(apiServiceUrl + "zworkplan/GetNewPlanNO");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<int> GetNewPlanSerial()
        {
            var response = await client.GetAsync(apiServiceUrl + "zworkplan/GetNewPlanSerial");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<List<ViewSaleOrderDetails>> GetZOrderItems()
        {
            var response = await client.GetAsync(apiServiceUrl + "saleorder/GetZOrderItems");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewSaleOrderDetails>>(content);
            return result;
        }

        public async Task<List<ViewWorkPlanChild>> GetZWorkPlanDetails(long planid)
        {
            var response = await client.GetAsync(apiServiceUrl + "zworkplan/GetZWorkPlanDetails/" + planid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewWorkPlanChild>>(content);
            return result;
        }

        public async Task<List<ViewSaleOrder>> SaleOrderRecord()
        {
            var response = await client.GetAsync(apiServiceUrl + "saleorder");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewSaleOrder>>(content);
            return result;
        }

        public async Task<string> Post(WorkPlanMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "zworkplan", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> PutWPStatus(long planid)
        {
            var response = await client.GetAsync(apiServiceUrl + "zworkplan/PutWPStatus/" + planid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetBuyerList()
        {
            var response = await client.GetAsync(apiServiceUrl + "brand/GetBuyerList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetContractorList()
        {
            var response = await client.GetAsync(apiServiceUrl + "contractor/GetContractorList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetOrderNoList(long buyerid)
        {
            var response = await client.GetAsync(apiServiceUrl + "zworkplan/GetOrderNoList/" + buyerid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetProSubCategoryFromBuyer(long buyerid)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetProSubCategoryFromBuyer/" + buyerid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetFItemCodeList(long subcategoryid)
        {
            var response = await client.GetAsync(apiServiceUrl + "product/GetFItemCodeList/" + subcategoryid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetFitemDetails(long fitemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "SaleOrder/FItemShow/" + fitemid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
         
        public async Task<List<ViewWorkPlanChild>> GetOrderItemsForBalance(long orderid)
        {
            var response = await client.GetAsync(apiServiceUrl + "zworkplan/GetOrderItemsForBalance/" + orderid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewWorkPlanChild>>(content);
            return result;
        }

        public async Task<List<ViewWorkPlanBOM>> GetWorkPlanRMDetails(long planid)
        {
            var response = await client.GetAsync(apiServiceUrl + "zworkplan/GetWorkPlanRMDetails/" + planid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewWorkPlanBOM>>(content);
            return result;
        }

        public async Task<string> GetProcessList()
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductionProcess/ProcessList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetProcessOfWP(long planid)
        {
            var response = await client.GetAsync(apiServiceUrl + "ProductionProcess/ProcessOfWP/"+ planid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetPlanNoList()
        {
            var response = await client.GetAsync(apiServiceUrl + "zworkplan/GetPlanNoList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
         
        public async Task<string> GetComponentList()
        {
            var response = await client.GetAsync(apiServiceUrl + "zworkplan/GetComponentList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetNewPRNo()
        {
            var response = await client.GetAsync(apiServiceUrl + "zworkplan/GetNewPRNo");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> PostProcessExecution(ProcessExecutionMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "zworkplan/PostProcessExecution", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ViewProcessExecution>> ProcessList(ProcessSearch PS)
        {
            var myContent = JsonConvert.SerializeObject(PS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "zworkplan/ProcessList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProcessExecution>>(content);
            return result;
        }

        public async Task<string> PostProcessRecv(ProcessRecvMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "zworkplan/PostProcessRecv", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ViewProcessRecv>> ProcessRecvList(ProcessSearch PS)
        {
            var myContent = JsonConvert.SerializeObject(PS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "zworkplan/ProcessRecvList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProcessRecv>>(content);
            return result;
        }


        public async Task<List<ViewWIP>> WipList(ProcessSearch PS)
        { 
            var myContent = JsonConvert.SerializeObject(PS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "zworkplan/WipList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewWIP>>(content);
            return result;
        }


        public async Task<double> GetProcessCharge(SearchModel SM)
        {  
            var myContent = JsonConvert.SerializeObject(SM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "zworkplan/GetProcessCharge", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = Convert.ToDouble(content);
            return result; 
        }

        public async Task<string> GetProcessNoList()
        {
            var response = await client.GetAsync(apiServiceUrl + "zworkplan/GetProcessNoList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }


        public async Task<ViewProcessExecution> GetProcessBalQty(SearchModel SM)
        {
            var myContent = JsonConvert.SerializeObject(SM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "zworkplan/GetProcessBalQty", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewProcessExecution>(content);
            //var result = Convert.ToInt32(content);
            return result;
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
         
        public async Task<List<ViewWorkPlanChild>> ZWPList(WorkPlanSearch WPS)
        {
            var myContent = JsonConvert.SerializeObject(WPS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "zworkplan/ZWPList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewWorkPlanChild>>(content);
            return result;
        }

        public async Task<string> GetSessionYearList()
        {
            var response = await client.GetAsync(apiServiceUrl + "sessionyear/GetSessionYearList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetYearList()
        {
            var response = await client.GetAsync(apiServiceUrl + "year/GetYearList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetMonthList()
        {
            var response = await client.GetAsync(apiServiceUrl + "month/GetMonthList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewWorkPlanChild>> GetZWorkPlanPrint(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "zworkplan/GetZWorkPlanPrint/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewWorkPlanChild>>(content);
            return result;
        }
    }
}
