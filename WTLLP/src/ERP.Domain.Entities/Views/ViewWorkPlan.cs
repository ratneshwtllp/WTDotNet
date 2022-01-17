using System;

namespace ERP.Domain.Entities
{
    public class ViewWorkPlan
    {
        //WorkPlan
        public long PlanId { get; set; }
        public DateTime PlanDate { get; set; }
        public string PlanNo { get; set; }
        public string PlanBy { get; set; }
        public int PlanSNo { get; set; }
        public int Order_Sample { get; set; }
        public int WorkplanCancelStatus { get; set; }

        //Contractor
        public long ContractorID { get; set; }
        public string ContractorName { get; set; }

        //Buyer 
        public long BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; }
         
        //Saleordermaster
        public long OrderMasterID { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int CancelStatus { get; set; }

        //IssueMaster
        public long IssueID { get; set; }


        //Company Details
        public string CName { get; set; }
        public string AddressOffice { get; set; }
        public string AddressWork { get; set; }
        //public string Phone { get; set; }
        //public string Fax { get; set; }
        //public string Email { get; set; }
        //public string Web { get; set; }
        //public string CSTT { get; set; }
        //public string UPTT { get; set; }
        //public string TIN { get; set; }
        //public string PANNo { get; set; }
        //public string POFooter1 { get; set; }
        //public string POFooter2 { get; set; }


        //    product ,category,subcategory,size,color

        //    public long FitemId { get; set; }
        //    public string Name { get; set; }
        //    public string Code { get; set; }
        //    public string Description { get; set; }
        //    public string ProductCode { get; set; }
        //    public string ProductName { get; set; } 
        //    public long MainProductId { get; set; }
        //    public string ProductSubCategoryName { get; set; }
        //    public long ProductSubCategoryID { get; set; }
        //    public long ProductCategoryID { get; set; }
        //    public int Qty { get; set; }
        //    public double Price { get; set; }
        //    public double Amount { get; set; } 
        //    public string CLName { get; set; }
        //    public string Strap { get; set; }
        //    public string Unit { get; set; }
        //    public int Size { get; set; }
        //    public string ProductFabric { get; set; }
        //    public string ProductQuilting { get; set; }
        //    public string Embroidery { get; set; }
        //    public string ProductSizeName { get; set; }
        //    public int ProductSizeDesc { get; set; }
        //    public int FakeOrder { get; set; }


    }
}
