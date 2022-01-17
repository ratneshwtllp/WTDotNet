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
    public class WorkPlan : IWorkPlanContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;
        string reportServiceUrl;

        public WorkPlan(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
            this.reportServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ReportServiceUrl");
            client.DefaultRequestHeaders.ConnectionClose = true;
        }

        public async Task<List<ViewWorkPlan>> Get()
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewWorkPlan>>(content);
            return result;
        }

        public async Task<ViewWorkPlan> Get(long id)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewWorkPlan>(content);
            return result;
        }

        public async Task<long> GetNewPlanID()
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetNewPlanID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<long> GetNewPlanChildID()
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetNewPlanChildID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content); ;
        }

        public async Task<string> GetNewPlanNO()
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetNewPlanNO");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<int> GetNewPlanSerial()
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetNewPlanSerial");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<List<ViewSaleOrderDetails>> GetOrderItems(long orderid)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "saleorder/GetOrderItems/" + orderid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewSaleOrderDetails>>(content);
            return result;
        }

        public async Task<List<ViewWorkPlanChild>> GetWorkPlanDetails(long planid)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetWorkPlanDetails/" + planid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewWorkPlanChild>>(content);
            return result;
        }

        public async Task<List<ViewSaleOrder>> SaleOrderRecord()
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "saleorder");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewSaleOrder>>(content);
            return result;
        }

        public async Task<string> Post(WorkPlanMaster value)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "workplan", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
         
        public async Task<string> DeleteWorkPlan(long planid)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.DeleteAsync(apiServiceUrl + "workplan/DeleteWorkPlan/" + planid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> PutWPStatus(long planid)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/PutWPStatus/" + planid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> PutWPCompleteStatus(long planid)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/PutWPCompleteStatus/" + planid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetBuyerList()
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "brand/GetBuyerList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetContractorList()
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "contractor/GetContractorList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetActiveContractorList()
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "contractor/GetActiveContractorList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetPayableContractorList()
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "contractor/GetPayableContractorList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetOrderNoList(long buyerid)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetOrderNoList/" + buyerid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetProSubCategoryFromBuyer(long buyerid)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "product/GetProSubCategoryFromBuyer/" + buyerid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetFItemCodeList(long subcategoryid)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "product/GetFItemCodeList/" + subcategoryid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetFitemDetails(long fitemid)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "SaleOrder/FItemShow/" + fitemid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewWorkPlanChild>> GetOrderItemsForBalance(long orderid)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetOrderItemsForBalance/" + orderid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewWorkPlanChild>>(content);
            return result;
        }

        public async Task<List<ViewWorkPlanBOM>> GetWorkPlanRMDetails(long planid)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetWorkPlanRMDetails/" + planid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewWorkPlanBOM>>(content);
            return result;
        }

        public async Task<string> GetProcessList()
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "ProductionProcess/ProcessList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetProcessListForPayment()
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "ProductionProcess/ProcessListForPayment");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }


        public async Task<string> ProductProcessList(long FitemId)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "ProductionProcess/ProductProcessList/" + FitemId);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetProcessOfWP(long planid)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "ProductionProcess/ProcessOfWP/" + planid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetPlanNoList()
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetPlanNoList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetComponentList()
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetComponentList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetComponentList(long FitemId)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetComponentList/"+ FitemId);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetNewPRNo()
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetNewPRNo");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> PostProcessExecution(ProcessExecutionMaster value)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "workplan/PostProcessExecution", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ViewProcessExecutionMaster>> ProcessList(ProcessSearch PS)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(PS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "WorkPlan/ProcessList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProcessExecutionMaster>>(content);
            return result;
        }

        public async Task<List<ViewProcessExecution>> GetProcessListForRecv(ProcessSearch PS)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(PS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "WorkPlan/GetProcessListForRecv", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProcessExecution>>(content);
            return result;
        }

        public async Task<List<ViewProcessExecutionMaster>> GetProcessListForContractorPayment(ProcessSearch PS)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(PS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "WorkPlan/GetProcessListForContractorPayment", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProcessExecutionMaster>>(content);
            return result;
        }
         
        public async Task<string> PostProcessRecv(ProcessRecvMaster value)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "workplan/PostProcessRecv", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ViewProcessRecvMaster>> ProcessRecvList(ProcessSearch PS)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(PS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "WorkPlan/ProcessRecvList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProcessRecvMaster>>(content);
            return result;
        }

        public async Task<List<ViewWIP>> GetViewWIP(ProcessSearch PS)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(PS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "WorkPlan/GetViewWIP", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewWIP>>(content);
            return result;
        }

        public async Task<List<ViewWIP>> WipList(ProcessSearch PS)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(PS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "WorkPlan/WipList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewWIP>>(content);
            return result;
        }
         
        public async Task<double> GetProcessCharge(SearchModel SM)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(SM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "WorkPlan/GetProcessCharge", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = Convert.ToDouble(content);
            return result;
        }

        public async Task<int> GetWPQtyForNextProcess(SearchModel SM)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(SM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "WorkPlan/GetWPQtyForNextProcess", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = Convert.ToInt16(content);
            return result;
        }

        public async Task<int> IsStartWPDone(SearchModel SM)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(SM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "WorkPlan/IsStartWPDone", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = Convert.ToInt16(content);
            return result;
        }

        public async Task<string> GetProcessNoList()
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetProcessNoList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetProcessNoListForProcessRecv()
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetProcessNoListForProcessRecv");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<ViewProcessExecution> GetProcessBalQty(SearchModel SM)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(SM);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "WorkPlan/GetProcessBalQty", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewProcessExecution>(content);
            //var result = Convert.ToInt32(content);
            return result;
        }

        public async Task<string> PutReview(long PRMasterId)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "WorkPlan/PutReview/" + PRMasterId);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> PutWPProcessCharge(long PRMasterId, double ProcessCharge)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "WorkPlan/PutWPProcessCharge/" + PRMasterId +"/"+ ProcessCharge);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<ViewContractorBalance> GetContractorBalance(long contractorid)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "WorkPlan/GetContractorBalance/" + contractorid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewContractorBalance>(content);
            return result;
        }

        public async Task<long> GetNewContractorPaymentId()
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetNewContractorPaymentId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<long> GetNewContractorPaymentChildId()
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetNewContractorPaymentChildId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<long> GetNewContractorAdvanceId()
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetNewContractorAdvanceId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> PostContractorPayment(ContractorPayment value)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "workplan/PostContractorPayment", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
         
        public async Task<string> PostContractorAdvance(ContractorAdvanceDetails value)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "workplan/PostContractorAdvance", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ViewContractorPayment>> GetContractorPaymentList(WorkPlanSearch WS)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(WS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "WorkPlan/GetContractorPaymentList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewContractorPayment>>(content);
            return result;
        }

        public async Task<List<ViewContractorPayment>> GetContractorPaymentPrint(long contractorpaymentid)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "WorkPlan/GetContractorPaymentPrint/" + contractorpaymentid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewContractorPayment>>(content);
            return result;
        }

        public async Task<List<ViewContractorAdvanceDetails>> GetContractorAdvancePrint(long contractoradvanceid)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "WorkPlan/GetContractorAdvancePrint/" + contractoradvanceid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewContractorAdvanceDetails>>(content);
            return result;
        }

        public async Task<List<ViewProcessExecution>> GetProductionProcessPrint()
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "WorkPlan/GetProductionProcessPrint");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProcessExecution>>(content);
            return result;
        }

        public async Task<List<ViewContractorAdvanceDetails>> GetContractorAdvanceList(WorkPlanSearch WS)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(WS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "WorkPlan/GetContractorAdvanceList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewContractorAdvanceDetails>>(content);
            return result;
        }

        public async Task<List<ViewWorkPlanChild>> GetViewWorPlan(WorkPlanSearch WP)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(WP);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "WorkPlan/GetViewWorPlan", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewWorkPlanChild>>(content);
            return result;
        }

        public async Task<long> GetNewTableId()
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetNewTableId");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> PostTempTableDetails(List<TempTableDetails> value)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "workplan/PostTempTableDetails", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ViewProductListForProcessCharge>> ProductListForProcessCharge(WorkPlanSearch PS)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(PS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "workplan/ProductListForProcessCharge", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProductListForProcessCharge>>(content);
            return result;
        }

        public async Task<string> PostProcessCharge(List<ProcessChargeDetails> value)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "workplan/PostProcessCharge", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<WorkPlanChild> GetProductDetail(long fitemid)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetProductDetail/" + fitemid.ToString()); 
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<WorkPlanChild>(content);
            return result;
        }

        public async Task<List<ViewProcessFinishGoodsStock>> FinishGoodsStockList(ProcessSearch PS)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(PS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "WorkPlan/FinishGoodsStockList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProcessFinishGoodsStock>>(content);
            return result;
        }
        public async Task<List<ViewWIP>> GetProductionFlowStatus(ProcessSearch PS)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(PS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "WorkPlan/GetProductionFlowStatus", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewWIP>>(content);
            return result;
        }

        public async Task<List<ViewProcessBOM>> GetViewProcessBOM(long prmasterid)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetViewProcessBOM/" + prmasterid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProcessBOM>>(content);
            return result;
        }

        public async Task<string> GetProductList(long subcategoryid)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "product/GetProductList/" + subcategoryid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewProductComponent>> GetViewProductComponentList(long fitemid)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "Product/GetViewProductComponentList/" + fitemid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProductComponent>>(content);
            return result;
        }

        public async Task<List<ViewProductProcessDetails>> ProductProcessListForCharge(long fitemid)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "ProductionProcess/ProductProcessListForCharge/" + fitemid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProductProcessDetails>>(content);
            return result;
        }

        public async Task<string> PostProductProcessCharge(List<ProductProcessCharge> value)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "workplan/PostProductProcessCharge", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<List<ProductProcessCharge>> GetProductProcessCharge(long fitemid, int sizeid)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetProductProcessCharge/" + fitemid + "/"+ sizeid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ProductProcessCharge>>(content);
            return result;
        }

        public async Task<List<ViewProductProcessCharge>> GetProductProcessCharge(long fitemid)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetProductProcessCharge/" + fitemid );
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProductProcessCharge>>(content);
            return result;
        }

        public async Task<string> GetProductionProcessDetails(long processid)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetProductionProcessDetails/" + processid.ToString());
            var content = await response.Content.ReadAsStringAsync();

            //var result = JsonConvert.DeserializeObject<ProductionProcessDetails>(content);
            return content;
        }

        public async Task<string> GetProductSizeList(long fitemid)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "Costing/GetSizeForAnlysis/" + fitemid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<ViewContractorDetails> GetContractorRecord(long id)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "contractor/GetContractorRecord/" + id);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewContractorDetails>(content);
            return result;
        }


        public async Task<List<ViewSaleOrderWPStatus>> GetSaleOrderWPStatus(ProcessSearch PS)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(PS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "WorkPlan/GetSaleOrderWPStatus", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewSaleOrderWPStatus>>(content);
            return result;
        }

        public async Task<List<ViewRunningProcessReport>> GetRunningProcessList(ProcessSearch PS)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(PS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "WorkPlan/GetRunningProcessList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRunningProcessReport>>(content);
            return result;
        }

        public async Task<List<ViewProcessRecvMaster>> GetWeeklyProduction(ProcessSearch PS)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(PS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "WorkPlan/GetWeeklyProduction", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProcessRecvMaster>>(content);
            return result;
        }

        public async Task<List<ViewProcessRecvMaster>> GetMonthlyProduction(ProcessSearch PS)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(PS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "WorkPlan/GetMonthlyProduction", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProcessRecvMaster>>(content);
            return result;
        }

        public async Task<string> GetMonthList()
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "Month/GetMonthList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetYearList()
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.GetAsync(apiServiceUrl + "Year/GetYearList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        #region Order Status
        public async Task<List<ViewOrderStatus>> GetOrderStatus(ProcessSearch PS)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(PS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "WorkPlan/GetOrderStatus", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewOrderStatus>>(content);
            return result;
        }

        public async Task<List<ViewProductProcess>> GetViewProductProcess(ProcessSearch PS)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(PS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "WorkPlan/GetViewProductProcess", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProductProcess>>(content);
            return result;
        }

        public async Task<List<ProcessExecutionMaster>> GetProcessExecutionMaster(ProcessSearch PS)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(PS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "WorkPlan/GetProcessExecutionMaster", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ProcessExecutionMaster>>(content);
            return result;
        }
        #endregion

        public async Task<string> DeletePR(long PRMasterId)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var response = await client.DeleteAsync(apiServiceUrl + "workplan/DeletePR/" + PRMasterId);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewProcessExecutionMaster>> BalProcessForSupervisorList(ProcessSearch PS)
        {
            client.DefaultRequestHeaders.ConnectionClose = true;
            var myContent = JsonConvert.SerializeObject(PS);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "WorkPlan/BalProcessForSupervisorList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProcessExecutionMaster>>(content);
            return result;
        }
    }
}
