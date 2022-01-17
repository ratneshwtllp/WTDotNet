using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace ERP.Business.Contracts
{
    public interface IIssueContract
    {
        Task<List<IssueMaster>> Get();
        Task<IssueMaster> Get(long issueid);

        Task<List<ViewIssueRMWP>> GetList(StoreSearch obj); 

        Task<long> GetNewIssueMasterID();
        Task<long> GetNewIssueChildID(); 

        Task<long> GetNewIssueSerial();
        Task<string> GetNewIssueNO();
          
        Task<string> Post(IssueMaster value); 
        Task<string> Delete(long issueid);
         
        Task<string> GetPlanNoList();
        Task<string> PutIssueStatus(long issueid);

        Task<List<ViewWorkPlanBOM>> GetWorkPlanRMDetails(long planid);
        Task<List<ViewWorkPlanBOM>> GetRequiredQtyDetail(long planid, long rmid);
        Task<string> GetSupplierListFromRM(long rmid);
        Task<string> GetRackListFromRitemNSupplierid(long ritemid, long supplierid);
        Task<ViewRItemShow> GetRItemShow(long ritemid);
        Task<List<ViewWorkPlanChild>> GetWorkPlanDetails(long planid);
        Task<ViewWorkPlan> GetViewWorkPlan(long planid);
        Task<List<ViewIssueRMWP>> GetRMIssuePrint(long id);
         
        Task<string> GetMonthList();
        Task<string> GetYearList();
        Task<string> GetSessionYearList();
        Task<string> GetShortSessionYearList();
        Task<string> GetSupplierList();
        Task<string> GetContractorList();

        //return
        Task<long> GetNewReturnID();
        Task<string> PostReturn(ReturnRMWP value);
        Task<List<ViewReturnRMWP>> GetReturnRMWP(long planid);
        Task<List<ViewReturnRMWP>> GetReturnRMWP(StoreSearch obj);
        Task<string> GetRackNoList(long ritemid, long supplierid);
        Task<double> GetRMRackStock(long ritemid, long suppid, long rackid, string lotno);
        Task<string> GetBatch_LotNoFromRM_Supp_Rack(long ritemid, long suppid, long rackid);
        //Task<double> GetRMRackStockFromBatchLotNo(long rowidlotid);
        Task<double> GetLotRMRate(long rmid, long suppid, string lotno);


        //Request RawMaterial
        Task<string> GetRMCategoryList();
        Task<string> GetRMSubCategoryList(long catid);
        Task<string> GetRMList(int scatid);
        Task<string> GetRitemDetails(long id);
        Task<string> PostRequestRM(RequestRM value);
        Task<string> GetReqRMList();
        Task<string> PutRequestRM(RequestRM value);
        Task<string> GetNewReqRMNo();

        Task<string> GetReqRMNoList(long DepartmentId);
        Task<List<ViewReqRMList>> GetViewReqRMList(long ReqRMID);
        Task<List<ViewRequestRM>> GetViewRequestRM(StoreSearch obj);
        Task<List<ViewRequestRM>> RequestRMPrint(long ReqRMID);
        Task<List<ViewRequestRM>> GetViewRequestRMPending(string search);

        Task<string> PostRequestRMIssue(RequestRMIssue value);
        Task<string> GetNewReqRMIssueNo();
        Task<List<ViewRequestRMIssue>> GetViewRequestRMIssue(string search);
        Task<List<ViewRequestRMIssue>> GetViewRequestRMIssue(StoreSearch obj);
        Task<string> PutRequestCompleteStatus(long ReqRMID);
        Task<string> PutRequestCancelStatus(long ReqRMID);

        Task<string> PostReturnRM(ReturnRM value);
        Task<string> GetNewReturnRMNo();
        Task<string> GetRetRMNoList();
        Task<string> GetNewReturnRMRcvNo();
        Task<List<ViewRetRMList>> GetViewRetRMList(long ReturnRMID);
        Task<string> PostReturnRMRecv(ReturnRMRecv value);
        Task<List<ViewReturnRMRecv>> GetViewReturnRMRecv(StoreSearch obj);
        Task<string> GetDepartmentList();
        Task<List<ViewRequestRM>> GetReqRMRecord(long ReqRMId);
        Task<List<ViewRetRMList>> GetReturnRMRecord(long RetRMId);

        Task<string> GetProcessNoList();
        Task<List<ViewProcessBOM>> GetViewProcessBOM(long prmasterid);
        Task<ViewProcessExecutionMaster> GetProcessDetails(long prmasterid);
        Task<List<ViewProcessExecutionMaster>> GetProcessItemList(long prmasterid);
        Task<string> GetViewPRNoForIssueRM();
        #region  StoreRMRequest

        Task<string> GetBranchList();
        Task<string> GetStockTransferRequestNo();
        Task<int> GetNewStockTransferRequestID();
        Task<int> GetNewStockTransferRequestSNo();
        Task<int> GetNewStockTransferRequestChildID();
        Task<string> PostStockTransferRequest(StockTransferRequest value);
        Task<string> PutStockTransferRequest(StockTransferRequest value);
        Task<List<ViewStockTransferRequest>> GetStockTransferRequestList(StoreSearch obj);
        Task<ViewStockTransferRequest> GetStockTransferRequestDetails(long id);
        Task<List<ViewStockTransferRequest>> GetStockTransferRequestChild(long id);
        Task<string> DeleteStockTransferRequest(long id);
        Task<List<ViewStockTransferRequest>> GetStockTransferRequestPrint(long id);

        Task<int> GetNewStockTransferIssueID();
        Task<string> GetStockTransferIssueNo();
        Task<string> GetStockTransferRequestList();
        Task<string> TransportList();
        Task<List<ViewStockTransferRequest>> GetStockTransferRequestForIssue(long StockTransferRequestId);
        Task<int> GetNewStockTransferIssueChildID();

        Task<string> PostStockTransferIssue(StockTransferIssue value);
        Task<string> PutStockTransferIssue(StockTransferIssue value);
        Task<List<ViewStockTransferIssue>> GetStockTransferIssueList(StoreSearch obj);
        Task<List<ViewStockTransferIssue>> GetStockTransferIssuePrint(long id);

        Task<ViewStockTransferIssue> GetStockTransferIssueDetails(long id);
        Task<List<ViewStockTransferIssue>> GetStockTransferIssueChild(long id);
        Task<string> DeleteStockTransferIssue(long id);
        #endregion

        #region DailyMaterialIssueNote
        Task<List<ViewIssueRMWP>> GetDailyMaterialIssueNote(StoreSearch obj);
        Task<List<ViewRequestRMIssue>> GetDailyMaterialIssueNoteList(StoreSearch obj);
        #endregion
    }
}