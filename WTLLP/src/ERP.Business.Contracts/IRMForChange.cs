using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace ERP.Business.Contracts
{
    public interface IRMForChangeContract
    {
        Task<List<ViewIssueRMForChange >> GetViewIssueRMForChange(long IssueRMChangeID);
        Task<List<ViewIssueRMForChange>> GetViewIssueRMForChange(SearchDetails SD);
        Task<List<ViewIssueRMForChangeMaster>> GetViewIssueRMForChangeMaster(long IssueRMChangeID);
        Task<List<RecvJW>> GetRecvJW(long IssueRMChangeID);
        Task<List<ViewIssueRMForChangeMaster>> GetMaster();
        Task<List<ViewIssueRMForChangeMaster>> GetMasterCompleted();
        Task<List<ViewIssueRMForChangeMaster>> GetBalanceList();
        Task<long> GetNewIssueRMChangeID();
        Task<long> GetNewRecvJWID();
        Task<long> GetNewIssueRMChangeChildID();
        Task<long> GetNewIssueRMChangeItemID();
        Task<string> Post(IssueRMforChangeMaster  value);
        Task<string> PostJW(RecvJW value);
        Task<long> GetIssueRMChangeSerial(int jwtype);
        Task<string> GetIssueRMChangeNo(int jwtype);
        Task<string> GetContractorList();
        Task<string> GetRMCategoryList();
        Task<string> GetRMSubCategoryList(long catid);
        Task<string> GetRMList(int scatid);
        Task<string> GetRMListFromSupplier(long SupplierId);
        Task<string> GetSupplierList(long ritemid);
        Task<string> GetRackListFromRitemNSupplierid(long ritemid, long supplierid);
        Task<string> GetRackNoList(long ritemid, long supplierid);
        Task<string> GetQtyOnRack(long ritemid, long supplierid,long rackid);
        Task<string> GetRitemDetails(long id);
        Task<string> DeleteIssueRMchange(long id);
        Task<string> UpdateIssueRM(long IssueRMChangeChildID);
        Task<string> IsComplete(long id);
        Task<string> RMSuppRate(long rmid, long suppid);
        Task<SupplierDetails> GetSupplierDetails(long supplierid);
        Task<string> GetCurrencyofSupplier(long suppid);

        Task<string> GetMonthList();
        Task<string> GetYearList();
        Task<string> GetSessionYearList();
        Task<string> GetSupplierList();

        #region ReturnToSupplier RTS
        Task<string> GetBatch_LotNoFromRM_Supp_Rack(long ritemid, long suppid, long rackid);
        //Task<double> GetRMRackStockFromBatchLotNo(long rowidlotid);
        Task<double> GetRMRackStock(long ritemid, long suppid, long rackid, string lotno);
        Task<string> GetRMLotStock(long ritemid, long suppid, long rackid, string lotno);
        Task<string> GetRTSNo();
        Task<string> PostRTS(ReturnToSupplier value);
        Task<List<ViewRTS>> ViewRTS(string search);
        Task<List<ViewRTS>> ViewRTS(StoreSearch obj);
        Task<string> GetLastPO(long ritemid, long supplierid);
        Task<string> PutRTSApproved(long RTSID);
        Task<string> PutRTSDecline(long RTSID);

        Task<string> GetRMList();
        Task<string> GetShortSessionYearList();
        #endregion
    }
}