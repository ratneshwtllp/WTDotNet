using System;

namespace ERP.Domain.Entities
{
    public class ViewReceiveWP
    {
        //ReceiveWP Master 
        public long ReceiveWPID { get; set; }
        public long PlanChildId { get; set; }
        public DateTime ReceiveDate { get; set; }
        public int ReceiveQty { get; set; }
        public string Remark { get; set; } 

        //WorkPlanChild 
        public long FitemId { get; set; }

        //workplan master
        public long ContractorID { get; set; } 
        public DateTime PlanDate { get; set; } 
        public string PlanNo { get; set; }
        public long OrderId { get; set; }

        //Product
        public string Name { get; set; }
        public string Code { get; set; }
        public string Unit { get; set; }
        public string PhotoPath { get; set; }

        //Contractor 
        public string ContractorName { get; set; }

        //saleorder
        public string OrderNo { get; set; }

    }
}
