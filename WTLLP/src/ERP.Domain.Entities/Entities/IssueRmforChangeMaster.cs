using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class IssueRMforChangeMaster
    {
        public long IssueRMChangeID { get; set; }
        public DateTime IssueRMChangeDate { get; set; }
        public long ContractorID { get; set; }
        public string SessionYear { get; set; }
        public int userId { get; set; }
        public long IssueRMChangeSNo { get; set; }
        public string IssueRMChangeNo { get; set; }
        public int AgainstNo { get; set; }
        public long OrderMasterID { get; set; }
        public long FitemId { get; set; }
        public long RItem_Id_For { get; set; }
        public string Remark { get; set; }
        public double RItem_Id_ForQty { get; set; }
        public int IsComplete { get; set; }
        public long SupplierId { get; set; }
        public int SuppAddressId { get; set; }
        public int InHouse_Outdoor { get; set; }

        public double TotalAmount { get; set; }
        public double TotalIGST { get; set; }
        public double TotalCGST { get; set; }
        public double TotalSGST { get; set; }
        public double TotalAmountAfterTax { get; set; }
        public string AmountInWords { get; set; }
        public string VehicleNo { get; set; }
        public string Transport { get; set; }
        public int TransportId { get; set; }
        public string PlaceofSupply { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }

        public virtual ICollection<IssueRMforChangeItem> IssueRMforChangeItem { get; set; }
        public virtual ICollection<IssueRMForChangeChild> IssueRMForChangeChild { get; set; }
    }
}
