using ERP.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace ERP.Business.Contracts
{
    public interface IZWorkPlanContract
    {
        Task<List<ViewWorkPlan>> Get();
        Task<ViewWorkPlan> Get(long Poid);          
        Task<long> GetNewPlanID();
        Task<long> GetNewPlanChildID();
        Task<string> GetNewPlanNO();
        Task<int> GetNewPlanSerial();         
        Task<List<ViewWorkPlanChild>> GetZWorkPlanDetails(long planid);
        Task<List<ViewSaleOrder>> SaleOrderRecord();
        Task<string> Post(WorkPlanMaster value);
        Task<string> PutWPStatus(long planid);         
        Task<string> GetBuyerList();
        Task<string> GetOrderNoList(long buyerid);
        Task<List<ViewSaleOrderDetails>> GetZOrderItems();

        Task<string> GetContractorList();
        Task<string> GetProSubCategoryFromBuyer(long buyerid);
        Task<string> GetFItemCodeList(long id);
        Task<string> GetFitemDetails(long id);

        Task<List<ViewWorkPlanChild>> GetOrderItemsForBalance(long orderid);
        Task<List<ViewWorkPlanBOM>> GetWorkPlanRMDetails(long planid);

        Task<string> GetProcessList();
        Task<string> GetPlanNoList();
        Task<string> GetComponentList();
        Task<string> GetNewPRNo();
        Task<string> PostProcessExecution(ProcessExecutionMaster value);
        Task<List<ViewProcessExecution>> ProcessList(ProcessSearch PS);
        Task<string> GetProcessOfWP(long planid);
        Task<string> PostProcessRecv(ProcessRecvMaster value);
        Task<List<ViewProcessRecv>> ProcessRecvList(ProcessSearch PS);

        Task<List<ViewWIP>> WipList(ProcessSearch PS);
        Task<double> GetProcessCharge(SearchModel SM);

        Task<string> GetProcessNoList();
        Task<ViewProcessExecution> GetProcessBalQty(SearchModel SM);
         
        Task<string> GetProCategoryList();
        Task<string> GetProSubCategoryList(long pcategoryId);
        Task<List<ViewWorkPlanChild>> ZWPList(WorkPlanSearch WPS);
        Task<string> GetMonthList();
        Task<string> GetYearList();
        Task<string> GetSessionYearList();
        Task<List<ViewWorkPlanChild>> GetZWorkPlanPrint(long id);
    }
}