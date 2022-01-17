using System;

namespace ERP.Domain.Entities
{
    public class ViewIssueRMWP
    {
        //Issue Master 
        public long IssueID { get; set; }
        public long IssueSNo { get; set; }
        public string IssueNo { get; set; }
        public long PlanId { get; set; }
        public DateTime IssueDate { get; set; }
        public string IssueBy { get; set; }
        public string ReceiveBy { get; set; }
        public int ExtraIssue { get; set; }
        public string Comments { get; set; }
        public int PCKIssue { get; set; }
        public int CancelStatus { get; set; }
        public string SessionYear { get; set; }

        //Issue Child
        public long IssueChildID { get; set; }
        public double IssueQty { get; set; }
        public double ReqQty { get; set; }
        public double Side { get; set; }
        public int SNo { get; set; }
        public string LotNo { get; set; }
        //Ritem 
        public long RitemId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Full_Name { get; set; }
        public int CostUnit { get; set; }
        public string CostUnitName { get; set; }
        public string CostUnitSName { get; set; }
        public string Title { get; set; }

        //Rack 
        public long RackId { get; set; }
        public string RackNo { get; set; }

        //Supplier 
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
         
        //WorkPlan  
        public DateTime PlanDate { get; set; }
        public string PlanNo { get; set; }

        //Contractor 
        public long ContractorID { get; set; }
        public string ContractorName { get; set; }

        //RM category 
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }

        //RM sub category 
        public long SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }

        public long PRMasterId { get; set; }
        public string PRNo { get; set; }
        public string ProcessName { get; set; }

        ////WorkPlanChild
        //public long PlanChildId { get; set; }
        //public long OrderChildId { get; set; }
        //public long FitemId { get; set; }
        //public int PlanQty { get; set; }
        //public double LabourCharges { get; set; }
        //public string Instruction1 { get; set; }


    }
}
