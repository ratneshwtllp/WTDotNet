using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewContractorPayment
    {
        public long ContractorPaymentId { get; set; }
        public long ContractorId { get; set; }
        public double PaidAmount { get; set; } 
        public string PaidTo { get; set; }
        public DateTime PaidDate { get; set; }
        public string Remark { get; set; } 
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public string ContractorName { get; set; }
        public double PreviousBalance { get; set; }
        public double TotalProcessAmount { get; set; }
        public double TotalAmount { get; set; }
        public double Balance { get; set; }
        public string AmountInWords { get; set; }

        public long ContractorPaymentChildId { get; set; }
        public long PRMasterId { get; set; }
        public string PRNo { get; set; }
        public DateTime PRDate { get; set; }
        public long PlanChildId { get; set; }
        public long FitemId { get; set; }
        public string Code { get; set; }
        public int ProductSizeId { get; set; }
        public string ProductSizeName { get; set; }
        public string PlanNo { get; set; }
        public DateTime PlanDate { get; set; }
        public long OrderMasterID { get; set; }
        public string OrderNo { get; set; }
        public long BuyerId { get; set; }
        public string BuyerCode { get; set; }
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }
        public string WPContractor { get; set; }
        public int PlanQty { get; set; }
        public double ProcessCharge { get; set; }
        public double ProcessAmount { get; set; }
        //public long PRRecvId { get; set; }
        //public string PRRecvNo { get; set; }
        //public DateTime PRRecvDate { get; set; }
        public string CName { get; set; }
        public int SNo { get; set; }

        public int CPSNo { get; set; }
        public string CPNo { get; set; }
    }
}
