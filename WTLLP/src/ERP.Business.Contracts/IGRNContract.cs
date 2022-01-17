using ERP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IGRNContract
    {
        Task<long> GetNewGRNID();
        Task<long> GetNewGRNChildID();
        Task<long> GetNewGRNSerial();
        Task<long> GetNewGRNFileId();
        Task<string> GetNewGRNNO();

        Task<string> GetSupplierList();
        Task<string> GetPONoList(long suppid);
        Task<string> Post(GRNMaster value);
        Task<string> Put(GRNMaster value);
        //Task<string> PostGRNFile(List<GRNFile> value);
        Task<string> PutGRNStatus(long grnid);
        Task<string> PutStoreKeepingStatus(long grnid);
        Task<List<ViewGRNMaster>> Get();
        Task<GRNMaster> Get(long Grnid);
        Task<List<ViewPOForGRN>> GetPoChildRecord(long poid);
        Task<List<ViewPOForGRN>> GetMultiPOChildRecord(long[] poids);
        Task<List<ViewGRN>> GetList(string search);
        Task<List<ViewGRN>> GetListCancel(string search);
        void Delete(int id);
        Task<List<ViewGRN>> GetGRNList(GRNSearch GS);
        Task<List<GRNMaster>> CheckBillChallan(GRNSearch GS);
        Task<string> GetMonthList();
        Task<string> GetYearList();
        Task<string> GetSessionYearList();
        Task<List<ViewGRN>> GetGRNPrint(long GRNId);
        Task<int> GetIsBatchOfRM();

        Task<string> GetRMList(long supplierid);
        Task<string> GetRMDetails(int ritemid);
        Task<string> GetListBranch();
        Task<List<ViewGRNFile>> GetGRNFileRecord(long id);
        Task<string> GRNDocumentTypeList(long GRNId);
        Task<string> PostGRNFile(GRNFile value);
        Task<int> DeleteGRNFile(long id);
        Task<string> GRNFileLocation(long Id);

        Task<string> GetJWNoList(long suppid);
        Task<List<ViewJWForGRN>> GetMultiJWChildRecord(long[] issuermchangeid);
        Task<List<ViewIssueRMForChange>> GetMultiJWIssueRMList(long[] issuermchangeid);

        Task<string> GetPONoListAgainstDate(GRNSearch search);
        Task<string> GetJWNoListAgainstDate(GRNSearch search);
        Task<GRNMaster> GetGRNNo_Session(GRNMaster _oGRNMaster);

        Task<ViewSupplierDetails> GetSupplierDetails(long suppid);
        Task<ViewBranchDetails> GetBranchDetails(long branchid);
        Task<string> GetTaxList();
    }

}