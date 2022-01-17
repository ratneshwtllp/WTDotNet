using System;

namespace ERP.Domain.Entities
{
    public class ViewGRNChild
    {
        //grn child
        public long GRNChildID { get; set; } 
        public long POChildID { get; set; }
        //public double TodayRecQty { get; set; }

        //grn master
        public long GRNID { get; set; }
        public long SupplierID { get; set; }
        public long POID { get; set; }
        public string GRNNO { get; set; }
        public DateTime GRNDate { get; set; }
        public string BillNo { get; set; }
        public string ChallanNo { get; set; }
        public string VehicleNo { get; set; }
        public string DriverName { get; set; }
        public string Remark { get; set; }
        public long GRNSerial { get; set; }
        public double TotalQty { get; set; }
        public double TotalAmount { get; set; }
        public int StoreID { get; set; }
        public int POManual { get; set; }
        public int Verify { get; set; }
        public DateTime EntryDate { get; set; }
        public string SessionYear { get; set; }
        public int UserID { get; set; }

        public double GRNQty { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }

        public long RItem_Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int BelongTo { get; set; }
        public int MasterBelongTo { get; set; }
        public string Full_Name { get; set; }
        public long SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Unit { get; set; }
    }
}
