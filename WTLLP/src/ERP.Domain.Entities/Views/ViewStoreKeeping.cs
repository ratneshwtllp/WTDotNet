using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.Domain.Entities
{
    public class ViewStoreKeeping
    {
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
        public string PONO { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long RItem_Id { get; set; }
        public string CostUnit { get; set; }
        public string SubCategoryName { get; set; }
        public string CategoryName { get; set; }
        public string Full_Name { get; set; }
        public long GRNID { get; set; }
        public long SupplierID { get; set; }
        public long POID { get; set; }
        public string GRNNO { get; set; }
        public DateTime GRNDate { get; set; }
        public string BillNo { get; set; }
        public string ChallanNo { get; set; }
        public long StoreKeepingID { get; set; }
        public string StoreKeepingNO { get; set; }
        public DateTime StoreKeepingDate { get; set; }
        public string StoreKeepingBY { get; set; }
        public string Remark { get; set; }
        public string RackNo { get; set; }
        public long StoreKeepingChildID { get; set; }
        public long GRNChildID { get; set; }
        public long RackID { get; set; }
        public double StoreKeepingQty { get; set; }
        public double Side { get; set; }
        public string SessionYear { get; set; }
        public int  SNo { get; set; }
        public string LotNo { get; set; }
        public string RackLocation { get; set; }
        public string StoreName { get; set; }
    }
}
