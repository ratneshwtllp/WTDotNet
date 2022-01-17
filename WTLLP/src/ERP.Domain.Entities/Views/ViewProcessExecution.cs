using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewProcessExecution
    {
        public string PlanNo { get; set; }
        public long PlanId { get; set; }
        public long PRMasterId { get; set; }
        public long PRSerialNo { get; set; }
        public string PRNo { get; set; }
        public long ContractorID { get; set; }
        public DateTime PRDate { get; set; }
        public int ProcessID { get; set; }
        public string Through { get; set; }
        public string SessionYear { get; set; }
        public int UserId { get; set; }
        public string ProcessName { get; set; }
        public string CName { get; set; }
        public string AddressOffice { get; set; }
        public string AddressWork { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int IsComponent { get; set; }
        public int Comp_Id { get; set; }
        public string Comp_Name { get; set; }
        public string Comp_Descr { get; set; }
        public int Comp_Qty { get; set; }
        public int SizeId { get; set; }
        public string ProductSizeName { get; set; }
        public double Charges { get; set; }
        public double Amount { get; set; }
        public string Remark { get; set; }
        public string ContractorName { get; set; }
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; }
        public string OrderNo { get; set; }
        public long PRChildId { get; set; }
        public long PlanChildId { get; set; }
        public long FitemId { get; set; }
        public long BuyerId { get; set; }
        public string CLName { get; set; }
        public int TotalCompRecvQty { get; set; }
        public double ProcessCharge { get; set; }
        public double ProcessAmount { get; set; }
        public string ProcessRemark { get; set; }
        public int SNo { get; set; }
        public int IsRead { get; set; }
        public int ProcessToQty { get; set; }
        public string PhotoPath { get; set; }
        public int PRRecvQty { get; set; }
        public DateTime EstimatedRecieveDate { get; set; }

        public int IsRMIssue { get; set; }
        public int IsRMRequired { get; set; }
    }
}
