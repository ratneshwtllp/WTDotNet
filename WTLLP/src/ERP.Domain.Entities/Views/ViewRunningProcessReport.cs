using System;
using System.Collections.Generic;

namespace ERP.Domain.Entities
{
    public partial class ViewRunningProcessReport
    {
        public string PlanNo { get; set; }
        public long PlanId { get; set; }
        public long PRMasterId { get; set; } 
        public long PRSerialNo { get; set; } 
        public string PRNo { get; set; }
        public DateTime PRDate { get; set; }
        public long ContractorID { get; set; }
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
        public int ProductSizeId { get; set; }
        public string ProductSizeName { get; set; }
        public string ContractorName { get; set; }
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; }
        public string OrderNo { get; set; }
        public long PlanChildId { get; set; }
        public long FitemId { get; set; }
        public long BuyerId { get; set; }
        public string CLName { get; set; }
        public double ProcessCharge { get; set; }
        public double ProcessAmount { get; set; }
        public string ProcessRemark { get; set; }
        public int PlanQty { get; set; }
        public int IsRead { get; set; }
        public int IsProcessReceive { get; set; }
        public double KnottingCharge { get; set; }
        public double ToolingCharge { get; set; }

        public int ProcessFromId { get; set; }
        public string ProcessFromName { get; set; }
        public int ProcessFromQty { get; set; }
        public int ProcessToQty { get; set; }
        public string Unit { get; set; }
        public DateTime EstimatedRecieveDate { get; set; }
    }
}
