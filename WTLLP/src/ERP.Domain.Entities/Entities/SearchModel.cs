using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Domain.Entities
{
    public partial class SearchDetails
    {       
        public long[] IssueRMChangeID { get; set; }
        public long[] PRMasterId { get; set; }
    }

    public class SearchModel
    {
        public string searchvalue;
        public double RequiredQty { get; set; }
        public double ReserveQty { get; set; }
        public double StockQty { get; set; }
        public long RitemId { get; set; }
        public long OrderId { get; set; }
        public long belongto { get; set; }
        public long[] fitemids { get; set; }
        public int DieId { get; set; }
        public long FitemId { get; set; }
        public int SizeId { get; set; }
        public string ProcessName { get; set; }
        public long PlanChildId { get; set; }
        public int ComponentId { get; set; }
        public int ProcessId { get; set; }
    }

    public class OrderSearch
    {
        public long BuyerId { get; set; }
        public string OrderNo { get; set; }
        public string Session_Year { get; set; }
        public int MonthId { get; set; }
        public int YearName { get; set; }
        public string Code { get; set; }

        public int ChkOrderDateFromInt { get; set; }
        public DateTime OrderdateFrom { set; get; }

        public int ChkOrderDateToInt { get; set; }
        public DateTime OrderDateTo { set; get; }

        public int ChkDeliveryDateFromInt { get; set; }
        public DateTime DeliveryDateFrom { set; get; }

        public int ChkDeliveryDateToInt { get; set; }
        public DateTime DeliveryDateTo { set; get; }

        public int ChkCancelStatusInt { get; set; }
        public int IsReportForPRD { get; set; }
        public int IsShoesOrder { get; set; }
    }

    public class InvoiceSearch
    {
        public long BuyerID { get; set; }
        public string InvoiceNo { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int MonthId { get; set; }
        public int YearName { get; set; }
        public string FinalDestination { get; set; }
        public int CID { get; set; }
        public string Session_Year { get; set; }
        public long SupplierId { get; set; }
        public long RItem_ID { get; set; }
        public string DeliveryStatus { get; set; }
        public short? CancelStatus { get; set; }
        public short? POBalaceTypeId { get; set; } //
        public string PONO { get; set; }
        public int ChkPODateFromInt { get; set; }
        public DateTime PODateFrom { set; get; }

        public int ChkPODateToInt { get; set; }
        public DateTime PODateTo { set; get; }

        public int ChkDeliveryDateFromInt { get; set; }
        public DateTime DeliveryDateFrom { set; get; }

        public int ChkDeliveryDateToInt { get; set; }
        public DateTime DeliveryDateTo { set; get; }
        public string RMName { get; set; }
        public long RMCategoryID { get; set; }
        public long RMSubCategoryID { get; set; }

        public int ChkDateFromInt { get; set; }
        public DateTime DateFrom { set; get; }

        public int ChkDateToInt { get; set; }
        public DateTime DateTo { set; get; }

        public long BranchID { get; set; }
        public int ChkCreationDateFromInt { get; set; }
        public DateTime CreationDateFrom { set; get; }

        public int ChkCreationDateToInt { get; set; }
        public DateTime CreationDateTo { set; get; }
    }

    public class GRNSearch
    {
        public long SupplierID { get; set; }
        public string GRNNO { get; set; }
        public int MonthId { get; set; }
        public int YearName { get; set; }
        public string Session_Year { get; set; }
        public string ChallanNo { get; set; }
        public string BillNo { get; set; }
        public string PONO { get; set; }
        public string RMName { get; set; }
        public int ChkGRNDateFromInt { get; set; }
        public DateTime GRNDateFrom { set; get; }

        public int ChkGRNDateToInt { get; set; }
        public DateTime GRNDateTo { set; get; }

        public int PO_JW { get; set; }
        public long GRNId { get; set; }
    }

    public class ProductSearch
    {
        public long ProductTypeId { get; set; }
        public long FItemCategory_ID { get; set; }
        public long FItemSubCategory_ID { get; set; }
        public long FItemId { get; set; }
        public string Code { get; set; }
        public long BuyerId { get; set; }
        public string PartyCode { get; set; }

        public int ChkCreateDateFromInt { get; set; }
        public DateTime CreateDateFrom { set; get; }

        public int ChkCreateDateToInt { get; set; }
        public DateTime CreateDateTo { set; get; }

        public long QCPointID { get; set; }
    }

    public class ProcessSearch
    {
        public long PlanId { get; set; }
        public long PlanChildId { get; set; }
        public long ContractorId { get; set; }
        public string Code { get; set; }
        public long BuyerId { get; set; }
        public string PartyCode { get; set; }
        public int ProcessID { get; set; } 

        public string PlanNo { get; set; }
        public string OrderNo { get; set; }
        public string ProcessNo { get; set; }
        public DateTime ProcessDate { get; set; }
        public bool ChkProcessDate { get; set; }
        public int ChkProcessDateInt { get; set; }
        public string Comp_Name { get; set; }

        public long PRMasterId { get; set; }
        public int IsRead { get; set; }

        public int ChkDateFromInt { get; set; }
        public DateTime DateFrom { set; get; }

        public int ChkDateToInt { get; set; }
        public DateTime DateTo { set; get; }
        public int IsProcessReceive { get; set; }
        public int IsFloorReport{ get; set; }
        public int WorkPlanStatusId { get; set; }

        public int OrderBy { get; set; }

        public int ReportType { get; set; }
        public int MonthId { get; set; }
        public int YearName { get; set; }

        public int DeliveryDays { get; set; }
        public string SizePartyCode { get; set; }
        public long RItemID { get; set; }

        public int UserId { get; set; }
    }

    public class RMSearch
    {
        public long RMCategory_ID { get; set; }
        public long RMSubCategory_ID { get; set; }
        public long RItemId { get; set; }
        public string RMCode { get; set; }
        public string RMName { get; set; }
        public string RMDescription { get; set; }
        public int Color_ID { get; set; }
        public string Color { get; set; }
        public int RMBrandId { get; set; }
        public string RMBrandName { get; set; }
        public string RMBrandCode { get; set; }
        public string Finish { get; set; }
        public string Thickness { get; set; }
        public string SizeName { get; set; } 
        public DateTime RMUpdateOn { get; set; }
        public int ChkRMUpdateOnInt { get; set; }
        public DateTime RMReviewOn { get; set; }
        public int ChkRMReviewOnInt { get; set; }
        public int ChkCreateDateFromInt { get; set; }
        public DateTime CreateDateFrom { set; get; }
        public int ChkCreateDateToInt { get; set; }
        public DateTime CreateDateTo { set; get; }
        public DateTime RMStockDate { set; get; }
        public int StockFilterId { get; set; }
        public int StoreId { get; set; }
        public string RackNo { get; set; }

        public long SupplierId { get; set; }
    }

    public class CostingSearch
    {
        public long ProductCategoryID { set; get; }
        public long ProductSubCategoryID { set; get; }
        public string Code { get; set; }
        public long BuyerID { get; set; }       

        public DateTime CreationDate { get; set; }
        public int ChkCreationDateInt { get; set; }

        public int ChkUpdationDateFromInt { get; set; }
        public DateTime UpdationDateFrom { set; get; }

        public int ChkUpdationDateToInt { get; set; }
        public DateTime UpdationDateTo { set; get; }

        public long OrderMasterId { get; set; }
        public long RitemId { get; set; }


        public long CostingID { get; set; }
        public long UpdateRItemID { get; set; }

        public int SizeId { get; set; }
    }

    public class StoreSearch
    {
        public long ContractorId { get; set; }
        public long SupplierID { get; set; }
        public string IssueNo { get; set; }
        public string GRNNO { get; set; }
        public string Rack { get; set; }
        public long CategoryID { get; set; }
        public long SubCategoryID { get; set; }
        public string RawMaterial { get; set; }
        public string WorkPlan { get; set; }
        public int MonthId { get; set; }
        public int YearName { get; set; }
        public string Session_Year { get; set; }
        public long DepartmentId { get; set; }

        public string ReqRMNo { get; set; }
        public string RMFor { get; set; }
        public string ReturnRMNo { get; set; }
        public string ReturnRMRecvNo { get; set; }
        public string ReqRMIssueNo { get; set; }
        public string RTSNo { get; set; }
        public string PONO { get; set; }
        public string StoreKeepingNO { get; set; }

        public int ChkDateFromInt { get; set; }
        public DateTime DateFrom { set; get; }

        public int ChkDateToInt { get; set; }
        public DateTime DateTo { set; get; }

        public string RMName { get; set; }
        public string RackNo { get; set; }
        public string LotNo { get; set; }
        public int StockType { get; set; } // increment/damage/decrement



        public string RTSGRNNo { get; set; }
        public string BillNo { get; set; }
        public string ChallanNo { get; set; }
        public int RequestBranchId { get; set; }
        public int RequestToBranchId { get; set; }

        public string StockTransferIssueNo { get; set; }
        public string PRNo { get; set; }
    }

    public class WorkPlanSearch
    {
        public long ProductCategoryID { set; get; }
        public long ProductSubCategoryID { set; get; }
        public string Code { get; set; }
        public string OrderNo { get; set; }
        public string PlanNo { get; set; }
        public long ContractorId { set; get; }
        public long ProcessId { set; get; }
        public string Session_Year { get; set; }
        public int MonthId { get; set; }
        public int YearName { get; set; }
        public int ChkDateFromInt { get; set; }
        public DateTime DateFrom { set; get; }
        public int ChkDateToInt { get; set; }
        public DateTime DateTo { set; get; }
        public long BuyerId { get; set; }
        public long PlanId { get; set; }
        public int WorkPlanStatusId { get; set; }
    }

    public class PackingSearch
    {
        public long BuyerId { set; get; } 
        public long OrderId { set; get; }
        public long CartonNo { set; get; }
        public string BuyerCartonNo { set; get; }
        public string Code { get; set; }
        public string OrderNo { get; set; }
        public string Barcode { get; set; }
        public string PackingNo { get; set; } 

        public long ProductCategoryID { set; get; }
        public long ProductSubCategoryID { set; get; }
        public string PlanNo { get; set; }
         
        public string Session_Year { get; set; }
        public int MonthId { get; set; }
        public int YearName { get; set; }

        public int ChkDateFromInt { get; set; }
        public DateTime DateFrom { set; get; }

        public int ChkDateToInt { get; set; }
        public DateTime DateTo { set; get; }
    }

    public class RMProSearch
    {
        public int RMPropertiesID { get; set; }
        public string RMPropertiesName { get; set; }
        public string RMPropertiesRemark { get; set; }
        public int RMPropertiesIsPrintOnPo { get; set; }
        public int RMPropertiesIsMaster { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public DateTime EntryDate { get; set; }

        public int RMPropertiesValId { get; set; }
        public string RMPropertiesValue { get; set; }
        public long CategoryID { get; set; }
    }

}
