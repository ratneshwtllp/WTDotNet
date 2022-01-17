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
    public class Issue : IIssueContract
    {
        private static HttpClient client = new HttpClient();
        private IConfiguration Configuration { get; set; }
        string apiServiceUrl;
        string reportServiceUrl;

        public Issue(IConfiguration configuration)
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Configuration = configuration;
            this.apiServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ApiServiceUrl");
            this.reportServiceUrl = Configuration.GetValue<string>("WebApplicationSettings:ReportServiceUrl");
        }

        public async Task<List<IssueMaster>> Get()
        {
            var response = await client.GetAsync(apiServiceUrl + "issue");
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<IssueMaster>>(content);
            return result;
        }

        public async Task<IssueMaster> Get(long issueid)
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/" + issueid.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IssueMaster>(content);
            return result;
        }

        public async Task<long> GetNewIssueMasterID()
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetNewIssueMasterID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<long> GetNewIssueChildID()
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetNewIssueChildID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content); ;
        }

        public async Task<long> GetNewIssueSerial()
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetNewIssueSerial");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content);
        }

        public async Task<string> GetNewIssueNO()
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetNewIssueNO");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> Post(IssueMaster value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "issue", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> Delete(long issueid)
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/Delete/" + issueid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetPlanNoList()
        {
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetPlanNoList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewWorkPlanBOM>> GetWorkPlanRMDetails(long planid)
        {
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetWorkPlanRMDetails/" + planid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewWorkPlanBOM>>(content);
            return result;
        }

        public async Task<List<ViewWorkPlanBOM>> GetRequiredQtyDetail(long planid, long rmid)
        {
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetRequiredQtyDetail/" + planid + "/" + rmid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewWorkPlanBOM>>(content);
            return result;
        }

        public async Task<string> GetSupplierListFromRM(long rmid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetSupplierListFromRM/" + rmid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetRackListFromRitemNSupplierid(long ritemid, long supplierid)
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetRackListFromRitemNSupplierid/" + ritemid + "/" + supplierid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<ViewRItemShow> GetRItemShow(long ritemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/RItemShow/" + ritemid.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewRItemShow>(content);
            return result;
        }

        public async Task<string> PutIssueStatus(long issueid)
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/PutIssueStatus/" + issueid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewWorkPlanChild>> GetWorkPlanDetails(long planid)
        {
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetWorkPlanDetails/" + planid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewWorkPlanChild>>(content);
            return result;
        }

        public async Task<ViewWorkPlan> GetViewWorkPlan(long planid)
        {
            var response = await client.GetAsync(apiServiceUrl + "workplan/" + planid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ViewWorkPlan>(content);
            return result;
        }

        public async Task<List<ViewIssueRMWP>> GetRMIssuePrint(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetRMIssuePrint/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewIssueRMWP>>(content);
            return result;
        }

        public async Task<List<ViewIssueRMWP>> GetList(StoreSearch obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "issue/GetList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewIssueRMWP>>(content);
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

        public async Task<string> GetSupplierList()
        {
            var response = await client.GetAsync(apiServiceUrl + "supplier/GetSupplier");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetContractorList()
        {
            var response = await client.GetAsync(apiServiceUrl + "contractor/GetContractorList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<long> GetNewReturnID()
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetNewReturnID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt64(content); ;
        }
        public async Task<string> PostReturn(ReturnRMWP value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "issue/PostReturn", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
        public async Task<List<ViewReturnRMWP>> GetReturnRMWP(long planid)
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetReturnRMPWP/" + planid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewReturnRMWP>>(content);
            return result;
        }
        public async Task<List<ViewReturnRMWP>> GetReturnRMWP(StoreSearch obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "issue/GetReturnRMWP", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewReturnRMWP>>(content);
            return result;
        }

        public async Task<string> GetRackNoList(long ritemid, long supplierid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rackmaster/GetRackNoList/" + supplierid + "/" + ritemid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<double> GetRMRackStock(long ritemid, long suppid, long rackid, string lotno)
        {
            var response = await client.GetAsync(apiServiceUrl + "RMStock/GetRMRackStock/" + ritemid + "/" + suppid + "/" + rackid + "?lotno=" + lotno.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToDouble(content); ;
        }

        //public async Task<double> GetRMRackStockFromBatchLotNo(long rowidlotid)
        //{
        //    var response = await client.GetAsync(apiServiceUrl + "RMStock/GetRMRackStockFromBatchLotNo/" + rowidlotid);
        //    var content = await response.Content.ReadAsStringAsync();
        //    return Convert.ToDouble(content); ;
        //}

        public async Task<double> GetLotRMRate(long rmid, long suppid, string lotno)
        {
            var response = await client.GetAsync(apiServiceUrl + "RMStock/GetLotRMRate/" + rmid + "/" + suppid + "/" + lotno.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToDouble(content); ;
        }

        public async Task<string> GetBatch_LotNoFromRM_Supp_Rack(long ritemid, long suppid, long rackid)
        {
            var response = await client.GetAsync(apiServiceUrl + "RMStock/GetBatch_LotNoFromRM_Supp_Rack/" + ritemid + "/" + suppid + "/" + rackid);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        #region Request RawMaterial
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
        public async Task<string> GetRitemDetails(long ritemid)
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/RItemShow/" + ritemid.ToString());
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetNewReqRMNo()
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetNewReqRMNo");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> PostRequestRM(RequestRM value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "issue/PostRequestRM", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
        public async Task<string> GetReqRMList()
        {
            var response = await client.GetAsync(apiServiceUrl + "rm/GetRMList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> PutRequestRM(RequestRM value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PutAsync(apiServiceUrl + "issue/PutRequestRM", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
        public async Task<string> GetReqRMNoList(long DepartmentId)
        {
            var response = await client.GetAsync(apiServiceUrl + "Issue/GetReqRMNoList/"+ DepartmentId);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<List<ViewReqRMList>> GetViewReqRMList(long ReqRMID)
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetViewReqRMList/" + ReqRMID);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewReqRMList>>(content);
            return result;
        }
        public async Task<List<ViewRequestRM>> GetReqRMRecord(long ReqRMId)
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetReqRMRecord/" + ReqRMId);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRequestRM>>(content);
            return result;
        }

        public async Task<List<ViewRetRMList>> GetReturnRMRecord(long RetRMId)
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetReturnRMRecord/" + RetRMId);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRetRMList>>(content);
            return result;
        }


        public async Task<List<ViewRequestRM>> GetViewRequestRM(StoreSearch obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "issue/GetViewRequestRM", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRequestRM>>(content);
            return result;
        }
        public async Task<List<ViewRequestRM>> GetViewRequestRMPending(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetViewRequestRMPending/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRequestRM>>(content);
            return result;
        }
        public async Task<List<ViewRequestRM>> RequestRMPrint(long ReqRMID)
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/RequestRMPrint/" + ReqRMID);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRequestRM>>(content);
            return result;
        }
        public async Task<string> PostRequestRMIssue(RequestRMIssue value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "issue/PostRequestRMIssue", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
        public async Task<string> GetNewReqRMIssueNo()
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetNewReqRMIssueNo");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<List<ViewRequestRMIssue>> GetViewRequestRMIssue(string search)
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetViewRequestRMIssue/?search=" + search);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRequestRMIssue>>(content);
            return result;
        }
        public async Task<List<ViewRequestRMIssue>> GetViewRequestRMIssue(StoreSearch obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "issue/GetViewRequestRMIssue", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRequestRMIssue>>(content);
            return result;
        }
        public async Task<string> PutRequestCompleteStatus(long ReqRMID)
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/PutRequestCompleteStatus/" + ReqRMID);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> PutRequestCancelStatus(long ReqRMID)
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/PutRequestCancelStatus/" + ReqRMID);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> PostReturnRM(ReturnRM value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "issue/PostReturnRM", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
        public async Task<string> GetNewReturnRMNo()
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetNewReturnRMNo");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetRetRMNoList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Issue/GetRetRMNoList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> GetNewReturnRMRcvNo()
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetNewReturnRMRcvNo");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<List<ViewRetRMList>> GetViewRetRMList(long ReturnRMID)
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetViewRetRMList/" + ReturnRMID);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRetRMList>>(content);
            return result;
        }
        public async Task<string> PostReturnRMRecv(ReturnRMRecv value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "issue/PostReturnRMRecv", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
        public async Task<List<ViewReturnRMRecv>> GetViewReturnRMRecv(StoreSearch obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "issue/GetViewReturnRMRecv", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewReturnRMRecv>>(content);
            return result;
        }

        public async Task<string> GetDepartmentList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Department/GetListDepartment");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetProcessNoList()
        {
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetProcessNoList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewProcessBOM>> GetViewProcessBOM(long prmasterid)
        {
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetViewProcessBOM/" + prmasterid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProcessBOM>>(content);
            return result;
        }

        public async Task<ViewProcessExecutionMaster> GetProcessDetails(long prmasterid)
        {
            try
            {
                var response = await client.GetAsync(apiServiceUrl + "workplan/GetProcessDetails/" + prmasterid);
                var content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<ViewProcessExecutionMaster>(content);
                return result;
            }
            catch (Exception ex)
            {
                throw ;
            }
        }

        public async Task<List<ViewProcessExecutionMaster>> GetProcessItemList(long prmasterid)
        {
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetProcessItemList/" + prmasterid);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewProcessExecutionMaster>>(content);
            return result;
        }

        public async Task<string> GetViewPRNoForIssueRM()
        {
            var response = await client.GetAsync(apiServiceUrl + "workplan/GetViewPRNoForIssueRM");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        #endregion


        #region StockRMRequest
        public async Task<string> GetBranchList()
        {
            var response = await client.GetAsync(apiServiceUrl + "Branch/GetListBranch");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<int> GetNewStockTransferRequestID()
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetNewStockTransferRequestID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<int> GetNewStockTransferRequestSNo()
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetNewStockTransferRequestSNo");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<int> GetNewStockTransferRequestChildID()
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetNewStockTransferRequestChildID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<string> PostStockTransferRequest(StockTransferRequest value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "issue/PostStockTransferRequest", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> PutStockTransferRequest(StockTransferRequest value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "issue/PutStockTransferRequest", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> GetStockTransferRequestNo()
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetStockTransferRequestNo");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewStockTransferRequest>> GetStockTransferRequestList(StoreSearch obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "issue/GetStockTransferRequestList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewStockTransferRequest>>(content);
            return result;
        }

        public async Task<ViewStockTransferRequest> GetStockTransferRequestDetails(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetStockTransferRequestDetails/" + id);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ViewStockTransferRequest>(content);
            return result;
        }

        public async Task<List<ViewStockTransferRequest>> GetStockTransferRequestChild(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "Issue/GetStockTransferRequestChild/" + id);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewStockTransferRequest>>(content);
            return result;
        }
        public async Task<string> DeleteStockTransferRequest(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/DeleteStockTransferRequest/" + id);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<string> GetShortSessionYearList()
        {
            var response = await client.GetAsync(apiServiceUrl + "sessionyear/GetShortSessionYearList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        public async Task<List<ViewStockTransferRequest>> GetStockTransferRequestPrint(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetStockTransferRequestPrint/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewStockTransferRequest>>(content);
            return result;
        }

        public async Task<string> GetStockTransferRequestList()
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetStockTransferRequestList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }





        public async Task<int> GetNewStockTransferIssueID()
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetNewStockTransferIssueID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<string> GetStockTransferIssueNo()
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetStockTransferIssueNo");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<string> TransportList()
        {
            var response = await client.GetAsync(apiServiceUrl + "transport/TransportList");
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        public async Task<List<ViewStockTransferRequest>> GetStockTransferRequestForIssue(long StockTransferRequestId)
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetStockTransferRequestForIssue/" + StockTransferRequestId);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewStockTransferRequest>>(content);
            return result;
        }
        public async Task<int> GetNewStockTransferIssueChildID()
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetNewStockTransferIssueChildID");
            var content = await response.Content.ReadAsStringAsync();
            return Convert.ToInt16(content);
        }

        public async Task<string> PostStockTransferIssue(StockTransferIssue value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "issue/PostStockTransferIssue", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }

        public async Task<string> PutStockTransferIssue(StockTransferIssue value)
        {
            var myContent = JsonConvert.SerializeObject(value);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(apiServiceUrl + "issue/PutStockTransferIssue", byteContent);
            var content = response.Content.ReadAsStringAsync();
            return (content.Result);
        }
        public async Task<List<ViewStockTransferIssue>> GetStockTransferIssueList(StoreSearch obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "issue/GetStockTransferIssueList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewStockTransferIssue>>(content);
            return result;
        }
        public async Task<List<ViewStockTransferIssue>> GetStockTransferIssuePrint(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetStockTransferIssuePrint/" + id.ToString());
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewStockTransferIssue>>(content);
            return result;
        }

        public async Task<ViewStockTransferIssue> GetStockTransferIssueDetails(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/GetStockTransferIssueDetails/" + id);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ViewStockTransferIssue>(content);
            return result;
        }

        public async Task<List<ViewStockTransferIssue>> GetStockTransferIssueChild(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "Issue/GetStockTransferIssueChild/" + id);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ViewStockTransferIssue>>(content);
            return result;
        }

        public async Task<string> DeleteStockTransferIssue(long id)
        {
            var response = await client.GetAsync(apiServiceUrl + "issue/DeleteStockTransferIssue/" + id);
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }

        #endregion

        #region DailyMatrialIssueNote
        public async Task<List<ViewIssueRMWP>> GetDailyMaterialIssueNote(StoreSearch obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "issue/GetDailyMaterialIssueNote", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewIssueRMWP>>(content);
            return result;
        }

        public async Task<List<ViewRequestRMIssue>> GetDailyMaterialIssueNoteList(StoreSearch obj)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(apiServiceUrl + "issue/GetDailyMaterialIssueNoteList", byteContent);
            var content = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<ViewRequestRMIssue>>(content);
            return result;
        }
        #endregion
    }
}
