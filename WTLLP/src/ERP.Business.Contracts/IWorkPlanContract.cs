using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.Business.Contracts
{
    public interface IWorkPlanContract
    {
        Task<List<ViewWorkPlan>> Get();
        Task<ViewWorkPlan> Get(long Poid);
        Task<long> GetNewPlanID();
        Task<long> GetNewPlanChildID();
        Task<string> GetNewPlanNO();
        Task<int> GetNewPlanSerial();
        Task<List<ViewWorkPlanChild>> GetWorkPlanDetails(long planid);
        Task<List<ViewSaleOrder>> SaleOrderRecord();
        Task<string> Post(WorkPlanMaster value);
        Task<string> PutWPStatus(long planid);
        Task<string> PutWPCompleteStatus(long planid);
        Task<string> GetBuyerList();
        Task<string> GetOrderNoList(long buyerid);
        Task<List<ViewSaleOrderDetails>> GetOrderItems(long orderid);
        Task<string> DeleteWorkPlan(long planid);

        Task<string> GetContractorList();
        Task<string> GetActiveContractorList();
        Task<string> GetPayableContractorList();


        Task<string> GetProSubCategoryFromBuyer(long buyerid);
        Task<string> GetFItemCodeList(long id);
        Task<string> GetFitemDetails(long id);

        Task<List<ViewWorkPlanChild>> GetOrderItemsForBalance(long orderid);
        Task<List<ViewWorkPlanBOM>> GetWorkPlanRMDetails(long planid);

        Task<string> GetProcessList();
        Task<string> GetProcessListForPayment();
        Task<string> ProductProcessList(long FitemId);
        Task<List<ViewProcessExecution>> GetProductionProcessPrint();
        Task<string> GetPlanNoList();
        Task<string> GetComponentList();
        Task<string> GetComponentList(long FitemId);

        Task<string> GetNewPRNo();
        Task<string> PostProcessExecution(ProcessExecutionMaster value);
        Task<List<ViewProcessExecutionMaster>> ProcessList(ProcessSearch PS);
        Task<List<ViewProcessExecution>> GetProcessListForRecv(ProcessSearch PS);
        Task<List<ViewProcessExecutionMaster>> GetProcessListForContractorPayment(ProcessSearch PS);
        Task<string> GetProcessOfWP(long planid);
        Task<string> PostProcessRecv(ProcessRecvMaster value);
        Task<List<ViewProcessRecvMaster>> ProcessRecvList(ProcessSearch PS);

        Task<List<ViewWIP>> GetViewWIP(ProcessSearch PS);

        Task<List<ViewWIP>> WipList(ProcessSearch PS);
        Task<double> GetProcessCharge(SearchModel SM);
        Task<int> GetWPQtyForNextProcess(SearchModel SM);
        Task<int> IsStartWPDone(SearchModel SM);

        Task<string> GetProcessNoList();
        Task<string> GetProcessNoListForProcessRecv();
        Task<ViewProcessExecution> GetProcessBalQty(SearchModel SM);

        Task<long> GetNewContractorPaymentId();
        Task<long> GetNewContractorPaymentChildId();
        Task<long> GetNewContractorAdvanceId();
        Task<string> PutReview(long PRMasterId);
        Task<string> PutWPProcessCharge(long PRMasterId, double ProcessCharge);

        Task<string> PostContractorPayment(ContractorPayment value);
        Task<ViewContractorBalance> GetContractorBalance(long contractorid);
        Task<string> PostContractorAdvance(ContractorAdvanceDetails value);

        Task<List<ViewContractorPayment>> GetContractorPaymentList(WorkPlanSearch WS);
        Task<List<ViewContractorPayment>> GetContractorPaymentPrint(long contractorpaymentid);

        Task<List<ViewContractorAdvanceDetails>> GetContractorAdvanceList(WorkPlanSearch WS);
        Task<List<ViewContractorAdvanceDetails>> GetContractorAdvancePrint(long contractoradvanceid);

        Task<List<ViewWorkPlanChild>> GetViewWorPlan(WorkPlanSearch WP);

        Task<long> GetNewTableId();
        Task<string> PostTempTableDetails(List<TempTableDetails> value);

        Task<List<ViewProductListForProcessCharge>> ProductListForProcessCharge(WorkPlanSearch PS);
        Task<string> PostProcessCharge(List<ProcessChargeDetails> value);

        Task<WorkPlanChild> GetProductDetail(long fitemid);

        Task<List<ViewWIP>> GetProductionFlowStatus(ProcessSearch PS);
        Task<List<ViewProcessFinishGoodsStock>> FinishGoodsStockList(ProcessSearch PS);

        Task<List<ViewProcessBOM>> GetViewProcessBOM(long prmasterid);

        Task<string> GetProductList(long subcategoryid);
        Task<List<ViewProductComponent>> GetViewProductComponentList(long fitemid);
        Task<List<ViewProductProcessDetails>> ProductProcessListForCharge(long fitemid);
        Task<string> PostProductProcessCharge(List<ProductProcessCharge> value);
        Task<List<ProductProcessCharge>> GetProductProcessCharge(long fitemid,int sizeid);
        Task<List<ViewProductProcessCharge>> GetProductProcessCharge(long fitemid);
        Task<string> GetProductionProcessDetails(long processid);
        Task<string> GetProductSizeList(long fitemid);
        Task<ViewContractorDetails> GetContractorRecord(long id);
        Task<List<ViewSaleOrderWPStatus>> GetSaleOrderWPStatus(ProcessSearch PS);


        Task<List<ViewProcessRecvMaster>> GetWeeklyProduction(ProcessSearch PS);
        Task<List<ViewProcessRecvMaster>> GetMonthlyProduction(ProcessSearch PS);
        Task<string> GetMonthList();
        Task<string> GetYearList();
        Task<List<ViewRunningProcessReport>> GetRunningProcessList(ProcessSearch PS);

        Task<List<ViewOrderStatus>> GetOrderStatus(ProcessSearch PS);
        Task<List<ViewProductProcess>> GetViewProductProcess(ProcessSearch PS);

        Task<List<ProcessExecutionMaster>> GetProcessExecutionMaster(ProcessSearch PS);

        Task<string> DeletePR(long PRMasterId);

        Task<List<ViewProcessExecutionMaster>> BalProcessForSupervisorList(ProcessSearch PS);
    }
}