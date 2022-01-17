using System;

namespace ERP.Domain.Entities
{
    public class ViewSaleOrderSheetClose
    {
        public long OrderMasterID { get; set; }
        public string OrderNo { get; set; }
        public long BuyerId { get; set; }
        public DateTime OrderDate { get; set; }      
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; }
        public long OrderSheetCloseId { set; get; }
        public DateTime CloseDate { set; get; }
        public string CloseRemark { set; get; }
        public DateTime EntryDate { set; get; }
        public int IsShoesOrder { set; get; }
        public string UserName { set; get; }
        public string LoginName { set; get; }
        public int UserId { set; get; }        
    }
}
