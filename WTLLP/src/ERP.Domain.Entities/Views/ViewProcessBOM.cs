using System;

namespace ERP.Domain.Entities
{
    public class ViewProcessBOM
    {  
        public long PlanChildId { get; set; }
        public long PlanId { get; set; }
        public long FitemId { get; set; }        
        public string Name { get; set; }
        public string Code { get; set; }
        public string Full_Name { get; set; }
        public int CostUnit { get; set; }
        public string CostUnitName { get; set; }
        public string CostUnitSName { get; set; }
        public string Title { get; set; }
        public double RMStock { get; set; }
        public long PRMasterId { get; set; }
        public int ProcessToQty { get; set; }
        public int ProcessID { get; set; }
        public double Required { get; set; }
        public long RItem_id { get; set; }
        public double IssueQty { get; set; }

        public string PlanNo { get; set; }
        public DateTime PlanDate { get; set; }
        public string ProductCode { get; set; }
        public string ProductSizeName { get; set; }
        public DateTime EstimatedRecieveDate { get; set; }
        public DateTime PRDate { get; set; }
        public string Through { get; set; }
        public string Remark { get; set; }
        public string CName { get; set; }
        public string AddressOffice { get; set; }
        public string AddressWork { get; set; }
        public string LogoPath { get; set; }
        public string ProcessName { get; set; }
        public string ContractorName { get; set; }
        public string PRNo { get; set; }
        //public double ReserveQty { get; set; }
    }
}
