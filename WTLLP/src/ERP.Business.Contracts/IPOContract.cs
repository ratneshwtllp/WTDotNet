
using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace ERP.Business.Contracts
{
    public interface IPOContract
    {
        void Delete(int id);
        Task<string> GetRMCategoryList(long suppid);
        Task<string> GetRMSubCategoryList(long suppid, long catid);
        Task<string> GetRMList(long suppid, long scatid);
        Task<string> GetRMListFromSupplier(long SupplierId);
        Task<string> GetSupplierList();
        Task<string> FillTax();
        Task<double> GetTaxRate(int taxid);
        Task<string> RMSuppRate(long rmid, long suppid);
        Task<string> GetRMDetails(int ritemid);
        Task<string> GetNewPONO();
        Task<int> GetNewPOSerial();
        Task<long> GetNewPOID();
        Task<long> GetNewPOChildID();
        Task<List<ViewPOPrint>> GetListCancel(string search);
        Task<List<ViewPOPrint>> GetPOForPrint(long Poid);
        Task<POMaster> Get(long Poid);
        Task<List<ViewPOChild>> GetPOChild(long Poid);
        Task<string> Post(POMaster value);
        Task<string> Put(POMaster value);
        Task<string> GetCurrencyofSupplier(long suppid);
        Task<string> PutPOStatus(long poid);
        Task<string> PutPOEmailStatus(long poid);
        Task<List<ViewPOBalance>> GetPOBalanceList(InvoiceSearch po);
        Task<long> GetNewEmailSendID();
        Task<string> PostEmailSend(EmailSendDetails value);
        Task<ViewSendEmail> GetViewSendEmail(long id);
        Task<ViewRItemShow> RItemShow(int ritemid);
        Task<List<TempPOChild>> GetTempPOChild();
        Task<List<ViewPODelay>> PODelayReport();
        Task<List<ViewPODelaySum>> PODelaySum();
        Task<List<ViewSupplierPerformance>> GetSupplierPerformanceList(long supplierid);
        Task<List<ViewSupplierPerformanceSumarray>> GetSupplierAverageDelayList(string search);
        Task<string> GetSessionYearList();
        Task<string> GetRMList();
        Task<List<ViewPOPrint>> ViewPo(InvoiceSearch IS);
        Task<string> GetCategoryList();
        Task<string> GetSubCategoryList(long categoryId);
        Task<string> GetMonthList();
        Task<string> GetYearList();

        Task<string> GetListPOT();
        Task<string> GetListPODT();
        Task<string> GetListPOPT();
        Task<string> GetListBranch();

        Task<ViewBranchDetails> GetBranchDetails(long branchid);
        Task<ViewSupplierDetails> GetSupplierDetails(long suppid);

        Task<POMaster> GetPONO_Session(POMaster _oPOMaster);

        Task<string> GetSaleOrderList(long BuyerId);
        Task<string> GetBuyerCodeList();

        #region IPOContract
        Task<List<ViewRunningPOList>> GetRunningPOList(InvoiceSearch IS);
        Task<string> RunningPOListComplete(long poid);
        Task<string> PutPOCompleteStatus(List<POMaster> value);
        Task<List<ViewPOPrint>> GetCompletedPOList(InvoiceSearch IS);
        Task<string> UndoCompletedPOList(long poid);
        Task<List<ViewDelayedMaterial>> GetDelayedMaterialList(InvoiceSearch IS);
        #endregion
    }
}