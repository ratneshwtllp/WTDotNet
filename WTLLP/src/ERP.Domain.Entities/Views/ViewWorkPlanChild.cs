using System;

namespace ERP.Domain.Entities
{
    public class ViewWorkPlanChild
    {
        public long PlanChildId { get; set; }
        public long OrderChildId { get; set; }
        public long FitemId { get; set; }
        public int PlanQty { get; set; }
        public double LabourCharges { get; set; }
        public string Instruction1 { get; set; }
        public string Instruction2 { get; set; }
        public long PlanId { get; set; }
        public long OrderId { get; set; }
        public DateTime PlanDate { get; set; }
        public string PlanNo { get; set; }
        public string PlanBy { get; set; }
        public int Order_Sample { get; set; }
        public long ContractorID { get; set; }
        public string ContractorName { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Code { get; set; }
        public string Unit { get; set; }
        public int Color { get; set; }
        public string PhotoPath { get; set; }
        public string CLName { get; set; }
        public long RitemId { get; set; } 
        public int ReceiveQty { get; set; }
        public int BalanceQty { get; set; }
        public int ProductSizeId { get; set; }
        public string ProductSizeName { get; set; }
        public string OrderNo { get; set; }
        public long ProductSubCategoryID { get; set; }
        public long ProductCategoryID { get; set; }
        public string SessionYear { get; set; }
        public int SNo { get; set; }
        public string ProductSubCategoryName { get; set; }
        public string ProductCategoryName { get; set; }
        public string RitemName { get; set; }
        public string RitemCode { get; set; }
        //public string PRDWidth { get; set; }
        //public string Thickness { get; set; }
        //public string Comments { get; set; }
        //public string Paper1 { get; set; }
        //public string Fabric1 { get; set; }
        //public string BuyerName { get; set; }
        //public string Finish { get; set; }
        //public int PreSkinGSM { get; set; }
        //public int SkinGSM { get; set; }
        //public int FoamGSM { get; set; }
        //public int AdhesiveGSM { get; set; }
        //public int FabricGSM { get; set; }
        public int IsPending { get; set; }
        public int IsProcessReceive { get; set; }

        public long BuyerId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; }
        public DateTime DiliveryDatePlan { get; set; }
        public DateTime MyDeliveryDate { get; set; }        
    }
}
